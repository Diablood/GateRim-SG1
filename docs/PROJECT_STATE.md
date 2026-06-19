# Project state

Current milestone: `0.3.3-dev - Rework organic Tok'ra wounded agent care`.

## Active development base

- Functional base tag: `v0.3.2-dev`.
- Dedicated branch: `feature/tokra-wounded-agent-care-rework`.
- Planned final tag after local validation: `v0.3.3-dev`.
- Current local test archive revision: `0.3.3-dev-r1`.

## Milestone scope

This milestone keeps the validated wounded-agent event flow intact and changes only the recovery pace after emergency treatment.

The existing sequence remains:

1. a wounded Tok'ra agent arrives downed under acute symbiote shock;
2. the colony rescues the agent into a player medical bed;
3. a doctor tends the shock itself;
4. the shock is lifted and ordinary medical care continues;
5. the agent leaves only after becoming fit to travel;
6. success is resolved only after the agent exits the map alive;
7. death before effective exit remains the priority failure condition.

## Recovery rework

Before initial treatment, `SG1_TokraWoundedAgentSymbioteShock` still blocks movement and suppresses injury healing completely.

After the shock is tended:

- the acute shock is removed;
- the operation adds `SG1_TokraWoundedAgentPostShockRecovery`;
- acute symbiote shock suspends the direct Tok'ra therapeutic injury regeneration; after emergency treatment, the post-shock condition limits that regeneration to `25%` of its normal rate and also multiplies vanilla Injury Healing Factor by `0.25`;
- for vanilla natural healing, the adult symbiote's normal `1.75` factor is reduced to about `0.4375` of a normal human; the separate therapeutic regeneration also runs at only `25%` of its normal Tok'ra rate;
- conventional tending, bleeding control, medical rest and protection therefore remain relevant;
- the condition is removed when the operation resolves, including success, failure or capture cleanup.

No mandatory medicine consumption, artificial fixed waiting timer or change to the validated departure criteria is added.

## Player-facing rules

- The event remains optional before acceptance.
- The shock itself remains tendable even when it is the patient's only remaining condition.
- After first aid, the player is explicitly told that the symbiote remains weakened and that continued care is required.
- The agent does not need complete healing, but must meet the existing travel-fitness checks.
- Success still requires a living exit from the map.
- Death at any moment before effective exit still causes failure, including during the departure journey.

## Save compatibility

- `0.3.0-dev` remains the compatibility baseline.
- Existing `0.3.2-dev` saves with an untreated patient keep the acute shock and receive the new recovery condition after tending.
- Existing saves with initial care already recorded receive the post-shock recovery condition when the framework next checks the patient.
- The new condition is stored as a normal pawn Hediff and needs no additional manager save field.

## Debug requirements

The existing single communicator debug menu remains unchanged in structure.

The wounded-agent phase advance action now applies the same post-shock recovery condition as normal medical treatment. Existing force-offer, accept, arrival, failure, expiration, state-report and reset actions remain available only in RimWorld developer mode or through the GateRim SG-1 advanced debug option.

## Files intentionally removed

None for this milestone revision.

## Required local validation

1. Build the assembly and confirm version `0.3.3.0`.
2. Force and accept the wounded-agent offer on a save compatible with `0.3.0-dev` or later.
3. Confirm the untreated shock still blocks movement and injury healing.
4. Rescue the agent into a player medical bed and tend the shock.
5. Confirm the acute shock disappears and `récupération affaiblie du symbiote` appears.
6. Confirm the patient no longer recovers at the former accelerated Tok'ra rate.
7. Continue ordinary treatment and confirm eventual travel fitness remains achievable before the five-day operation deadline.
8. Save and reload before treatment, during weakened recovery and during departure.
9. Confirm success only after living map exit and failure on death before exit.
10. Confirm the operation-specific recovery condition is removed after resolution.
11. Re-run the debug phase advance and verify it uses the same weakened-recovery state.
12. Review `Player.log` for XML, Hediff, Scribe or duplicate-resolution errors.

## Deferred follow-up

- Review the observation site's visual so it clearly resembles a field scope or telescope.
- Continue consolidating device-specific debug actions into grouped menus when several exist on the same object.
- Add further distinct operation archetypes only after the existing four remain stable on the shared framework.

## Publication after validation

Follow `docs/MILESTONE_PUBLICATION.md`.

Expected final publication identifiers:

- commit: `0.3.3-dev - rework organic Tok'ra wounded agent care`;
- branch: `feature/tokra-wounded-agent-care-rework`;
- annotated tag: `v0.3.3-dev`.

## Repository rules reminder

- Generate ZIP archives directly at the repository root; they are ignored by Git.
- Publish only the final milestone tag without an `-rN` suffix.
- Explicitly list every file that must be deleted before extraction.
- Preserve `About/ModIcon.png`.
- Keep metadata only in `About/About.xml`.
- Keep the changelog only in `docs/CHANGELOG.md`.
- Keep durable validation in `docs/TESTING.md`.
