# Changelog

This file documents all notable changes to the Boa.Constrictor.Xunit project and its unit tests for each NuGet package release.

Its format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [Unreleased]

### Added

- Added project `Boa.Constrictor.Playwright` for items that improve [Playwright](https://playwright.dev/dotnet/) support
- Added `BrowseTheWebSynchronously` ability to manage playwright, browsers, and pages
- Added `PlaywrightLocator` to store description + selector logic of a playwright locator
- Added `AbstractPageTask` that makes it easier to write Tasks that use the BrowseTheWebSynchronously Ability
- Added `AbstractLocatorTask` to make it easier to write tasks that perform operations on a single locator
- Added the following Tasks:
  - `OpenNewPage`
  - `Click`
  - `DblClick`
  - `Fill`
  - `SetChecked`
  - `SelectOption`
  - `Go`
  - `Hover`
  - `Clear`
  - `Focus`
  - `Press`
- Added `Text` and `Attribute` questions
- Added an `Expects` extension method to `IActor` to make it easier to use playwrights built in `ILocatorAssertions`
  - e.g. `await MyActor.Expects(WikiPage.Title).ToHaveTextAsync("Giant panda");`