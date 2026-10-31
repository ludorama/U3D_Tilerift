# Content production and visual/audio direction

## 3D board style

Use 3D geometry with a readable toy/diorama board silhouette, fixed angled top-down orthographic-like framing, clear cardinal paths and orientation. No 2D sprite world; 2D UI/icons permitted. Final art theme OPEN; avoid locking fantasy-only terminology into data architecture.

## Grid assets

- Consistent cell footprint, pivot at center, integer-coordinate snapping; independent layers for terrain, environment, pickup, unit and user action tile. Prohibit visual overlap that hides facing or tile marks.
- Prefab contracts: visible facing marker, interaction affordance, tile reuse/consumable material distinction, correct collider footprint, event-driven highlight and animation.
- Future elevation: cell level field, ramps and ledges, landing target and optional camera rotation; not active in MVP.

## Characters and effects

- Hero: walk, turn, pause, pickup, clean strike, HP-spending struggle, death, jump/fall later.
- Enemies: idle, rotate, patrol, chase, contact, hit, defeat; boss multi-hit warp later. Combat is single math event driving animation/VFX variation; no hitframe recalculation.
- Hazards: spike trigger, gas cloud, fire, collapsing terrain, blades, moving obstacles, clearly telegraphed. VFX readable on portrait and <= gameplay occlusion budget.

## Audio and localization

Audio cues for placement validity, queue movement, pickups, timers, enemy facing and attacks, safe/danger hazards. Mixer groups for music/SFX/UI. Do not rely solely on audio to communicate critical events. Text via localization keys; icons use universal recognizable silhouettes plus labels when needed.

## Optimization

Object pooling for enemies/pickups/VFX, instanced/static board materials where appropriate, texture atlas and draw call targets set after device profiling, conservative particle counts. Never compromise gameplay readability to meet art goals.
