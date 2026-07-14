# Intrinsic Jaffa forehead-mark data

Version: `0.3.91-dev`

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

The validated visual rank convention is:

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

## Final overlay textures

The three canonical families remain at their stable paths under
`Textures/Things/Pawn/Humanlike/JaffaForeheadMarks`:

```text
GenericJaffaForeheadMark
GenericSilverJaffaForeheadMark
GenericGoldJaffaForeheadMark
```

Each family contains four `128×128` directional PNGs. The final contract is:

```text
South              compact visible Apophis forehead symbol
North / East / West fully transparent image
```

The three visible files use one identical alpha footprint and differ only by the
validated rank color: black, silver or gold. This keeps the mark within the
forehead, compatible with visible hair and free from the above-head overflow of
the former temporary artwork.

## Manual test checklist

1. Apply ordinary black, elite silver and First Prime gold with the existing
   developer actions to the same pawn.
2. Face the pawn `South` and confirm the mark is small, centered and readable.
3. Rotate to `North`, `East` and `West` and confirm no mark is visible.
4. Test several hairstyles and confirm the mark remains a forehead detail rather
   than a floating attachment.
5. Equip and remove a helmet to confirm existing layering remains acceptable.
6. Save and reload each assigned rank and confirm the same intrinsic mark returns.
7. Remove the mark manually, save and reload, and confirm it remains absent.
8. Confirm the mark is absent from genes, apparel and inventory.
9. Confirm Goa'uld-domain Jaffa assignment and Free Jaffa exclusion remain
   unchanged.
10. Inspect `Player.log` for missing-texture or render-node errors.

## Validation state

The maintainer validated the twelve final PNGs directly in their gameplay paths
in final visual revision `r1`. Build `0.3.91.0`, the duration, visual and project
consistency checks, `git diff --check`, focused save/reload and removal checks,
and the wiki references all pass. Publication-only revision `r2` changes status
documentation only; the milestone does not modify the intrinsic data or render
implementation.
