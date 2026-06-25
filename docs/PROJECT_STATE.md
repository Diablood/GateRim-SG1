# Current project state

Current milestone: `0.3.42-dev - Separate advanced diagnostics from developer actions` — final local revision `r2` validated and published.

## Repository state

- Starting tag: `v0.3.41-dev`.
- Published branch: `feature/debug-command-visibility-audit`.
- Published versions: `0.3.42-dev` and `0.3.42.0`.
- Final local revision: `r2`.
- Final unique tag: `v0.3.42-dev`.
- Main repository and separate wiki synchronized.

## Published behavior

The GateRim advanced-information option is now consistently diagnostic. It may reveal technical reports, detailed inspection strings, persistent identifiers and routine lifecycle traces, but it no longer grants commands that force, complete, reset or otherwise bypass gameplay state.

State-changing test commands now use the shared `GR_Debug.DeveloperActionsEnabled` rule and therefore require RimWorld developer mode. This includes:

- the Tok'ra communicator menu that forces, progresses, resolves or resets organic operations;
- the duplicate direct launch gizmo on the decoded Goa'uld relay site;
- forced Goa'uld implantation and autonomous-hunt controls;
- deterministic emergency extraction;
- unrestricted Goa'uld queen harvest testing.

Ordinary player interactions remain under their existing contextual rules.

## Progressive communicator discovery

The selected-pawn right-click menu no longer advertises trusted-tier Tok'ra support before the colony reaches the `Trusted` tier.

Below that tier, the communicator exposes only:

- the read-only channel status consultation;
- an interaction for an organic operation that is already genuinely proposed or active.

Once the trusted tier is reached, support requests appear normally. Immediate restrictions tied to the selected pawn, access, reservation, power, cooldown, threat or patient availability remain visible as disabled entries with their normal reason.

Execution-time trust checks remain in place as defensive validation.

## Final validation

- `check-project-consistency.cmd` passed for `0.3.42-dev` and `0.3.42.0`.
- The Windows build of `GateRimSG1.dll` version `0.3.42.0` passed.
- The normal-play / advanced-information / developer-mode visibility matrix passed.
- Progressive communicator discovery passed below, at and after returning from the trusted tier.
- Contextual disabled reasons remained visible after unlock.
- Save/reload preserved trust-dependent visibility and did not alter mission or operation state.
- Ordinary player actions and the caravan relay flow remained available.
- `Player.log` showed no new GateRim SG-1 error.

## Durable boundaries

- Advanced information must remain read-only.
- Commands that modify artificial test state must require RimWorld developer mode, even when their caller is already hidden.
- Player-facing progression menus must not reveal future unlocks through disabled labels.
- Once an action is legitimately unlocked, immediate contextual failures should remain readable instead of hiding the action.
- No save identifier, mission state, Def, storyteller weight, balance value, texture or persistent-data format changed in this milestone.
- `docs/IDEAS_TO_REVISIT.md` remains separate from the roadmap; no exploratory idea was promoted by this maintenance milestone.

## Next milestone

No `0.3.43-dev` scope is imposed. Before starting new work, read `docs/ROADMAP.md`, `docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`, select a bounded backlog item, then branch explicitly from `v0.3.42-dev`.
