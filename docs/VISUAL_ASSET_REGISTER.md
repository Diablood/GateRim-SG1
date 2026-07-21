# Visual asset register

## Milestone

- Version: `0.3.104-dev`
- Target assembly: `0.3.104.0`
- Status: `0.3.104-dev` adds and validates the shared directional adult Goa'uld/Tok'ra symbiote family.

## Purpose

This document is the authoritative inventory of GateRim SG-1 visual assets.
Every local texture family is represented exactly once inside the marked table.
Directional and body-type files are counted as physical files belonging to one
canonical family. Branch names and local revision numbers belong in
`docs/PROJECT_STATE.md`, not in this durable register.

The companion command `./tools/check-visual-assets.cmd` verifies the real PNG
files, direct XML and C# references, protected wiki copies and the exact final
family whitelist. `./tools/check-documentation-consistency.cmd` verifies the
metadata and all summary counts against the marked table.

## Approved final references

- `About/ModIcon.png`: `final` public mod identity. It is intentionally outside
  the `Textures/` family count and must never be reused as gameplay art.
- Accepted final local families: `56` (`56` validated families).
- Every other local family remains temporary unless its table row is explicitly
  marked `final` and `done`.

## Classification

| Status | Meaning |
|---|---|
| `final` | Dedicated custom art accepted after real-surface review. |
| `temporary-original` | Functional custom art still below the definitive-art target. |
| `temporary-recolor` | Color variant derived from another temporary family. |
| `temporary-reuse` | One image intentionally represents several distinct concepts. |
| `placeholder-personal-icon` | The public demon icon is reused as gameplay art. |
| `accepted-vanilla` | Vanilla art is semantically appropriate and may remain. |
| `placeholder-vanilla` | Vanilla art is functional but still generic. |

| Priority | Meaning |
|---|---|
| `P0` | Misleading or highly visible placeholder. |
| `P1` | Prominent temporary art. |
| `P2` | Visible development art. |
| `P3` | Low-priority polish or accepted generic dependency. |
| `done` | Accepted final art. |

## Audit summary

- Local PNG files: `617`.
- Local texture families: `82`.
- Accepted final local families: `56` (`56` validated families).
- Temporary original families: `14`.
- Temporary recolor families: `11`.
- Temporary reuse families: `1`.
- Project-icon placeholder families: `0`.
- Priorities: `0` P0, `18` P1, `8` P2, `56` done.
- Direct external texture paths: `3` registered string paths.
- Runtime vanilla icon constants: `2`.
- Missing referenced local texture families: `0`.
- Unreferenced local texture families: `0`.

## Highest-priority findings

1. The Goa'uld queen still uses the former single-image adult placeholder and
   requires its own later directional visual family.
2. Jaffa ground and inventory icons are finalized where covered by
   `0.3.102-dev`, but visible directional worn variants remain temporary
   alongside Tok'ra and SGC apparel.
3. Remaining `P2` work is concentrated in deferred apparel recolors and other
   temporary visual families.

## Local texture inventory

