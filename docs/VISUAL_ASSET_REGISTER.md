# Visual asset register

## Milestone

- Version: `0.3.98-dev`
- Branch: `feature/final-tokra-mission-object-visuals`
- Target assembly: `0.3.98.0`
- Status: `0.3.95-dev` Tok'ra mission-object visual lot validated in game; documentary finalization prepares publication.

## Purpose

This document is the authoritative inventory of GateRim SG-1 visual assets. It
records every local texture family, direct vanilla texture dependency and current
art status. Paths remain stable unless a later milestone proves that a technical
rename is required.

The companion command `./tools/check-visual-assets.cmd` verifies that this
register and the repository remain synchronized. `0.3.95-dev` replaces the
remaining temporary Tok'ra mission-object reuses and presentation prototypes
with dedicated validated art while preserving established Def names and
serialized gameplay identities.

## Approved final references

- `About/ModIcon.png`: `final` public mod identity. It is intentionally outside
  the `Textures/` family count and must never be reused as gameplay art.
- The `Textures/` families accepted as `final` are the two storyteller
  portraits, six buildings, four Tok'ra mission-item families, two hand devices,
  two xenotype icons, six gameplay-gene icons, three intrinsic Jaffa
  forehead-mark overlays, four command icons, four world-faction icons and seven
  world-event site icons.
- Every other equipment, building, pawn overlay, projectile, pawn or command
  family remains temporary unless explicitly listed below.

Validated hand-device families:

- `Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh`
- `Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet`

Validated building families:

- `Things/Building/SG1_GoauldRitualBasin`
- `Things/Building/SG1_PrimtaIncubationBasin`
- `Things/Building/SG1_PrimtaPreservationBasin`
- `Things/Building/SG1_TokraRelaySabotageDevice`
| `Things/Building/SG1_TokraRelaySabotageDevice` | 1 | 128×128 | Map/building | `final` | `done` | SG1_TokraRelaySabotageDevice | Dedicated top-down Goa'uld relay control node validated in game. |
| `Things/Building/SG1_TokraSecureCommunicator` | 1 | 128×128 | Map/building | `final` | `done` | SG1_TokraSecureCommunicator | Dedicated silver-grey Tok'ra secure communication station with cyan crystal core, validated in game. |
| `Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot` | 1 | 128×128 | Ground marker | `final` | `done` | SG1_TokraDeliveryDropSpot | Thick black-outlined priority delivery marker with integrated light transparency, validated in game. |

Validated Tok'ra mission-item families:

- `Things/Item/SG1_TokraIntroductionArtifact`
| `Things/Item/SG1_TokraIntroductionArtifact` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraIntroductionArtifact | Dedicated sealed Tok'ra cipher module validated in game. |
| `Things/Item/SG1_TokraMissionIntelPacket` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraMissionIntelPacket | Dedicated open Tok'ra intelligence coffer validated in game. |
| `Things/Item/SG1_TokraObservationDevice` | 1 | 128×128 | Map/inventory and deployed map object | `final` | `done` | SG1_TokraObservationDevice, SG1_TokraObservationPoint | Dedicated observation device validated as a portable item and intentionally reused by the deployed observation point. |
| `Things/Item/SG1_TokraOrganicDeadDrop` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraOrganicDeadDrop | Dedicated organic dead-drop pod validated in game; remains an Item-category mission object. |
- `Things/Item/SG1_TokraObservationDevice`
- `Things/Item/SG1_TokraOrganicDeadDrop`

`SG1_TokraObservationPoint` intentionally reuses the validated
`Things/Item/SG1_TokraObservationDevice` family and has no separate local
texture family.

Validated storyteller families:

- `Storytellers/SG1_Command`
- `Storytellers/SG1_Command_Tiny`

Validated xenotype families:

- `UI/Xenotypes/SG1_GoauldHost`
- `UI/Xenotypes/SG1_Jaffa`

Validated gameplay-gene families:

- `UI/Genes/SG1_GoauldLongevity`
- `UI/Genes/SG1_JaffaLineage`
- `UI/Genes/SG1_JaffaPhysiology`
- `UI/Genes/SG1_JaffaPouchPotential`
- `UI/Genes/SG1_JaffaSymbioteCompatibility`
- `UI/Genes/SG1_NaquadahBlood`

Validated intrinsic Jaffa forehead-mark overlay families:

- `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark`
- `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark`
- `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark`

Validated command families:

