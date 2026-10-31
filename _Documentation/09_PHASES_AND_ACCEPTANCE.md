# Ordered phases and measurable acceptance

## Phase 0 — inspect and scaffold

Inspect Unity project safely (version, packages, scenes, git changes), confirm SDK baseline and OPEN questions. Introduce assembly boundaries, ScriptableObject config examples, test runner, smoke scene and docs. **Gate:** project opens/builds cleanly, data-driven hero settings proven, no user assets overwritten.

## Phase 1 — core movement and commands

Implement integer grid (future level), hero auto-movement, start countdown, Moving/Paused/Blocked, tile center turns, pause, camera pan/recenter, live placement highlights, distance/occupancy validation, tile lock/consumption. **Gate:** seeded replay same result; >20 automated path tests; tap/drag conflict test; no invalid placement from moving-hero race.

## Phase 2 — inventory, queue, combat

Hand start3 max5 from asset, FIFO queue all rewards/retrievals, preview 3 + overflow label, one-use and persistent tile variants; stationary enemies, positional CB/HP formula and armor replacement, death/VFX and short combat presentation. **Gate:** test all 6 approach orientations, example CB2/HP5 vs CB4 costs 3, armor-first consumption, HP=0 defeat, hero priority contention, FIFO invariants and retrieval lifecycle. Require formula owner signoff before merging.

## Phase 3 — first complete Travel slice

Handcrafted bounded timed levels, start/exit, coins/jewels, objective UI, results, retry, authored terrain and simple hazards, onboarding, audio/3D readability. **Gate:** playable offline from launch to results, successful and failed runs, scene reload and persistence, phone portrait UX review, no gameplay hardcoding.

## Phase 4 — Explore foundation

Objective types and finite authored/procedural maps, keys/doors, pickups, equipment, trap varieties, generator/validator, context-aware tile reward selection. **Gate:** seeds reproducible, generator validation budget bounded, varied objectives and fair seeded tests, no impossible map labeled validated.

## Phase 5 — enemy expansion and content

Rotating, patrolling, chasing AI then bosses with multi-hit relocation; future ranged weapons after owner specification. **Gate:** timing/facing/collision regression; pathfinding grid budget; telegraphs readable; no double combat events.

## Phase 6 — Survival and progression

Chunk streaming, endless pressure, scoring, difficulty profiles, local leaderboards and save schema. **Gate:** long-running soak test, memory stable, seed reproducibility, failure summary correct, no world stalls.

## Phase 7 — services and monetization (mocks before live)

Analytics schema, consent, guest identity then optional provider login, cloud-save strategy, online rankings, ads between runs per X, purchase entitlement hooks, restore flow, retention dashboards. **Gate:** offline functionality, consent/opt-out tests, failure fallbacks, compliance review, explicitly owner-approved providers and live keys; no unapproved live integrations.

## Phase 8 — polish, localization, launch candidates

Translation, accessibility, performance budgets, art/VFX, QA sweeps, Android/iOS test devices, store builds, monetization sanity, deployment checklists. PC/console remains future phase with gamepad from input abstraction. **Gate:** device matrix, crash-free sessions, functional tests, store policy audit and final owner acceptance.

## Deferred R&D gate — elevation

Experiment with cell levels, ramps, falls and required Jump tile; test occlusion and camera yaw in isolated branch. Do not add rotating camera or elevated shipped gameplay without usability test and owner signoff. Landing CB bonus provisional +1 / +2 test.

## Phase completion rule

Cannot advance without Production/Lead acceptance, QA evidence, no blockers/majors, all assets configurable, docs updated and owner confirmation for unresolved design decisions. Design iteration inside phase is expected; avoid speculative feature creep.
