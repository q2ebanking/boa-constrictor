# Changelog

Boa Constrictor is released publicly as the [Boa.Constrictor NuGet package](https://www.nuget.org/packages/Boa.Constrictor)
on [NuGet.org](https://www.nuget.org/).
This file documents all notable changes to the project for each NuGet package release.

This file's format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Unreleased]

### Added

- Added dumpers under `Boa.Constrictor.Dumping`

### Changed

- Refactored RestSharp interactions to use dumpers
- Moved RestSharp serialization classes into their own files


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
