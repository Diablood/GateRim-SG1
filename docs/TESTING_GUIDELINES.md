# Test-writing guidelines

## Purpose

A test procedure must remain usable after a long pause by someone who did not implement the change. It must explain how to reach the tested state, what action to perform and what exact result proves success.

Avoid shorthand such as:

```text
Complete observation: +3 trust and 250 XP.
```

Prefer a complete procedure:

```text
Force a Tok'ra observation opportunity, accept it through the powered communicator, advance the active operation with the developer action, then transmit the report through the communicator. Compare the Tok'ra trust and the transmitting colonist's Intellectual XP with the values recorded before the test. Trust must increase by 3, Intellectual XP by 250, and no active organic operation must remain.
```

## Required structure for every test

Each durable test must contain:

1. **Purpose** — the behavior being verified.
2. **Starting state** — required map, objects, pawns, trust tier, developer mode and save version.
3. **Procedure** — exact actions in execution order, including the visible command or menu label when useful.
4. **Expected result** — observable outcome, numerical changes and objects that must appear or disappear.
5. **End state** — whether the next test may continue immediately or whether a reset, save reload or game restart is required.

A test must not rely on the reader remembering a previous milestone or conversation.

## Execution categories

Mark or group tests according to the cheapest required reset:

- **Continuous session** — may run after the previous test without loading a save.
- **Tracker reset** — requires only `Reset Tok'ra organic operation tracker` or another documented debug reset.
- **Checkpoint reload** — requires loading a named save created during the same test session.
- **Legacy-save migration** — requires a save produced by an older published version.
- **Full restart** — requires closing and restarting RimWorld, usually for XML, assembly or mod-list validation.

## Ordering rules

Order tests to minimize repeated setup:

1. common preparation performed once;
2. non-destructive success paths that can run consecutively;
3. alternate placement or condition checks using the same running game;
4. save and reload checks using named checkpoints;
5. failure, destruction and expiry paths;
6. legacy-save migration;
7. broad regression and log review.

Create a named checkpoint before branching into mutually exclusive outcomes. For example, save once after an operation is accepted, then use copies of that checkpoint for success, destruction and expiry tests.

Do not reload a save when a documented reset action can safely restore the required state without affecting the behavior under test.

## Wording rules

- Use exact visible labels in backticks when the player or tester must click them.
- State who performs the action: selected colon, communicator, module, caravan or developer action.
- Record baseline values before checking numerical gains or losses.
- State whether ignored offers, accepted failures and completed operations have different consequences.
- Separate player-facing wording checks from technical debug-label checks.
- Do not hide multiple independent expectations behind a vague result such as `works correctly`.

## Repository policy

- Durable procedures belong in `docs/TESTING.md`.
- Do not create milestone-specific `TEST_PLAN_*.md` files.
- Update the relevant durable section when behavior changes.
- `docs/PROJECT_STATE.md` should list only the milestone's priority scenarios and link to the durable section.
- Git history and `docs/CHANGELOG.md` preserve historical changes; the active test procedure should describe the current behavior.
