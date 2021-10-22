---
title: Backstory
layout: single
permalink: /project/backstory/
---

In April 2018,
[Pandy Knight](https://twitter.com/AutomationPanda) joined 
[PrecisionLender](https://precisionlender.com/) as the company's first 
[Software Engineer in Test](https://automationpanda.com/2018/10/02/the-software-engineer-in-test/).
His first major project was to develop an end-to-end test automation solution for the PrecisionLender web app.
Pandy and the team developed *Boa*, a .NET solution written in C# using
[SpecFlow](https://specflow.org/),
[Selenium WebDriver](https://www.selenium.dev/documentation/en/webdriver/), and
[RestSharp](https://restsharp.dev/).

The team originally used
[page objects]({{ "/getting-started/page-objects/" | relative_url }})
to model Web UI interactions.
Unfortunately, page objects became long and repetitive as the team added more tests to the project.
Waiting for race conditions was cumbersome.
Page objects also did not provide a pattern for handling service API calls.

Pandy wanted to solve these problems by switching from page objects to the
[Screenplay Pattern]({{ "/getting-started/screenplay/" | relative_url }}).
Unfortunately, at that time, there were no major Screenplay implementations in C#.
The most popular test framework using the Screenplay Pattern was
[Serenity BDD](http://www.serenity-bdd.info/#/),
which supports Java and JavaScript.
So, Pandy decided to implement the Screenplay Pattern in C# as part of the Boa test solution.

The Screenplay Pattern helped PrecisionLender automate hundreds of Boa tests quickly, reliably, and with minimal code duplication.
It truly became the cornerstone of the Boa test solution.
After battle-hardening the basic Web UI and REST API interactions,
Pandy and the team separated the Screenplay code into its own standalone project:
***Boa Constrictor***.

Knowing that a .NET implementation of the Screenplay Pattern could benefit others,
PrecisionLender and its parent company [Q2](https://www.q2.com/)
released Boa Constrictor publicly as an open source project in October 2020.
Q2 continues to support Boa Constrictor's development.
[Tia Nieland](https://www.linkedin.com/in/tia-nieland-2b68a4152/)
from Q2's Brand and Design team designed Boa Constrictor's awesome logo.

Boa Constrictor's main goal is to provide a better way to model interactions for automation.
It can be extended for any type of interaction, not just Web UI and REST API.
Everyone is welcome to become part of Boa Constrictor's story by
[using it]({{ "/getting-started/quickstart/" | relative_url }})
and even
[contributing]({{ "/contributing/contributing-code/" | relative_url }})
to its development!
