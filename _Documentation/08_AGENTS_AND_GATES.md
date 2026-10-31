# Agent hierarchy, reviews and acceptance boundaries

## Roles

**Production/Lead (one accountable approver):** receives owner priorities; breaks each phase into scoped tasks; assigns specialist; resolves cross-system integration; reads spec; checks evidence and diff; tests acceptance; accepts, requests revision or escalates. Lead does not invent gameplay or silently relax tests. Maintains decision register and release manifest.

**Gameplay Agent:** movement, tile verbs, interactions, combat and enemies. Must demonstrate deterministic cases and avoid Inspector-invisible balancing values.

**Systems Agent:** grid, simulation clocks, save, configs, queue, generation, services interfaces, performance and CI; owns tests for deterministic arbitration.

**UI/UX Agent:** portrait HUD, hand/queue, camera gestures, grid highlights, accessibility, input mapping and responsive layouts; captures device evidence.

**Graphics/Technical Art Agent:** 3D toy-like assets, rig/animation, materials, VFX, optimized prefabs and render budget; never encodes gameplay truth in animation curves.

**Audio Agent:** UI, footsteps, trap, pickup, contact and combat SFX; mixer, loudness and user controls.

**Content/Level Agent:** modes, authored levels, enemies/traps/items and procedural profiles, tutorials, generation validation.

**Economy/LiveOps Agent:** tuning, item sources/sinks, monetization experiments, ads/purchases mocks, fairness, balancing evidence; no live SDK without permission.

**Online/Analytics Agent:** identity, saves, leaderboards, logging, telemetry privacy and service adapters; no deployment or account creation by default.

**Localization/Translation Agent:** terminology, locale keys, translations, pseudo-localization and UI-fit checks. No final language claims without owner approval.

**QA/Automation Agent:** writes spec-to-test traceability, CI/regression checks, captures failures/repros, verifies mobile builds and performance independently from implementer.

One agent may hold multiple specialties in a small environment, but a task's final signoff must be performed in a separate lead review pass. If agent parallelism is not supported, simulate roles sequentially and preserve audit trail.

## Mandatory task contract

Lead creates task with: ID, phase, owner, affected files, spec links/sections, exact acceptance criteria, config variables/assets required, test plan, dependencies, risk, rollback plan, performance target and explicit exclusions. Before coding, agent reports expected design and conflicts. If conflict/OPEN question changes outcome, block and ask owner.

## Specialist handoff

Every submission must include: changed file list, behavioral summary, asset/config changes, unit/integration/manual tests and results, reproduction steps, relevant screenshots/log excerpts, known limitations and check against all acceptance criteria. Unsupported assertion 'works' is rejected.

## Bounded review workflow

1. Specialist submits completed task. Lead inspects diff, docs, asset schema and evidence against referenced spec.
2. If complete, lead integrates, runs focused tests, accepts and records signoff. For high-risk systems, QA independently runs critical regressions before gate closure.
3. If incomplete, lead sends **specific numbered findings** tagged blocker/major/minor with expected behavior, evidence and reproduction; specialist corrects and resubmits.
4. Maximum **two revision rounds per task**. After second failed review, lead stops automatic ping-pong, performs root-cause analysis and either splits the task, revises the plan, or asks owner to resolve contradictions. Never infinite retries.
5. An accepted task can reopen only if integration reveals a regression or change request, documented as a new issue; no unbounded polishing. Minor nonblocking cosmetics can be backlog items if acceptance criteria allow.

## Gate thresholds

**Blockers (zero tolerated):** wrong game rules; balance variables hardcoded where assets specified; data loss; runaway loop; nondeterministic grid; broken build; illegal/unsafe SDK use; core control failures; unexplained death; missed acceptance tests; regression in earlier phase.

**Majors (zero tolerated at phase closure):** queue ordering defect, placement highlight disagreeing with final validation, poor readable facing, missing localization, mobile UI occlusion, unrecoverable service fault, failing test coverage for core modules.

**Minors (max 3 explicitly logged noncritical visual/audio polish issues):** can be deferred with owner-visible backlog, no effect on correctness or accessibility.

## Anti-surprise checklist at every review

- Every tweakable value is in documented ScriptableObject/profile with bounds, labels and validation; no magic numbers.
- Code adheres to module dependencies; no service calls inside combat/grid core.
- Touch and desktop test harness function; UI safe area passes.
- Regression tests, examples and seeded replay pass, no warnings from new code.
- Existing work is preserved, no unrelated deletions, no undocumented vendor additions.
- Instrumentation, accessibility and localization hooks present as phase calls for.

## Gate deliverables

`TASKS.md` (living task ledger), `DECISIONS.md` (approved deltas referencing 00), `QA_REPORT.md` (tests/evidence), `PHASE_SIGNOFF.md` (lead signature and owner-needed items). Keep in project _Documentation during implementation; do not fabricate completion.
