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
