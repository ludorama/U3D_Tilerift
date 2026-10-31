# Testing and verification matrix

## Automation

- EditMode tests: grid neighbor/orientation logic, Manhattan distance, compatibility, immutable activated tile, queue FIFO, retrieval tail, auto-fill, overflow count, CB equations in 6 orientation cases, armor depletion/replacement, death HP=0, currency arithmetic, seeds and config validation.
- PlayMode tests: continuous movement and center turns; 3s countdown, 2s Pause still advances world timer; collision once; simultaneous same-cell hero attacker; combat animation <= configured upper bound; camera drag empty vs UI; tile drag hero approaches and drop rejected; tile persists locked after passing; consumer disappears.
- Property/fuzz tests: random tile operations never duplicate/remove wrong tile, queue keeps order, no actor overlaps without encounter, repeated seed replays match outcome; generated map validator passes acceptance threshold.
- Integration tests: mock auth/offline save, schema migration, leaderboards mock submit, ads count eligibility, fail-safe no network, analytics event fields and consent.
- Performance: profile supported Android/iOS device tiers (to be selected), CPU/frame timing, memory/chunk streaming, UI fill rate, GC spikes and touch response.

## Example acceptance vectors

1. Hero `(2,2)` action tile `(2,3)` invalid distance1; `(2,4)` valid distance2 if empty; target becomes occupied during drag → reject without consuming tile.
2. Hand 5, queue A B C D E → preview A B C +2. Place one hand tile → dequeue A, preview B C D +1. Retrieve unactivated tile R → append after E; ordering preserved.
3. Hero CB2, HP5, Enemy CB4: hero front +0 cost3 left2; hero side +1 cost2 left3; hero back +2 cost1 left4; enemy side attack +1 cost4 left1; enemy back attack +2 cost5 dead.
4. Hero base2 armor1 spends1 → base2 armor0. Replace armor+2 → base2 armor2 total4. Hero base2 damaged1 with no armor; equip armor+1 → total2.
5. A reusable tile survives activation locked; cannot retrieve; a one-shot tile disappears. Tile placed in already visited cell remains editable until its own activation.
6. While combat animation plays, world timer and other enemy schedules continue. Multiple contacts cannot double charge HP in one encounter.

## Manual QA

Test phone orientation/safe areas, overlay and queue hidden count, low-end readability, enemy facing, trap warnings, frame stability under camera pan, drag cancel, lost focus, mid-gesture hero movement, localization overflow, sound off, run retry, no checkpoints, ads only on eligible results (mocked).

## Evidence

Each task provides test runner output, minimum 1 deterministic seed/replay trace for gameplay, screenshot or short device capture for UI, profiler sample for perf-sensitive changes, and explicit spec line references. Failed test -> lead blocker and revision loop.
