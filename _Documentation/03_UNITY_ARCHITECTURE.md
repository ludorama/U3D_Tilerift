# Unity implementation architecture

## Modules (assembly definition boundaries where useful)

- `Core/Grid`: coordinates, cell state, occupancy layers, neighbor queries, future elevation compatibility.
- `Core/Simulation`: fixed-step scheduler, deterministic RNG, orders, movement reservations, collision arbitration, events, replay log.
- `Gameplay/Hero`, `Gameplay/Enemies`, `Gameplay/Combat`, `Gameplay/Tiles`, `Gameplay/Items`, `Gameplay/Traps`, `Gameplay/Objectives`.
- `Generation`: finite map builders, endless chunk streaming, difficulty model and independent solvability verifier.
- `Presentation`: 3D view actors, tile meshes/highlights, camera, animations, VFX, SFX and responsive UI.
- `Services`: local persistence, optional auth, analytics, ads, purchases, cloud saves, rankings; each behind an interface with a null/mock implementation.
- `Content`: ScriptableObjects + validation/editor tools and migration/versioning.

## ScriptableObject assets (designer-owned)

`HeroConfig` base HP, CB, tiles/sec, entry facing, collision size, starting delay; `EnemyConfig` CB, HP, stationary/rotate/patrol/chase, patrol data, turn interval, awareness, speed; `CombatConfig` positional CB bonuses, animation time budget and spending convention; `MovementConfig` tick frequency, tile duration, pause, jump range, height damage threshold; `TileDefinition` verb, reuse count/consumable, cost/rarity, icon, material, triggers; `InventoryConfig` max hand/start hand/visible queue/queue cap; `PlacementConfig` min distance, occupancy layer policy/highlight; `TrapDefinition` hazard schedule, telegraph, damage, effects; `ItemDefinition` pickup behavior, currency and equipment stats; `CameraConfig` fixed angle, pan speed, drag deadzone, double tap interval, follow/recenter easing; `DifficultyProfile` target danger, enemy tiers, rewards, ramp; `MapGenerationProfile` dimensions, geometry, seed, objectives and validation budget; `RunModeConfig` time and failure conditions; `EconomyConfig` coins, jewels, sinks, tile reward weights; `AdsConfig` interstitial cadence, caps, eligibility; `AnalyticsConfig` schema version and sampling; `LocalizationConfig` supported locales and fallbacks; `SaveConfig` schema version and policy.

Each content asset must be validated via `OnValidate` plus dedicated editor validators; avoid fragile implicit default values. Asset references via GUID/typed references; duplicate config IDs rejected. Profiles can override global defaults without runtime mutation of shared ScriptableObjects. Maintain debug Inspector to view effective resolved config.

## Anti-hardcoding requirements

- Do not embed balance numbers, ad cadence, queue capacity, grid size, combat bonuses, timer lengths, animation duration or level targets in controller scripts. Constants allowed only for mathematical invariants (e.g. four cardinal directions).
- Do not include service-specific SDK calls in core gameplay. Do not call Unity APIs from deterministic core logic except trivial portable math wrappers.
- Avoid singleton grab-bags. Use injected interfaces, events and explicit run-scoped contexts. No per-frame FindObjectOfType, no allocations in hot loops where avoidable.
- Use Unity's New Input System for touch, mouse and gamepad. Keep gameplay device-agnostic through data-driven actions, commands and rebinding.

## Project hygiene

- Respect existing Unity project, assets, packages, git status and user changes. Do not overwrite scenes or package settings blindly.
- Folder suggestion: `Assets/Tilerift/{Code,Configs,Art,Audio,UI,Scenes,Prefabs,Tests,Editor,Localization}`.
- A bootstrap scene loads config and services; a gameplay scene can run fully offline with mocks. `Development`, `Staging`, `Production` environment settings distinct and secrets never committed.
- Single source of truth for save schema, stable content IDs, replay seeds and analytics event names.
