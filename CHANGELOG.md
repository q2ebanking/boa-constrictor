# Changelog

Boa Constrictor is released publicly as the [Boa.Constrictor NuGet package](https://www.nuget.org/packages/Boa.Constrictor)
on [NuGet.org](https://www.nuget.org/).
This file documents all notable changes to the project for each NuGet package release.

This file's format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Unreleased]

(None)


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
