# Tilerift task ledger

## TIL-000 — Phase 0 foundation

| Field | Value |
|---|---|
| Phase | 0 — inspect and scaffold |
| Specialist | Systems / Input |
| Spec refs | `00`, `03`, `04`, `09`, `10`, `12` |
| Scope | Establish data-driven config examples, deterministic grid primitives, New Input System actions, and EditMode coverage. |
| Acceptance | Project compiles; grid coordinates support integer x/y/level and cardinal directions; config values validate; touch/mouse/gamepad share action-based input; deterministic primitive tests exist. |
| Explicit exclusions | No hero movement, tile placement, camera, combat, scene changes, services, ads, or production economy. |
| Status | Smoke scene verified — script compilation and 8/8 Unity EditMode tests pass; Sandbox Play Mode smoke check completed. |

## TIL-001 — Phase 1 movement and placement core

| Field | Value |
|---|---|
| Phase | 1 — core movement and commands |
| Specialist | Gameplay / Systems |
| Spec refs | `00`, `01`, `02`, `03`, `04`, `09`, `10` |
| Scope | Deterministic countdown, continuous grid movement, pause/block states, placement revalidation, action-tile lifecycle, and New Input System camera pan/recenter controller. |
| Acceptance | Integer grid movement; countdown; center-to-center travel; terminal unexpected blockage; configured pause; live-validation-ready placement service; reusable lock and consumable removal; camera pan/double-tap recenter controller; EditMode coverage. |
| Explicit exclusions | Scene wiring/smoke presentation, combat, inventory queue, procedural generation, and service integrations. |
| Status | Core plus smoke wiring complete — source compile errors fixed, 8/8 Unity EditMode tests pass, and Sandbox Play Mode smoke check completed. |

## TIL-002 — Phase 2 inventory and combat foundation

| Field | Value |
|---|---|
| Phase | 2 — inventory, tile variants, enemies, and combat |
| Specialist | Gameplay / Systems |
| Spec refs | `02`, `05`, `09`, `10` |
| Scope | FIFO inventory and queue preview, one-use/persistent tile lifecycle coverage, stationary enemy configuration, positional combat resolution, armor-first damage, armor replacement, defeat state, and short combat presentation timing. |
| Acceptance | 20/20 Unity EditMode tests pass; implementation follows the owner-approved combat formula. |
| Explicit exclusions | Production UI/VFX presentation, procedural generation, and service integrations. |
| Status | Phase 2 implementation accepted; production presentation and later content remain future work. |

### Owner-blocking decisions

- Combat formula signoff completed on 2026-10-31. Remaining open decisions are tracked in 00_DECISION_REGISTER.md.
- Camera projection, economy, timer-loss rules, and service providers remain OPEN.

## TIL-003 — Phase 3 first complete Travel slice

| Field | Value |
|---|---|
| Phase | 3 — first complete Travel slice |
| Specialist | Gameplay / UI |
| Spec refs | `01`, `03`, `04`, `09`, `10`, `11` |
| Scope | Authored Travel level asset, bounded route, countdown and run timer, start/exit objectives, coins/jewels, hazards, success/failure/retry state, simple HUD, and input-driven hero route control. |
| Acceptance | Deterministic EditMode coverage added for countdown, timer expiry, objectives, hazards, retry, and terminal results; Unity refresh and Play Mode smoke verification remain to be run. |
| Explicit exclusions | Production art/audio, full results screen polish, persistence services, Explore generation, and monetization. |
| Status | Implementation in place; Unity verification pending. |

### Changed files

- `Assets/Tilerift/Code/Core/Grid/*`
- `Assets/Tilerift/Code/Gameplay/Configs/*`
- `Assets/Tilerift/Code/Input/*`
- `Assets/Tilerift/Code/Presentation/Camera/*`
- `Assets/Tilerift/Code/Core/Simulation/*`
- `Assets/Tilerift/Code/Gameplay/Tiles/*`
- `Assets/Tilerift/Tests/EditMode/*`
- `_Documentation/TASKS.md`
