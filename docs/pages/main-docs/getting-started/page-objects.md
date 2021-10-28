---
title: Screenplay vs. Page Objects
layout: single
permalink: /getting-started/page-objects/
toc: true
---

[Page objects](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/)
are the traditional way to model Web pages for automation.
Unfortunately, page objects cause problems like duplicate code, missing waits, and messy inheritance.
The Screenplay Pattern solves those problems with a stronger separation of concerns.
This guide walks through how to refactor a basic Web UI test from raw WebDriver calls into page objects and then Screenplay interactions.
It reveals pain points at each step to show why the Screenplay Pattern is ultimately the best solution.


## Phase 1: Raw WebDriver Calls

Let's say you want to automate a test that performs a [DuckDuckGo](https://duckduckgo.com/) web search.
You could simply write raw [Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/) code like this:

```csharp
// Initialize the WebDriver
IWebDriver driver = new ChromeDriver();

// Open the search engine
driver.Navigate().GoToUrl("https://duckduckgo.com/");

// Search for a phrase
driver.FindElement(By.Id("search_form_input_homepage")).SendKeys("panda");
driver.FindElement(By.Id("search_button_homepage")).Click();

// Verify results appear
driver.Title.ToLower().Should().Contain("panda");
driver.FindElements(By.CssSelector("a.result__a")).Should().BeGreaterThan(0);

// Quit the WebDriver
driver.Quit();
```

Unfortunately, this code has a big problem: *race conditions*.
There are two race conditions in which the automation does *not* wait for the page to be ready before making interactions:

1. Waiting for the search page to load after navigating to it.
2. Waiting for the results page to load after clicking the search button.

WebDriver does not automatically wait for elements to load or titles to appear.
Waiting is a huge challenge for Web UI automation, and it is one of the main reasons for "flaky" tests.


## Phase 2: Explicit Waits

The proper way to mitigate race conditions is proper waiting.
You could set an implicit wait that will make calls wait until target elements appear,
but they don't work for all cases, such as the title in race condition #2.
Explicit waits are a much better approach.
They provide much more control over waiting timeout and conditions.
They use a "WebDriverWait" object with a pre-set timeout value,
and they must be placed explicitly throughout the code.

Let's update the code with explicit waits:

```csharp
// Initialize the WebDriver
IWebDriver driver = new ChromeDriver();
WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

// Open the search engine
driver.Navigate().GoToUrl("https://duckduckgo.com/");

// Search for a phrase
wait.Until(d => d.FindElements(By.Id("search_form_input_homepage")).Count > 0);
driver.FindElement(By.Id("search_form_input_homepage")).SendKeys("panda");
driver.FindElement(By.Id("search_button_homepage")).Click();

// Verify results appear
wait.Until(d => d.Title.ToLower().Contains("panda"));
wait.Until(d => d.FindElements(By.CssSelector("a.result__a"))).Count > 0);

// Quit the WebDriver
driver.Quit();
```

These waits are necessary to make the code correct, but they cause new problems.
First, they cause duplicate code because Web element locators are used multiple times.
Notice how the locator `By.Id("search_form_input_homepage")` is written twice.
Second, raw calls with explicit waits make code more cryptic and less intuitive.
It is difficult to understand what this code does at a glance.


## Phase 3: Page Objects

To remedy these problems, most teams use the [Page Object Model](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/).
In the Page Object Model (or "Page Object Pattern"), each page is modeled as a class with locator variables and interaction methods.
So, a page object for the search page could look like this:

```csharp
public class SearchPage
{
    public const string Url = "https://duckduckgo.com/";
    public static By SearchInput => By.Id("search_form_input_homepage");
    public static By SearchButton => By.Id("search_button_homepage");

    public IWebDriver Driver { get; private set; }

    public SearchPage(IWebDriver driver) => Driver = driver;

    public void Load() => driver.Navigate().GoToUrl(Url);

    public void Search(string phrase)
    {
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        wait.Until(d => d.FindElements(SearchInput).Count > 0);
        driver.FindElement(SearchInput).SendKeys(phrase);
        driver.FindElement(SearchButton).Click();
    }
}
```

This page object class has a decent structure and a mild separation of concerns.
The `SearchPage` class has locators (`SearchInput` and `SearchButton`) and interaction methods (`Load` and `Search`).
The `Search` method uses an explicit wait before attempting to interact with elements.
It also uses the locator properties so that locator queries are not duplicated.
Locators and interaction methods have meaningful names.
Page objects require a few more lines of code that raw calls at first, but their parts can be called easily.

The original test steps can be rewritten using this new `SearchPage` class, as well as a hypothetical `ResultPage` class.
This new code looks much cleaner:

```csharp
IWebDriver driver = new ChromeDriver();

SearchPage searchPage = new SearchPage(driver);
searchPage.Load();
searchPage.Search("panda");

ResultPage resultPage = new ResultPage(driver);
resultPage.WaitForTitle("panda");
resultPage.WaitForResultLinks();

driver.Quit();
```

Unfortunately, page objects themselves suffer problems with duplication in their interaction methods.
Suppose a page object needs a method to click an element using a locator named `Button`.
The logic would be similar to the search page's `Search` method -
wait for the element to exist, and then click it.
But what about clicking another element named `OtherButton`?
There would be two very similar methods:

```csharp
public class AnyPage
{
    // ...

    public void ClickButton()
    {
        Wait.Until(d => d.FindElements(Button).Count > 0);
        driver.FindElement(Button).Click();
    }

    public void ClickOtherButton()
    {
        Wait.Until(d => d.FindElements(OtherButton).Count > 0);
        driver.FindElement(OtherButton).Click();
    }
}
```

