---
title: Testing with SpecFlow
layout: single
permalink: /user-guides/testing-with-specflow/
sidebar:
  nav: "user-guides"
toc: true
---

[SpecFlow](https://specflow.org/) is the most popular Behavior-Driven Development (BDD) test framework for .NET.
In SpecFlow, tests are written as Given-When-Then scenarios in Gherkin feature files.
Then, each line of Gherkin is automated via a step definition method written in a .NET programming language like C#.
BDD test frameworks like SpecFlow separate test *cases* from test *code*.

Boa Constrictor works together seamlessly with SpecFlow.
In fact, Boa Constrictor was initially developed as part of a large test automation project that used SpecFlow as its core framework!
With Boa Constrictor, SpecFlow's step definitions become very concise, readable, and safe.

This user guide shows how to integrate Boa Constrictor with SpecFlow.
The test case in the example code below will follow the same steps
as the test case from the [tutorial]({{ "/tutorial/overview/" | relative_url }}):
a Web UI test for DuckDuckGo searches.


## SpecFlow Test Projects

Boa Constrictor can integrate with any SpecFlow test project.
If you are new to SpecFlow, read [SpecFlow's official documentation](https://docs.specflow.org/projects/specflow/en/latest/)
to learn how to get started with SpecFlow.
Steps to set up a SpecFlow test project include:

1. Install the [SpecFlow extension for Visual Studio](https://docs.specflow.org/projects/specflow/en/latest/visualstudio/visual-studio-installation.html).
2. Create a new [SpecFlow project in Visual Studio](https://docs.specflow.org/projects/specflow/en/latest/Installation/Project-and-Item-Templates.html).
3. Install the [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor/) NuGet package into the project.

.NET Core projects are typically recommended over .NET Framework projects.
The project will need the [SpecFlow](https://www.nuget.org/packages/SpecFlow/) NuGet package,
but Visual Studio should automatically add it when creating a SpecFlow project.
You may need to add other NuGet packages like
[FluentAssertions](https://www.nuget.org/packages/FluentAssertions/) as well.

To run SpecFlow tests, you will also need to choose a
[unit test provider](https://docs.specflow.org/projects/specflow/en/latest/Installation/Unit-Test-Providers.html).
Choose only one unit test provider, and install it by adding its NuGet package to the project.
Boa Constrictor works with any unit test provider,
but [SpecFlow+ Runner](https://docs.specflow.org/projects/specflow-runner/en/latest/) is recommended.
To [install SpecFlow+ Runner](https://docs.specflow.org/projects/specflow-runner/en/latest/Installation/Installation.html),
you will need to register a (free) SpecFlow account.


## Gherkin Feature Files

In SpecFlow, all tests are written in [Gherkin](https://docs.specflow.org/projects/specflow/en/latest/Gherkin/Gherkin-Reference.html)
and saved in `.feature` files.
Gherkin's plain language Given-When-Then steps enable anyone to read the tests,
whether or not they know programming.

Below is a feature file for testing a DuckDuckGo search:

```gherkin
Feature: DuckDuckGo web search

  Scenario: Search for a phrase using DuckDuckGo
    Given the DuckDuckGo home page is displayed
    When the user searches for the phrase "panda"
    Then the results page shows result links for the phrase
```


## Before-Scenario Hooks

In SpecFlow, all automation code is located in "step definition" or "binding" classes.
These classes require SpecFlow's `[Binding]` attribute, and they may also be child classes of the `Steps` class.

"Hooks" are special methods that run before or after special points of execution.
For example, a before-scenario hook will execute before each Gherkin scenario is executed,
analogously to how an [NUnit `[SetUp]`]({{ "/user-guides/testing-with-nunit/#screenplay-setup-methods" | relative_url }}) method works.
Before-scenario hooks must be located in binding classes and bear the `[BeforeScenario]` attribute.
Binding classes may have multiple hooks, and hooks may optionally be assigned an order for execution.

As a recommended practice, each SpecFlow test project should have a binding class for project-wide hooks.
For Boa Constrictor, this binding class should have a before-scenario hook to
construct an Actor for each test,
add Abilities to the Actor,
and inject the Actor into SpecFlow's `ScenarioContext.ScenarioContainer` object.
The scenario context injection will enable other binding classes to access the Actor object.

Below is a binding class named `DuckDuckGoHooks`
with a before-scenario hook to create and inject the Actor.
The hook is set with `(Order = 1)` to make sure it runs before any other before-scenario hooks:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Boa.Constrictor.Example
{
  [Binding]
  public sealed class DuckDuckGoHooks : Steps
  {
    private IActor Actor { get; set; }

    [BeforeScenario(Order = 1)]
    public void InitializeScreenplay()
    {
      Actor = new Actor(name: "Andy", logger: new ConsoleLogger());
      Actor.Can(BrowseTheWeb.With(new ChromeDriver()));
      ScenarioContext.ScenarioContainer.RegisterInstanceAs(Actor);
    }

    // ...
  }
}
```


## Step Definition Methods

"Step definitions" are special methods that execute Gherkin steps from feature files.
Whenever a Gherkin scenario runs, SpecFlow "glues" each Gherkin step to its associated step definition.
Each step definition method has an annotation with the step type (`Given`, `When`, or `Then`)
and a regular expression to match the step text.
With Boa Constrictor, each step definition should be only a few lines of Screenplay interactions, like
`Actor.AttemptsTo(...)`, `Actor.AsksFor(...)`, and `Actor.WaitsUntil(...)`.

Below is a binding class named `DuckDuckGoSteps`
with step definitions for the three Gherkin steps from the DuckDuckGo search test.
The constructor receives the Actor object via dependency injection from SpecFlow through scenario context.
Each step definition then uses the Actor to call Interactions.
The specific pages (`SearchPage` and `ResultPage`) and custom interactions (`SearchDuckDuckGo`)
come from the [`Boa.Constrictor.Example`](https://github.com/q2ebanking/boa-constrictor/tree/main/Boa.Constrictor.Example) project.

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using TechTalk.SpecFlow;

namespace Boa.Constrictor.Example
{
  [Binding]
  public sealed class DuckDuckGoSteps : Steps
  {
    private IActor Actor { get; set; }

    // The `InitializeScreenplay` hook from `DuckDuckGoHooks`
    // injects the Actor object into scenario context.
    // SpecFlow can resolve that dependency
    // in other binding classes using constructor arguments.

    public DuckDuckGoSteps(IActor actor)
    {
      Actor = actor;
    }

    [Given(@"the DuckDuckGo home page is displayed")]
    public void GivenTheDuckDuckGoHomePageIsDisplayed()
    {
      Actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
    }

    [When(@"the user searches for the phrase ""(.*)""")]
    public void WhenTheUserSearchesForThePhrase(string phrase)
    {
      Actor.AttemptsTo(SearchDuckDuckGo.For(phrase));
    }

    [Then(@"the results page shows result links for the phrase")]
    public void ThenTheResultsPageShowsResultLinksForThePhrase()
    {
      Actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
    }
  }
}
```


## After-Scenario Hooks

After-scenario hooks are analogous to 
[NUnit `[TearDown]`]({{ "/user-guides/testing-with-nunit/#screenplay-teardown-methods" | relative_url }}) methods:
they run after each test to perform cleanup operations, whether the test passed or failed.
After-scenario hooks must be located in binding classes and bear the `[AfterScenario]` attribute.
Like before-scenario hooks, they may or may not have an explicit order.

Below is an after-scenario hook that quits the browser.
It should be located in the `DuckDuckGoHooks` binding class:

```csharp
    [AfterScenario]
    public void QuitBrowser()
    {
      Actor.AttemptsTo(QuitWebDriver.ForBrowser());
    }
```
