# Changelog

This file documents all notable changes to the Boa.Constrictor.Xunit project and its unit tests for each NuGet package release.

Its format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [Unreleased]

### Added

- Added project `Boa.Constrictor.Playwright` for items that improve [Playwright](https://playwright.dev/dotnet/) support
- Added `BrowseTheWebSynchronously` ability to manage playwright, browsers, and pages
- Added `Click`, `Fill`, and `Go` Tasks
- Added `OpenNewPage` task to initialize page and navigate to the specified url
- Added `AbstractPageTask` that makes it easier to write Tasks that use the BrowseTheWebSynchronously Ability
- Added `Text` and `Attribute` questions