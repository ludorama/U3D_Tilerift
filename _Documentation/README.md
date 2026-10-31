# TILERIFT — implementation documentation

Version: 0.1 preproduction baseline, 2026-10-31. Intended location: `D:\Games-Unity\U3D_Tilerift\_Documentation`.

## Start here

1. `00_DECISION_REGISTER.md` — authority, confirmed decisions, open decisions, conflict handling.
2. `01_GDD.md` — player-facing design, modes, tiles, enemies, items, hazards.
3. `02_SIMULATION_AND_COMBAT.md` — deterministic grid simulation, combat formulas, timing.
4. `03_UNITY_ARCHITECTURE.md` — project boundaries and configurable assets.
5. `04_UI_INPUT_ACCESSIBILITY.md` — mobile portrait UI, camera and controls.
6. `05_GENERATION_AND_BALANCE.md` — procedural maps, difficulty and fairness.
7. `06_ECONOMY_MONETIZATION.md` — tile rewards, currencies, ads, purchases.
8. `07_SERVICES_ANALYTICS_LOCALIZATION.md` — identity, saves, leaderboards, telemetry, translation.
9. `08_AGENTS_AND_GATES.md` — specialized agent contracts and bounded review loop.
10. `09_PHASES_AND_ACCEPTANCE.md` — phased backlog, acceptance checks, signoff.
11. `10_TEST_PLAN.md` — automated and manual verification matrix.
12. `11_CONTENT_PIPELINE.md` — 3D art, animation, audio, data, authoring.
13. `12_CODEX_START_HERE.md` — agent execution instructions.

## Mandatory discipline

- The documents contain **CONFIRMED**, **PROVISIONAL**, and **OPEN** rules. Never silently turn an OPEN item into a production decision. For prototypes use explicitly labeled defaults stored as data assets.
- No critical gameplay numbers hardcoded in MonoBehaviours. All designer-facing numeric parameters must be assets; runtime state is separate.
- The authoritative simulation must be independent from rendered positions and animation frames.
- Ship an end-to-end playable, tested core before expanding scope.
- A production/lead agent approves each work item against referenced sections and evidence. See review caps in `08_AGENTS_AND_GATES.md`.
- Do not install SDKs, create accounts, buy services, or enable monetization production credentials without explicit owner approval.

## Project setup

Unity, C#, 3D assets, portrait mobile first; keep desktop/gamepad support possible but not necessarily enabled at launch. Unity version, render pipeline, external packages and target minimum OS versions are **OPEN** pending environment inspection and owner approval. Prefer built-in Unity capabilities; add third-party packages only with a documented need, license and alternatives.
