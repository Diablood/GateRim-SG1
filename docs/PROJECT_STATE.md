# Project state

Current milestone: `0.3.12-dev - Audit Tok'ra active identity integration`.

## Active development base

- Functional base tag: `v0.3.11-dev`.
- Dedicated branch: `feature/tokra-active-identity-integration`.
- Planned final tag: `v0.3.12-dev`.
- Current local archive revision: `0.3.12-dev-r2`.

## Milestone goal

Audit how the active Tok'ra identity introduced in `0.3.11-dev` propagates through vanilla interfaces and correct only confirmed integration gaps without changing the validated shared-skill model.

The confirmed integration gap concerned caravans: `Pawn.IsColonistPlayerControlled` requires a spawned pawn, so a permanent player colonist travelling on the world map lost the map-only Hediff gizmo even though the caravan remained directly controlled by the player.

## Implemented integration

### Shared control boundary

`Source/GateRimSG1/Goauld/TokraPlayerControlUtility.cs` centralizes the eligibility rule:

- a spawned colon must satisfy the existing vanilla `IsColonistPlayerControlled` rule;
- a travelling pawn must belong to and be an owner of a player-controlled caravan;
- dead pawns and pawns in a mental state are excluded;
- guests, prisoners, slaves, visitors, allies and uncontrolled quest pawns remain excluded;
- the pawn must carry persistent Tok'ra symbiote data.

### Caravan interface

`Source/GateRimSG1/Goauld/WorldObjectComp_TokraCaravanIdentity.cs` adds one compact `Identités Tok'ra` command to a selected player caravan when at least one eligible Tok'ra is present.

The command opens a menu containing one action per eligible Tok'ra and delegates the switch to the same `HediffComp_GoauldSymbiote.TryTogglePersonality()` method used on a map. No duplicate identity or skill logic is introduced.

`1.6/Patches/SG1_TokraCaravanIdentity.xml` attaches the new world-object component to the vanilla `Caravan` definition without Harmony.

### Existing map behavior

`Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs` now consumes the shared eligibility utility and exposes a safe public switch method for the caravan component. The ordinary pawn gizmo, dual-identity health summary, save data and skill model remain unchanged.

## Audit results

The focused tests validate:

- map → caravan → map switching without skill stacking, XP loss or identity reroll;
- one grouped caravan gizmo with one action per eligible Tok'ra;
- unchanged exclusion of guests, prisoners, slaves, AI-managed Tok'ra and unrecruited quest pawns;
- coherent Bio, Social, Health and caravan information for the active personality;
- unchanged relations and persistent dual-identity health summary;
- save/load, extraction and the `0.3.11-dev` shared-skill model without regression;
- a clean `Player.log`.

Death, corpse, grave and resurrection remain a deferred compatibility audit because no dedicated correction was required for the validated caravan integration.

## Distinct generated-host identity gap

An exploratory developer spawn exposed a separate generation problem:

- `Spawn pawn > SG1_TokraVoluntaryHost` creates a Tok'ra already fused before any historical implantation exists;
- the current initializer can therefore record the same generated Tok'ra name for both the host and the symbiote while only the adulthood changes;
- this is not an interface or caravan regression and is intentionally not patched in `0.3.12-dev`.

The future fix must not infer the problem by comparing names. A persistent identity-source marker must distinguish:

- a real implantation into an already existing host;
- a pawn generated directly as a pre-joined Tok'ra.

For a pre-joined Tok'ra, the cultural framework must generate a separate host identity from configurable weighted origin profiles. The current default should be an off-world human host, without assuming Tau'ri origin, and the design must remain extensible to Tau'ri, Jaffa, Unas and other compatible hosts. The symbiote keeps its own Tok'ra identity. Existing real-implantation flows remain unchanged.

## Files changed

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `Source/GateRimSG1/Goauld/HediffComp_GoauldSymbiote.cs`;
- `Source/GateRimSG1/Goauld/TokraPlayerControlUtility.cs`;
- `Source/GateRimSG1/Goauld/WorldObjectComp_TokraCaravanIdentity.cs`;
- `1.6/Patches/SG1_TokraCaravanIdentity.xml`;
- `Languages/English/Keyed/SG1_TokraIdentityIntegration.xml`;
- `Languages/French/Keyed/SG1_TokraIdentityIntegration.xml`;
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

`0.3.12-dev-r1` is functionally validated:

- forced rebuild completed with assembly version `0.3.12.0`;
- focused map and caravan tests passed;
- interface and player-control boundaries passed;
- no regression was observed in personality switching, shared skills, save/load or extraction;
- `Player.log` is clean.

`0.3.12-dev-r2` only records the validated result and the deferred generated-host design. No new build or in-game test is required after extracting `r2`.

## Publication identifiers

- commit: `0.3.12-dev - integrate Tok'ra active identity interfaces`;
- branch: `feature/tokra-active-identity-integration`;
- annotated tag: `v0.3.12-dev`.

## Next step

Publish the validated branch and final tag, synchronize the wiki, then start a dedicated milestone for distinct host identities on pre-joined generated Tok'ra.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
