---
title: Why Use Boa Constrictor?
layout: single
permalink: /getting-started/why-boa-constrictor/
toc: true
---

There are many benefits to using Boa Constrictor for automation.
This page covers those benefits in detail.
It also addresses misunderstandings about the project.


## The Benefits

First and foremost, Boa Constrictor provides **rich, reusable, and reliable interactions** out of the box.
You don't need to implement your own pattern.
You don't need to rely on raw Selenium WebDriver or RestSharp calls.
The Screenplay actor will **automatically log every interaction**.

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

Boa Constrictor enables significant **code reusability**.
You can write a common interaction, locator, or request one time and use it repeatedly.
You will probably write less code with Boa Constrictor than with page objects or raw calls.
Plus, all Screenplay interactions will include their safety steps, making automation more **robust**.

Furthermore, the Screenplay Pattern handles scope using **dependency injection**.
Objects like the WebDriver instance and RestSharp client are injected into interactions through the actor's abilities.
This makes it easy to share objects safely, add new interactions, and even unit test interactions.

Finally, Boa Constrictor's fluent syntax style is very **readable and understandable**.
Even if you don't know exactly how an interaction is implemented, you can see exactly what it does.
Test automation code reads more like test cases.
The fluent syntax reduces the learning curve.


## The Misunderstandings

Boa Constrictor is **not another Selenium WebDriver wrapper**.
It is a design pattern for making *any* type of interaction, not just Web UI.
It provides WebDriver-based interactions to support Web UI test automation out of the box.
Any other types of interactions can be added.

Boa Constrictor is also **not another type of Page Object Model**.
The [Page Object Model](https://www.selenium.dev/documentation/en/guidelines_and_recommendations/page_object_models/)
is the traditional way to model Web pages for automation.
A *page object* contains locator objects and interaction methods (that use those locators).
Page objects are good for small test automation solutions, but they don't scale well.
By design, page objects couple the concerns of page structure with behavior.
As a result, they tend to have repetitive methods, bloated hierarchies, and little standardization.
Page objects are more of a "convention" than a true design pattern.
Boa Constrictor's [Screenplay Pattern](https://www.infoq.com/articles/Beyond-Page-Objects-Test-Automation-Serenity-Screenplay/)
applies [SOLID](https://en.wikipedia.org/wiki/SOLID) design principles to modeling behaviors under test.
Boa Constrictor provides the boilerplate code for the Screenplay Pattern so testers can readily use it.
As a result, Boa Constrictor can scale safely and remain maintainable for very large projects.
Read more about [why Screenplay interactions are better than page objects]({{ "/getting-started/page-objects/" | relative_url }}).

Boa Constrictor is **not limited to small-scale projects**.
Sometimes, folks who are new to the Screenplay Pattern wrongly assume that it is a toy project that does not scale well.
On the contrary, Screenplay scales *much* better than page objects for Web UI interactions
because it separates concerns better, enforces clearer design decisions, and causes less duplication.
The PrecisionLender team initially used page objects when they first started automating tests,
but after about 100 tests, they replaced all page objects with Screenplay interactions.
Now, the team runs up to 10K end-to-end tests against the PrecisionLender app *per day* using Boa Constrictor.

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
