
---

![Boa Constrictor Logo](https://raw.githubusercontent.com/q2ebanking/boa-constrictor/main/logos/title/no-margin/png/logo-title-black-400x64.png)

---

# Changelog

Boa Constrictor is released publicly as the [Boa.Constrictor NuGet package](https://www.nuget.org/packages/Boa.Constrictor)
on [NuGet.org](https://www.nuget.org/).
This file documents all notable changes to the project for each NuGet package release.

This file's format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Unreleased]

### Added

- Added unit tests for Question that derive from `AbstractWebPropertyQuestion`
- Added [Discord server](https://discord.gg/pP3dXzYQ82) invitation links to README and docs


## [2.0.0] - 2021-11-02

### Added

- Added `UploadFile` WebDriver Task
- Added user guide: "Calling Tasks Safely"
- Added and refined GitHub templates for issues and pull requests
- Added user guide: "Writing Actor Extension Methods"
- Added user guide: "Caching Answers"
- Reformatted section titles in Testing user guides
- Added supression for RestSharp 'Parameter is Obsolete' warnings
- Added ICacheableQuestion interface implementation to RestApiDownload (Equals and GetHashCode)
- Added unit tests for Question that derive from `AbstractWebQuestion`
- Added unit tests for Question that derive from `AbstractWebLocatorQuestion`
- Added utility for wrapping logging of long messages with name `MessagesWrapper`.

### Changed

- Updated Fluent Assertions dependency to 6.1.0
- Restructured projects for `docs`, `logos`, and `talk`
- Renamed `JavaScriptProperty` to `DomProperty`

### Fixed

- `Classes` Question will return empty array if class attribute returns null


## [1.5.0] - 2021-10-13

### Added

- [Experimental] Added async programming to Boa Constrictor
  - Interactions have async versions:
    - `IInteractionAsync`
    - `IQuestionAsync`
    - `ITaskAsync`
  - `IActor` has "Async" methods to call async Interactions
- Added additional badges from Shields.io to the README.md
- Added ToString methods for Questions that didn't have them

### Changed

- Reformatted task and question when they're referring to interactions
- Reformatted actor and ability when they're referring to Screenplay items
- Updated Selenium WebDriver packages to 4.0
- Standardized existing ToString methods for Questions

### Fixed

- Fixed CodeQL warnings in GitHub Action
- Fixed some Question summary descriptions


## [1.4.1] - 2021-10-06

### Added

- Added README to NuGet package
- Added user guide: "Testing with xUnit.net"

### Changed

- Replaced symbols package with embedded debugging


## [1.4.0] - 2021-10-06

### Added

- Added ToString methods for Tasks that didn't have them
- Added user guide groundwork to the doc site
- Added documentation stating that Boa Constrictor is not limited to small-scale projects
- Added user guide: "Testing with NUnit"
- Added user guide: "Testing with SpecFlow"
- Added ability for `Wait` to handle multiple pairs of `IQuestion` and `ICondition` pairs using boolean operators

### Changed

- Standardized existing ToString methods for Tasks


## [1.3.0] - 2021-09-20

### Added

- Added `SafeActions` for:
  1. Running `Action` objects
  2. Catching any exceptions
  3. Throwing the exceptions later as a combined `SafeActionsException`
- Added Screenplay support for running Tasks using `SafeActions`
  1. The `RunSafeActions` Ability holds a `SafeActions` object
  2. The `Safely` Task calls another task using the Ability


## [1.2.3] - 2021-08-09

### Added

- Added WebDriver task `RightClick.On(webElement)`
- Added WebDriver task `DoubleClick.On(webElement)` 
- Added WebDriver task `Check.On(webElement)` / `Check.Off(webElement)` for interacting with Checkboxes, Radio Buttons, etc.
- Added WebDriver task `Drag.AndDrop(webElement)` for dragging the mouse from one WebElement to another
- Added IsNullOrWhitespace Condition


## [1.2.2] - 2021-07-15

### Changed

- Updated RestSharp package version to fix security vulnerability
- Updated NUnit and other unit test package versions


## [1.2.1] - 2021-07-15

### Fixed

- Gemfile: required `addressable >= 2.8.0` to fix security vulnerability
- Removed execution of `codeql-analysis.yml` from cron


## [1.2.0] - 2021-06-10

### Added

- Added new Conditions for `IEnumerable` objects
- Reorganized Condition folders


## [1.1.0] - 2021-06-05

### Added

- Added overloaded for `Actor.AttemptsTo(...)` that accepts multiple Tasks as `params`
- Added Applitools webinar to doc site videos pages

### Changed

- Updated AbstractComparison constructor to protected
- Updated doc site Gemfile dependency versions


## [1.0.0] - 2021-05-24

This is Boa Constrictor's **1.0 Release!**
It does not contain any changes from 0.14.0.
Setting the version to 1.0 signals that the current code is "good" and will be supported.
You should not be hesitant to use 1.0 because its code has been hardened for months.


## [0.14.0] - 2021-05-14

### Added

- Added `ChangeWebDriver` Task for changing `BrowseTheWeb`'s WebDriver
- Added more Boa Constrictor logo assets under the `logos` directory
- Added Boa Constrictor PowerPoint master slide deck
- Added Boa Constrictor talk slides and script

### Changed

- Removed unused logo image assets from the doc site
- Changed a few of the images used in the doc site


## [0.13.1] - 2021-04-22

### Added

- Set the NuGet package icon to the new Boa Constrictor logo


## [0.13.0] - 2021-04-22

### Added

- Added Boa Constrictor logo and updated docs

### Fixed

- Improved `SendKeys` text clearing logic


## [0.12.0] - 2021-04-06

### Added

- Added official documentation!
  - Hosting: GitHub Pages at [https://q2ebanking.github.io/boa-constrictor/](https://q2ebanking.github.io/boa-constrictor/)
  - Static site generator: [Jekyll](https://jekyllrb.com/)
  - Theme: [Minimal Mistakes](https://mmistakes.github.io/minimal-mistakes/)
- Added REST APIs to the tutorial and example project

### Changed

- Moved `Rest` `DownloadUsing` and `RequestUsing` build methods to a new type-generic `Rest<TAbility>` class
- Updated NuGet package versions

### Fixed

- Removed "NonParallelWorker" from `Names.ConcatUniqueName` return values

### Removed

- Moved content from root-level Markdown files to the doc site
  - Removed much of the `README` content
  - Deleted files for tutorials, code of conduct, and contributing


## [0.11.2] - 2021-02-22

### Changed

- Enabled `IRestClient` objects to be directly injectable into `AbstractRestSharpAbility` and its child classes


## [0.11.1] - 2021-02-08

### Changed

- Added `ICacheableQuestion` as an interface for Questions that can be cached
- Updated `AnswerCache` and related classes to use `ICacheableQuestion` instead of `IQuestion`
- Updated WebDriver-based Questions to implement `ICacheableQuestion`


## [0.11.0] - 2021-02-02

### Added

- Added Screenplay answer cache
  - `AnswerCache` stores answers to Questions using the Question object as the key
  - `CacheAnswers` is the Ability that enables Actors to use `AnswerCache`
  - `CachedAnswer` returns a cached answer for a Question or calls the Question to store its answer
- Added `Equals` and `GetHashCode` methods to all WebDriver interactions so they can work with `AnswerCache`
- Added `Equals` and `GetHashCode` methods to `WebLocator`
- Added `Id` builder method to `WebLocator`
- Added body data to `RequestData` for dumping RestSharp requests

### Changed

- Refactored `HtmlAttribute` and `HtmlAttributeList` interactions
  - `IdAttribute`/`IdAttributeList`
  - `ValueAttribute`/`ValueAttributeList`


## [0.10.0] - 2021-01-26

### Added

- Added several new Questions to return lists of requested string values from multiple elements found by a locator
- Additions include `CssValueList`, `HtmlAttributeList`, `IdAttributeList`
- New Questions (and existing `TextList`) implement new extension `ElementLists.GetValues()`


## [0.9.0] - 2021-01-15

### Added

- Added .NET 5 support!
  - `Boa.Constrictor` targets both .NET 5 and .NET Standard 2.0
  - `Boa.Constrictor.Example` targets .NET 5 exclusively
  - `Boa.Constrictor.UnitTests` targets .NET 5 exclusively


## [0.8.3] - 2021-01-13

### Fixed

- Removed "file:" from screenshot and artifact relative links in the test log report HTML file


## [0.8.2] - 2021-01-11

### Added

- Added `TestLogReportDumper` to combine `TestLogData` objects into one pretty HTML report
- Refactored `AbstractDumper` to share more parts


## [0.8.1] - 2021-01-07

### Changed

- `TestLogData` now has a `Result` property for storing the test result
- `TestLogData`'s properties are all now public to *get* but remain private to *set*
- `TestLogger` now has a `LogResult` method for logging the test result


## [0.8.0] - 2021-01-06

### Added

- Added `TestLogger` for dumping JSON files with test steps, messages, and test artifact files


## [0.7.0] - 2020-12-22

### Added

- Added `WaitsUntil` extension method to `IActor` for more concise waiting calls
- Updated all interactions and the tutorial to use `WaitsUntil`


## [0.6.1] - 2020-12-08

### Added

- Added the `RequestDumper` class to automatically store the last request and response objects
- Added `LastRequest` and `LastResponse` properties to `IRestSharpAbility` to more conveniently access these values from the dumper

### Changed

- `IRestSharpAbility` now uses `RequestDumper` instead of `JsonDumper` to dump requests and responses


## [0.6.0] - 2020-12-02

### Changed

This release contains major changes to RestSharp Screenplay support.
Changes are **not** backwards-compatible.

- Previously, there was one Ability named `CallRestApi` that could hold multiple `IRestClient` RestSharp client objects.
  Each RestSharp Interaction would need a base URL as an input, and it would look up the appropriate client object from the Ability.
  However, this was not the best design because it required each call to include a base URL, which led to repetitive code.

- Now, there can be multiple Abilities for calling REST APIs using RestSharp.
  One RestSharp Ability has only one `IRestClient` object.
  Users should write their own Ability class for each base URL they want to use.
  `IRestSharpAbility` provides the interface for RestSharp Abilities, with properties and methods for clients, dumpers, and cookies.
  `AbstractRestSharpAbility` implements most of the pieces from `IRestSharpAbility`.
  Custom RestSharp Abilities should either implement `IRestSharpAbility` or extend `AbstractRestSharpAbility`.
  The `CallRestApi` Ability still exists as a "common" or "default" RestSharp Ability.
  It extends `AbstractRestSharpAbility`.

- All RestSharp interactions have been updated with a generic type parameter for an `IRestSharpAbility` Ability.
  This type parameter dictates which Ability will be used by the interaction, thus determining the RestSharp client and thereby the base URL.
  For simplicity, the `Rest` static class's builder methods now each have two versions:
  one with a type generic to specify the RestSharp Ability, and one without a type generic that uses `CallRestApi` as a "default" Ability.

- RestSharp interactions for cookies have been removed because they simply made changes to the `IRestClient` object instead of performing real interactions.
  They have been replaced by `IRestSharpAbility` methods.

- RestSharp interactions for calling requests and downloads have also changed significantly.
  `RestApiResponse` has been renamed to `RestApiCall` for readability.
  `RestFileDownload` has been renamed to `RestApiDownload` for consistency.
  Both no longer have in-class builder methods.
  Instead, the `Rest` class holds the only public builder methods for them.
  You *must* use the `Rest` class to instantiate them.
  Both also now share a parent class named `AbstractRestQuestion`.

Examples of new RestSharp interactions:

```csharp
IActor actor = new Actor();

// Simple "default" RestSharp client calls
actor.Can(CallRestApi.At("www.somebaseurl.com"));
IRestRequest request = new RestRequest(...);
IRestResponse response = actor.Calls(Rest.Request(request));

// Custom RestSharp client calls
// Assume `CallOtherApi` is a class that implements `IRestSharpAbility`
// Assume `DataObject` is a deserialization class
actor.Can(CallOtherApi.At("www.someotherapi.com"));
IRestRequest request2 = new RestRequest(...);
IRestResponse<DataObject> response2 = actor.Calls(Rest.RequestUsing<CallOtherApi, DataObject>(request2));
```


## [0.5.1] - 2020-12-01

### Added

- Added the `Rest` class to provide more fluent builder methods for RestSharp Questions


## [0.5.0] - 2020-11-30

### Added

- Added dumpers under `Boa.Constrictor.Dumping`
- Added dumpers for RestSharp requests/responses and file downloads to the `CallRestApi` Ability

### Changed

- Refactored RestSharp interactions to use dumpers
- `RestApiResponse` and `RestFileDownload` no longer directly take in output directories for dumps
- Moved RestSharp serialization classes into their own files
- Standardized access levels for properties of RestSharp-based interactions

### Removed

- Removed `AbstractRestQuestion`, `AbstractRestTask`, and `RequestLogger`


## [0.4.0] - 2020-11-25

### Added

- Added `Calls` methods to `IActor` interface for calling both Tasks and Questions

### Changed

- Replaced `JavaScriptCall` and `JavaScriptElementCall` with `JavaScript`


## [0.3.1] - 2020-11-18

### Added

- Updated NuGet packages used by unit tests

### Fixed

- Added `nuget-push.xml` to `GitHub Actions` solution folder
- Added `StaleElementReferenceException` retries to Web locator tasks


## [0.3.0] - 2020-11-12

### Added

- Created a GitHub Action named `nuget-push.yml` to automatically publish the Boa.Constrictor NuGet package to NugGet.org when the project version changes.

### Fixed

- Fixed namespace for `SwitchWindowToLatest` Task.
- Fixed broken README link.
- Fixed `Names.ConcatUniqueName()` for when the current thread's name is `null`.

### Removed

- Removed the `Names.GetCurrentThreadName()` method.


## [0.2.3] - 2020-10-27

### Added

- Created a GitHub Action to automatically run unit tests for every pull request.
- Created a solution folder for GitHub Action .yml files.
- Recorded a Boa Constrictor intro video and linked it in the README.
- Added `CONTRIBUTING.md` as the new guide for project development and contributions.
- Added `CHANGELOG.md` as the project changelog file.

### Changed

- Improved README text.
- Moved "Guidelines for Contribution" from `README.md` to `CONTRIBUTING.md`.

### Fixed

- Configured NuGet package to share XML docs using `GenerateDocumentationFile` in .csproj file.
- Corrected typos in README and XML docs.

### Removed

- Removed `IsDateAsString` Condition.


## [0.2.2] - 2020-10-15

### Changed

- Released the package a second time to resolve minor delivery pipeline issues.
- No code changes.


## [0.2.1] - 2020-10-15

### Changed

- Released the package via PrecisionLender's new internal delivery pipeline.
- No code changes.


## [0.2.0] - 2020-10-15

### Added

- Initial public release of the Boa.Constrictor NuGet package (uploaded manually).
