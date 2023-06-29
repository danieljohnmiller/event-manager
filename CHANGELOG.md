# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

- Event manager singleton

## [2.1.0] - 2023-06-29

### Added

- Added base class for event managers, removing much duplicate code.


## [2.0.0] - 2023-06-29

### Changed

- Changed event managers event ID from generic type to int. Generic type added little value, and made setting parameter types confusing.

## [1.2.0] - 2023-06-29

### Changed

- Refined event manager AddObserver implementation.
- Refined event manager RemoveObserver implementation.
- Refined event manager TriggerEvent implementation.

## [1.1.0] - 2023-06-28

### Added

- Event manager implementation with one parameter sent to observers.
- Event manager implementation with two parameters sent to observers.
- Unit tests for one parameter event manager.
- Unit tests for two parameter event manager.

### Changed

- Cleaned up unit tests for no parameter event manager.


## [1.0.0] - 2023-06-27

### Added

- Event manager with no return value and generic event ID.
- Unit tests for Unity editor.