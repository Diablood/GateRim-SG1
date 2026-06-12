# Intrinsic Jaffa forehead-mark data

Version: `0.1.74-dev r1`

## Design correction

The validated forehead-mark render node remains attached to the pawn's head,
but its storage no longer relies on a technical cosmetic gene.

A Jaffa forehead mark is cultural and political pawn data. It is independent
from:

```text
xenotype
hereditary genes
apparel
current faction
colonist, prisoner or enemy status
```

This also permits a non-Jaffa pawn to receive a mark manually, which covers an
infiltration scenario without changing the pawn's biology.

## Intrinsic Defs

```text
SG1_JaffaForeheadMark_GenericIntrinsic
SG1_JaffaForeheadMark_GenericSilverIntrinsic
SG1_JaffaForeheadMark_GenericGoldIntrinsic
```

The temporary rank convention remains:

```text
ordinary Jaffa       -> black mark
selected elites      -> silver mark
First Prime          -> gold embossed mark
```

A heavy-armored guard is not automatically a First Prime.

## Persistent storage

`GameComponent_JaffaForeheadMarks` stores an optional mark Def for each pawn
ThingID. Compatible Jaffa receive an ordinary mark once when first encountered
on a map only when their faction carries a Goa'uld System Lord-domain
extension. Free Jaffa intentionally remain unmarked. Removing a mark manually
does not cause it to reappear on the next scan.

## Native render-tree integration

`DynamicPawnRenderNodeSetup_JaffaForeheadMarks` is discovered by RimWorld's
native dynamic render-node setup. It reads the intrinsic mark data and injects
the validated head attachment into the pawn render tree.

The mark therefore creates:

```text
no inventory entry
no crafting recipe
no storage category
no armor coverage
no defensive statistic
no removable loot
no visible gene entry
```

## Legacy migration

The former technical genes remain declared as invisible migration placeholders:

```text
SG1_JaffaForeheadMark_Generic
SG1_JaffaForeheadMark_GenericSilver
SG1_JaffaForeheadMark_GenericGold
```

They have no render-node properties and are no longer patched into the Jaffa
xenotype. When an affected pawn is encountered, the component transfers the
appropriate rank mark into intrinsic data and removes every legacy technical
gene from that pawn.

Keeping these Defs temporarily prevents missing-Def errors when loading saves
created during the earlier prototypes.

## Developer tools

RimWorld developer actions under `GateRim SG-1` can target any map pawn:

```text
Set forehead mark: ordinary black
Set forehead mark: elite silver
Set forehead mark: First Prime gold
Remove forehead mark
```

## Manual test checklist

1. Extract the ZIP at the repository root and run the included PowerShell apply helper.
2. Build the C# assembly.
3. Start RimWorld and confirm that no XML, translation, DefOf or C# error appears.
4. Trigger each controlled Goa'uld Jaffa raid and confirm that ordinary Jaffa carry the black mark.
5. Confirm that the mark is absent from the pawn's genes and apparel.
6. Confirm south, east and west rendering, the hidden north view and helmet coverage.
7. Use the developer tools to assign silver and gold marks to selected pawns.
8. Use the developer tools to apply a black mark to a non-Jaffa pawn.
9. Save, reload and confirm persistence for each manually assigned mark.
10. Remove a Jaffa mark manually, save, reload and confirm that it does not return automatically.
11. Load a save containing an earlier technical mark gene and confirm automatic migration without errors.
12. Confirm that Goa'uld-domain Jaffa receive marks while Free Jaffa remain unmarked.
