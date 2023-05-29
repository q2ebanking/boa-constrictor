---
title: Quickstart Guide
layout: single
permalink: /getting-started/quickstart/
toc: true
---

This brief guide should help you start using Boa Constrictor quickly.
Please read [What is Screenplay?]({{ "/getting-started/screenplay/" | relative_url }})
to learn more about the Screenplay Pattern itself,
and please complete the [tutorial]({{ "/tutorial/overview/" | relative_url }})
to learn Boa Constrictor's code in depth.
You can also watch [videos]({{ "/learning/user-guides-and-videos/" | relative_url }})
and read [user guides]({{ "/learning/user-guides-and-videos/" | relative_url }})
about Boa Constrictor.


## Setup

Boa Constrictor is a .NET library written in C#.
To start using it:

1. Install the [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor) package from NuGet.org into your .NET project.
2. Install appropriate WebDriver executables on your system PATH.
   * For example, install [ChromeDriver](https://chromedriver.chromium.org/) to run tests using Google Chrome.
   * Follow the instructions on the [Driver Requirements](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/) page to set them up.
   * If your code raises a `DriverServiceNotFoundException`, then double-check your WebDriver installation.


## Example Code

Here's how to automate a [Wikipedia](https://en.wikipedia.org/wiki/Main_Page) search in Chrome using Boa Constrictor.

Write Screenplay calls:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;
using OpenQA.Selenium.Chrome;

// Set up the Screenplay actor
IActor actor = new Actor(logger: new ConsoleLogger());

// Inject the Selenium WebDriver instance
actor.Can(BrowseTheWeb.With(new ChromeDriver()));

// Load a page
actor.AttemptsTo(Navigate.ToUrl(MainPage.Url));

// Get the page's title
string title = actor.AsksFor(Title.OfPage());

// Search for something
actor.AttemptsTo(SearchWikipedia.For("Giant panda"));

// Wait for results
Actor.WaitsUntil(Text.Of(ArticlePage.Title), IsEqualTo.Value("Giant panda"));
```

With locator classes:

```csharp
using Boa.Constrictor.Selenium;
using OpenQA.Selenium;
using static Boa.Constrictor.Selenium.WebLocator;

public static class MainPage
{
  public const string Url = "https://en.wikipedia.org/wiki/Main_Page";

  public static IWebLocator SearchButton => L(
    "Wikipedia Search Button",
    By.XPath("//button[text()='Search']"));

  public static IWebLocator SearchInput => L(
    "Wikipedia Search Input",
    By.Name("search"));
}

public static class ArticlePage
{
  public static IWebLocator Title => L(
    "Title Span",
    By.CssSelector("[id='firstHeading'] span"));
}
```

And a custom `SearchWikipedia` Task:

```csharp
using Boa.Constrictor.Screenplay;
using Boa.Constrictor.Selenium;

public class SearchWikipedia : ITask
{
  public string Phrase { get; }

  private SearchWikipedia(string phrase) =>
    Phrase = phrase;

  public static SearchWikipedia For(string phrase) =>
    new SearchWikipedia(phrase);

  public void PerformAs(IActor actor)
  {
    actor.AttemptsTo(SendKeys.To(MainPage.SearchInput, Phrase));
    actor.AttemptsTo(Click.On(MainPage.SearchButton));
  }
}
```
