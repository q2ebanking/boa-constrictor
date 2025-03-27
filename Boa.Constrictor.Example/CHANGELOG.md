
---

![Boa Constrictor Logo](https://raw.githubusercontent.com/q2ebanking/boa-constrictor/main/logos/title/no-margin/png/logo-title-black-400x64.png)

---

# Changelog

This file documents all notable changes to the
[`Boa.Constrictor.Example`](https://www.nuget.org/packages/Boa.Constrictor) project.

Its format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

*Note:* This project does not have a NuGet package.
Versioning is performed purely for tracking changes.

## [Unreleased]

### Added

- Created Playwright tests

### Changed

- Restructured project to separate examples by technology


## [4.0.0] - 2023-05-29

### Changed

- Removed targets for `net5.0` and `net7.0`
- Now targets only `netstandard2.0`


## [3.1.0] - 2023-05-28

### Changed

- Replaced DuckDuckGo search test with Wikipedia search test since DuckDuckGo changes their home page


## [3.0.4] - 2022-12-13

### Changed

- DogRequests now uses `RestRequest` to support RestSharp updates


## [3.0.3] - 2022-12-13

### Added

- Enabled headless Chrome in the example Web UI test
- Created a GitHub Action to run the example tests for pull requests into `main`

### Changed

- Created separate changelog files for each project (except unit tests).


## [Older Releases]

Please see [Boa.Constrictor/CHANGELOG.md](../Boa.Constrictor/CHANGELOG.md) for project history before version 3.x.