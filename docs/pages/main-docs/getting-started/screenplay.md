---
title: What is Screenplay?
layout: single
permalink: /getting-started/screenplay/
toc: true
---

The *Screenplay Pattern* is a design pattern for automating interacting with software products.
It can handle any type of interaction - web browser, mobile app, service API, command line, etc.
Test automation is the most popular use case for the pattern.


## The Design

The Screenplay Pattern can be summarized in one line:
*Actors use Abilities to perform Interactions.*

 * **Actors** initiate Interactions.
 * **Abilities** enable Actors to initiate Interactions.
 * **Interactions** are procedures that exercise the behaviors under test.
   * **Tasks** execute procedures on the features under test.
   * **Questions** return state about the features under test.
   * **Interactions** may use **Locators**, **Requests**, and other **Models**.

The diagram below illustrates how these parts fit together:
![Screenplay Visual]({{ "/assets/images/screenplay-visual.png" | relative_url }})

For example, an Actor may be given an Ability to browse the Web using a specific browser like Chrome.
The Ability would hold a reference to a Chrome WebDriver instance.
Then, the Actor could call a Task to load a login page, a second Task to enter username and password, and a final Task to click the "login" button.
Each task would access the WebDriver instance through the calling Actor's Ability to control the Chrome browser.
Abilities provide a mechanism for dependency injection.
Actors can perform any kind of Interaction if it has the required Abilities.


## The Code

The Screenplay Pattern uses a fluent-like syntax to make test code very readable:

```csharp
IActor actor = new Actor(logger: new ConsoleLogger());
actor.Can(BrowseTheWeb.With(new ChromeDriver()));
actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
string title = actor.AsksFor(Title.OfPage());
actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
```

This block of code is easy to understand, even for those who haven't used Screenplay or Boa Constrictor before.
Re-read these lines in plain language:

1. The actor can browse the web with a new ChromeDriver.
2. The actor attempts to navigate to the search page URL.
3. The actor asks for the title of the page.
4. The actor attempts to search DuckDuckGo for "panda".
5. The actor waits until the result page links appear.

The Screenplay Pattern is a great design pattern for automating interactions.
The separation of concerns between the Actor, Abilities, and Interactions makes code less duplicative and more reusable.
For example, Web UI interactions like `Click` and `Text` can be written once and then operate safely on any element.
New interactions can also be added at any time without needing to change existing code.
There are [many reasons]({{ "/getting-started/why-boa-constrictor/" | relative_url }})
to choose the Screenplay Pattern (and specifically Boa Constrictor) for automation over other patterns like the
[Page Object Model]({{ "/getting-started/page-objects/" | relative_url }}).


## The Principles

The Screenplay Pattern adheres to [SOLID](https://en.wikipedia.org/wiki/SOLID) design principles: 

| SOLID Principle | Explanation |
| --------------- | ----------- |
| [Single-Responsibility Principle](https://en.wikipedia.org/wiki/Single-responsibility_principle) | Actors, Abilities, and Interactions are treated as separate concerns. |
| [Open-Closed Principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle) | Each new Interaction must be a new class, rather than a modification of an existing class. | 
| [Liskov Substitution Principle](https://en.wikipedia.org/wiki/Liskov_substitution_principle) | Actors can call all Abilities and Interactions the same way. |
| [Interface Segregation Principle](https://en.wikipedia.org/wiki/Interface_segregation_principle) | Actors, Abilities, and Interactions each have distinct, separate interfaces. |
| [Dependency Inversion Principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) | Interactions use Abilities via dependency injection from the Actor. |


## The Implementation

Boa Constrictor did not create the Screenplay Pattern.
In fact, the Screenplay Pattern (or the "Journey Pattern") has been around
[for several years](https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay/)!
Its most popular implementation is the [Serenity BDD](http://serenity-bdd.info/#/) framework.
Boa Constrictor is simply a standalone C# implementation
originally developed by [Pandy Knight](https://twitter.com/AutomationPanda)
at [PrecisionLender](https://precisionlender.com/).
It aims to make the Screenplay Pattern easy to understand and fast to adopt for .NET automation projects.
