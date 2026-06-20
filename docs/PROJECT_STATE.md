# Project state

Current milestone: `0.3.10-dev - Preserve implanted Tok'ra identity`.

## Active development base

- Functional base tag: `v0.3.9-dev`.
- Dedicated branch: `feature/tokra-identity-persistence`.
- Planned final tag: `v0.3.10-dev`.
- Current local archive revision: `0.3.10-dev-r2`.

## Milestone goal

Preserve and expose the two identities carried by a player-controlled Tok'ra host without yet implementing personality switching.

The milestone:

- extends the shared cultural profile schema with configurable persistent-identity childhood and adulthood pools;
- configures the existing Tok'ra profile with the six current Tok'ra adult careers;
- assigns one deterministic Tok'ra career to each persistent symbiote identity;
- stores the host name, host childhood and host adulthood when the symbiote attaches to a host;
- stores the symbiote name and configured cultural backstories in the existing `GoauldSymbioteData` object;
- transfers those records unchanged between free symbiote, recent implantation, active host and extraction states;
- displays both names and both recorded backgrounds in the health description of a directly player-controlled Tok'ra host;
- retains the previous technical identity report when advanced debug information is enabled;
- keeps AI-managed Tok'ra, visitors, allies, enemies and uncontrolled quest pawns on the existing classic behavior;
- does not change the pawn's active name, active backstories, skills, relations, faction, body or equipment;
- leaves the personality-switch gizmo and backstory-derived skill switching for a later dedicated milestone.

## Framework implementation

New framework consumer:

- `Source/GateRimSG1/Culture/CulturalIdentityUtility.cs` selects a stable persistent backstory from the resolved cultural profile and a saved identity key.

Extended framework files:

- `Source/GateRimSG1/Culture/CulturalPawnProfileDef.cs` exposes `identityChildhoods` and `identityAdulthoods`;
- `Source/GateRimSG1/Culture/CulturalProfileResolver.cs` resolves the highest-priority identity profile for a cultural name group;
- `1.6/Patches/SG1_TokraIdentityProfiles.xml` configures the six current Tok'ra careers without duplicating culture-specific C# conditions.

Persistent symbiote files:

- `Source/GateRimSG1/Goauld/GoauldSymbioteData.cs` stores host and symbiote backstory references alongside the existing names and host history;
- `Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs` exposes the RP summary only for directly player-controlled Tok'ra hosts.

## Save migration and compatibility

- Existing `0.3.0-dev` or later saves remain the compatibility baseline.
- Existing symbiote IDs and names are retained.
- On the first load with `0.3.10-dev`, missing Tok'ra career data is generated deterministically from the existing symbiote ID and then saved normally.
- Missing host backstories are captured from the current host without changing the pawn.
- Repeated loads do not add skills or replace active backstories.
- Goa'uld hosts keep their current display and behavior because no Goa'uld persistent-identity pool is configured in this milestone.
- AI-managed Tok'ra may carry the identity data internally for extraction and later implantation, but receive no new player interface or gizmo.
- Temporary guests and quest pawns remain excluded because the player-facing summary requires `IsColonistPlayerControlled`.

## Files changed

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `Source/GateRimSG1/Culture/CulturalPawnProfileDef.cs`;
- `Source/GateRimSG1/Culture/CulturalProfileResolver.cs`;
- `Source/GateRimSG1/Culture/CulturalIdentityUtility.cs`;
- `Source/GateRimSG1/Goauld/GoauldSymbioteData.cs`;
- `Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs`;
- `1.6/Patches/SG1_TokraIdentityProfiles.xml`;
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

Local build and focused in-game validation completed successfully on `0.3.10-dev-r1`:

- metadata versions are `0.3.10-dev` and `0.3.10.0`;
- the Tok'ra profile patch loads the six current adult careers without XML or cross-reference errors;
- voluntary implantation preserves the host's active name, backstories and skills;
- the symbiote name and Tok'ra career remain stable through recent implantation, active-host conversion, save/load, extraction and reimplantation;
- an older compatible save initializes missing identity data once without later rerolls;
- the detailed dual-identity summary is exposed only to directly player-controlled Tok'ra colonists;
- AI-managed Tok'ra keep the classic behavior and interface;
- Goa'uld implantation, active-host conversion and extraction show no regression;
- `Player.log` is clean for the tested scope;
- no Harmony dependency or new pawn component is introduced;
- `About/ModIcon.png` remains unchanged.

The active test record is in `docs/TESTING_CURRENT.md`. No further gameplay change is required before publication.

## Publication identifiers

- commit: `0.3.10-dev - preserve implanted Tok'ra identity`;
- branch: `feature/tokra-identity-persistence`;
- annotated tag: `v0.3.10-dev`.

## Next step

Publish the validated branch, create the final annotated tag `v0.3.10-dev`, synchronize the separate wiki and verify both repositories are clean.

After publication, a separate milestone may prototype the player-only personality-switch gizmo and safe backstory-derived skill offsets described in `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
