# Platform services, metrics and translation

## Identity and saves

- MVP offline-first: local guest profile, versioned save schema, deterministic level seed/replay metadata, run summaries and settings. Optional authentication architecture via `IAuthService` but no mandatory sign-in for core play without explicit owner decision.
- Later account providers (Apple, Google, platform-native, anonymous upgrade) require vendor selection; plan merge/guest-upgrade, consent, account deletion/export, parental restrictions and recovery. Do not collect personal data until legal basis and privacy design signed off.
- `ISaveService` local implementation now; `ICloudSaveService` mock. Make cross-device sync conflict policy explicit (timestamp alone insufficient), encrypt sensitive tokens, never store provider secrets in assets.

## Rankings and online

- `ILeaderboardService` local fake for MVP; later verified server submission for Travel time and Survival score, versioned rules, anti-cheat seed/config validation, ranked vs unranked if boosts/revives change fairness.
- Network failures must not block offline gameplay; queue consented submissions if appropriate. No unverified client-side ranking authority.

## Analytics event schema v1

Event naming lowercase snake case; common fields: event_schema_version, session_id (ephemeral), build, mode, level_id, seed, difficulty_profile, runtime_seconds, locale; pseudonymous user ID only with appropriate consent.

Events: `run_started`, `countdown_completed`, `tile_collected`, `tile_enqueued`, `tile_dequeued`, `tile_placed`, `tile_retrieved`, `tile_activated`, `tile_placement_rejected`, `hero_blocked`, `combat_resolved` (CB, facing, HP before/after), `item_picked`, `objective_completed`, `run_failed`, `run_succeeded`, `ad_eligible`, `ad_shown`, `ad_failed`, `purchase_started`, `purchase_completed`, `login_started`, `login_succeeded`, `login_failed`, `leaderboard_submitted`, `save_migrated`.

Analytics must be opt-in/out as applicable by region, batched/size bounded, resilient offline, sampled only where safe; no raw names, email, precise location or sensitive content. Test event idempotency and opt-out.

## Localization

- All user-facing strings use keyed localization tables, never hardcoded strings in prefabs/C#; placeholders typed and validated, plural forms and number formats locale-aware. Icons must not embed untranslated text.
- English source locale proposed; Spanish (Spain) and Portuguese (Brazil) sensible candidates, but **target launch languages are OPEN**. No machine translation automatically published. Translation agent maintains glossary, voice, context screenshots, character limits, RTL readiness if added.
- UI layouts must survive long strings, font fallbacks, safe area, dynamic text. Pseudo-localization tests mandatory before launching new locale.
