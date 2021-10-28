---
title: Caching Answers
layout: single
permalink: /user-guides/caching-answers/
sidebar:
  nav: "user-guides"
toc: true
---

This guide shows how to automatically cache answers to Questions.
Answer caching is an optional advanced feature of Boa Constrictor.


## When Should Actors Cache Answers?

Sometimes, an Actor needs to call a Question repeatedly to get the same answer.
For example, multiple tests in a suite might need to query the same product configurations.
Caching the answer on the first call and returning it on subsequent calls
would be much more efficient than repeatedly asking the same Question.
Boa Constrictor's support for answer caching spares programmers
from implementing their own mechanisms for storing and sharing answers.

**Answers should be cached when they are expected to be *immutable***, or unchanging.
For example, product configurations may be the same for the duration of a test suite.

However, **answers should *not* be cached when they represent "fresh" information or they could change when called again**.
For example, a web element's scraped text may not be appropriate to cache
because it represents the state of the page at a moment in time.


## Writing Cacheable Questions

Not all Questions are inherently cacheable.
Questions must implement the `ICacheableQuestion<TAnswer>` interface,
where `TAnswer` is the answer type:

```csharp
namespace Boa.Constrictor.Screenplay
{
    public interface ICacheableQuestion<TAnswer> : IQuestion<TAnswer>
    {
        bool Equals(object obj);
        int GetHashCode();
    }
}
```

Cacheable Questions must implement the standard `Equals` and `GetHashCode` methods.
These should be implemented the same way as they would be for any other C# class.
Typically, all properties and instance variables for a Question should be included
in the implementations of `Equals` and `GetHashCode` methods.
For example, here is how to implement them for a Question that gets a browser cookie by name:

```csharp
using Boa.Constrictor.Screenplay;

public class BrowserCookie : ICacheableQuestion<TAnswer>
{
    private string CookieName { get; set; }

    // ... other code ...

    public override bool Equals(object obj) =>
        obj is BrowserCookie cookie &&
        CookieName == cookie.CookieName;

    public override int GetHashCode() =>
        HashCode.Combine(GetType(), CookieName);
}
```

The `Equals` method must return true only for equivalent Questions.
The `GetHashCode` method must return a unique hash code based on the Question's properties and variables.
Otherwise, answer caching might have collisions, which could return incorrect answers for Questions.

All WebDriver Questions implement `ICacheableQuestion<TAnswer>`.


## Enabling Actors to Cache Answers

Actors use the `CacheAnswers` Ability to store answers from cacheable Questions in an `AnswerCache` object.
AnswerCache internally stores key/value pairs in which a Question is a key and its answer is the value.
(This is why cacheable Questions must implement `Equals` and `GetHashCode`.)

Set up the Ability like this:

```csharp
using Boa.Constrictor.Screenplay;

var actor = new Actor();
var cache = new AnswerCache();

actor.Can(CacheAnswers.With(cache));
```

The `AnswerCache` object is thread-safe.
Answers are added and cleared synchronously.


## Caching Answers When Asking Questions

An Actor uses the `AsksFor` method to ask Questions.
However, in order to automatically store the answer in the `AnswerCache` object,
the Actor should instead use the `GetsCached` method, like this:

```csharp
var cookie = actor.GetsCached(BrowserCookie.FromBrowser());
```

Whenever the Actor wants to use answer caching,
it will check if its `AnswerCache` object already has an answer for the given Question.
If the `AnswerCache` has an answer, then the Actor will return it without calling the Question.
Otherwise, the Actor will call the Question and then add the answer to its `AnswerCache`.
Any time the Actor asks the same Question again, they will get the stored response.
This is very useful in large test suites when multiple tests ask the same Questions.

The `Discovers` method is an alias for `GetCached` and works the same way.
The following call is equivalent to the one above:

```csharp
var cookie = actor.Discovers(BrowserCookie.FromBrowser());
```

The `GetsCached` and `Discovers` methods are
[Actor extension methods](/user-guides/writing-actor-extension-methods/).
Internally, they use a special Question named `CachedAnswer<TAnswer>`.
Their calls are equivalent to the following:

```csharp
var cookie = actor.AsksFor(CachedAnswer<TAnswer>.For(BrowserCookie.FromBrowser()));
```

The extension methods make caching calls much more concise.


## Bypassing the Answer Cache

Actors can call cacheable Questions without using the answer cache via the `AsksFor` method:

```csharp
var cookie = actor.AsksFor(BrowserCookie.FromBrowser());
```

In this case, the Actor will bypass the answer cache and call the Question as if it were any ordinary Question.
It will neither fetch nor store answers using the cache.

Remember to use `GetsCached` or `Discovers` instead of `AsksFor` (or `AskingFor`) when you want to cache answers.
It is a common mistake to call `AsksFor` when answer caching is desired.


## Clearing the Answer Cache

Sometimes, answers stored in the cache should be reset.
To clear the answer for a specific Question, call the `Invalidate` method with the Question:

```csharp
cache.Invalidate(BrowserCookie.FromBrowser());
```

To clear all answers from the cache, call the `InvalidateAll` method:

```csharp
cache.InvalidateAll();
```
