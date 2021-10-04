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

Here's how to automate a [DuckDuckGo](https://www.duckduckgo.com/) search in Chrome using Boa Constrictor.

Write Screenplay calls:

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
actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
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

