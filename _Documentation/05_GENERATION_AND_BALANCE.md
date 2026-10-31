# Procedural generation and difficulty

## Architecture

Use seeded deterministic grid generation + independent reachability/solvability analysis; generator yields data, not instantiated 3D GameObjects. Author maps with same schema and validation tools. Generated Explore levels finite size and shaped; Travel finite objective A→B; Survival streams successive finite chunks around player with safe cleanup of old chunks and bounded memory.

## Generation pass

1. Set mode profile, seed, dimensions/shape, hero starting facing, exit/objective, map time budget.
2. Carve grid terrain and traversable connectivity; place environmental gates, trap fields and item paths.
3. Insert enemy types and facing, restricted entry constraints and optional treasure, boss, key/door dependencies.
4. Estimate route feasibility given fixed movement and instruction inventory/queue. Validate critical path with conservative reachable-action simulation and timeout constraints.
5. Score danger, route diversity, CB/HP attrition, item access, placement clearance, time margin, and tutorial exposure; reject/regenerate maps outside profile budget.
6. Emit seed + validation summary. Playtest adjustable difficulty; never covertly cheat actor CB or RNG to 'save' hero.

## Context-aware rewards

Evaluate current hero cell/facing, future cells, blockers, reachable threats, existing placed tile locks, hand contents, full FIFO queue, prospective movement verbs, item goals and time. Produce a weighted candidate pool by `TileDefinition` cost/rarity and mode profile. Select via seeded RNG. Preferences should create creative possibilities, not guaranteed solutions. Include safeguards for degenerate selections/repeated impossible tile sets; log why weights were assigned for debug.

## Fairness definition

At spawn, at least one plausible solution must exist under declared reward assumptions and skill budget. During play, user choices can produce unavoidable failure; generator must not retroactively protect player. For Survival, enforce local fair opportunities and advance warning; indefinite solvability cannot be guaranteed. Distinguish 'solvable with known starting kit' vs 'probabilistically fair with rewards'. Configurable validator work budget prevents infinite generation stalls.

## Difficulty

`DifficultyProfile` controls time slack, enemy CB distribution, facing/patrol/chase mixes, hazards, currencies, tile rewards, queue pressure, size, boss cadence and procedural branch count. Runtime adjustment only at safe chunk/level boundaries unless explicitly authorized; prefer transparent progression to hidden rubber-banding. Never modify data assets directly at runtime.
