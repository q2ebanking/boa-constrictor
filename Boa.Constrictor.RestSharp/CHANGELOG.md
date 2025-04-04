
---

![Boa Constrictor Logo](https://raw.githubusercontent.com/q2ebanking/boa-constrictor/main/logos/title/no-margin/png/logo-title-black-400x64.png)

---

# Changelog

This file documents all notable changes to the
[`Boa.Constrictor.RestSharp`](https://www.nuget.org/packages/Boa.Constrictor.RestSharp) project and its unit tests
for each NuGet package release.

Its format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Unreleased]

- Updated RestSharp to 112.1.0


## [4.0.0] - 2023-05-29

### Changed

- Removed targets for `net5.0` and `net7.0`
- Now targets only `netstandard2.0`


## [3.2.0] - 2023-05-25

- Updated RestSharp to 110.2.0
- Changed `RequestDumper` to support RestSharp 110.2.0
- Fixed unit tests. Callers must now initialize the `CookieContainer` themselves in `RestClientOptions`


## [3.1.0] - 2023-05-24

### Changed

- Updated RestSharp to 108.0.3
	- All instances of the following interfaces have been replaced with concrete classes
		- `IRestClient` -> `RestClient`
		- `IRestRequest` -> `RestRequest`
		- `IRestResponse` -> `RestResponse`
	- See the [Migration Guide](https://restsharp.dev/v107/#brief-migration-guide) for more details
- Changed `RequestDumper` serialization to support RestSharp 108.3
- Fixed unit tests. Replaced the use of Moq with MockHttp where necessary as recommended [here](https://restsharp.dev/v107/#mocking)


## [3.0.3] - 2022-12-13

### Changed

- Created separate changelog files for each project (except unit tests).


## [3.0.2] - 2022-12-07

### Changed

- Rewrote `nuget-push.yml` to use `dotnet` commands directly instead of `rohith/publish-nuget`
- Updated doc site dependencies to resolve security warnings
- Wrote separate README files for each package


## [3.0.1] - 2022-12-03

### Changed

- Split unit tests into three new projects:
  - `Boa.Constrictor.Screenplay.UnitTests`
  - `Boa.Constrictor.Selenium.UnitTests`
  - `Boa.Constrictor.RestSharp.UnitTests`
- Updated unit test dependency package versions
- Updated GitHub Action to run all unit tests in the new projects

### Fixed

- Updated Boa Constrictor package dependencies to NOT refer to an alpha release


## [3.0.0] - 2022-12-02

### Added

- Split the `Boa.Constrictor` project into three new projects:
  - `Boa.Constrictor.Screenplay` for the "core" pattern
  - `Boa.Constrictor.Selenium` for Selenium WebDriver interactions
  - `Boa.Constrictor.RestSharp` for RestSharp interactions
- Published new NuGet packages for the three new projects
  - That way, you can choose which interaction libraries to use
  - The original `Boa.Constrictor` package will be empty but hold dependencies to these three for backwards compatibility
  - Updated GitHub Actions for publishing them
- Set dependencies based on configuration
  - Local development (Configuration=Debug) will use project references
  - NuGet packages (Configuration=Release) will use NuGet packages
  - The `Example` and `UnitTests` projects always use project references

### Changed

- Renamed the following namespaces:
  - `Boa.Constrictor.WebDriver` -> `Boa.Constrictor.Selenium`
  - `Boa.Constrictor.Dumping` -> `Boa.Constrictor.Screenplay`
  - `Boa.Constrictor.Logging` -> `Boa.Constrictor.Screenplay`
  - `Boa.Constrictor.Safety` -> `Boa.Constrictor.Screenplay`
  - `Boa.Constrictor.Utilities` -> `Boa.Constrictor.Screenplay`
  - *Warning:* this is a backwards-incompatible change, but a straightforward one to handle


## [Older Releases]

Please see [Boa.Constrictor/CHANGELOG.md](../Boa.Constrictor/CHANGELOG.md) for project history before version 3.x.