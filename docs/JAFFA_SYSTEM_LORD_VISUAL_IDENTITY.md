# Intrinsic Jaffa forehead-mark data

Version: `0.3.90-dev`

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

## Removed legacy gene scaffolding

The former technical genes were removed in `0.3.90-dev`:

```text
SG1_JaffaForeheadMark_Generic
SG1_JaffaForeheadMark_GenericSilver
SG1_JaffaForeheadMark_GenericGold
```

They no longer participate in gameplay, migration or UI inspection. The
maintainer explicitly accepts the loss of compatibility for private prototype
saves that still contain those genes. Current intrinsic forehead-mark data and
its stable `JaffaForeheadMarkDef` identifiers remain supported.

## Developer tools

RimWorld developer actions under `GateRim SG-1` can target any map pawn:

```text
Set forehead mark: ordinary black
Set forehead mark: elite silver
Set forehead mark: First Prime gold
Remove forehead mark
```

## Manual test checklist

1. Extract the ZIP at the repository root and apply the announced file deletions.
2. Build the C# assembly.
3. Start RimWorld and confirm that no XML, translation, DefOf or C# error appears.
4. Trigger each controlled Goa'uld Jaffa raid and confirm that ordinary Jaffa carry the black mark.
5. Confirm that the mark is absent from the pawn's genes and apparel.
6. Confirm south, east and west rendering, the hidden north view and helmet coverage.
7. Use the developer tools to assign silver and gold marks to selected pawns.
8. Use the developer tools to apply a black mark to a non-Jaffa pawn.
9. Save, reload and confirm persistence for each manually assigned mark.
10. Remove a Jaffa mark manually, save, reload and confirm that it does not return automatically.
11. Confirm the obsolete technical genes are absent from gene inspection.
12. Confirm that Goa'uld-domain Jaffa receive marks while Free Jaffa remain unmarked.

## Next intrinsic overlay art pass

`0.3.90-dev` does not finalize the textures rendered on pawns. It removes only
the obsolete gene scaffolding and deliberately preserves the existing intrinsic
render nodes, facings, offsets and temporary overlay families.

The next decided visual milestone replaces the actual files under
`Textures/Things/Pawn/Humanlike/JaffaForeheadMarks` with a compact Apophis
symbol. The ordinary, elite and First Prime variants share one geometry with
black, silver and gold treatments. The final mark must stay within the forehead,
remain compatible with visible hair, and render only for the `South` facing.
Its acceptance requires real-pawn tests before the families can move from
`temporary-*` to `final` in the visual register or appear in the wiki gallery.