- `UI/Commands/SG1_AutonomousHunt`
- `UI/Commands/SG1_EmergencyExtraction`
- `UI/Commands/SG1_ForcedImplantation`
- `UI/Commands/SG1_RitualImplantation`

Validated world-faction families:

- `World/WorldObjects/Expanding/SG1_FreeJaffa`
- `World/WorldObjects/Expanding/SG1_GoauldSystemLords`
- `World/WorldObjects/Expanding/SG1_SGCExpedition`
- `World/WorldObjects/Expanding/SG1_Tokra`

Validated world-event site families:

Every texture family under `Textures/World/WorldObjects/Expanding/Sites` is
maintainer-validated, final and excluded from future replacement lots.

- `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective`
- `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield`
- `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage`
- `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition`
- `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact`
- `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal`
- `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous`

## Classification

| Status | Meaning |
|---|---|
| `final` | Dedicated custom art already accepted; no replacement is scheduled. |
| `temporary-original` | Custom GateRim art that works but remains below the definitive-art target. |
| `temporary-recolor` | A color variant derived from another temporary family. |
| `temporary-reuse` | One asset is intentionally standing in for a different object or concept. |
| `placeholder-personal-icon` | The project demon icon is reused as a visible gameplay placeholder. |
| `accepted-vanilla` | Vanilla art is semantically appropriate and may remain. |
| `placeholder-vanilla` | Vanilla art is functional but does not provide the required GateRim identity. |

| Priority | Meaning |
|---|---|
| `P0` | Misleading or highly visible placeholder; replace first. |
| `P1` | Prominent temporary art; replace before presentation closure. |
| `P2` | Visible development art; replace after P0/P1. |
| `P3` | Low-priority polish or accepted generic dependency. |
| `done` | Accepted final art. |

## Audit summary

- Local PNG files: `610`.
- Local texture families: `77`.
- Accepted final local families: `44` (`2` storyteller portraits, `6`
  buildings, `4` Tok'ra mission-item families, `2` hand devices, `2` xenotype
  icons, `6` gameplay-gene icons, `3` intrinsic Jaffa forehead-mark overlays,
  `4` command icons, `4` world-faction icons and `7` world-event site icons).
- Temporary original families: `22`.
- Temporary recolor families: `11`.
- Temporary reuse families: `1`.
- Project-icon placeholder families: `0`.
- Priorities: `0` P0, `17` P1, `16` P2, `44` done.
- Direct external texture paths: `3` registered string paths.
- Runtime vanilla icon constants: `2`.
- Missing referenced local texture families: `0`.
- Unreferenced local texture families: `0`.

## Highest-priority findings

1. The Prim'ta larva and free Goa'uld symbiote still use the same image.
2. The free Goa'uld, Tok'ra and queen symbiote forms still share one pawn image.
3. Jaffa, officer, Tok'ra and SGC apparel are technically complete but remain
   temporary art families; many body-type variants are exact copies rather than
   tailored silhouettes.

## Local texture inventory

The file count includes directional and body-type variants belonging to the
same canonical family. Representative dimensions refer to the base image.

