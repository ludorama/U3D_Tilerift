# Instructions to Codex / agent runtime

You are implementing Tilerift under production-led review. Treat this folder as the authoritative design input. Read `README.md`, `00_DECISION_REGISTER.md`, `08_AGENTS_AND_GATES.md`, and `09_PHASES_AND_ACCEPTANCE.md` completely before initiating task work; read relevant system docs before editing.

## First execution

1. Inspect project and report Unity version, structure, package inventory, git status, current scenes and test facilities. Do not modify existing user assets during inspection.
2. Create a phase-0 task ledger with small subtasks, spec refs, acceptance criteria, test evidence and specialist ownership. Identify owner-blocking OPEN items. Ask for decisions only when phase demands them.
3. Production/Lead assigns each task, obtains architecture note, reviews submitted diff/tests, accepts or issues numbered findings; max two automatic revisions per task.
4. Implement configurable ScriptableObjects and validators before any gameplay scripts depend on numerical tuning. Use default data assets labeled provisional.
5. Deliver Phase 0 acceptance and explicitly ask owner permission to advance to Phase 1 if needed by workflow. Avoid generating all game systems at once.

## Never do

- Do not infer or invent spec details when marked OPEN; do not change combat formulas from examples unilaterally.
- Do not hardcode tweakable numbers or copy values into runtime MonoBehaviours.
- Do not pause game time for combat, fail to route retrieved tiles to queue, or directly control hero movement from touch input.
- Do not install unapproved services, ads SDKs, purchase plugins, authenticate to new vendors or place credentials in project assets.
- Do not mark a task complete without tests and production/lead signoff; do not continue unbounded revision loops.

## Reporting template

`Task ID | Phase | Specialist | Spec refs | Implementation summary | Config asset changes | Changed files | Tests/results | Evidence | Known issues | Lead verdict | Review iteration (0-2) | Next dependency`

For every implementation phase, update task ledger, decisions, QA evidence and signoff docs inside `_Documentation`. All rule changes require owner approval and a doc+test update in same task.
