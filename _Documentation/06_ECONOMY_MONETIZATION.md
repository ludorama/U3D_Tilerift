# Economy, progression and monetization design

## Core economy (CONFIRMED scope, unresolved balance)

- Coins and jewels are distinct in-run currencies/score inputs. Establish earning rates, spending sources and permanence later.
- Action tiles may be bought or awarded, each with reusable and cheaper consumable variants. Economy data asset contains costs, weights, unlock gates and per-mode restrictions; never hardcode prices.
- Every collected/retrieved tile enters FIFO queue. Inventory cap and sequence remain deterministic. Context-aware random generation uses mode, location, danger and hand/queue state.
- Start progression offline and local. Persistence of currencies/unlocks across runs is OPEN until user confirms economy model.

## Ads

- User preference: interstitial ads **between plays**, every configurable X completed runs; no ads during running simulation, timed puzzles or combat.
- `IAdService` mock first. Configurable cadence, session caps, cooldown, exclusion for paid ad removal and unavailable inventory. Count attempted/served/failed displays separately.
- Respect consent, age flags, store requirements, regional privacy and platform rules. Production ad network, placements, frequency and policies require owner approval and up-to-date compliance verification at integration time.
- Rewarded revive/continue is speculative, explicitly unapproved and not in MVP. If later approved, specify fairness, leaderboard separation and exact save state before implementation.

## IAP / subscriptions

- No approved SKUs or purchases yet. Prepare `IStoreService`, entitlements, verified receipts, restore purchases, offline fallback and duplicate-fulfilment protection; build with mocks first.
- Favor fair non-pay-to-win monetization. Do not sell competitive advantage or manipulate procedurally generated difficulty around ad/purchase prompts without explicit owner consent.

## Balancing / telemetry

Track run completion, failure reasons, session depth, queue size, tile usage/abandonment, pickup distribution, armor spend, completion time, ad eligibility vs actual shows, opt-in purchase funnel. Analytics must never drive invisible player-specific difficulty before approval.
