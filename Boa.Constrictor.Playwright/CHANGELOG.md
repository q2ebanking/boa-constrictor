# Changelog

This file documents all notable changes to the Boa.Constrictor.Xunit project and its unit tests for each NuGet package release.

Its format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [Unreleased]

(none)


## [4.0.0] - 2025-05-03

### Added

- Added project `Boa.Constrictor.Playwright` for items that improve [Playwright](https://playwright.dev/dotnet/) support
- Added `BrowseTheWebWithPlaywright` ability to manage playwright, browsers, and pages
- Added `PlaywrightLocator` to store description + selector logic of a playwright locator
- Added `AbstractPageTask` and `AbstractPageQuestion` to make it easier to write interactions that perform operations on the current page
- Added `AbstractLocatorTask` and `AbstractLocatorQuestion`to make it easier to write interactions that perform operations on a single locator
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
  - `PressSequentially`
  - `CloseBrowser`
  - `CloseBrowserContext`
- Added the following questions:
  - `InnerText`
  - `Attribute`
  - `PageTitle`
  - `Visibility`
  - `Enabled`
- Added an `Expects` extension method to `IActor` to make it easier to use playwrights built in `ILocatorAssertions`
  - e.g. `await MyActor.Expects(WikiPage.Title).ToHaveTextAsync("Giant panda");`