# Changelog

This file documents all notable changes to the Boa.Constrictor.Xunit project and its unit tests for each NuGet package release.

Its format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [Unreleased]

(none)


## [4.0.0] - 2023-05-29

### Changed

- Removed targets for `net5.0` and `net7.0`
- Now targets only `netstandard2.0`


## [3.1.0] - 2023-05-24

### Changed

- Updated `Boa.Constrictor.Screenplay` version to `3.1.0`


## [3.0.3] - 2022-12-16

### Added

- Added project `Boa.Constrictor.Xunit` for items that improve [xUnit](https://xunit.net/) support
- Added project `Boa.Constrictor.Xunit.UnitTests`
- Added XunitLogger to support the use of `ITestOutputHelper`
- Added `XunitLoggerTests`
- Updated `nuget-push.yml` to publish the `Boa.Constrictor.Xunit` package