<!-- LOCAL_ASSET_TABLE_START -->
| Canonical path under `Textures/` | Files | Base size | Surface | Status | Priority | Referenced by | Audit note |
|---|---:|---:|---|---|---|---|---|
| `Storytellers/SG1_Command` | 1 | 560×600 | Storyteller UI | `final` | `done` | SG1_GateRimStoryteller | Maintainer-provided transparent large portrait validated in the real storyteller-selection interface. |
| `Storytellers/SG1_Command_Tiny` | 1 | 122×130 | Storyteller UI | `final` | `done` | SG1_GateRimStoryteller | Dedicated close crop validated on small storyteller surfaces. |
| `Things/Building/SG1_GoauldRitualBasin` | 1 | 256×256 | Map/building | `final` | `done` | SG1_GoauldRitualBasin, SG1_GoauldRitualBasinLarge | Validated ceremonial platform shared by the `2×2` and `3×3` placement variants. |
| `Things/Building/SG1_PrimtaIncubationBasin` | 1 | 128×128 | Map/building | `final` | `done` | SG1_PrimtaIncubationBasin | Validated green one-cell incubation basin. |
| `Things/Building/SG1_PrimtaPreservationBasin` | 1 | 128×128 | Map/building | `final` | `done` | SG1_PrimtaPreservationBasin | Validated blue one-cell preservation storage. |
| `Things/Building/SG1_TokraRelaySabotageDevice` | 1 | 128×128 | Map/building | `final` | `done` | SG1_TokraRelaySabotageDevice | Dedicated top-down Goa'uld relay control node validated in game. |
| `Things/Building/SG1_TokraSecureCommunicator` | 1 | 128×128 | Map/building | `final` | `done` | SG1_TokraSecureCommunicator | Dedicated silver-grey Tok'ra secure communication station with cyan crystal core, validated in game. |
| `Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot` | 1 | 128×128 | Ground marker | `final` | `done` | SG1_TokraDeliveryDropSpot | Thick black-outlined priority delivery marker with integrated light transparency, validated in game. |
| `Things/Item/Equipment/WeaponRanged/SG1_Bolas` | 1 | 128×128 | Map/inventory weapon | `temporary-original` | `P2` | SG1_Bolas | Functional custom art pending final presentation. |
| `Things/Item/Equipment/WeaponRanged/SG1_MatokStaff` | 1 | 128×48 | Map/inventory weapon | `temporary-original` | `P2` | SG1_MatokStaff | Functional custom art pending final presentation. |
| `Things/Item/Equipment/WeaponRanged/SG1_TokraHypodermicRifle` | 1 | 128×128 | Equipped/map/inventory weapon | `final` | `done` | SG1_TokraHypodermicRifle | Simplified horizontal high-contrast Tok'ra capture rifle, validated equipped and on the ground under Camera+ zoom. |
| `Things/Item/Equipment/WeaponRanged/SG1_ZatnikTel` | 1 | 128×128 | Map/inventory weapon | `temporary-original` | `P2` | SG1_ZatnikTel | Functional custom art pending final presentation. |
| `Things/Item/SG1_ImmaturePrimtaSymbiote` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_ImmaturePrimtaSymbiote | Dedicated pale curled pre-larval symbiote, validated in game after correcting its XML texture path. |
| `Things/Item/SG1_PrimtaLarva` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_PrimtaLarva | Dedicated pale elongated implantable Prim'ta larva, validated in game. |
| `Things/Item/SG1_TokraIntroductionArtifact` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraIntroductionArtifact | Dedicated sealed Tok'ra cipher module validated in game. |
| `Things/Item/SG1_TokraMissionIntelPacket` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraMissionIntelPacket | Dedicated open Tok'ra intelligence coffer validated in game. |
| `Things/Item/SG1_TokraObservationDevice` | 1 | 128×128 | Map/inventory and deployed map object | `final` | `done` | SG1_TokraObservationDevice, SG1_TokraObservationPoint | Dedicated observation device validated as a portable item and intentionally reused by the deployed observation point. |
| `Things/Item/SG1_TokraOrganicDeadDrop` | 1 | 128×128 | Map/inventory item | `final` | `done` | SG1_TokraOrganicDeadDrop | Dedicated organic dead-drop pod validated in game; remains an Item-category mission object. |
| `Things/Item/SG1_TretoninDose` | 1 | 128×128 | Map/inventory medical item | `final` | `done` | SG1_TretoninDose | Maintainer-approved transparent cyan medical ampoule with stronger outline, validated in game. |
| `Things/Pawn/Animal/SG1_GoauldSymbiote/SG1_GoauldSymbiote` | 1 | 128×128 | Pawn/map | `temporary-reuse` | `P1` | SG1_GoauldQueen, SG1_GoauldSymbiote, SG1_TokraSymbiote | Shared image between biologically distinct forms. |
| `Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet` | 1 | 128×128 | Pawn apparel item | `final` | `done` | SG1_GoauldHealingBracelet | Approved gold-and-silver healing bracelet with orange luminous core, validated on all item surfaces. |
| `Things/Pawn/Humanlike/Apparel/JaffaDeployedHelmet/JaffaDeployedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaDeployedHelmet | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/JaffaGauntlets/JaffaGauntlets` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaGauntlets | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/JaffaHeavyArmor/JaffaHeavyArmor` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaHeavyArmor | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/JaffaLightArmor/JaffaLightArmor` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaLightArmor | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerArmor/JaffaOfficerArmor` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerArmor | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerDeployedHelmet/JaffaOfficerDeployedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerDeployedHelmet | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/JaffaOfficerRetractedHelmet/JaffaOfficerRetractedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-recolor` | `P1` | SG1_JaffaOfficerRetractedHelmet | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/JaffaReinforcedBoots/JaffaReinforcedBoots` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaReinforcedBoots | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/JaffaRetractedHelmet/JaffaRetractedHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_JaffaRetractedHelmet | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh` | 1 | 128×128 | Pawn apparel item | `final` | `done` | SG1_KaraKesh | Approved articulated bronze-and-gold hand device with red central gem, validated on all item surfaces. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalBoots/SGTacticalBoots` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalBoots | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalGloves/SGTacticalGloves` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalGloves | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTacticalVest/SGTacticalVest` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTacticalVest | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldCap/SGTeamFieldCap` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTeamFieldCap | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTeamFieldHelmet/SGTeamFieldHelmet` | 5 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_SGTeamFieldHelmet | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketBlack/SGTeamJacketBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamJacket | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketDesert/SGTeamJacketDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamJacket | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamJacketOlive/SGTeamJacketOlive` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_OliveSGTeamJacket | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsBlack/SGTeamPantsBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamPants | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsDesert/SGTeamPantsDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamPants | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamPantsOlive/SGTeamPantsOlive` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_OliveSGTeamPants | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniform/SGTeamUniform` | 29 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_GenericSGTeamUniform | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformBlack/SGTeamUniformBlack` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_BlackSGTeamUniform | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/SGTeamUniformDesert/SGTeamUniformDesert` | 29 | 128×128 | Pawn apparel | `temporary-recolor` | `P2` | SG1_DesertSGTeamUniform | Color variant derived from temporary art. |
| `Things/Pawn/Humanlike/Apparel/TokraFieldGarb/TokraFieldGarb` | 25 | 128×128 | Pawn apparel | `temporary-original` | `P1` | SG1_TokraFieldGarb | Functional custom art pending final presentation. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `final` | `done` | SG1_JaffaForeheadMark_GenericGoldIntrinsic | Validated compact gold South-only Apophis mark. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `final` | `done` | SG1_JaffaForeheadMark_GenericIntrinsic | Validated compact black South-only Apophis mark. |
| `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark` | 4 | 128×128 | Pawn overlay | `final` | `done` | SG1_JaffaForeheadMark_GenericSilverIntrinsic | Validated compact silver South-only Apophis mark. |
| `Things/Projectile/SG1_BolasProjectile` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_BolasProjectile | Functional custom art pending final presentation. |
| `Things/Projectile/SG1_MatokBlast` | 1 | 64×32 | Map projectile | `temporary-original` | `P2` | SG1_MatokStaffProjectile | Functional custom art pending final presentation. |
| `Things/Projectile/SG1_TokraHypodermicDart` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_TokraHypodermicDart | Functional custom art pending final presentation. |
| `Things/Projectile/SG1_ZatnikTelBlast` | 1 | 64×64 | Map projectile | `temporary-original` | `P2` | SG1_ZatnikTelProjectile | Functional custom art pending final presentation. |
| `UI/Commands/SG1_AutonomousHunt` | 1 | 64×64 | Command UI | `final` | `done` | C# forced-implantation component | Validated autonomous-hunt command icon. |
| `UI/Commands/SG1_EmergencyExtraction` | 1 | 64×64 | Command UI | `final` | `done` | C# emergency-extraction component | Validated emergency-extraction command icon. |
| `UI/Commands/SG1_ForcedImplantation` | 1 | 64×64 | Command UI | `final` | `done` | C# forced-implantation component | Validated forced-implantation command icon. |
| `UI/Commands/SG1_JaffaHelmetMode` | 1 | 64×64 | Command UI | `temporary-original` | `P2` | C# retractable-helmet component | Functional custom art pending final presentation. |
| `UI/Commands/SG1_RitualImplantation` | 1 | 64×64 | Command UI | `final` | `done` | Goa'uld and Jaffa ritual components | Validated shared ritual command icon. |
| `UI/Genes/SG1_GoauldLongevity` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_GoauldLongevity | Validated cyclic-longevity symbol. |
| `UI/Genes/SG1_JaffaLineage` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_JaffaLineage | Validated Jaffa-lineage symbol. |
| `UI/Genes/SG1_JaffaPhysiology` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_JaffaPhysiology | Validated physiology symbol. |
| `UI/Genes/SG1_JaffaPouchPotential` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_JaffaPouchPotential | Validated pouch-potential symbol. |
| `UI/Genes/SG1_JaffaSymbioteCompatibility` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_JaffaSymbioteCompatibility | Validated immature-symbiote compatibility symbol. |
| `UI/Genes/SG1_NaquadahBlood` | 1 | 64×64 | Gene UI | `final` | `done` | SG1_NaquadahBlood | Validated naquadah-blood symbol. |
| `UI/Xenotypes/SG1_GoauldHost` | 1 | 64×64 | Xenotype UI | `final` | `done` | SG1_GoauldHost | Validated simplified Goa'uld symbiote silhouette. |
| `UI/Xenotypes/SG1_Jaffa` | 1 | 64×64 | Xenotype UI | `final` | `done` | SG1_Jaffa | Validated human head with Apophis mark. |
| `World/WorldObjects/Expanding/SG1_FreeJaffa` | 1 | 128×128 | World faction | `final` | `done` | SG1_FreeJaffa | Accepted world-faction icon. |
| `World/WorldObjects/Expanding/SG1_GoauldSystemLords` | 1 | 128×128 | World faction | `final` | `done` | SG1_GoauldSystemLordPrototype | Accepted world-faction icon. |
| `World/WorldObjects/Expanding/SG1_SGCExpedition` | 1 | 128×128 | World faction | `final` | `done` | SG1_PlayerSGCExpedition | Accepted world-faction icon. |
| `World/WorldObjects/Expanding/SG1_Tokra` | 1 | 128×128 | World faction | `final` | `done` | SG1_Tokra | Accepted world-faction icon. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraIntroductionArtifactWorldSite | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` | 1 | 64×64 | World site | `final` | `done` | SG1_GoauldOpenConflictBattlefieldSite | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraDecodedMissionWorldSite | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraJaffaOfficerCaptureSite | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraHiddenSafehouseMarker, SG1_TokraHiddenSafehouseSitePart | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraDistressCallWorldSite | Validated event-site icon. |
| `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` | 1 | 128×128 | World site | `final` | `done` | SG1_TokraTemporaryBaseDeliverySite | Validated event-site icon. |
<!-- LOCAL_ASSET_TABLE_END -->

## Direct vanilla texture dependencies

<!-- EXTERNAL_ASSET_TABLE_START -->
| External path | Surface | Status | Priority | Referenced by | Audit note |
|---|---|---|---|---|---|
| `World/WorldObjects/DefaultSettlement` | World settlement | `accepted-vanilla` | `P3` | Free Jaffa, Goa'uld and SGC factions | Shared vanilla settlement silhouette; dedicated faction icons carry identity. |
| `World/WorldObjects/Sites/GenericSite` | World site base | `accepted-vanilla` | `P3` | GateRim temporary sites | Generic base retained because visible expanding icons are dedicated. |
| `UI/Icons/Study` | Command UI | `accepted-vanilla` | `P3` | SG1_TokraIntroductionArtifact | Semantically appropriate vanilla study action. |
<!-- EXTERNAL_ASSET_TABLE_END -->

## Runtime vanilla icon constants

| Runtime icon | Usage | Status | Priority | Audit note |
|---|---|---|---|---|
| `TexCommand.Attack` | Kara kesh attack abilities | `accepted-vanilla` | `P3` | Generic attack affordance remains appropriate. |
| `TexCommand.GatherSpotActive` | Tok'ra identity and symbiote controls | `placeholder-vanilla` | `P2` | Functional but generic. |

## Repository-level public icon

| Path | Status | Rule |
|---|---|---|
| `About/ModIcon.png` | `final` | Preserve Diablood's personal red-and-black demon icon; never reuse it as gameplay art. |

## Stable-path contract for later art lots

- Replace PNG contents in place whenever the Def and rendering contract are stable.
- Do not rename Defs or texture paths merely to improve art.
- Preserve required directional and body-type filenames for apparel.
- Keep English Defs and French public documentation aligned when a visual identity changes.
- Update this register and run the visual checker in the same revision as every added, removed or renamed texture.
- Move a family to `final` only after its actual inventory, map, pawn, world or UI presentation has been reviewed.

## Explicit non-effects

`0.3.95-dev` finalizes only the presentation and documentation of the listed
Tok'ra mission objects. Mission behavior, balance, trust gating, research,
serialized identifiers and unrelated visuals remain unchanged.

## Validation procedure

Run from the repository root:

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd
git diff --check
```

Expected visual-audit result:

```text
Final local texture families: 44
Local PNG files: 610
Local texture families: 77
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

Focused manual review confirmed every `0.3.95-dev` object on its relevant
ground, inventory, blueprint and deployed surfaces, including save/reload and
mission interaction behavior. Protected wiki copies must remain byte-identical
to their gameplay PNGs.
