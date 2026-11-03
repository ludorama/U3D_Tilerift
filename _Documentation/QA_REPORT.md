# QA report

## TIL-000 — Phase 0 foundation

### Automated evidence

- EditMode coverage added for cardinal offsets, Manhattan distance including future elevation, and cardinal turns.
- Unity project refresh and script compilation completed with no `error CS`, `Compilation failed`, or failed assembly messages in the editor log.
- Unity Test Runner executed in the licensed Unity Editor session: 8 tests passed, 0 failed.

### Manual evidence

- New Input System action asset includes shared Point and Press bindings for touch and mouse.
- Navigate, Submit and Cancel include gamepad bindings plus keyboard editor fallbacks.
- Sandbox smoke scene now contains the input router, configured camera controller, visible hero marker, and a 5x5 board.
- Sandbox was entered into Play Mode successfully with no visible compiler/runtime errors.

## TIL-001 — Phase 1 movement and placement core

### Automated evidence

- Added deterministic tests for countdown, travel, pause duration, terminal blockage, placement revalidation, and tile activation lifecycle.
- Unity Test Runner executed in the licensed Unity Editor session: 8 tests passed, 0 failed.

### Known limitations

- Production board presentation and hero movement visualization are not included in this smoke harness.
- Fixed the initial Phase 1 compile errors: test assembly references now include simulation and tile assemblies, and the camera controller aliases `UnityEngine.Camera` to avoid the `Tilerift.Presentation.Camera` namespace collision.

- The smoke scene is intentionally a presentation harness; hero movement visualization and production tile prefabs are still future work.

## Phase 2 — inventory and combat foundation

### Automated evidence

- Added FIFO tile inventory with configured starting hand, maximum hand size, queue preview, hidden overflow count, and retrieval-at-tail behavior.
- Added one-use and persistent tile state coverage, armor-first combat resolution, front/side/back approach bonuses, stationary enemy configuration, armor replacement, defeat state, and capped combat presentation duration.
- Unity Test Runner executed in the licensed Unity Editor session: 20 tests passed, 0 failed.

### Acceptance note

- The combat resolver is isolated behind CombatResolver and follows the owner-approved formula in _Documentation/02_SIMULATION_AND_COMBAT.md.

## Phase 3 — first complete Travel slice

### Implementation evidence

- Added a designer-authored Travel level asset schema with bounded walkable cells, start/exit, time limit, collectible locations, objectives, and hazards.
- Added deterministic run state for countdown, live timer, objective-gated exit, success, failure reasons, and retry.
- Added a scene controller that connects the Travel run to the New Input System router, hero movement simulation, authored collectibles/hazards, and a readable prototype HUD.
- Added 8 EditMode tests covering countdown, timer expiry, objective gating, hazard failure, retry reset, immediate exits, and timer behavior.

### Verification status

- Unity compilation and Play Mode smoke verification are pending the next licensed editor refresh. The headless Unity path is currently unavailable because batch mode cannot connect to the Unity Personal licensing client.
