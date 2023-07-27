# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

none

## [8.1.0] - 2023-07-28

### Changed

- Sealed and set accessability to internal for event manager services that do not need to be exposed outside of the package assembly.
- Changed EventManagerBase from an abstract class to a concrete class that is used internally by EventManager.

## [8.0.0] - 2023-07-13


### Added

- New version of event manager interface (`IEventManager`).
- New instantiatable event manager implementing `IEventManager`.
- Unit tests for new instantiatable event manager.

### Changed

- Updated readme for v8.0.0

### Removed

-Removed legacy GlobalEventManager and EventManager.

## [7.0.0] - 2023-07-03

### Changed

- Renamed IGlobalEventManager to IEventManager.

### Added 
- added project and usage information to readme.
- Added instantiatable version of event manager that implements IEventManager.
- Summary to IEventManager.

### Removed

- Obsolete event manager components including old IEventManager interface.
- Obsolete event manager unit tests.
- Duplicate summary info from GlobalEventManager implementation (most moved to IEventManager).


## [6.0.0] - 2023-07-03

### Changed

- Changed global event manager interface method names to be more consistent. RemoveEventHandlerType no longer defaults to null.
- Moved class summary from global event manager implementation to interface.

## [5.0.0] - 2023-07-03

### Changed

- Removed generic event ID from global event manager interface. Event ID must be an int.
- Updated global event manager interface to include more granular removal methods.
- Tagged original event manager as obsolete.

### Added

- Added more unit tests for global event manager.

## [4.2.0] - 2023-07-03

### Changed

- Global event manager structure now handled by NestedEventTable class.

## [4.1.0] - 2023-07-01

### Changed

- Refactored global event manager implementation for clarity and consistency.

## [4.0.0] - 2023-06-30

### Added

- Added additional global event manager unit tests.

### Removed

- Deleted event message base as it was unused.

### Changed

- Updated event manager singleton to ensure thread safety.
- Implemented remaining methods on global event manager.
- Renamed global event manager methods for consistency.
- Removed unimplemented 'clear' methods from global event manager. These are not 100% necessary, so have been de-scoped for now.

### Fixed

- Bug in global event manager that prevented more than one observer from invoking.

## [3.0.0] - 2023-06-30

### Added

- Partially implemented global (singleton) event manager.
- Global event manager interface.
- Global event manager tests.
- Event message base class.

### Changed

- Updated Event manager base class and interface to be more flexible.

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