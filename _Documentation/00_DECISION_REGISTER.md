# Decisions, precedence and outstanding questions

## Precedence

Latest explicit user decision > earlier user decision > this documentation's explicit recommendation > agent suggestion. Any mismatch is a clarification issue for the lead, not permission to improvise. Record approved amendments here with date, reason, affected files, tests and owner.

## CONFIRMED

- **Input:** Use Unity's New Input System for all game input. Support touch for the mobile launch, mouse for editor testing, and gamepad through the same action abstraction for future console support. Gameplay systems must consume input actions/commands rather than device-specific APIs.
- Tilerift is a mobile-first 3D top-down grid puzzle-adventure inspired by *Mario and Donkey Kong: Minis on the Move*. Phone portrait view, fixed orientation, no camera zoom or rotation at launch; pan by dragging empty space; double-tap empty space to recenter hero.
- Hero advances continuously at fixed designer-configurable rate, modified only by items/effects. Starting countdown; Pause instructions stop for a fixed duration (prototype default 2s). Intentional movement state: Moving, Paused, Blocked; combat is a transient scripted action, not accidental Blocked.
- Unexpected blockage is fatal. Game-over at combined HP 0; no checkpoints. World timer runs through combat animations and pauses unless an explicitly designed time-stop effect applies.
- Four-way cardinal grid, no diagonal movement initially. Future elevation, slopes, falls, landing attacks and camera rotation must be anticipated but not activated in flat-map MVP.
- A Jump instruction is required to jump; otherwise movement off a ledge falls. Initial jump spans 1 cell; boots may extend it. Falls greater than 1 height level kill. Landing on enemy proposes +1 CB, +2 to test once elevation ships.
- Place player-owned action tiles only on empty, compatible cells at least 2 cardinal-grid steps from hero; live valid-cell highlights update while dragging; validate again on release. Activation on entry, turning at cell center. Unactivated placed tiles can be moved or retrieved; retrieval enqueues at queue tail. After first activation, reusable tiles remain locked indefinitely; consumables disappear.
- All newly collected/retrieved action tiles enter FIFO queue. Automatically move queue head to available hand slots. Initial hand 3; default cap 5; show first 3 queued and `+N` for hidden items if any.
- Battle on attempted conflicting occupancy with attacker facing movement direction; hero wins simultaneous contention priority. Attacker gains CB +0 against defender front, +1 side, +2 back. Armor absorbs HP cost first, then base HP. Existing armor is replaced rather than stacked; replacing armor changes only armor HP, never heals depleted base HP.
- Combat computed once with short presentation (target <=0.5s), clock never pauses automatically. Enemy HP default 1; bosses have multiple HP and relocate after hits (destination logic OPEN).
- Enemy order: stationary, rotating, patrol, chase, bosses. All use one grid simulation. Player action tiles are distinct from map-embedded enemies, pickups, traps, gates, switches and environment.
- Travel: timed start-to-finish puzzle; Explore: finite bounded handcrafted or procedural map with objective; Survival: endless expanding procedural map.
- Ads only between runs every configurable X matches; never during live play. Final policy pending.
- **Combat formula owner approval (2026-10-31):** Approved the documented exact HP-cost formula, including CB-equality cost 1 and defeat when surviving HP reaches 0 or less. Affected implementation: CombatResolver; verification: 20/20 EditMode tests passed.

## PROVISIONAL / TEST VALUES

- Hero 1 grid cell per second; 3s start countdown; Pause 2s; inventory 5; initial 3; queue preview 3; placement clearance Manhattan distance >=2; target combat presentation <=0.5s.
- Base hero HP 1. CB baseline and all entity stats per content asset. Early playtest maps ~6x6 to 8x8. Defaults are not balancing promises.

## OPEN — explicit owner signoff before feature's production implementation

1. Collision of moving actors attempting to swap two cells or meeting head-on in one simulation tick: hero-as-attacker priority is confirmed for same-cell simultaneous contention; exact swap resolution should be validated in phase 2.
2. What does an enemy win mean if hero has enough HP to absorb cost? Current rule: hero always defeats standard 1-HP enemy if hero remains alive after spending cost; test feedback/signoff.
3. Tile destination can be revisited while an unactivated tile sits there; lock tile only on its own first activation. Retrieval timing when queue auto-fills a free hand slot should follow FIFO consistently.
4. Is a tile placed on the board removed from hand immediately (assume yes), and can the player discard tiles without placing them (OPEN)? Queue exploit balancing deferred.
5. Currency earning/spending, scoring, star thresholds, initial tile catalog, level objectives, explicit movement-pause interpretation of busy combat, game timer loss conditions, and bosses' retreat locations need content design and playtest.
6. Account model, platform services, backend vendor, analytics provider, ad network, purchase SKUs, consent/age policy, liveops and release regions need owner-approved service selection. No fabricated decisions.
7. Unity version, render pipeline, art direction, localization target languages, revenue model and store policy must be decided with owner.

If a question blocks a phase, implement a narrow mock behind an interface only with lead's logged approval; never silently implement the entire undefined feature.
