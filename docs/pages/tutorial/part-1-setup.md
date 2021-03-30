---
title: Tutorial Part 1 - Setup
layout: single
permalink: /tutorial/part-1-setup/
toc: true
---

In this part of the tutorial, you will create a new .NET project for testing with Boa Constrictor.
You will also install all the dependencies you need to run that project.


## 1. Installing the IDE

[Microsoft Visual Studio](https://visualstudio.microsoft.com/) is the recommended editor/IDE for this tutorial.
Visual Studio makes it easy to create projects, add packages, navigate code, and run tests.
Be sure to install the latest version.
This tutorial will use *Visual Studio 2019*.

**Note:** You may also use other editors/IDEs, like
[Visual Studio Code](https://code.visualstudio.com/)
or [JetBrains Rider](https://www.jetbrains.com/rider/).
However, tutorial instructions for project setup will use Visual Studio.
{: .notice--info}


## 2. Setting up the Web Browser

This tutorial uses [Google Chrome](https://www.google.com/chrome/) for Web UI automation.
Chrome is fast and has wide market share.
[Chrome DevTools](https://developer.chrome.com/docs/devtools/)
also make it easy to find elements when writing locators.
Install or update the latest version of Chrome on your machine before starting the tutorial.

Since Boa Constrictor uses [Selenium WebDriver](https://www.selenium.dev/) under the hood,
you will also need to download and install the latest version of [ChromeDriver](https://chromedriver.chromium.org/) on your system PATH.
Follow the instructions on the
[Selenium WebDriver Driver Requirements](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/)
page to set it up.

**Note:** You may also use other browsers for testing,
like [Mozilla Firefox](https://www.mozilla.org/en-US/firefox/),
[Apple Safari](https://www.apple.com/safari/),
or [Microsoft Edge](https://www.microsoft.com/en-us/edge).
To use other browsers, install the appropriate WebDriver executable,
and substitute the desired WebDriver constructor in the example code in place of `ChromeDriver`.
However, avoid using [Microsoft Internet Explorer 11](https://en.wikipedia.org/wiki/Internet_Explorer_11):
it is slow and may not work the same as other browsers.
{: .notice--info}


## 3. Creating the Project

Open Visual Studio 2019 and select **Create a new project**:

![Create New Project]({{ "/assets/images/vs-create-new-project.png" | relative_url }})

Select **NUnit Test Project** as the project type:

![Select Project Type]({{ "/assets/images/vs-select-project-type.png" | relative_url }})

**Note:**
The tutorial will use [NUnit](https://nunit.org/) as the core test framework.
Boa Constrictor can also work with other test frameworks,
like [SpecFlow](https://specflow.org/) and [xUnit.net](https://xunit.net/).
It can also be used for automation without a test framework.
{: .notice--info}

**Note:**
You may create either a [.NET Framework or .NET Core](https://dzone.com/articles/net-framework-vs-net-core) project
because Boa Constrictor supports both target frameworks.
However, it is recommended to create a [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/introduction) project
or even a [.NET 5](https://devblogs.microsoft.com/dotnet/introducing-net-5/) project.
The `Boa.Constrictor.Example` reference project in the repository targets .NET 5.
{: .notice--info}

Name the project "Boa.Constrictor.Example",
and set the *Location* field to the folder where you want to create the project.
You may keep the default value for *Solution name* (which should default to the *Project name*).

![Name Project]({{ "/assets/images/vs-name-project.png" | relative_url }})

**Note:**
In .NET, one "solution" may have one to many "project".
If you are new to .NET development, please read the
[Visual Studio docs on projects and solutions](https://docs.microsoft.com/en-us/visualstudio/get-started/tutorial-projects-solutions?view=vs-2019).
{: .notice--info}

The project should appear in *Solution Explorer* once it is created.
It may contain a placeholder class file named `UnitTest1.cs`.
If so, right-click the file in *Solution Explorer* and select **Delete**.

![Remove Auto Generated Class]({{ "/assets/images/vs-remove-auto-generated-class.png" | relative_url }})


## 4. Installing NuGet Packages

You will need to add the following NuGet packages to the project:

* [Boa.Constrictor](https://www.nuget.org/packages/Boa.Constrictor/)
* [FluentAssertions](https://www.nuget.org/packages/FluentAssertions/)
* [NUnit](https://www.nuget.org/packages/NUnit/)
* [NUnit3TestAdapter](https://www.nuget.org/packages/NUnit3TestAdapter/)

To add them, right-click the solution in *Solution Explorer*,
 and select **Manage Nuget Packages for Solution...**.

![Manage Nuget Packages]({{ "/assets/images/vs-manage-nuget-packages.png" | relative_url }})

On the *NuGet* page, go to the *Browse* tab, search for each NuGet package by name, and click the package when it appears.
Then, select the `Boa.Constrictor.Example` project for the package and click *Install*.

![Install Nuget Package]({{ "/assets/images/vs-install-nuget-packages.png" | relative_url }})

**Note:**
If you created an *NUnit Test Project* through Visual Studio,
then the `Boa.Constrictor.Example` project should already have the *NUnit* and *NUnit3TestAdapter* packages installed.
{: .notice--info}

These NuGet packages should pull in other dependencies as well.
For example, *Boa.Constrictor* should pull in
[Selenium.WebDriver](https://www.nuget.org/packages/Selenium.WebDriver),
[RestSharp](https://www.nuget.org/packages/RestSharp),
and possibly other packages.


## All Set!

This new project should now be ready for Boa Constrictor!
Build the project to make sure everything is set up correctly.
Then, proceed to [Part 2 - Web UI Testing]({{ "/tutorial/part-2-web-ui-testing/" | relative_url }}).
