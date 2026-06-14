# Tok'ra field clothing set

Version: `0.2.19-dev`

## Purpose

This milestone adds the first dedicated Tok'ra clothing set and uses it to fix
the validated safehouse contact from `0.2.18-dev` appearing naked.

The implementation intentionally avoids a temporary vanilla-clothing workaround:
Tok'ra contacts now receive their own faction-appropriate outfit immediately.

## Scope

```text
one Tok'ra clothing set
no visual variants
no armor role
no trader, recruitment, aid, raid or quest behavior
no change to the safehouse contact generation flow
```

## Definitions

```text
ThingDef: SG1_TokraFieldGarb
PawnKindDef user: SG1_TokraVoluntaryHost
French label: tenue de terrain Tok'ra
Texture path: Textures/Things/Pawn/Humanlike/Apparel/TokraFieldGarb
```

## Apparel profile

The outfit is a light textile `OnSkin` apparel piece covering:

```text
Torso
Neck
Shoulders
Arms
Legs
```

Its stats are intentionally modest. It provides visual identity and basic
clothing coverage, not combat protection.

## Contact integration

`SG1_TokraVoluntaryHost` now declares:

```text
apparelMoney = 0
apparelRequired = SG1_TokraFieldGarb
```

This keeps the safehouse contact deterministic while leaving the existing C#
contact generation step unchanged.

## Crafting

The outfit is craftable at:

```text
HandTailoringBench
ElectricTailoringBench
```

after:

```text
SG1_SGFieldEquipment
```

The research hook is provisional and reuses the current field-equipment gate
until a dedicated Tok'ra acquisition or diplomacy path exists.

## Manual test checklist

1. Start RimWorld after applying the XML and texture files.
2. Confirm no red XML load error references `SG1_TokraFieldGarb`.
3. Open a tailoring bench after `SG1_SGFieldEquipment` is available and confirm
   `Tok'ra field garb` appears as a bill.
4. Create or visit a Tok'ra hidden safehouse site.
5. Confirm the generated Tok'ra contact is no longer naked and wears the Tok'ra
   outfit.
6. Confirm the contact remains peaceful, non-trading and non-recruitable.

## Notes

The current graphics are a first readable RimWorld-scale prototype inspired by
sober sand/beige Tok'ra clothing. A later art pass can refine the embroidery,
quilting and shoulder shape without changing the gameplay definition.

## 0.2.19-dev-r1 texture correction

Initial testing validated the contact and clothing behavior, but RimWorld reported
a missing worn graphic for the generated contact's body type:

```text
Things/Pawn/Humanlike/Apparel/TokraFieldGarb/TokraFieldGarb_Female
```

The correction adds explicit worn apparel textures for the standard adult body
types used by RimWorld:

```text
Male
Female
Thin
Fat
Hulk
```

Gameplay definitions are unchanged. This is a texture-only correction.

