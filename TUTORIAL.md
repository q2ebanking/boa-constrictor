# Boa Constrictor Quickstart Tutorial

This tutorial will teach you how to use Boa Constrictor and the Screenplay Pattern step-by-step.
In this tutorial, you will build a small test project using [NUnit](https://nunit.org/).
Inside, you will create a simple Web UI test case that performs a search using [DuckDuckGo](https://duckduckgo.com/).
You will use Boa Constrictor to handle all interactions.
At the end of the tutorial, you will have a complete test project that you can extend with new tests or use as a reference for future projects.

*This tutorial should take about 30 minutes to complete.*


## Tutorial Setup

This section contains instructions for setting up the tutorial.


### Example Code

This document provides all code for the tutorial.
The *Boa.Constrictor.Example* project in this repository also contains this tutorial's completed example code.
It is a .NET Core project.
If you get stuck during the tutorial, refer to this project.


### Prerequisites

Boa Constrictor is a C# library.
You will need C# programming skills to use it.
If you are new to C#, take some time to learn it before trying Boa Constrictor.
C# is very similar to Java and C++.

This tutorial uses [Google Chrome](https://www.google.com/chrome/) for Web UI automation.
Install or update the latest version of Chrome on your machine before starting the tutorial.

Since Boa Constrictor uses [Selenium WebDriver](https://www.selenium.dev/) under the hood,
you will also need to download and install the latest version of [ChromeDriver](https://chromedriver.chromium.org/) on your system PATH.
Follow the instructions on the
[Selenium WebDriver Driver Requirements](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/)
page to set it up.

(Note: You may also use other browsers for testing.
To use other browsers, install the appropriate WebDriver executable,
and substitute the desired WebDriver constructor in the example code.)

[Microsoft Visual Studio](https://visualstudio.microsoft.com/) is the recommended editor/IDE for this tutorial.
Visual Studio makes it easy to create projects, add packages, navigate code, and run tests.


### Creating the Project
Below are the high-level instructions for creating the example project. 
You may also follow our [Tutorial Setup Guide Using Visual Studio 2019](TUTORIAL-SETUP-GUIDE-VS2019.md), which offers more in-depth instructions on creating the example project.

You will need to create a new .NET test project named *Boa.Constrictor.Example* for this tutorial.
You may create it as part of a new solution or inside an existing solution.
If you are new to .NET development, please read the
[Visual Studio docs on projects and solutions](https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-projects-solutions?view=vs-2019).
The project may be either [.NET Framework or .NET Core](https://dzone.com/articles/net-framework-vs-net-core),
though [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/introduction) is recommended.
You may delete any stub files that Visual Studio automatically creates in the project, like `UnitTest1.cs`.

The tutorial project requires the following NuGet packages:

* Boa.Constrictor
* FluentAssertions
* NUnit
* NUnit3TestAdapter
* Selenium.Support
* Selenium.WebDriver

This tutorial will use [NUnit](https://nunit.org/) as the core test framework.
(Boa Constrictor can also be used with any other .NET test framework, such as
[xUnit.net](https://xunit.net/) and [SpecFlow](https://specflow.org/).)
Inside the project, create a new file named `DuckDuckGoTest.cs`.
Add the following NUnit code stub to the file:

```csharp
using NUnit.Framework;

namespace Boa.Constrictor.Example
{
    public class DuckDuckGoTest
    {
        [Test]
        public void TestDuckDuckGoSearch()
        {

        }
    }
}
```

`DuckDuckGoTest` will contain the main NUnit test case for this tutorial.
To make sure it works, build the project and run the test.
You can run the test from [Test Explorer](https://docs.microsoft.com/en-us/visualstudio/test/run-unit-tests-with-test-explorer?view=vs-2019) in Visual Studio
or from the command line using the [NUnit Console Runner](https://docs.nunit.org/articles/nunit/running-tests/Console-Runner.html).
The test should run and pass.


## Tutorial Instructions

This section contains the main tutorial instructions.
It covers how to use Boa Constrictor and its implementation of the Screenplay Pattern to automate a simple DuckDuckGo search test.
The test case steps will be simple:

1. Load the DuckDuckGo home page
2. Verify the search field is empty
3. Enter a search phrase
4. Verify result links are returned

Build and run your code after each step to make sure it works.


### Screenplay Basics

The [Screenplayer Pattern](https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay/)
is a design pattern for automating interactions with features under test.
The Screenplay Pattern has existed for years,
but Boa Constrictor's implementation of it is different from other popular implementations
(like [Serenity BDD](http://serenity-bdd.info/#/)).
One of Boa Constrictor's goals is to provide a simpler, standalone version of the Screenplay Pattern.
Rather than giving a bunch of definitions and diagrams up front,
this tutorial will show how to use Boa Constrictor's Screenplay Pattern step-by-step with example code.

The heart of the Screenplay Pattern can be defined in one line:
**Actors use Abilities to perform Interactions.**

* *Actors* initiate Interactions.
* *Abilities* enable Actors to initiate Interactions.
* *Interactions* are procedures that exercise behaviors under test.

The tutorial steps will explain each in detail.


### Creating the Actor

The *Actor* is the entity that initiates Interactions.
It represents the main caller.
For example, it could represent a user logged into a Web app.
All Screenplay calls start with an Actor.
Most test cases need only one Actor.

To create an actor, add the following import statements to `DuckDuckGoTest`:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
```

Then, add the following constructor call to its `TestDuckDuckGoSearch` method:

```csharp
IActor actor = new Actor(name: "Andy", logger: new ConsoleLogger());
```

Actors implement the `IActor` interface, which is part of the `Boa.Constrictor.Screenplay` namespace.
The `Actor` class optionally takes two arguments:

1. The first argument is a *name*, which can help describe who the actor is.
   The default name is "Screenplayer".
   The name will appear in logged messages.
2. The second argument is a *logger*, which will send log messages from Screenplay calls to a target destination.
   Loggers must implement the `ILogger` interface.
   They are part of the `Boa.Constrictor.Logging` namespace.
   `ConsoleLogger` is a class that will log messages to the system console.
   You can define your own custom loggers by implementing `ILogger`.
   You can also combine multiple loggers together using `TeeLogger`.

Build and run the test.
It should pass.


### Adding Abilities

*Abilities* enable Actors to initiate Interactions.
That might sound a little weird at first.
In a programming sense, Abilities provide objects that Actors use when calling Interactions.
For example, an Actor needs a Selenium WebDriver instance in order to click elements on a Web page.

Add the following imports to `DuckDuckGoTest`:

```csharp
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium.Chrome;
```

Then, add the following Ability to the Actor:

```csharp
actor.Can(BrowseTheWeb.With(new ChromeDriver()));
```

Read this line in plain English:
"The actor can browse the Web with a new ChromeDriver."
Boa Constrictor's fluent-like syntax makes its call chains very readable.
Let's unpack what this line does:

1. Web UI automation uses Selenium WebDriver to control a browser.
   `new ChromeDriver()` instantiates a new WebDriver object for [ChromeDriver](https://chromedriver.chromium.org/).
   (You could use a different browser type here, and you could also specify WebDriver options.)
2. `BrowseTheWeb` is an Ability that enables Actors to perform Web UI Interactions.
   `BrowseTheWeb.With(...)` constructs the Ability with the given WebDriver object.
3. `actor.Can(...)` adds the given Ability to the Actor.
   In this case, the `actor` Actor is given the `BrowseTheWeb` Ability with a `ChromeDriver` object.

Abilities must implement the `IAbility` interface:

```csharp
public interface IAbility
{
}
```

`IAbility` does *not* have any required methods.
It simply provides a type system for Abilities.
You can implement your own Abilities using this interface.

The `BrowseTheWeb` Ability is part of the `Boa.Constrictor.WebDriver` namespace.
It looks like this:

```csharp
public class BrowseTheWeb : IAbility
{
    public IWebDriver WebDriver { get; }

    private BrowseTheWeb(IWebDriver driver) =>
        WebDriver = driver;

    public static BrowseTheWeb With(IWebDriver driver) =>
        new BrowseTheWeb(driver);
}
```

It simply holds a reference to the WebDriver object.
Web UI Interactions will retrieve this WebDriver object from the Actor.
Thus, Abilities act as a form of [dependency injection](https://en.wikipedia.org/wiki/Dependency_injection) for Interactions.

Actors can be given any number of Abilities.
For example, one Actor can have both `BrowseTheWeb` and `CallRestApi` (from the `Boa.Constrictor.RestSharp` namespace).
For best practice, give Actors only the Abilities they need.

Build and run the test again.
This time, the test should open a Chrome browser window and then stop.
This means the Ability is working!
Close the browser window when the test stops.


### Modeling Web Pages

Before the Actor can call any WebDriver-based Interactions,
the Web pages under test need models.
These models should be static classes that include locators for elements on the page and possibly page URLs.
Page classes should only model structure - they should **not** include any interaction logic.

The Screenplay Pattern separates the concerns of page structure from interactions.
That way, interactions can target *any* element, maximizing code reusability.
Interactions like clicks and scrapes work the same regardless of the target elements.
(This is different from the
[Page Object Model](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/),
in which page object classes place locators together with interaction methods.)

The DuckDuckGo search test interacts with two pages:
the "search" page (or the "home" page) and the "result" page.
Each page should have its own class.

Create a file named `SearchPage.cs` and add the following code:

```csharp
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class SearchPage
    {
        public const string Url = "https://www.duckduckgo.com/";

        public static IWebLocator SearchInput => L(
          "DuckDuckGo Search Input",
          By.Id("search_form_input_homepage"));
    }
}
```

The `SearchPage` class has two members.
The first member is a URL string named `Url`.
Sometimes, it is convenient to hard-code URLs for pages.

The second member is a locator for the search input element named `SearchInput`.
A *locator* is a pointer to a Web page element.
Boa Constrictor locator objects must implement the `IWebLocator` interface:

```csharp
public interface IWebLocator
{
    string Description { get; }
    By Query { get; }
}
```

A locator has two parts:

1. A *Description* that describes the element in plain language.
   It will be used for logging.
2. A *Query* that is used to find the element on the page.
   Boa Constrictor uses Selenium WebDriver's `By` queries.
   Learn more about locator queries by reading
   [Web Element Locators for Test Automation](https://automationpanda.com/2019/01/15/web-element-locators-for-test-automation/).

For convenience, locators can be constructed using the `Boa.Constrictor.WebDriver.WebLocator.L` static builder method.
Since `SearchPage` uses a static import for this method, it can use the short `L` method call.

Furthermore, notice that `SearchInput` uses the `=>` operator instead of the `=` operator for defining the locator.
The `=>` operator makes `SearchInput` a read-only property: its value *cannot* be changed.
Locators should be treated as immutable.

Boa Constrictor Interactions uses locators to interact with elements on a Web page.
Interactions always fetch "fresh" element objects using locators instead of caching element objects.
Fresh fetches avoid stale element exceptions.

In addition to `SearchPage.cs`, create a file named `ResultPage.cs` with the following code:

```csharp
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

namespace Boa.Constrictor.Example
{
    public static class ResultPage
    {
    }
}
```

Leave `ResultPage` empty for now.
You will add locators to both classes later in this tutorial.


### Attempting a Task

The Screenplay Pattern has two types of Interactions.
The first type of Interaction is called a Task.
A *Task* performs actions without returning a value.
Examples of Tasks include clicking an element, refreshing the browser, and loading a page.
These interactions all "do" something rather than "get" something.

The test case's first step should load the DuckDuckGo search page.
Boa Constrictor provides a Task named `Navigate` under the `Boa.Constrictor.WebDriver` namespace for loading a Web page using a target URL.

Add this line to `TestDuckDuckGoSearch`:

```csharp
actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
```

Read this line in plain English:
"The actor attempts to navigate to the URL for the search page."
Again, Boa Constrictor's fluent-like syntax is very readable.
Clearly, this line will load the DuckDuckGo search page.
Let's unpack it:

* `SearchPage.Url` is the target URL.
  It is a member of the `SearchPage` model class so that it can be used by any Interaction.
* `Navigate.ToUrl(...)` constructs a Task object using the given URL string.
  The `Navigate` class provides the logic for performing the page load.
* `actor.AttemptsTo(...)` calls the given Task.
  The call is an "attempt" because the Task may or may not ultimately be successful.

All Interactions, including Tasks, must implement the `IInteraction` interface for common typing:

```csharp
public interface IInteraction
{
}
```

Tasks must implement the `ITask` interface:

```csharp
public interface ITask : IInteraction
{
    void PerformAs(IActor actor);
}
```

A Task's main logic is in its `PerformAs` method.
The method's return type is `void` because Tasks don't return anything.

Boa Constrictor provides several WebDriver-based Interactions under the `Boa.Constrictor.WebDriver` namespace.
The `Navigate` Task is one of them.
You do not need to create it in the tutorial project.
Below is a simplified version of the `Navigate` Task's code:

```csharp
public class Navigate : ITask
{
    private string Url { get; set; }

    private Navigate(string url) =>
        Url = url;

    public static Navigate ToUrl(string url) =>
        new Navigate(url);

    public void PerformAs(IActor actor)
    {
        var driver = actor.Using<BrowseTheWeb>().WebDriver;
        driver.Navigate().GoToUrl(Url);
    }
}
```

The `Navigate` Task has a property named `Url` for its target URL.
Its constructor is private so that callers must use the static `Navigate.ToUrl(...)` builder method, which makes calls more readable.
Its `PerformAs` method gets a reference to the WebDriver object by "using" the Actor's Ability to "browse the Web,"
and then it makes WebDriver calls to navigate to the target URL.

Actors can call any Tasks using the `AttemptsTo` method:

```csharp
public void AttemptsTo(ITask task)
{
    task.PerformAs(this);
}
```

The `AttemptsTo` method takes in a Task and calls the Task's `PerformAs` method.
It also injects a reference to the Actor so that the Task can access the Actor's Abilities.
This pattern preserves the separation of concerns between Actors and Interactions.
The Actor can call *any* Tasks as long as it has the appropriate Abilities.
Actor code does not need to be modified to call more types of Tasks.

Build and run the test again.
This time, the browser should load the DuckDuckGo search page.
Close the browser once the test stops.


### Asking a Question

The second type of Interaction is called a Question.
A *Question* returns an answer after performing actions.
Examples of Questions include getting an element's text, location, and appearance.
Each of these interactions return some sort of value.

The test case's second step verifies that the search field is empty.
The `ValueAttribute` Question gets the "value" of the text currently inside an input field.
(Note: this is different from regular element text, which uses the `Text` Question.)
To use `ValueAttribute`, add the following line to `TestDuckDuckGoSearch`:

```csharp
string text = actor.AsksFor(ValueAttribute.Of(SearchPage.SearchInput));
```

Read this line in plain English:
"The actor asks for the value attribute of the search page's search input element."
Let's break it down:

* `SearchPage.SearchInput` is the locator for the search input field.
  You previously added this locator to the `SearchPage` class.
* `ValueAttribute.Of(...)` constructs a Question using the given locator.
  It returns the "value" attribute of the locator's target element.
* `actor.AsksFor(...)` calls the given Question.
  The Actor "asks for" the answer to the Question.

Questions must implement the `IQuestion` interface:

```csharp
public interface IQuestion<TAnswer> : IInteraction
{
    TAnswer RequestAs(IActor actor);
}
```

Questions are generic in their return value type.
The main logic is in the `RequestAs` method, which returns a type-appropriate answer.

The `ValueAttribute` Question is one of several WebDriver-based Questions available under the `Boa.Constrictor.WebDriver` namespace.
Below is a simplified version of its code:

```csharp
public class ValueAttribute : IQuestion<string>
{
    public IWebLocator Locator { get; }

    private ValueAttribute(IWebLocator locator) =>
        Locator = locator;

    public static ValueAttribute Of(IWebLocator locator) =>
        new ValueAttribute(locator);

    public string RequestAs(IActor actor)
    {
        var driver = actor.Using<BrowseTheWeb>().WebDriver;
        actor.AttemptsTo(Wait.Until(Existence.Of(Locator), IsEqualTo.True()));
        return driver.FindElement(Locator.Query).GetAttribute("value");
    }
}
```

The `ValueAttribute` Question has a property for the target element's locator named `Locator`.
Just like the `Navigate` Task, it has a private constructor and a public static builder method.
The `RequestAs` method gets the WebDriver object from the Actor's `BrowseTheWeb` Ability.
It then waits for the element to exist on the page, finds the element, and return's the element's "value" attribute.
Under the hood, these are all just Selenium WebDriver calls.
(Waiting will be explained later in the tutorial.)

Actors can call any Questions using the `AsksFor` method:

```csharp
public TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question)
{
    return question.RequestAs(this);
}
```

This method is analogous to `AttemptsTo` for Tasks.

Simply getting the search field's value is not sufficient for testing.
The test case must also verify that the value is empty using an assertion.
The recommended assertion library to use with Boa Constrictor is
[Fluent Assertions](https://fluentassertions.com/).

Add the following import statement to `DuckDuckGoTest`:

```csharp
using FluentAssertions;
```

Then, update the Question call like this:

```csharp
string text = actor.AsksFor(ValueAttribute.Of(SearchPage.SearchInput));
text.Should().BeEmpty();
```

You can also shorten this call to one line:

```csharp
actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
```

The `AskingFor` method is simply an alias for `AsksFor`.
It improves readability when using Fluent Assertions.

Build and run the test again.
It should open the browser, load DuckDuckGo, and pass just like before.
If you want to make sure the assertion is really working, you can temporarily change it to `Should().NotBeEmpty()` and watch the test fail.


### Composing a Custom Interaction

The test case's next step is to enter a search phrase.
Doing this requires two interactions: typing the phrase into the search input and clicking the search button.

Add a new locator for the search button to `SearchPage`:

```csharp
public static IWebLocator SearchButton => L(
    "DuckDuckGo Search Button",
    By.Id("search_button_homepage"));
```

Then, add the following lines to `TestDuckDuckGoSearch`:

```csharp
actor.AttemptsTo(SendKeys.To(SearchPage.SearchInput, "panda"));
actor.AttemptsTo(Click.On(SearchPage.SearchButton));
```

`SendKeys` and `Click` are two more Tasks provided by Boa Constrictor.
They do precisely what they say.
However, these two Interactions truly represent one larger interaction: entering a search phrase.
The Screenplay Pattern makes it possible to easily *compose* multiple Interactions together into one new Interaction.
Composition improves readability and reusability.

Create a new file named `SearchDuckDuckGo.cs` and add the following code:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;

namespace Boa.Constrictor.Example
{
    public class SearchDuckDuckGo : ITask
    {
        public string Phrase { get; }

        private SearchDuckDuckGo(string phrase) =>
          Phrase = phrase;

        public static SearchDuckDuckGo For(string phrase) =>
          new SearchDuckDuckGo(phrase);

        public void PerformAs(IActor actor)
        {
            actor.AttemptsTo(SendKeys.To(SearchPage.SearchInput, Phrase));
            actor.AttemptsTo(Click.On(SearchPage.SearchButton));
        }
    }
}
```

`SearchDuckDuckGo` is a new Task that takes in a search phrase
and internally calls `SendKeys` and `Click` to enter the phrase on the DuckDuckGo search page.

Replace the old calls in `TestDuckDuckGoSearch` with this new Task:

```csharp
actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
```

Read this line in plain English:
"The actor attempts to search DuckDuckGo for 'panda'."
This call is much more intuitively understandable than the previous calls.
It conveys *intention*: the purpose of the step is not to send arbitrary keystrokes and clicks
but rather to perform a DuckDuckGo search.

Custom Interactions like `SearchDuckDuckGo` add a little more code at first,
but they can ultimately avoid lots of repetitive code.
You should make custom Interactions for common operations shared by multiple tests.
For example, `SearchDuckDuckGo` would be very useful for additional search tests.

Build and run the test again.
This time, you should see the search happen.


### Waiting for Questions to Yield Answers

The last test case step should verify that result links appear after entering a search phrase.
Unfortunately, this step has a *race condition*:
the result page takes a few seconds to display result links.
Automation must wait for those links to appear.
Checking too early will make the test case fail.

Boa Constrictor makes waiting easy.
Add the following locator to the `ResultPage` class:

```csharp
public static IWebLocator ResultLinks => L(
    "DuckDuckGo Result Page Links",
    By.ClassName("result__a"));
```

This locator will find all result links on the result page.

Then, add the following line to `TestDuckDuckGoSearch`:

```csharp
actor.AttemptsTo(Wait.Until(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True()));
```

Read this line in plain English:
"The actor attempts to wait until the appearance of result page result links is equal to true."
In simpler terms, "Wait until the result links appear."
Let's break it down:

* `ResultPage.ResultLinks` is the locator for the result link elements.
* `Appearance.Of(...)` is a Question that returns true if the target elements are currently displayed on the page.
* `IsEqualTo.True()` is a *Condition* for checking if the return value of a Question is true.
* `Wait.Until(...)` is a Task that halts execution until the given Question's answer meets the given Condition.
  In this case, the apperance of the result links must become true.

The `Wait` Task is located under the `Boa.Constrictor.Screenplay` namespace.
It works for any type of Question, not just WebDriver-based Questions.
If the given Question fails to meet the given Condition within a timeout limit,
then `Wait` throws a `WaitingException`.
The default timeout is 30 seconds, but it may be overridden like this:
`Wait.Until(...).ForUpTo(60)'`.
There is also a waiting Question named `ValueAfterWaiting` that works the same way as `Wait`
except that it returns the final answer from the given Question.

Waiting also requires Conditions.
A *Condition* is a required state for a value.
Boa Constrictor provides several basic conditions under the `Boa.Constrictor.Screenplay` namespace,
such as `IsNot`, `IsLessThan`, `IsGreaterThan`, and `Matches`.
All conditions must implement the `ICondition` interface:

```csharp
public interface ICondition<TValue>
{
    bool Evaluate(TValue actual);
}
```

The `Wait` Task will repeatedly call its given Question and pass the answer into its given Condition's `Evaluate` method
until `Evaluate` returns true or the elapsed waiting time exceeds the timeout limit.

Many WebDriver-based Interactions do waiting internally.
For example, the `ValueAttribute` Question shown in a previous step waits for the existence of the target element before getting its "value" attribute.
Automatic waiting is a major advantage of Boa Constrictor's Interactions.
Raw WebDriver calls do *not* wait, and they cause race conditions (resulting in "flakiness") when testers don't remember to add waiting.
Typically Boa Constrictor Interactions that perform an action on an element wait for the target element's existence or appearance,
and Interactions that check existence (like `Appearance`, `Existence`, and `Count`) do not do waiting.

Since this test case step simply needs to verify that the result links appeared,
it does not need to make an explicit assertion.
The `Wait` Task has an implicit assertion in that failure to meet the Condition throws an exception.

Build and run the test again.
The browser should do the same things as before, and the test case should pass.
This time, the test won't stop until the result links appear.


### Quitting the Browser

Before ending the test case, the browser must be quit safely.
Otherwise, the browser and its associated WebDriver executable will keep running,
hogging system resources and possibly causing other problems.

Add the following call to the bottom of `TestDuckDuckGoSearch`:

```csharp
actor.AttemptsTo(QuitWebDriver.ForBrowser());
```

Read this line in plain English:
"The actor attempts to quit the WebDriver for the browser."
Internally, this Task calls the WebDriver's `Quit` method.

Build and run the test again.
This time, when the test finishes, it will automatically quit the browser window.


### Refactoring the Project

The test steps are complete, and if you run the test case, it should pass.
However, it should be refactored a bit for better setup and cleanup.
Rewrite `DuckDuckGoTest` with the following code:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace Boa.Constrictor.Example
{
    public class DuckDuckGoTest
    {
        private IActor Actor;

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
        public void TestDuckDuckGoSearch()
        {
            Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
            Actor.AskingFor(ValueAttribute.Of(SearchPage.SearchInput)).Should().BeEmpty();
            Actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
            Actor.AttemptsTo(Wait.Until(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True()));
        }
    }
}
```

Actor and Ability creation are part of the `SetUp` method because they could be shared by multiple tests.
The `QuitWebDriver` Task is part of the `TearDown` method so that every test quits the browser even upon failure.
Never forget to do that!

Furthermore, source files should be organized by concern.
Create new folders and move source files like this:

```
Boa.Constrictor.Example
 |- Interactions
 |   `- SearchDuckDuckGo.cs
 |- Pages
 |   |- ResultPage.cs
 |   `- SearchPage.cs
 `- Tests
     `- DuckDuckGoTest.cs
```

Build and run the test code one final time to make sure it passes.


## Conclusion

Congrats on finishing the tutorial!

Boa Constrictor provides several Interactions that could not be covered in this brief tutorial:
* Web UI interactions using [Selenium WebDriver](https://www.selenium.dev/) are located under `Boa.Constrictor\WebDriver`.
* REST API interactions using [RestSharp](https://restsharp.dev/) are located under `Boa.Constrictor\RestSharp`.

Take some time to review them.
They will be very useful when writing new tests.
You can also use them as examples for writing new Interactions.

If you'd like to contribute to the Boa Constrictor project, please read the [README](README.md#guidelines-for-contribution) for guidelines.
