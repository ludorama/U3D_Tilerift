# Deterministic simulation contract

## Coordinates and time

- Canonical cells use integer `(x,y,level)` with four cardinal facings N/E/S/W; initial `level=0` but retain elevation field.
- Authoritative simulation runs in fixed ticks, with configured tile travel duration and phase ordering. Rendering interpolates; never derive game position from Transform or Animator.
- Entry, center and exit are separate events: reserve transition, move through cell, resolve at center where directional command changes next heading. Tile activation on entry only once per placement instance. Exact phase is unit-tested.
- Simulated world time and countdown advance continuously during animation and normal Pause tile occupancy. Pause status is authorized and expires after designer duration; only explicit time-stop mechanics stop the world clock.
- Unexpected blocked movement yields terminal death; combat resolves before ordinary wall blocking, using transient combat presentation state while world time continues.
- All queues and conflicts resolve deterministically by step, stable cell/entity ID; log random seeds, commands and collisions. When hero and enemy contest entry into same cell simultaneously, treat hero as attacker, one combat only. Edge swap case flagged in decision register.

## Universal combat

An attacker is the moving unit facing into the defender cell. Determine whether approach hits defender front, side or back from defender orientation, then grant attacker +0, +1 or +2 CB. Both hero-attacks-enemy and enemy-attacks-hero use identical positional logic. No diagonal attacks in MVP.

Provisional approved-example-compatible formula:

```
effectiveHeroCB = heroCB + (heroIsAttacker ? facingBonus : 0)
effectiveEnemyCB = enemyCB + (enemyIsAttacker ? facingBonus : 0)
requiredHPCost = max(0, effectiveEnemyCB - effectiveHeroCB + 1)
survives = (heroBaseHP + heroArmorHP - requiredHPCost) >= 1
```

Hero CB2 / 5 HP vs enemy CB4: frontal costs 3 HP (2 remain); attacking side costs 2; back costs 1. Enemy approaches hero from side: enemy CB5 and cost4; from back enemy CB6 and cost5, killing 5-HP hero. At CB parity, cost 1 under current formula. Request signoff on formula in `00_DECISION_REGISTER.md` before combat production acceptance.

Spend cost from armor first, then base HP. Base HP remains damaged after armor swaps. New armor sets current armor HP to new item's value, not cumulative, not restoration. Defeat occurs on total HP <=0, even if monster otherwise slain. Standard 1-HP enemy defeated when hero remains alive; boss damage/retreat are separate configurable contract.

## Event ordering — proposal to confirm in automated test

1. Accept time-stamped player command (tile drop/retrieval) and validate against current grid snapshot; enforce Manhattan clearance >=2.
2. Advance simulation tick and resolve simultaneous movement intents deterministically.
3. Resolve contacts/combat/traps; only surviving actors commit transitions.
4. Fire tile entry activation and lock/consume semantics; fire center turns at precise crossing.
5. Apply pickups, score, objectives and FIFO queue; grant queue items to available hand slots.
6. Emit typed simulation events for animations, audio, UI and analytics.

Any revision to this order requires a design note and regression tests. Animation can hold hero's next step up to configured duration but cannot halt timer or enemy/world ticks. Avoid double hits from a single contact via encounter IDs and occupancy locks.

## Runtime invariants

- One authoritative cell owner per solid occupancy except explicit encounter reservation.
- No tile created inside forbidden cells or within restricted hero distance at commit time.
- Tile activating once locks reusable or consumes one-shot; never re-trigger one-shot after deletion.
- Queue ordering is stable across retrieval, reward and refill.
- No accidental simulation freeze in UI transitions, victory/death except explicitly terminal run.
