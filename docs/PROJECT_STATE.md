# Project state

Current milestone: `0.3.13-dev - Generate distinct identities for pre-joined Tok'ra`.

## Active development base

- Functional base tag: `v0.3.12-dev`.
- Dedicated branch: `feature/tokra-generated-host-identities`.
- Planned final tag: `v0.3.13-dev`.
- Current local archive revision: `0.3.13-dev-r1`.

## Milestone goal

Give every Tok'ra generated directly in an already fused state a complete historical host identity that is separate from the symbiote identity.

This milestone targets `SG1_TokraVoluntaryHost` pawns generated through developer tools, incidents, visitors, faction leadership or other world-generation paths where no real implantation event existed beforehand.

Real implantations into an existing pawn remain unchanged: the pawn's current name and backstories are still captured as the host identity.

## Implemented generated-host framework

### Persistent source marker

`GoauldSymbioteData` now records a `TokraHostIdentitySource` value:

- `Unknown` for older data awaiting classification;
- `ImplantedExistingHost` for an actual implantation into an existing pawn;
- `GeneratedPreJoined` for a Tok'ra created already fused.

The system never infers the source by comparing the two displayed names.

The marker and the selected generated-host origin are deep-saved with the existing symbiote data and included in the technical debug report.

### Configurable host origins

`GeneratedHostOriginDef` configures one possible historical host culture through XML:

- selection weight;
- cultural name group;
- optional race and xenotype restrictions;
- compatible childhood pool;
- compatible adulthood pool.

`CulturalPawnProfileDef.generatedHostOrigins` lets a cultural profile expose one or more of these weighted origins. The generic resolver selects a stable origin, name and backstory pair from the persistent symbiote ID.

The initial Tok'ra profile uses `SG1_GeneratedHost_OffworldHuman`:

- Human race;
- off-world-human name generator;
- six existing off-world-human childhoods;
- six new civilian off-world-human adult careers.

The schema can later add weighted Tau'ri, Jaffa, Unas or other compatible origins without replacing the Tok'ra-specific persistence code.

### Distinct generated identities

For a new pre-joined Tok'ra:

1. the generated Tok'ra name and adulthood are preserved as the symbiote identity;
2. a stable off-world-human host name, childhood and adulthood are generated;
3. the host identity becomes active by default;
4. the existing reversible backstory-skill service applies only the difference between the generated host and symbiote backgrounds;
5. the cultural name manager later assigns or confirms the Tok'ra symbiote name without overwriting the active host name.

The generated host name is explicitly kept distinct from the stored symbiote name.

### Save migration

Older saves containing an already fused `SG1_TokraVoluntaryHost` with an unknown identity source are migrated once from the persistent symbiote ID.

Migration:

- does not rely on duplicate-name detection;
- preserves the existing symbiote name and Tok'ra adulthood;
- creates one stable generated host identity;
- does not reroll on later loads;
- refuses to convert data that already shows evidence of a real implantation or previous transfer.

### Real implantation and transfer boundary

A real implantation marks the identity source as `ImplantedExistingHost` and captures the pawn that actually received the symbiote.

When a symbiote originally generated as pre-joined is extracted and implanted into another pawn, the generated historical-host marker is replaced by the real new-host identity. The symbiote name and Tok'ra career continue to persist normally.

## Backstory addition

Six narrowly scoped adult backstories support complete off-world-human host identities:

- village steward;
- caravan guide;
- field healer;
- frontier hunter;
- settlement artisan;
- keeper of records.

They use moderate skill bonuses, the RimWorld 1.6 skill-gain map syntax and the dedicated `SG1_OffworldHumanAdulthood` category with `requiresSpawnCategory=true`. They do not alter normal human starter randomization or the existing PawnKind filters.

The total GateRim SG-1 backstory catalogue is now `58` entries. The player-facing wiki catalogue is updated in the same milestone.

## Files changed

- `About/About.xml`;
- `Source/GateRimSG1/GateRimSG1.csproj`;
- `Source/GateRimSG1/Culture/CulturalGeneratedHostIdentityUtility.cs`;
- `Source/GateRimSG1/Culture/CulturalPawnProfileDef.cs`;
- `Source/GateRimSG1/Culture/GeneratedHostOriginDef.cs`;
- `Source/GateRimSG1/Goauld/GameComponent_TokraHostPrototypeInitializer.cs`;
- `Source/GateRimSG1/Goauld/GoauldSymbioteData.cs`;
- `Source/GateRimSG1/Names/CulturalPawnNameUtility.cs`;
- `Source/GateRimSG1/Names/GameComponent_CulturalPawnNameManager.cs`;
- `1.6/Defs/BackstoryDefs/SG1_OffworldHumanAdultBackstories.xml`;
- `1.6/Defs/GeneratedHostOriginDefs/SG1_GeneratedHostOrigins.xml`;
- `1.6/Patches/SG1_TokraGeneratedHostOrigins.xml`;
- `Languages/English/Keyed/SG1_GeneratedHostIdentities.xml`;
- `Languages/French/DefInjected/BackstoryDef/SG1_OffworldHumanAdultBackstories.xml`;
- `Languages/French/Keyed/SG1_GeneratedHostIdentities.xml`;
- `docs/CULTURAL_BACKSTORIES.md`;
- `docs/CULTURAL_FRAMEWORK.md`;
- `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`;
- `docs/wiki/Cultural-Backstories.md`;
- `docs/wiki/Tokra-Dual-Identity.md`;
- `docs/wiki/Home.md`;
- `docs/PROJECT_STATE.md`;
- `docs/ROADMAP.md`;
- `docs/TESTING.md`;
- `docs/TESTING_CURRENT.md`;
- `docs/CHANGELOG.md`.

The separate wiki must be synchronized when the milestone is published.

## Validation status

Static preparation completed:

- mod metadata version prepared as `0.3.13-dev`;
- assembly version prepared as `0.3.13.0`;
- custom Def XML, patches and French translations prepared;
- generated-host source marker and stable selection logic added;
- name-manager sequencing corrected so the host remains visibly active;
- documentation and wiki catalogue updated.

A forced local rebuild and the focused RimWorld tests in `docs/TESTING_CURRENT.md` remain required.

## Publication identifiers

- commit: `0.3.13-dev - generate distinct pre-joined Tok'ra host identities`;
- branch: `feature/tokra-generated-host-identities`;
- annotated tag: `v0.3.13-dev`.

## Next step

Extract `0.3.13-dev-r1`, force a rebuild, then validate new generated Tok'ra, old-save migration, real implantation boundaries, extraction/reimplantation and `Player.log`.

## Files intentionally removed

None.

## Repository rules reminder

- Extract the archive at the repository root.
- Preserve `About/ModIcon.png`.
- Do not commit root ZIP archives.
- Publish only the final tag without an `-rN` suffix.
- Synchronize the separate wiki because `docs/wiki/*.md` changes are included.
