---
title: Boa Constrictor
layout: single
permalink: /
---

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
Any .NET project can use Boa Constrictor by installing the
[Boa.Constrictor NuGet package](https://www.nuget.org/packages/Boa.Constrictor).

This [32-minute YouTube video](https://youtu.be/i26B1afosCo)
introduces Screenplay Pattern concepts and Boa Constrictor code:

<p align="center">
<iframe width="560" height="315" src="https://www.youtube.com/embed/i26B1afosCo" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</p>