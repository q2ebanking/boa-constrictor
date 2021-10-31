---
title: Testing with NUnit
layout: single
permalink: /user-guides/testing-with-nunit/
sidebar:
  nav: "user-guides"
toc: true
---

[NUnit](https://nunit.org/) is one of the most popular .NET test frameworks.
It is part of the [xUnit](https://en.wikipedia.org/wiki/XUnit) family of test frameworks.
Boa Constrictor works seamlessly with NUnit.
In fact, the [tutorial]({{ "/tutorial/overview/" | relative_url }}) uses NUnit as its core test framework!
Boa Constrictor's fluent-like syntax makes NUnit test classes easy to read and understand.

This user guide shows how to integrate Boa Constrictor with NUnit.
The example code matches the `ScreenplayWebUiTest` class in the `Boa.Constrictor.Example` project
(which is also part of the tutorial).


## NUnit Test Projects

Boa Constrictor can integrate with any NUnit test project.
Full instructions for project setup are given by [Part 1 of the tutorial]({{ "/tutorial/part-1-setup/" | relative_url }}).
In brief:

1. Create a new NUnit test project in Visual Studio.
2. Install the [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor/) NuGet packages into the project.

.NET Core projects are typically recommended over .NET Framework projects.
The project will need the [NUnit](https://www.nuget.org/packages/NUnit/)
and [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/) NuGet packages,
but Visual Studio should automatically add them when creating an NUnit project.
You may need to add other NuGet packages like
[FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) as well.


## NUnit Test Classes

NUnit test classes should have all the appropriate `using` statements for the namespaces they need.
They should also have an instance variable for the Screenplay Actor object.

Below is a class stub for the example `ScreenplayWebUiTest` class:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Boa.Constrictor.Example
{
  public class ScreenplayWebUiTest
  {
    private IActor Actor { get; set; }

    // ...
  }
}
```


## Screenplay SetUp Methods

In NUnit test classes, methods with the `[SetUp]` attribute run before each test case to "set up" the test.
Each test should have its own Actor with its own set of Abilities to preserve test case independence.

Below is the `[SetUp]` method for the example `ScreenplayWebUiTest` class.
It constructs an Actor and gives it the Ability to browse the web with ChromeDriver:

```csharp
    [SetUp]
    public void InitializeScreenplay()
    {
      Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }
```


## Screenplay Test Methods

In NUnit test classes, methods with the `[Test]` or `[TestCase]` attributes are individual test cases.
With Boa Constrictor, most test cases should be a series of Screenplay interactions, like
`Actor.AttemptsTo(...)`, `Actor.AsksFor(...)`, and `Actor.WaitsUntil(...)`.

Below is a test case method for the example `ScreenplayWebUiTest` class:

```csharp
    [Test]
    public void TestDuckDuckGoWebSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
      Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
      Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
      Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
    }
```


## Screenplay TearDown Methods

In NUnit test classes, methods with the `[TearDown]` attribute run after each test case to "tear down" (or "clean up") the test.
Cleanup procedures are not required for all types of tests, but they are required for WebDriver-based tests.

Below is the `[TearDown]` method for the example `ScreenplayWebUiTest` class.
It safely quits the browser for Web UI test cleanup:

```csharp
    [TearDown]
    public void QuitBrowser()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }
```


## A Complete NUnit Test Class

The complete code for `ScreenplayWebUiTest` is below:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Boa.Constrictor.Example
{
  public class ScreenplayWebUiTest
  {
    private IActor Actor { get; set; }

    [SetUp]
    public void InitializeScreenplay()
    {
      Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }

    [TearDown]
    public void QuitBrowser()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }

    [Test]
    public void TestDuckDuckGoWebSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
      Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
      Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
      Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
    }
  }
}
```

The example project contains this class and two other NUnit test classes under
[`Boa.Constrictor.Example/Tests`](https://github.com/q2ebanking/boa-constrictor/tree/main/Boa.Constrictor.Example/Tests).
