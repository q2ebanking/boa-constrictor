---
title: Writing Actor Extension Methods
layout: single
permalink: /user-guides/writing-actor-extension-methods/
sidebar:
  nav: "user-guides"
toc: true
---

Sometimes, a Question can become quite complex and cumbersome to parse.
When this happens, it may be helpful to write an extension method to create more a more readable, fluent call.
For example, consider the following Question:

```csharp
actor.AsksFor(ValueAfterWaiting
    .Until(Appearance.Of(SearchPage.SearchInput), IsEqualTo.True())
    .ForUpTo(30)
    .ForAnAdditional(30));
```

In this scenario, you could use the built-in `WaitsUntil` extension method:

```csharp
public static class WaitingExtensions
{
    public static TAnswer WaitsUntil<TAnswer>(
        this IActor actor,
        IQuestion<TAnswer> question,
        ICondition<TAnswer> condition,
        int? timeout = null,
        int additional = 0) =>

        actor.AsksFor(ValueAfterWaiting.Until(question, condition).ForUpTo(timeout).ForAnAdditional(additional));
}
```

With this extension method, we can simplify the previous call:

```csharp
actor.WaitsUntil(Appearance.Of(SearchPage.SearchInput), IsEqualTo.True(), 30, 30);
```


## Conclusion

Actor extension methods are a useful way to simplify complex calls and make them more readable.
An important caveat: in order to avoid oversaturating `IActor` with methods and making the code difficult to follow,
it is recommended to use extension methods only when they would have a large benefit in terms of readability.
