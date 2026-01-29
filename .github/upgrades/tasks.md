# PkHex_Raid_Plugin .NET Upgrade Tasks

## Overview

This document tracks the execution of the All-At-Once upgrade for the repository: update all project TargetFramework values, package references, build fixes, and test validation in a single coordinated operation followed by test execution. Prerequisites are verified first and a single final commit completes the workflow.

**Progress**: 0/4 tasks complete (0%) ![0%](https://progress-bar.xyz/0)

---

## Tasks

### [▶] TASK-001: Verify prerequisites
**References**: Plan §Implementation Timeline (Phase 0), Plan §Detailed Execution Steps

- [▶] (1) Verify required .NET SDK/runtime version is installed per Plan §Implementation Timeline (Phase 0)
- [ ] (2) Runtime version meets minimum requirements (**Verify**)
- [ ] (3) Check `global.json` (if present) and update/validate compatibility per Plan §Implementation Timeline (Phase 0)
- [ ] (4) Configuration files (e.g., `Directory.Build.props`, `Directory.Packages.props`) compatibility check per Plan §Detailed Execution Steps (**Verify**)

### [ ] TASK-002: Atomic framework and package upgrade with compilation fixes
**References**: Plan §Detailed Execution Steps, Plan §Package Update Reference, Plan §Breaking Changes Catalog, Plan §Implementation Timeline (Phase 1)

- [ ] (1) Update TargetFramework/TargetFrameworks in all project files as specified in Plan §Detailed Execution Steps (all projects simultaneously)
- [ ] (2) All project files updated to target framework per plan (**Verify**)
- [ ] (3) Update all NuGet package references per Plan §Package Update Reference (apply grouped updates and replacements listed in plan)
- [ ] (4) All package references updated per plan (**Verify**)
- [ ] (5) Restore all dependencies (e.g., `dotnet restore`) and verify restore success (**Verify**)
- [ ] (6) Build solution and fix all compilation errors caused by framework/package upgrades per Plan §Breaking Changes Catalog (one bounded pass)
- [ ] (7) Solution builds with 0 errors (**Verify**)

### [ ] TASK-003: Run full test suite and validate upgrade
**References**: Plan §Implementation Timeline (Phase 2), Plan §Testing & Validation Strategy, Plan §Breaking Changes Catalog

- [ ] (1) Run all test projects listed in Plan §Implementation Timeline (Phase 2) / Plan §Testing & Validation Strategy
- [ ] (2) Fix any test failures referencing Plan §Breaking Changes Catalog for common issues
- [ ] (3) Re-run tests after fixes
- [ ] (4) All tests pass with 0 failures (**Verify**)

### [ ] TASK-004: Final commit
**References**: Plan §Source Control Strategy

- [ ] (1) Commit all remaining changes with message: "TASK-004: Complete atomic upgrade — update target frameworks, package updates, build and tests"
