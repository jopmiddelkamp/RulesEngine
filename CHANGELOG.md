# CHANGELOG

All notable changes to this project will be documented in this file.
## [2.1.3] - 12-10-2020
- Optional parameter for rethrow exception on failure of expression compilation.

## [2.1.2] - 02-10-2020
- Fixed binary expression requirement. Now any expression will work as long as it evalutes to boolean.

## [2.1.1] - 01-09-2020
- Fixed exception thrown when errormessage field is null
- Added better messaging when identifier is not found in expression
- Fixed other minor bugs

## [2.1.0] - 18-05-2020
- Adding local param support to make expression authroing more intuitive.

## [2.0.0] - 18-05-2020
### Changed
- Interface simplified by removing redundant parameters in the IRulesEngine.
- Custom Logger replaced with Microsoft Logger.

## [1.0.2] - 16-01-2020
### Added
- Cache system added so that rules compilation is stored and thus made more efficient.

### Fix
- Concurrency issue which arose by dictionary was resolved.

## [1.0.1] - 24-09-2019
### Added
- Exceptions handling scenario in the case a rule execution throws an exception 

## [1.0.0] - 20-08-2019

### Added
- The first version of the NuGet
