# Boa Constrictor: The .NET Screenplay Pattern

*Boa Constrictor* is a C# implementation of the
[Screenplay Pattern](https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay/).
Its primary use case is Web UI and REST API test automation.
**Boa Constrictor helps you make better interactions for better automation!**

*Interactions* are units of behavior.
Every test case is essentially a sequence of interactions.
When test cases are written, interactions are described in plain language, like, "Log into the app as an admin."
Most of those high-level interactions are composed of smaller interactions, like clicks, scrapes, and waits.

Boa Constrictor can automate interactions at any level in a readable way.
Out of the box, it includes Web UI interactions using [Selenium WebDriver](https://www.selenium.dev/)
and REST API interactions using [RestSharp](https://restsharp.dev/).
It also provides a standard pattern for combining interactions together.
Each interaction is safe, reliable, and has automatic logging.
For Web UI automation, the Screenplay Pattern is an improvement over the
[Page Object Pattern](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/).


## Intro Video

This 32-minute YouTube video introduces Screenplay Pattern concepts and Boa Constrictor code:

[![Intro Video Thumbnail](Images/IntroVideoThumbnail.png?raw=true)](https://youtu.be/i26B1afosCo)


## The Screenplay Pattern

The [Screenplay Pattern](https://precisionlender.atlassian.net/wiki/spaces/PD/pages/515801239/Screenplay+Pattern)
is a design pattern for modeling interactions with features under test.
It can be summarized in one line:
**Actors use Abilities to perform Interactions.**

 * **Actors** initiate Interactions.
 * **Abilities** enable Actors to initiate Interactions.
 * **Interactions** are procedures that exercise the behaviors under test.
   * **Tasks** execute procedures on the features under test.
   * **Questions** return state about the features under test.
   * **Interactions** may use **Locators**, **Requests**, and other **Models**.

For example, an Actor may be given an Ability to browse the Web using a specific browser like Chrome.
The Ability would hold a reference to a Chrome WebDriver instance.
Then, the Actor could call a Task to load a login page, a second Task to enter username and password, and a final Task to click the "login" button.
Each task would access the WebDriver instance through the calling Actor’s Ability to control the Chrome browser.
Abilities provide a mechanism for dependency injection.
Actors can perform any kind of Interaction if it has the required Abilities.

The Screenplay Pattern also uses a fluent-like syntax to make test code very readable.
Here is an example call:
`Actor.AttemptsTo(Click.On(LoginPage.LoginButton));`

Boa Constrictor provides several Interactions out of the box:
* Web UI interactions using [Selenium WebDriver](https://www.selenium.dev/) are located under `Boa.Constrictor\WebDriver`.
* REST API interactions using [RestSharp](https://restsharp.dev/) are located under `Boa.Constrictor\RestSharp`.

The Screenplay Pattern adheres to [SOLID](https://en.wikipedia.org/wiki/SOLID) design principles: 

| SOLID Principle | Explanation |
| --------------- | ----------- |
| [Single-Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) | Actors, Abilities, and Interactions are treated as separate concerns. |
| [Open-Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) | Each new Interaction must be a new class, rather than a modification of an existing class. | 
| [Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle) | Actors can call all Abilities and Interactions the same way. |
| [Interface Segregation Principle](https://en.wikipedia.org/wiki/Interface_segregation_principle) | Actors, Abilities, and Interactions each have distinct, separate interfaces. |
| [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) | Interactions use Abilities via dependency injection from the Actor. |

Below is a diagram illustrating the Screenplay Pattern:
![Screenplay Pattern Diagram](Images/ScreenplayDiagram.png?raw=true)


## Getting Started

Boa Constrictor is a .NET Standard library written in C#.
To use it, install the [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor)
package from NuGet.org into your .NET project.

Then, make sure to install the appropriate WebDriver executables on your system PATH.
For example, you will need to install [ChromeDriver](https://chromedriver.chromium.org/)
to run tests using Google Chrome.
Follow the instructions on the
[Selenium WebDriver Driver Requirements](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/)
page to set them up.
(Alternatively, you can install certain WebDrivers as [NuGet packages](https://www.nuget.org/packages?q=webdriver).)
If you try to run tests using Boa Constrictor but get a `DriverServiceNotFoundException`,
then double-check your WebDriver installation.

Once your project is set up, follow the [quickstart tutorial](TUTORIAL.md)
to learn about the Screenplay Pattern and how to write Boa Constrictor code.
Example code is located under the `Boa.Constrictor.Example` project.


### Brief Example Code

Here's how to do a [DuckDuckGo](https://www.duckduckgo.com/) search using Boa Constrictor.

Screenplay calls:

```csharp
using Boa.Constrictor.Logging;
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium.Chrome;

// Set up the Screenplay actor
IActor actor = new Actor(logger: new ConsoleLogger());

// Inject the Selenium WebDriver instance
actor.Can(BrowseTheWeb.With(new ChromeDriver()));

// Load a page
actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));

// Get the page's title
string title = actor.AsksFor(Title.OfPage());

// Search for something
actor.AttemptsTo(SearchDuckDuckGo.For("panda"));

// Wait for results
actor.AttemptsTo(Wait.Until(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True()));
```

With locator classes:

```csharp
using Boa.Constrictor.WebDriver;
using OpenQA.Selenium;
using static Boa.Constrictor.WebDriver.WebLocator;

public static class SearchPage
{
  public const string Url = "https://www.duckduckgo.com/";

  public static IWebLocator SearchInput => L(
    "DuckDuckGo Search Input", 
    By.Id("search_form_input_homepage"));

  public static IWebLocator SearchButton => L(
    "DuckDuckGo Search Button",
    By.Id("search_button_homepage"));
}

public static class ResultPage
{
  public static IWebLocator ResultLinks => L(
    "DuckDuckGo Result Page Links",
    By.ClassName("result__a"));
}
```

And a custom `SearchDuckDuckGo` task:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.WebDriver;

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
```


### Why Should I Use Boa Constrictor?

There are many benefits to using Boa Constrictor for automation.
First and foremost, it provides **rich, reusable, and reliable interactions** out of the box.
You don't need to implement your own pattern.
You don't need to rely on raw Selenium WebDriver or RestSharp calls.
The Screenplay actor also **automatically logs every interaction**.

Screenplay interactions are also **composable**.
You can make new interactions that call other interactions!
This turns automation from a series of clicks and scrapes into meaningful tests or procedures.

Waiting for things to be ready is one of the toughest parts of test automation,
and it is frequently the source of "flaky" test failures.
Boa Constrictor provides generic **waiting interactions** that can wait for any interaction to meet any condition.
For example, automation can wait for a page to load or for a response to contain a certain value.
Since waits are just special interactions, they fit naturally into the fluent syntax.

Web UI interactions also include **safety steps**.
They always fetch fresh elements using locators to avoid stale element exceptions.
Most of them automatically wait for elements to appear on the page before attempting to interact with them.
The Screenplay Pattern enforces conformity.
That makes automation much more reliable and less "flaky."

As a result, Boa Constrictor enables significant **code reusability**.
You can write a common interaction, locator, or request one time and use it repeatedly.
You will probably write less code with Boa Constrictor than with the Page Object Pattern or raw calls.
Plus, all Screenplay interactions will include their safety steps, making automation more **robust**.

Furthermore, the Screenplay Pattern handles scope using **dependency injection**.
Objects like the WebDriver instance and RestSharp client are injected into interactions through the actor's abilities.
This makes it easy to share objects safely, add new interactions, and even unit test interactions.

Finally, Boa Constrictor's fluent syntax style is very **readable and understandable**.
Even if you don't know exactly how an interaction is implemented, you can see exactly what it does.
Test automation code reads more like test cases.
The fluent syntax reduces the learning curve.


### What Boa Constrictor is NOT!

Boa Constrictor is **not another Selenium WebDriver wrapper**.
It is a design pattern for making *any* type of interaction, not just Web UI.
It provides WebDriver-based interactions to support Web UI test automation out of the box.
Any other types of interactions can be added.

Boa Constrictor is also **not another type of Page Object Pattern**.
The [Page Object Pattern](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/)
is the traditional way to model Web pages for automation.
A *page object* contains locator objects and interaction methods (that use those locators).
Page objects are great for small test automation solutions, but they don't scale well.
By design, page objects couple the concerns of page structure with behavior.
As a result, they tend to have repetitive methods, bloated hierarchies, and little standardization.
Boa Constrictor's [Screenplay Pattern](https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay/)
applies [SOLID](https://en.wikipedia.org/wiki/SOLID) design principles to modeling behaviors under test.
Boa Constrictor provides the boilerplate code for the Screenplay Pattern so testers can readily use it.
As a result, Boa Constrictor can scale safely and maintainably for very large projects.

Boa Constrictor is also **not a Serenity BDD clone**.
[Serenity BDD](http://serenity-bdd.info/) is an open-source acceptance test automation framework.
It strongly supports [Behavior-Driven Development](https://automationpanda.com/bdd/) techniques,
and it has implementations in Java and JavaScript.
Serenity BDD is notable for containing one of the most widely available implementations of the Screenplay Pattern.
The Serenity BDD project has significantly influenced the Boa Constrictor project,
and the Boa Constrictor authors deeply respect the Serenity BDD authors.
However, Boa Constrictor is a completely separate project with distinct goals, different authors, and unique designs.
Boa Constrictor is *not* "Serenity BDD for .NET."

Finally, Boa Constrictor is **not a full test framework**.
It is a C# .NET library for a design pattern.
Boa Constrictor's Screenplay Pattern can be used within a test automation framework for making interactions.
It can replace raw calls or existing patterns like page objects.
However, other test automation concerns like the runner, test case structure, and reporting must be handled by other entities.


## Guidelines for Contribution

As an open source project, Boa Constrictor welcomes contributions from the community.
All contributors must abide by the [Code of Conduct](CODE-OF-CONDUCT.md).


### Development Setup

To work on the Boa Constrictor code, you will need to clone the repository using [Git](https://git-scm.com/).
The recommended editor/IDE is [Microsoft Visual Studio](https://visualstudio.microsoft.com/).
Simply open `Boa.Constrictor.sln` in Visual Studio to open the solution and get to work!
For Web UI development, remember to add target [WebDriver executables](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/) to your system PATH.


### Running Unit Tests

Unit tests are located in the `Boa.Constrictor.UnitTests` project.
They are written using [NUnit](https://nunit.org/).
They can be executed from Visual Studio using *Test Explorer*
or from the command line using the [NUnit Console](https://docs.nunit.org/articles/nunit/running-tests/Console-Runner.html).
Unit tests must pass for every pull request.
They do *not* require WebDriver executables.

*Warning:* Tests under `Boa.Constrictor.Example` are *not* unit tests.
They are example tests for the [tutorial](TUTORIAL.md),
and they *do* require WebDriver executables.
Nevertheless, it would be good to run them when making code changes as an integration-like test.


### Requesting Features or Reporting Bugs

To suggest a new feature or report a bug, please open an issue.
Please be as clear as possible in the description.
Core contributors will triage issues and reply in comments.


### Contributing Code

To contribute code, please clone the repository, make the changes, and submit a pull request.
Please include explanations for the code change. Attach any relevant issues.
All changes must pass unit tests.
Core contributors will review all pull requests and either approve or reject them.


### Becoming a Core Contributor

A *core contributor* is someone who has write access to the Boa Constrictor repository.
They guide the project and review all code changes.
To express interest in becoming a core contributor, please contact [Andy Knight](mailto:andy.knight@q2.com).


## License

Boa Constrictor is licensed under [Apache License 2.0](LICENSE.md).


## Code of Conduct

Boa Constrictor conforms to the [Q2 Code of Conduct](CODE-OF-CONDUCT.md).


## Backstory

Boa Constrictor originally started in 2018 as the cornerstone of [PrecisionLender](https://precisionlender.com/)'s end-to-end test automation solution.
PrecisionLender and its parent company [Q2](https://www.q2.com/) released it publicly as an open source project in October 2020.
Boa Constrictor's main goal is to provide a better way to model interactions for automation.
The project aims to showcase the Screenplay Pattern as an improvement over the Page Object Convention.
