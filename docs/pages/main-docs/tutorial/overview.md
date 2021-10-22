---
title: Tutorial Overview
layout: single
permalink: /tutorial/overview/
toc: true
---

The Boa Constrictor tutorial will teach you how to use Boa Constrictor and the Screenplay Pattern step-by-step.
This page provides an overview for starting the tutorial.


## Outline

This tutorial has two parts:

1. [Part 1 - Setup]({{ "/tutorial/part-1-setup/" | relative_url }})
2. [Part 2 - Web UI Testing]({{ "/tutorial/part-2-web-ui-testing/" | relative_url }})
3. [Part 3 - REST API Testing]({{ "/tutorial/part-3-rest-api-testing/" | relative_url }})

In the tutorial, you will build a small test project using [NUnit](https://nunit.org/).
Inside, you will create a simple Web UI test case that performs a search using [DuckDuckGo](https://duckduckgo.com/).
You will use Boa Constrictor to handle all interactions.
At the end of the tutorial, you will have a complete test project that you can extend with new tests or use as a reference for future projects.

The tutorial should take about **60 minutes** to complete.
Take your time to understand the concepts.
{: .notice--warning}


## Prerequisites

The tutorial will use the following tools and concepts:

* Programming in [C#](https://en.wikipedia.org/wiki/C_Sharp_(programming_language))
* Developing code in [Microsoft Visual Studio](https://visualstudio.microsoft.com/)
* Automating tests with [NUnit](https://nunit.org/)
* Using [Selenium WebDriver](https://www.selenium.dev/) with [Google Chrome](https://www.google.com/chrome/)
* Calling REST API with [RestSharp](https://restsharp.dev/)

You should also be familiar with the basic of web app architecture, like HTML, CSS, and JavaScript.
You don't need to be an "expert" in these things.
Just make sure you have a basic understanding of how they work.


## Tutorial Code

The tutorial is designed to be hands-on.
At the start of the project, you will create a new .NET project.
Every step will introduce new concepts with example code.
You should add the code from each tutorial step to your project as you progress.
That way, you can practice writing Boa Constrictor calls, and you can test the code at each step.

The `Boa.Constrictor.Example` project in this repository contains the tutorial's completed example code.
It targets .NET 5.
You can refer to this project if you get stuck during the tutorial.
You can also run the tests in this project to see how Boa Constrictor should work.


## Ready to Start?

If you are ready to start the tutorial, please proceed to [Part 1 - Setup]({{ "/tutorial/part-1-setup/" | relative_url }}).
