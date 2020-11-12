# Contributing Guide

As an open source project, Boa Constrictor welcomes contributions from the community.
This step-by-step guide explains how to set up the project and make code contributions.


## Where Do I Start?

Sometimes, joining a new project can feel overwhelming or intimidating.
Never fear, if you are reading this doc, then you are in the right place!
There are many ways to contribute to Boa Constrictor,
such as opening issues, adding comments, and creating pull requests for code changes.
Start by reading this guide, reading the [Code of Conduct](CODE-OF-CONDUCT.md), and perusing open issues to see where you can help.
You can also contact [Andrew Knight](https://twitter.com/AutomationPanda) for help.


## 1. Following the Code of Conduct

All contributors must abide by the [Code of Conduct](CODE-OF-CONDUCT.md).
Anyone who violates the code of conduct may be banned from the project and the community.


## 2. Handling Issues

All Boa Constrictor work items are handled using GitHub Issues.
If you want to report a bug, request a new feature, or propose an idea,
then please open a new issue against the repository.
Maintainers will triage issues and reply in comments.
Others in the community are also welcome to comment.

If you want to work on an issue, then please assign it to yourself.
Since the project is still young and its processes are not yet mature,
you might want to ask in comments before starting to work on an issue.


## 3. Forking the Repository

If you want to make code contributions to Boa Constrictor, start by forking the repository on GitHub.
That way, you can make changes to the code without affecting the original repository.
Later, after you make changes in your forked repository, you can propose to merge those changes into the main repository using a pull request.

If forks are new to you, then you can learn about them from GitHub Docs:
[Working with forks](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/working-with-forks).


## 4. Setting Up the .NET Solution

Boa Constrictor is implemented in C#.
To work on the Boa Constrictor code, you will need to clone your forked repository to your development machine using [Git](https://git-scm.com/).
The recommended editor/IDE is [Microsoft Visual Studio](https://visualstudio.microsoft.com/).
Simply open `Boa.Constrictor.sln` in Visual Studio to open the solution and get to work!

You must also add [WebDriver executables](https://www.selenium.dev/documentation/en/webdriver/driver_requirements/)
to your system PATH for target browsers.
For example, if you wish to use Google Chrome as your browser, then you will need to install [ChromeDriver](https://chromedriver.chromium.org/).
Make sure browser and driver versions align because older versions are not always compatible with each other!


## 5. Running Tests

After you set up the solution, and also after making each code change,
try running the unit tests to make sure everything works correctly.
Unit tests are located in the `Boa.Constrictor.UnitTests` project.
They are written using [NUnit](https://nunit.org/).
They can be executed from Visual Studio using *Test Explorer*
or from the command line using the [NUnit Console](https://docs.nunit.org/articles/nunit/running-tests/Console-Runner.html).
Unit tests must pass for every pull request.
They do *not* require WebDriver executables.

The `Boa.Constrictor.Example` project also contains tests, but they are *not* unit tests.
Instead, they are example tests for the [tutorial](TUTORIAL.md), and they *do* require WebDriver executables.
If you simply check out the repository and try to run all tests without setting up WebDriver,
then the `Boa.Constrictor.Example` tests will fail.
Nevertheless, you should run these tests in addition to the unit tests when doing WebDriver work
because they are de facto integration tests for WebDriver-based interactions.


## 6. Creating Branches

The Boa Constrictor repository uses the [Feature Branch Workflow](https://www.atlassian.com/git/tutorials/comparing-workflows/feature-branch-workflow).
The main version of code is in the *main* branch.
To make a change, create a new branch off *main*, and commit all changes to that new branch.
Later, you can open a pull request to merge those changes into the original repository's *main* branch.

Use sensible names with appropriate prefixes for branch names.
For example, names like `feature/add-mobile-interactions` and `bugfix/readme-typos` would be good,
whereas names like `fix` or `do_stuff` would be bad.

If branches are new to you, then you can learn about them from GitHub Docs:
[About branches](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/about-branches)
and [Creating and deleting branches within your repository](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/creating-and-deleting-branches-within-your-repository).


## 7. Making Code Changes

[Commit](https://docs.github.com/en/free-pro-team@latest/github/committing-changes-to-your-project)
code changes to your branch.
Include concise, helpful messages in each commit.
[Squashing commits](https://medium.com/@slamflipstrom/a-beginners-guide-to-squashing-commits-with-git-rebase-8185cf6e62ec)
is recommended but not required.

Along with your code changes, update the `[Unreleased]` section of `CHANGELOG.md` with a concise description of your changes.
Follow the format of [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).
Put your description under the appropriate type heading:
`Added`, `Changed`, `Deprecated`, `Fixed`, `Removed`, or `Security`.


## 8. Opening Pull Requests

Once your changes are complete, push your branch to your forked repository and
[open a pull request from your fork to the original repository](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request-from-a-fork).
Maintainers will review all pull requests.
The Boa Constrictor repository's GitHub Actions will automatically trigger unit tests and code analysis for every pull request.

Here are guidelines for opening good pull requests:

* Focus on one main change or concern per pull request.
* Merge the latest changes from the *main* branch into your branch before opening the pull request to avoid merge conflicts.
* Include explanations for the code change.
* Link any relevant issues.
* Make sure unit tests pass.
* Answer review feedback promptly.

Once maintainers approve your pull request, they will merge the code.
Then, you can move onto the next change!

If pull requests are new to you, then you can learn about them from GitHub Docs:
[Collaborating with issues and pull requests](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests).


## 9. Releasing NuGet Packages

Boa Constrictor is released publicly as the [Boa.Constrictor NuGet package](https://www.nuget.org/packages/Boa.Constrictor/).
Package versions follow [Semantic Versioning](https://semver.org/).

This repository has a GitHub Action named [`nuget-push.yml`](.github/workflows/nuget-push.yml)
that will automatically build and publish the Boa.Constrictor NuGet package to [NuGet.org](https://www.nuget.org/packages/Boa.Constrictor/)
whenever the project version changes in [`Boa.Constrictor.csproj`](Boa.Constrictor/Boa.Constrictor.csproj#L5)
in the *main* branch.

To release a new package, maintainers must:

1. Create a milestone for the new version in GitHub.
2. Mark all included issues and pull requests for the milestone.
3. Run tests to make sure the code is good to ship.
4. Open a pull request into the *main* branch to set the new version.
   * Update the `Version` field in `Boa.Constrictor.csproj`.
   * Add a new entry for the new version in `CHANGELOG.md`.
   * Move the changelog's `[Unreleased]` section contents to the new version's section.
5. Review, approve, and merge the pull request into *main*.
6. Verify the GitHub Action to publish the package completes successfully.
7. Close the milestone in GitHub.


## 10. Understanding Different Roles

A *contributor* is anyone who opens an issue or submits a pull request to the Boa Constrictor repository.
Anyone can become a contributor by following this guide.

A *maintainer* is a regular contributor who helps guide the Boa Constrictor project.
Maintainers have write access to the repository and review code changes.
To express interest in becoming a maintainer, please contact [Andrew Knight](https://twitter.com/AutomationPanda).

An *administrator* is a contributor who maintains operations for Boa Constrictor's GitHub repository.
Administrators handle things like permissions, groups, and policies.
To keep privileges separate, maintainers should not be administrators.
(For example, a maintainer should not be able to change settings to bypass code reviews.)
