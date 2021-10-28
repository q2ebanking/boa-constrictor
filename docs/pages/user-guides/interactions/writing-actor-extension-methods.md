---
title: Writing Actor Extension Methods
layout: single
permalink: /user-guides/writing-actor-extension-methods/
sidebar:
  nav: "user-guides"
toc: true
---

Sometimes, a Question can become quite complex and cumbersome to parse. When this happens, it may be helpful to write an
extension method to create more a more readable, fluent call. For example, consider the following Question:

```csharp
actor.AsksFor(ValueAfterWaiting
    .Until(Appearance.Of(SearchPage.SearchInput), IsEqualTo.True())
    .ForUpTo(30)
    .ForAnAdditional(30));
```

In this scenario, you could use the built-in `WaitsUntil` extension method:

```csharp
/// <summary>
/// Provides IActor extension methods to simplify waiting syntax.
/// 
/// </summary>
public static class WaitingExtensions
{
    /// <summary>
    /// A simplified extension method for waiting.
    /// Calls will look like `Actor.WaitsUntil(...)` instead of `Actor.AsksFor(ValueAfterWaiting.Until(...))`.
    /// </summary>
    /// <typeparam name="TAnswer">The type of the Question's answer value.</typeparam>
    /// <param name="actor">The Screenplay Actor.</param>
    /// <param name="question">The Question upon whose answer to wait.</param>
    /// <param name="condition">The expected condition for which to wait.</param>
    /// <param name="timeout">The timeout override in seconds. If null, use the standard timeout value.</param>
    /// <param name="additional">An additional amount to add to the timeout. Defaults to 0.</param>
    /// <returns></returns>
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

Actor extension methods are a useful way to simplify complex calls and make them more readable. An important caveat:
in order to avoid oversaturating `IActor` with methods and making the code difficult to follow, it is recommended to use
extension methods only when they would have a large benefit in terms of readability.
