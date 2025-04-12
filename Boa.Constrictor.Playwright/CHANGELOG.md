# Changelog

This file documents all notable changes to the Boa.Constrictor.Playwright project and its unit tests for each NuGet package release.

Its format is based on Keep a Changelog, and this project adheres to Semantic Versioning.

## [Unreleased]

### Added

- Added project `Boa.Constrictor.Playwright` for items that improve [Playwright](https://playwright.dev/dotnet/) support 
- Added `IPlaywrightLocator` interface and implementation, which will be used to create locators for Playwright
- Added `IBoaWebLocator` interface and implementation, which can be used to easily convert Selenium IWebLocators into Playwright IPlaywrightLocators