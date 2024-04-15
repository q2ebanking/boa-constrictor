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
a Web UI test for Wikipedia searches.


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

Below is a class stub for the example test class named `WikipediaTest`:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using Boa.Constrictor.Xunit;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Boa.Constrictor.Example
{
  public class WikipediaTest
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

xUnit also does not capture console output. Instead, it offers [ITestOutputHelper](https://xunit.net/docs/capturing-output#output-in-tests). 
In order to configure an actor to use this mechanism you may use `TestOutputLogger`

Below is the `WikipediaTest` constructor that 
constructs an Actor and gives it the Ability to browse the web with ChromeDriver:

```csharp
    public WikipediaTest(ITestOutputHelper output)
    {
      Actor = new Actor(name: "Andy", logger: new TestOutputLogger(output));
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }
```


## Screenplay Test Case

In xUnit.net test classes, methods with the `[Fact]` or `[Theory]` attributes are individual test cases.
With Boa Constrictor, most test cases should be a series of Screenplay interactions, like
`Actor.AttemptsTo(...)`, `Actor.AsksFor(...)`, and `Actor.WaitsUntil(...)`.

Below is a test case method for the example `WikipediaTest` class.
The specific pages (`MainPage` and `ArticlePage`) and custom interactions (`SearchWikipedia`)
come from the [`Boa.Constrictor.Example`](https://github.com/q2ebanking/boa-constrictor/tree/main/Boa.Constrictor.Example) project:

```csharp
    [Fact]
    public void TestWikipediaSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(MainPage.Url));
      Assert.Equal("", Actor.AskingFor(ValueAttribute.Of(MainPage.SearchInput)));
      Actor.AttemptsTo(SearchWikipedia.For("Giant panda"));
      Actor.WaitsUntil(Text.Of(ArticlePage.Title), IsEqualTo.Value("Giant panda"));
    }
```

xUnit.net provides an `Assert` class for built-in assertions, which is used in the test method above.
You can use [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) instead for more readable Screenplay calls:

```csharp
      Actor.AskingFor(ValueAttribute.Of(MainPage.SearchInput)).Should().BeEmpty();
```


## Screenplay Cleanup

Cleanup procedures are not required for all types of tests, but they are required for WebDriver-based tests.
xUnit.net test classes must implement the `System.IDisposable` interface to handle test case cleanup:

```csharp
  public class WikipediaTest : System.IDisposable
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

The complete code for `WikipediaTest` is below:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Boa.Constrictor.Example
{
  public class WikipediaTest : System.IDisposable
  {
    private IActor Actor { get; set; }

    public WikipediaTest(ITestOutputHelper output)
    {
      Actor = new Actor(name: "Andy", logger: new TestOutputLogger(output));
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
    }

    public void Dispose()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }

    [Fact]
    public void TestWikipediaSearch()
    {
      Actor.AttemptsTo(Navigate.ToUrl(MainPage.Url));
      Assert.Equal("", Actor.AskingFor(ValueAttribute.Of(MainPage.SearchInput)));
      Actor.AttemptsTo(SearchWikipedia.For("Giant panda"));
      Actor.WaitsUntil(Text.Of(ArticlePage.Title), IsEqualTo.Value("Giant panda"));
    }
  }
}
```

## Shared Context
xUnit offers a [number of ways](https://xunit.net/docs/shared-context) to share Setup and Cleanup code

For example, a [Class Fixture](https://xunit.net/docs/shared-context#class-fixture) could be used to perform a 
setup that occurs only once for
all the tests in a class (Similar to NUnits `[OneTimeSetup]`` method). Instead, one time setup 
is done in the class fixture constructor. It's not advised to have a public Actor in a shared context.
Each test should have its own Actor with its own set of Abilities to preserve test case independence.
However, you could use a private Actor to perform any screenplay task you wish in your one-time setup.

xUnit does not capture console output, or make use of its `ITestOutputHelper` in any of it's extensibility objects. 
Instead, it offers [IMessageSink](https://xunit.net/docs/capturing-output#output-in-extensions). In order to configure
an actor to use this mechanism you may use `MessageSinkLogger`

Below is an example of a possible Test Fixture usage. It connects and initializes some database data. 
xUnit injects `IMessageSink` which can be used to initialize `MessageSinkLogger`. The connection is a public property
so that it can be shared to the test case
```csharp
public class DatabaseFixture : IDisposable
{
    public DatabaseFixture(IMessageSink messageSink)
    {
        Connection = new SqlConnection("MyConnectionString");
        var actor = new Actor("Fixture Actor", new MessageSinkLogger(messageSink));
        actor.Can(ConnectToDatabase.Using(Connection));
        // ... initialize data in the test database ...
    }

    public void Dispose()
    {
        // ... clean up test data from the database ...
    }

    public SqlConnection Connection{ get; private set; }
}
```
In the test case, it implements the IClassFixture interface which allows the fixture to be injected into the constructor.
From here we can extract the database connection, and initialize our actor as we would for any other test.
```csharp
public class DatabaseTest : IClassFixture<DatabaseFixture>
{
    public Actor DatabaseAdmin { get; set; }

    public DatabaseTest(DatabaseFixture databaseFixture, ITestOutputHelper outputHelper)
    {
        DatabaseAdmin = new Actor("Admin", new TestOutputLogger(outputHelper));
        DatabaseAdmin.Can(ConnectToDatabase.Using(databaseFixture.Connection));
    }

    [Fact]
    public void CanDeleteWidget()
    {
        /* Use DatabaseAdmin to get access to database connection */
    }
}
```
`IMessageSink` does not write to the test output. Instead, it writes to the test runner diagnostics.
This is a limitation of xUnit and cannot be controlled by Boa.Constrictor. Make sure you have the [diagnosticMessages flag](https://xunit.net/docs/configuration-files#diagnosticMessages)
enabled. Where you find this info will depend on your IDE. In Visual Studio, it's available in the output window.
{: .notice--warning}