# Free Jaffa pawn kinds

Version: `0.2.2-dev`

## Added PawnKindDefs

```text
SG1_FreeJaffaWarrior
SG1_FreeJaffaGuard
```

## Shared Jaffa baseline

Both kinds force:

```text
SG1_Jaffa
```

They reuse:

```text
SG1_MatokStaff
SG1_JaffaLightArmor or SG1_JaffaHeavyArmor
SG1_JaffaGauntlets
SG1_JaffaReinforcedBoots
SG1_JaffaDeployedHelmet
```

## Prim'ta provisioning

The historical component:

```text
GameComponent_GoauldJaffaPrimtaInitializer
```

now covers the two Free Jaffa kinds as well as the existing Goa'uld-aligned
kinds. Its historical type name and save-data key are preserved for
compatibility.

## Forehead-mark distinction

Free Jaffa do not receive an automatic forehead mark.

`GameComponent_JaffaForeheadMarks` now auto-initializes a mark only when the
pawn's faction carries a `GoauldSystemLordDomainExtension`.

Manual developer assignment and legacy mark migration remain available for
infiltration stories or migrated saves.

## Manual test checklist

1. Generate a Free Jaffa settlement map.
2. Inspect both pawn kinds.
3. Confirm Jaffa xenotype.
4. Confirm initial Prim'ta.
5. Confirm Ma'Tok.
6. Confirm modular armor.
7. Confirm retractable helmet.
8. Confirm absence of automatic forehead marks.
9. Apply a mark manually and confirm that manual assignment still works.

## Trade extension (`0.3.43-dev`)

`SG1_FreeJaffaTrader` extends the same cultural and biological baseline to a
dedicated caravan contact. The pawn kind uses Free Jaffa backstories and names,
receives automatic Prim'ta initialization, carries a Ma'Tok and light Jaffa
armor, omits the guard helmet for immediate identification, and explicitly sets
the PawnKind trader flag required by vanilla caravan generation.
