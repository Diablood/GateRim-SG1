# Intrinsic generic Jaffa forehead-mark prototype

Version: `0.1.73-dev r2`

## Design correction

The first local prototype modeled the mark as an internal apparel overlay.
That approach was discarded.

A Jaffa forehead mark is intrinsic to the pawn: it behaves like a tattoo or
scarification, not like removable equipment.

## Implementation

The current prototype defines a dedicated cosmetic technical gene:

```text
SG1_JaffaForeheadMark_Generic
```

The gene contributes a native RimWorld render-tree node:

```xml
<renderNodeProperties>
    <li>
        <nodeClass>PawnRenderNode_AttachmentHead</nodeClass>
        <workerClass>PawnRenderNodeWorker_FlipWhenCrawling</workerClass>
        <texPath>Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark</texPath>
        <parentTagDef>Head</parentTagDef>
        <rotDrawMode>Fresh, Rotting</rotDrawMode>
        <visibleFacing>
            <li>East</li>
            <li>South</li>
            <li>West</li>
        </visibleFacing>
        <drawData>
            <defaultData>
                <layer>60</layer>
            </defaultData>
        </drawData>
    </li>
</renderNodeProperties>
```

The visual is attached to the pawn's head render node and uses no apparel
item. It therefore creates:

```text
no inventory entry
no crafting recipe
no storage category
no armor coverage
no defensive statistic
no removable loot
```

## Generic black mark

The prototype uses a black temporary emblem suitable for ordinary Jaffa.

Future rank variants should remain separate:

```text
ordinary Jaffa       -> black mark
selected elites      -> silver mark
First Prime          -> gold embossed mark
```

A heavy-armored guard is not automatically a First Prime.

## Xenotype integration

The existing `SG1_Jaffa` xenotype receives the cosmetic technical gene
through a small XML patch. This keeps the mark separate from the hereditary
lineage gene and makes future replacement straightforward.

## Local cleanup required

Before testing this revision, remove the obsolete apparel prototype:

```text
1.6/Defs/ThingDefs_Apparel/SG1_JaffaSystemLordMarks.xml
Languages/French/DefInjected/ThingDef/SG1_JaffaSystemLordMarks.xml
Textures/Things/Pawn/Humanlike/Apparel/JaffaSystemLordMarks
```

## Manual test checklist

1. Remove the obsolete apparel-prototype files.
2. Start RimWorld and confirm that no XML or translation error appears.
3. Trigger any controlled Goa'uld Jaffa raid.
4. Confirm that generated Jaffa visibly carry a black forehead mark while
   facing south, east or west.
5. Confirm that the mark is not rendered from behind.
6. Confirm that the mark is visible when the helmet is retracted.
7. Confirm that the deployed helmet visually covers the mark.
8. Confirm that the mark does not appear as worn apparel or loot.
9. Recheck the direct-assault, abduction and destruction incidents.
10. Confirm that natural Goa'uld raids remain disabled.

## r3 temporary position calibration

The intrinsic render-node architecture is validated in-game. The remaining
prototype issue was visual placement: the temporary emblem floated above the
forehead.

This revision:

- moves the painted emblem lower inside the transparent texture canvas;
- applies a small negative vertical render offset on east, south and west
  facings;
- keeps the intrinsic gene, render-node class, parent head tag and layer
  unchanged.

The artwork remains temporary. This calibration only aims to place the
prototype visibly on the forehead before final domain-specific designs.

## r4 side-view calibration

South and north views are now considered acceptable in-game. The remaining
prototype issue concerned only east and west side views.

This revision keeps the intrinsic gene and render-node offsets unchanged and
re-centers only the east/west emblem paintings toward the visible forehead.