<!-- LOCAL_ASSET_TABLE_START -->
| Canonical path under `Textures/` | Files | Base size | Surface | Status | Priority | Referenced by | Audit note |
|---|---:|---:|---|---|---|---|---|
| `Storytellers/SG1_Command` | 1 | variable | Storyteller UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `Storytellers/SG1_Command_Tiny` | 1 | variable | Storyteller UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/SG1_GoauldRitualBasin` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/SG1_PrimtaIncubationBasin` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/SG1_PrimtaPreservationBasin` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/SG1_TokraRelaySabotageDevice` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/SG1_TokraSecureCommunicator` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/Equipment/WeaponMelee/SG1_JaffaKnife` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved outlined Jaffa knife ground, inventory and equipped family. |
| `Things/Item/Equipment/WeaponRanged/SG1_Bolas` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved reusable bolas ground/inventory family. |
| `Things/Item/Equipment/WeaponRanged/SG1_MatokStaff` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved Ma'Tok staff ground/inventory family. |
| `Things/Item/Equipment/WeaponRanged/SG1_TokraHypodermicRifle` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/Equipment/WeaponRanged/SG1_ZatnikTel` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved Zat'nik'tel ground/inventory family. |
| `Things/Item/SG1_ImmaturePrimtaSymbiote` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_PrimtaLarva` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_TokraIntroductionArtifact` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_TokraMissionIntelPacket` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_TokraObservationDevice` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_TokraOrganicDeadDrop` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Item/SG1_TretoninDose` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Pawn/Animal/SG1_AdultSymbiote/SG1_AdultSymbiote` | 3 | 128×128 | Pawn | `final` | `done` | Registered runtime reference | Approved shared directional adult Goa'uld/Tok'ra family. |
| `Things/Pawn/Animal/SG1_GoauldSymbiote/SG1_GoauldSymbiote` | 1 | 128×128 | Pawn | `temporary-reuse` | `P1` | Registered runtime reference | Former adult image retained temporarily for the deferred Goa'uld queen visual. |
| `Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Pawn/Humanlike/Apparel/JaffaArmorBelt/JaffaArmorBelt` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved non-directional Jaffa armor-belt family. |
| `Things/Pawn/Humanlike/Apparel/JaffaPants/JaffaPants` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved non-directional Jaffa trousers family. |
| `Things/Pawn/Humanlike/Apparel/JaffaUnderArmor/JaffaUnderArmor` | 1 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon finalized; directional worn art remains deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaDeployedHelmet/JaffaDeployedHelmet` | 5 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon finalized in `0.3.102-dev`; directional worn files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaGauntlets/JaffaGauntlets` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon and reduced draw size finalized; directional files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaHeavyArmor/JaffaHeavyArmor` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon finalized in `0.3.102-dev`; directional worn files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaLightArmor/JaffaLightArmor` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon finalized in `0.3.102-dev`; directional worn files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerArmor/JaffaOfficerArmor` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P1` | Registered runtime reference | Officer ground/inventory icon finalized with red rank accents; directional worn files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerDeployedHelmet/JaffaOfficerDeployedHelmet` | 5 | 128×128 | Map/item/pawn | `temporary-recolor` | `P1` | Registered runtime reference | Officer ground/inventory icon finalized with red rank accents; directional worn files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerRetractedHelmet/JaffaOfficerRetractedHelmet` | 5 | 128×128 | Map/item/pawn | `temporary-recolor` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/JaffaReinforcedBoots/JaffaReinforcedBoots` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Ground/inventory icon and reduced draw size finalized; directional files remain deferred. |
| `Things/Pawn/Humanlike/Apparel/JaffaRetractedHelmet/JaffaRetractedHelmet` | 5 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh` | 1 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalBoots/SGTacticalBoots` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalGloves/SGTacticalGloves` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalVest/SGTacticalVest` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldCap/SGTeamFieldCap` | 5 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldHelmet/SGTeamFieldHelmet` | 5 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketBlack/SGTeamJacketBlack` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketDesert/SGTeamJacketDesert` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketOlive/SGTeamJacketOlive` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsBlack/SGTeamPantsBlack` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsDesert/SGTeamPantsDesert` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsOlive/SGTeamPantsOlive` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniform/SGTeamUniform` | 29 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformBlack/SGTeamUniformBlack` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformDesert/SGTeamUniformDesert` | 29 | 128×128 | Map/item/pawn | `temporary-recolor` | `P2` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/Apparel/TokraFieldGarb/TokraFieldGarb` | 25 | 128×128 | Map/item/pawn | `temporary-original` | `P1` | Registered runtime reference | Registered temporary family. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark` | 4 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark` | 4 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark` | 4 | 128×128 | Map/item/pawn | `final` | `done` | Registered runtime reference | Approved final family. |
| `Things/Projectile/SG1_BolasProjectile` | 1 | 128×128 | Projectile | `final` | `done` | Registered runtime reference | Approved bolas projectile family. |
| `Things/Projectile/SG1_MatokBlast` | 1 | 128×128 | Projectile | `final` | `done` | Registered runtime reference | Approved Ma'Tok energy-bolt family. |
| `Things/Projectile/SG1_TokraHypodermicDart` | 1 | 128×128 | Projectile | `final` | `done` | Registered runtime reference | Approved Tok'ra non-lethal energy-dart family. |
| `Things/Projectile/SG1_ZatnikTelBlast` | 1 | 128×128 | Projectile | `final` | `done` | Registered runtime reference | Approved Zat'nik'tel blast family. |
| `UI/Commands/SG1_AutonomousHunt` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Commands/SG1_EmergencyExtraction` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Commands/SG1_ForcedImplantation` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Commands/SG1_JaffaHelmetMode` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family; manual deploy/retract action. |
| `UI/Commands/SG1_RitualImplantation` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_GoauldLongevity` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_JaffaLineage` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_JaffaPhysiology` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_JaffaPouchPotential` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_JaffaSymbioteCompatibility` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Genes/SG1_NaquadahBlood` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Xenotypes/SG1_GoauldHost` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `UI/Xenotypes/SG1_Jaffa` | 1 | 64×64 | UI | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/SG1_FreeJaffa` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/SG1_GoauldSystemLords` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/SG1_SGCExpedition` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/SG1_Tokra` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` | 1 | 128×128 | World | `final` | `done` | Registered runtime reference | Approved final family. |
<!-- LOCAL_ASSET_TABLE_END -->

## Direct vanilla texture dependencies

<!-- EXTERNAL_ASSET_TABLE_START -->
| External path | Surface | Status | Priority | Referenced by | Audit note |
|---|---|---|---|---|---|
| `World/WorldObjects/DefaultSettlement` | World settlement | `accepted-vanilla` | `P3` | Free Jaffa, Goa'uld and SGC factions | Dedicated faction icons carry identity. |
| `World/WorldObjects/Sites/GenericSite` | World site base | `accepted-vanilla` | `P3` | GateRim temporary sites | Expanding site icons remain dedicated. |
| `UI/Icons/Study` | Command UI | `accepted-vanilla` | `P3` | SG1_TokraIntroductionArtifact | Appropriate vanilla study action. |
<!-- EXTERNAL_ASSET_TABLE_END -->

## Runtime vanilla icon constants

| Runtime icon | Usage | Status | Priority | Audit note |
|---|---|---|---|---|
| `TexCommand.Attack` | Kara kesh attack abilities | `accepted-vanilla` | `P3` | Generic attack affordance remains appropriate. |
| `TexCommand.GatherSpotActive` | Tok'ra identity and symbiote controls | `placeholder-vanilla` | `P2` | Functional but generic. |

## Repository-level public icon

| Path | Status | Contract |
|---|---|---|
| `About/ModIcon.png` | `final` | Preserve Diablood's red-and-black demon icon; never reuse it as gameplay art. |

## Stable-path contract for later art lots

- Replace PNG contents in place whenever the Def and rendering contract are stable.
- Do not rename Defs or texture paths merely to improve art.
- Preserve directional and body-type filenames only for renderers that require them.
- A unique gizmo, event icon, weapon or inert item does not gain directional files
  unless its actual renderer requires directional variants.
- Update this register and run both consistency checks whenever a texture is added,
  removed, renamed, reclassified or newly referenced.
- Move a family to `final` only after its real map, inventory, pawn, world or UI
  presentation has been reviewed.

## Validation procedure

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-documentation-consistency.cmd
./tools/check-project-consistency.cmd
git diff --check
```

Published `0.3.104-dev` visual baseline:

```text
Final local texture families: 56
Local PNG files: 617
Local texture families: 82
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
