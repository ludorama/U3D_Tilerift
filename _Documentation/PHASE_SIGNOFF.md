# Phase signoff

## Phase 0 — inspect and scaffold

**Status:** Not yet accepted.

Unity compilation, 8/8 EditMode tests, and a Sandbox Play Mode smoke check are verified in the licensed editor session. The configuration asset, New Input System router, camera controller, hero marker, and board load without visible errors.

## Phase 1 — movement and placement core

**Status:** Core implementation verified.

Deterministic movement, countdown, pause/block states, placement revalidation, tile lifecycle, and camera input controller are implemented. Unity Test Runner evidence: 8 passed, 0 failed. Scene wiring and manual smoke presentation remain outside this core signoff.

## Phase 2 — inventory and combat foundation

**Status:** Accepted.

FIFO inventory, queue preview/overflow tracking, one-use and persistent tile behavior coverage, stationary enemy configuration, positional combat, armor-first damage, armor replacement, defeat state, and presentation duration limits are implemented. Unity Test Runner evidence: 20 passed, 0 failed. The resolver follows the owner-approved formula documented in _Documentation/02_SIMULATION_AND_COMBAT.md.