The code will be the same for any other click method, too.
This is [copypasta](https://en.wikipedia.org/wiki/Copypasta),
and it happens frequently in page objects.
Page objects can grow to be thousands of lines long due to duplicate methods like this.


## Phase 4: Page Object Inheritance

In Object-Oriented Programming, one of the most popular ways to avoid code duplication is to use inheritance.
Page objects can have parent pages that hold shared methods for common interactions like clicks.
Parent page object classes typically look like this:

```csharp
public abstract class BasePage
{
    public IWebDriver Driver { get; private set; }
    public WebDriverWait Wait { get; private set; }

    public SearchPage(IWebDriver driver)
    {
        Driver = driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
    }

    protected void Click(By locator)
    {
        Wait.Until(d => d.FindElements(locator).Count > 0);
        driver.FindElement(locator).Click();
    }
}
```

The class will typically be `abstract` so that it cannot be directly instantiated.
It holds references to the WebDriver instance as well as perhaps the waiting object.
It also has methods for generic interactions like `Click` that take in a locator and perform the desired action.
Page objects can then declare this `BasePage` as a parent to inherit all the shared goodness:

```csharp
public class AnyPage : BasePage
{
    // ...

    public AnyPage(IWebDriver driver) : base(driver) {}

    public void ClickButton() => Click(Button);

    public void ClickOtherButton() => Click(OtherButton);
}
```

In this example, the amount of code in `AnyPage` shrank a lot.
This is good, but it's still not good enough.
The base page helps mitigate code duplication, but it does not solve its root cause.
Page objects inherently combine two separate concerns: page structure and interactions.
Interactions are often generic enough to be used on any Web element.
Coupling interaction code with specific locators or pages forces testers to add new page object methods for every type of interaction needed for an element.
Every element could potentially need a click, a text scrape, an appearance check, or any other type of WebDriver interaction.
That's a lot of extra code that should not be necessary.

Consider what `AnyPage` could look like with additional methods:

```csharp
public class AnyPage : BasePage
{
    // ...

    public void ClickButton()      => Click(Button);
    public void ClickOtherButton() => Click(OtherButton);
    public void ClickThirdButton() => Click(ThirdButton);

    public void ButtonText()      => Text(Button);
    public void OtherButtonText() => Text(OtherButton);
    public void ThirdButtonText() => Text(ThirdButton);

    public void IsButtonDisplayed()      => IsDisplayed(Button);
    public void IsOtherButtonDisplayed() => IsDisplayed(OtherButton);
    public void IsThirdButtonDisplayed() => IsDisplayed(ThirdButton);
}
```

The parent page also becomes very top-heavy as testers add more and more code to share.
Editing the parent page becomes increasingly risky as it carries more responsibilities.

Most frustratingly, the page object code shown here is merely one type of implementation.
Page objects are completely free form. Every team implements them differently.
There is no official version of the Page Object Pattern.
There is no conformity in its design.
Every team implements page object classes differently.
Even worse, within its design, there is almost no way for the pattern to enforce good practices.
For example, programmers could forget to add explicit waits before attempting to call elements.
Page objects would be better described as a *convention* than as a true *design pattern*.

## Final Phase: Screenplay Interactions

Thankfully, there is a better way: the [Screenplay Pattern]({{ "/getting-started/screenplay/" | relative_url }}).
The Screenplay Pattern separates the concerns of page structure from interactions.
Page classes exclusively contain locator objects that denote page structure.
Interactions are written as *Task* or *Question* classes that work with any locator object.
Interactions like clicks and text scrapes are implemented once and simply reused anywhere.
Higher-level interactions, such as logging into an app, can be composed directly from lower-level interactions.

The test could be rewritten using Boa Constrictor's Screenplay calls like this:

```csharp
IActor actor = new Actor(logger: new ConsoleLogger());
actor.Can(BrowseTheWeb.With(new ChromeDriver()));
actor.AttemptsTo(Navigate.ToUrl(SearchPage.Url));
string title = actor.AsksFor(Title.OfPage());
actor.AttemptsTo(SearchDuckDuckGo.For("panda"));
actor.WaitsUntil(Appearance.Of(ResultPage.ResultLinks), IsEqualTo.True());
```

The page classes would provide locators:

```csharp
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

Performing the DuckDuckGo search could use a custom interaction like this:

```csharp
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

To learn more about how this code works, read the [tutorial]({{ "/tutorial/overview/" | relative_url }}).

Screenplay's design offers the following [benefits]({{ "/getting-started/why-boa-constrictor/#the-benefits" | relative_url }}) over raw calls and page objects:

* Screenplay forces programmers to focus on behavior more than page structure.
* Screenplay calls are readable and intuitively understandable.
* Screenplay interactions can be written to automatically avoid race conditions by waiting.
* Screenplay code resists copy-paste errors because each interaction can be written once and then reused.
* Locators can be used by any interaction, instead of being locked into one page object class.
* Actors automatically log all interactions, making output easily traceable.


## More Refactoring

Boa Constrictor's Screenplay calls can easily replace raw WebDriver calls, page objects, and other patterns in existing test automation projects.
Check out this livestream to see how
[Andreas Willich](https://twitter.com/SabotageAndi) and [Pandy Knight](https://twitter.com/AutomationPanda)
converted a SpecFlow project to use Boa Constrictor:

<p align="center">
<iframe width="560" height="315" src="https://www.youtube.com/embed/hJ_ni5s6vhA" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</p>
