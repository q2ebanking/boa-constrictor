---
title: Caching Answers
layout: single
permalink: /user-guides/caching-answers/
sidebar:
  nav: "user-guides"
toc: true
---

This guide shows how to automatically cache answers to Questions.
Sometimes, an Actor needs to call a Question repeatedly to get the same answer.
For example, multiple tests in a suite might need to query the same product configurations that do not change.
Caching the answer on the first call and returning it on subsequent calls
would be much more efficient than repeatedly asking the same Question.
Boa Constrictor's support for answer caching spares programmers
from implementing their own mechanisms for storing and sharing answers.


## Writing Cacheable Questions

Not all Questions are inherently cacheable.
Questions must implement the `ICacheableQuestion<TAnswer>` interface, where `TAnswer` is the answer type.

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
These should be implemented the same way as they would be for any other C# object.
Typically, all properties and instance variables for a Question should be included
in the implementations of `Equals` and `GetHashCode` methods.
For example, here is how to implement them for a Question that gets a browser cookie by name:

```csharp
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


## Creating an Answer Cache

TBD


## Enabling Actors to Cache Answers

TBD


## Caching Answers When Asking Questions

TBD


## Bypassing the Answer Cache

TBD


## Resetting the Answer Cache

TBD
