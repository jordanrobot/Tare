# Copilot usage for this repository

This repo contains a small .NET library that implements a unit-of-measure value type (`Quantity`) and supporting utilities. Use this guide to keep Copilot changes tight, safe, and consistent with the project’s design.

## Scope and intent

- Primary type: `Tare.Quantity` (readonly struct) representing a numeric value + unit, with value semantics and arithmetic/operators.
- Supporting types: `UnitDefinition`, `UnitDefinitions` (catalog + parsing), `UnitTypeEnum`, and extension methods in `Extensions`.
- Targets: `netstandard2.0` and `net7.0`. Do not drop, change, or add TFMs without explicit instruction.
- No external runtime dependencies beyond those already in the project file. Avoid adding packages.

## Non-goals (don’t do by default)

- Don’t introduce new public APIs casually. Prefer small, additive changes and keep binary compatibility.
- Don’t refactor broadly across files. Make surgical edits tied to the explicit task.
- Don’t switch numeric types. `decimal` is deliberate for precision; keep it for quantity values and calculations.
- Don’t add async/DI or layered architecture patterns—this is a simple library, not an app.

## Coding rules that Copilot must follow

- Immutability/value semantics:
	- `Quantity` is a readonly struct; preserve immutability and value semantics (equality, hashing, comparisons).
	- Any new state must be computed, not mutated.
- Units and compatibility:
	- All conversions must go through `UnitDefinitions.Parse` and its `Factor` and `UnitType` values.
	- Use base-value comparisons for cross-unit equality/ordering within the same `UnitTypeEnum`.
	- Throw informative exceptions when operating on incompatible units (match existing messages where possible).
- Arithmetic/operators:
	- Maintain existing operator behaviors and error modes; add tests if you change or extend any operator.
	- Prefer constructing via existing factory/parse helpers (`Quantity.Parse(...)`) to ensure consistent initialization.
- Parsing/formatting:
	- Reuse existing regexes and patterns in `Quantity` for parsing; don’t introduce alternate parsers unless asked.
	- `Format` and `ToString` must remain locale-stable and include units where applicable.
- Performance and allocations:
	- Avoid unnecessary allocations in hot paths (operators, conversions, formatting).
	- Keep code compatible with netstandard2.0 (no APIs newer than that unless guarded per TFM).
- Style and clarity:
	- Favor explicit, straightforward code over cleverness. Minimal changes only.
	- Keep exception text and public method names stable unless there is a bug to fix.

## Public API and compatibility

- Treat `Quantity` members and operators as public surface area. Changing signatures or behavior requires:
	- A clear justification in the PR description.
	- New/updated tests demonstrating the intent.
	- A note in `docs/CHANGELOG.md`.

## Tests and verification

- Tests live under `tests/` and use xUnit. Add tests for:
	- New operators or conversions (happy path + incompatible-unit error).
	- Parsing/formatting edge cases (no units, unknown units, spacing, decimals).
	- Equality and ordering across different but compatible units.
- Name tests using `MethodName_Condition_ExpectedResult()` and ensure they pass on both TFMs.
- After changes, run the workspace “build” task to validate:
	- Build: PASS
	- Lint/Style: N/A (no analyzers enforced)
	- Tests: PASS

## Documentation

- XML docs are enabled and API reference is generated with DefaultDocumentation to `docs/api/`.
- When adding public members, include concise XML summaries and parameter docs.
- If behavior changes, add an entry to `docs/CHANGELOG.md` and update README if user-facing.

## Unit catalog updates

- Add/modify units only via `UnitDefinitions.Definitions` and ensure:
	- `Name`, `Factor`, `UnitType` and `Aliases` are correct and non-duplicative.
	- Parsing accepts common aliases case-insensitively where appropriate.
	- Add tests for `IsValidUnit`, `Parse`, conversions, and formatting.

## Safety checklist for Copilot before submitting edits

- Kept changes minimal and localized to the files required.
- Preserved `decimal` for value computations; no floating-point drift introduced.
- Preserved immutability and value semantics of `Quantity`.
- Used `UnitDefinitions.Parse` and factors for any conversions.
- Added/updated tests for any new behavior or bug fixes and ran the build task successfully.
- Updated docs (XML comments, CHANGELOG) if public behavior/APIs changed.

## Quick pointers for this repo

- Build/test tasks (use these in the editor when validating your changes):
	- “build” task builds and runs tests: `tests/TareTests.csproj`
	- “watch” task runs the tests continuously for quick feedback
	- “publish” task is for packaging validation; use sparingly for local checks
- Versioning scripts (`update-version.ps1`) and packaging metadata live in `src/Tare.csproj`—don’t modify versions unless explicitly asked.

## Feature Backlog

- A feature backlog is maintained in `.github/features/feature-backlog.md` to track planned work. A sample feature entry is included there for reference (F-000).

---

Keep edits focused, additive, and verifiable. If a change would ripple across multiple files or alter public behavior, pause and request explicit confirmation before proceeding.

