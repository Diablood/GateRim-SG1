# Project state

Current milestone: `0.3.11-dev - Add player-controlled Tok'ra personality switching`.

## Active development base

- Functional base tag: `v0.3.10-dev`.
- Dedicated branch: `feature/tokra-personality-switching`.
- Planned final tag: `v0.3.11-dev`.
- Current local archive revision: `0.3.11-dev-r3` (final documentation and publication archive).

## Milestone goal

Allow a directly player-controlled Tok'ra host to choose which consciousness is currently active without creating a second pawn or altering the shared body, faction, relations, traits, equipment or health state.

The milestone:

- adds one player-only gizmo to Tok'ra host Hediffs;
- switches the displayed primary name between the stored host name and the stored symbiote name;
- switches the active backstories between the two persistent identity records, while preserving the host childhood whenever no dedicated symbiote childhood is configured;
- applies only the skill-level differences granted by the active backstories;
- keeps one shared XP progression that is synchronized before every switch;
- persists the active personality and shared skill state through save/load;
- restores the host identity automatically before extraction, transfer or ordinary Hediff removal;
- keeps the two identities visible in the health description at all times;
- leaves AI-managed Tok'ra, visitors, allies, enemies and uncontrolled quest pawns unchanged;
- adds no separate pawn, Harmony dependency, faction change, relation change or body replacement.

## Implementation

New reusable cultural-framework utility:

- `Source/GateRimSG1/Culture/BackstorySkillOffsetUtility.cs` stores a shared XP baseline per skill, synchronizes XP earned under the currently active identity and reapplies only the target backstory offsets.

Extended persistent data:

- `Source/GateRimSG1/Goauld/GoauldSymbioteData.cs` stores the active personality, exact host-name structure and shared skill-progress records;
- the existing host and symbiote backstories remain the authoritative identity records;
- old `0.3.10-dev` saves default safely to the host personality and initialize missing switch data on first use.

Player interface:

- `Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs` exposes one short gizmo only while `Pawn.IsColonistPlayerControlled` is true;
- the gizmo target is the inactive identity;
- the health description now also reports the active personality;
- switching marks colonist and pawn-table interfaces dirty and shows a short RP message.

## Skill model

The skill model is:

`effective skill = shared progression + active backstory offset`

Before every switch, the framework compares the pawn's current raw XP state with the state last applied by the framework. The difference is merged into the shared progression. It then applies the target identity's backstory offsets once.

This must preserve:

- passions;
- gene aptitudes;
- XP and levels earned during play;
- skill decay;
- the randomized skill baseline created with the pawn.

Repeated switching must not stack, duplicate or erase backstory bonuses.

## Save migration and compatibility

- Existing `0.3.0-dev` or later saves remain the compatibility baseline.
- Existing `0.3.10-dev` Tok'ra identities load with the host active by default.
- Missing exact host-name components are captured from the current host while the host identity is active.
- Existing symbiote names and cultural backstories are retained.
- The shared skill baseline is created only on the first personality switch.
- A save made with the symbiote active must reload with the symbiote name, backstories and offsets still active.
- Extraction or Hediff removal restores the host identity before the symbiote data leaves the body.
- Reimplanting the same symbiote into a new host resets host-specific switch state while preserving the symbiote identity.
- AI-managed Tok'ra keep the classic interface and behavior.

## Files changed

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `Source/GateRimSG1/Culture/BackstorySkillOffsetUtility.cs`;
- `Source/GateRimSG1/Goauld/GoauldSymbioteData.cs`;
- `Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs`;
- `Languages/English/Keyed/SG1_TokraDualIdentity.xml`;
- `Languages/French/Keyed/SG1_TokraDualIdentity.xml`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`;
- `docs/wiki/Tokra-Dual-Identity.md`;
- `docs/wiki/Home.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/CHANGELOG.md`.

The separate wiki must be synchronized when the milestone is published.

## Validation status

Functional validation completed for `0.3.11-dev-r2`:

- metadata versions are `0.3.11-dev` and `0.3.11.0`;
- the first gizmo click succeeds without assigning a missing Tok'ra childhood to the vanilla story tracker;
- the host childhood remains active while the Tok'ra adulthood and symbiote name become active;
- switching back restores the exact host identity;
- ten repeated host / symbiote cycles produce no skill drift, stacking or cumulative bonuses;
- XP and levels earned under either personality remain part of the shared progression;
- save/load is validated with both the host and the symbiote active;
- extraction while the symbiote is active restores the host before removal;
- reimplanting the same symbiote preserves its identity without transferring the previous host's skill state;
- the gizmo is limited to directly controlled player Tok'ra and remains absent for AI-managed Tok'ra and Goa'uld;
- Goa'uld implantation and extraction show no regression;
- `Player.log` is clean;
- no Harmony dependency or second pawn is introduced;
- English and French translation XML files are valid;
- `About/ModIcon.png` remains unchanged.

The milestone is ready for commit, branch publication, the unique final tag and wiki synchronization.

## Publication identifiers

- commit: `0.3.11-dev - add player-controlled Tok'ra personality switching`;
- branch: `feature/tokra-personality-switching`;
- annotated tag: `v0.3.11-dev`.

## Next step

Publish the validated branch, create the unique final tag `v0.3.11-dev`, synchronize the separate wiki and verify both repositories are clean.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
