---
title: Testing with xUnit.net
layout: single
permalink: /user-guides/testing-with-xunit-net/
sidebar:
  nav: "user-guides"
toc: true
---

[xUnit.net](https://xunit.net/) is one of the most popular .NET test frameworks.
It is part of the [xUnit](https://en.wikipedia.org/wiki/XUnit) family of test frameworks,
and it is a popular alternative to [NUnit](https://nunit.org/).
Boa Constrictor works seamlessly with xUnit.net.
Boa Constrictor's fluent-like syntax makes xUnit.net test classes easy to read and understand.

This user guide shows how to integrate Boa Constrictor with xUnit.net.
The test case in the example code below will follow the same steps
as the test case from the [tutorial]({{ "/tutorial/overview/" | relative_url }}):
a Web UI test for DuckDuckGo searches.


## xUnit.net Test Projects

Boa Constrictor can integrate with any xUnit.net test project.
If you are new to xUnit.net, read [xUnit.net's official documentation](https://xunit.net/#documentation)
to learn how to get started with xUnit.net.
Steps to set up an xUnit.net test project include:

1. Create a new xUnit.net test project in Visual Studio.
2. Install the [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor/) NuGet packages into the project.

.NET Core projects are typically recommended over .NET Framework projects.
The project will need the [xunit](https://www.nuget.org/packages/xunit/)
and [xunit.runner.visualstudio](https://www.nuget.org/packages/xunit.runner.visualstudio/) NuGet packages,
but Visual Studio should automatically add them when creating an xUnit.net project.
You may need to add other NuGet packages like
[FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) as well.


## xUnit.net Test Classes

xUnit.net test classes should have all the appropriate `using` statements for the namespaces they need.
They should also have an instance variable for the Screenplay Actor object.

Below is a class stub for the example test class named `DuckDuckGoTest`:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Boa.Constrictor.Example
{
  public class DuckDuckGoTest
  {
    private IActor Actor { get; set; }

    // ...
}
```


## Screenplay Setup

xUnit.net test classes do not have "set up" methods like
[NUnit's `[SetUp]` methods]({{ "/user-guides/testing-with-nunit/#screenplay-setup-methods" | relative_url }}).
Instead, test case setup is done in test class constructors.
Each test should have its own Actor with its own set of Abilities to preserve test case independence.

Below is the `DuckDuckGoTest` constructor that 
constructs an Actor and gives it the Ability to browse the web with ChromeDriver:

```csharp
    public DuckDuckGoTest()
    {
      Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }
```


## Screenplay Test Case

In xUnit.net test classes, methods with the `[Fact]` or `[Theory]` attributes are individual test cases.
With Boa Constrictor, most test cases should be a series of Screenplay interactions, like
`Actor.AttemptsTo(...)`, `Actor.AsksFor(...)`, and `Actor.WaitsUntil(...)`.

Below is a test case method for the example `DuckDuckGoTest` class.
The specific pages (`SearchPage` and `ResultPage`) and custom interactions (`SearchDuckDuckGo`)
come from the [`Boa.Constrictor.Example`](https://github.com/q2ebanking/boa-constrictor/tree/main/Boa.Constrictor.Example) project:

```csharp
    [Fact]
    public void TestDuckDuckGoSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
      Assert.Equal("", Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)));
      Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
      Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
    }
```

xUnit.net provides an `Assert` class for built-in assertions, which is used in the test method above.
You can use [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) instead for more readable Screenplay calls:

```csharp
      Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
```


## Screenplay Cleanup

Cleanup procedures are not required for all types of tests, but they are required for WebDriver-based tests.
xUnit.net test classes must implement the `System.IDisposable` interface to handle test case cleanup:

```csharp
  public class DuckDuckGoTest : System.IDisposable
```

The `Dispose` method performs cleanup operations after each test.
The code below safely quits the browser for Web UI test cleanup:

```csharp
    public void Dispose()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }
```


## A Complete xUnit.net Test Class

The complete code for `DuckDuckGoTest` is below:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Boa.Constrictor.Example
{
  public class DuckDuckGoTest : System.IDisposable
  {
    private IActor Actor { get; set; }

    public DuckDuckGoTest()
    {
      Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }

    public void Dispose()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }

    [Fact]
    public void TestDuckDuckGoSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
      Assert.Equal("", Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)));
      Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
      Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
    }
  }
}
```
