# Game design document

## Pillars

1. Indirect control: never steer hero directly; manipulate the route and instructions.
2. Unbroken time pressure: hero's forward movement is autonomous; no generic game pause during encounters.
3. Expressive simple interactions: small verbs combine with traps, loot, enemies and map geometry.
4. Fair tension: challenges are explainable, readable and winnable with adequate planning; contextual rewards help create opportunities, not guaranteed rescues.

## Mode definitions

**Travel**: bounded authored or generated timed A→B route, collectible score incentives; objective is reach destination by deadline. Preserve separately until testing shows it redundant.

**Explore**: finite map of configurable dimensions/shape, procedural or authored; one or more goals: boss, exit, keys/doors, designated treasure collection, timed retrieval, etc. End when objective fulfilled; no endless growth.

**Survival**: infinite-progress expanding map with streaming sections, scaling challenge, score and defeat endpoint. Scope after Explore validates.

## Core loop

Countdown → inspect map and 3-tile starting hand → drag instruction tiles to valid vacant cells ≥2 Manhattan steps from hero → hero advances on grid → instructions activate at tile entry/center → collect goods, face hazards/monsters → acquire tile rewards via FIFO → place/revise unused instructions while moving → succeed objective or game over → summary and permitted between-run ad → next attempt.

## Tiles and board

- Player instruction examples: straight/neutral, turn left/right, U-turn, timed pause, jump, directional attack/special behavior later. Reusable and consumable variants may share the same verb, with distinct cost/rarity/appearance.
- Do not assume any player tile can overwrite existing environment, item, enemy, trap, or action tile. Board layers for terrain, occupancy, hazards and action tiles are separately modeled.
- Valid cells highlight during dragging and update as hero moves. No diagonal movement at launch. Existing tile unactivated = move/retrieve; activated persistent = locked forever; consumable activates and disappears.
- Tile hand cap default 5; start 3; all rewards/retrievals enter FIFO; refill hand automatically and show first 3 queue entries plus hidden remainder count.

## Enemies and combat

- Base enemy 1 HP. CB relates to player CB and available HP as documented in `02_SIMULATION_AND_COMBAT.md`. Position of initiator relative to defender front/side/back grants +0/+1/+2 CB. Weapons boost CB; armor boosts combined HP; no second separately displayed health bar.
- Stationary, timed facing rotation, scripted patrol, chasing, then multi-hit boss with jump/relocation after damage. Monsters, traps and timed gates create navigation risk.
- Combat is one calculation, represented by up to ~0.5s exchange animation; timer remains live. Hero loss may use enemy-specific death/VFX rather than wall collision presentation.

## Pickups and effects

- Coins and jewels: separate currencies and score inputs. Conversion ratio, shop sinks and thresholds **OPEN**.
- Weapons: CB modifiers. Armor: supplies temporary HP; replacing it discards old remaining armor HP and grants new armor HP without healing base HP.
- Keys open doors; object collection automatic on occupied cell. Invincibility, speed boost, time stop and other pickups possible with designer-configurable duration and activation semantics to be specified before shipping.
- Tiles earned via map pickups, currency thresholds, objectives or other contextual rewards. Weighted selection accounts for hero position, facing, danger, inventory/queue, possible routes and level difficulty. No guarantee of immediate rescue.

## Traps and environment

- Spike, poison gas, fire, moving obstacle, rotating blade, collapsing floor; each has authorable telegraph, activation, cooldown, damage and collision semantics. Environmental traps belong to map data, never to player action inventory.
- Future bridge, lift, conveyor, rotating platform, spring, ramp and multi-level cells. Jump requires Jump tile, not automatic gap traversal. Falling >1 vertical cell kills. Landing attacks use provisional +1 CB until testing.

## Run finish and scoring

- No checkpoints. Death is game over. Travel/Explore victory ends level. Survival ends on death (optional cash-out is OPEN). Show objective state, elapsed time, coins, jewels, defeated enemies, score and reason for failure. Continue via paid/rewarded offer is speculative; never implement without approved economy and store compliance.
