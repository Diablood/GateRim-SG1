# Cultural starter loadouts

Version: `0.3.22-dev`

## Purpose

`CulturalStarterRule` configures player-starter restrictions and equipment from cultural profile Defs. It is evaluated only for `PawnGenerationContext.PlayerStarter`; raids, visitors, world pawns and existing saves remain outside this consumer.

## Candidate restrictions

```xml
<minimumBiologicalAge>20</minimumBiologicalAge>
<requireViolenceCapable>true</requireViolenceCapable>
```

Both fields are optional.

## Legacy ordered apparel

The original deterministic list remains supported for compatibility:

```xml
<replaceStartingApparel>true</replaceStartingApparel>
<apparelQuality>Normal</apparelQuality>
<apparel>
    <li>SomeApparelDef</li>
</apparel>
```

Items are worn in list order. Stuffable apparel should use the weighted-slot schema instead so its material can be declared explicitly.

## Weighted apparel slots

A rule may define weighted equipment slots:

```xml
<replaceStartingApparel>true</replaceStartingApparel>
<apparelQuality>Normal</apparelQuality>
<apparelSlots>
    <li>
        <selectionChance>1</selectionChance>
        <options>
            <li>
                <apparel>Apparel_BasicShirt</apparel>
                <stuff>Cloth</stuff>
                <weight>1</weight>
            </li>
        </options>
    </li>
    <li>
        <selectionChance>0.6</selectionChance>
        <options>
            <li>
                <apparel>SomeHelmet</apparel>
                <weight>2</weight>
            </li>
            <li>
                <apparel>SomeCap</apparel>
                <weight>1</weight>
            </li>
        </options>
    </li>
</apparelSlots>
```

Each slot is resolved in list order:

1. `selectionChance` decides whether the slot produces an item;
2. one option is selected by relative `weight`;
3. `stuff` is passed to `ThingMaker` for stuffable apparel;
4. the rule-level quality is applied when the apparel has `CompQuality`;
5. the selected item is worn immediately.

Slots may share a visual choice without losing per-pawn randomness:

```xml
<li>
    <selectionChance>1</selectionChance>
    <variantGroup>FieldUniformColor</variantGroup>
    <options>
        <li>
            <apparel>OlivePants</apparel>
            <weight>1</weight>
            <variantKey>Olive</variantKey>
        </li>
        <li>
            <apparel>BlackPants</apparel>
            <weight>1</weight>
            <variantKey>Black</variantKey>
        </li>
    </options>
</li>
<li>
    <selectionChance>0.75</selectionChance>
    <variantGroup>FieldUniformColor</variantGroup>
    <options>
        <li>
            <apparel>OliveJacket</apparel>
            <weight>1</weight>
            <variantKey>Olive</variantKey>
        </li>
        <li>
            <apparel>BlackJacket</apparel>
            <weight>1</weight>
            <variantKey>Black</variantKey>
        </li>
    </options>
</li>
```

The first selected slot records its `variantKey`. Later slots in the same `variantGroup` reuse that key. In the SG-team loadout, pants select olive, black or desert independently for each pawn; an optional jacket then matches that pawn's pants.

A slot with `selectionChance=1` is mandatory. A lower value represents an optional layer and naturally supports a `nothing` result without a fake ThingDef.

## Configuration validation

The profile reports configuration errors for:

- selection chances outside `0..1`;
- empty or null slots;
- null options;
- non-positive weights;
- non-apparel ThingDefs;
- stuffable apparel without a configured material;
- material configured for non-stuffable apparel;
- a ThingDef used as material that is not actually stuff;
- variant keys without a variant group;
- grouped options without variant keys;
- duplicate keys inside one slot;
- grouped slots that do not expose the same key set.

## Scenario identification

Rules may retain `requiredScenarioParts`. The stranded SG-team scenario uses the hidden marker `SG1_StrandedSGTeamStartingGear`, whose class is the generic `ScenPart_CulturalMarker`. The marker contains no loadout behavior.

## Stranded SG-team consumer

`SG1_Culture_TauriSGC` currently configures:

- minimum biological age `20`;
- violence capability required;
- mandatory cloth T-shirt using vanilla `Apparel_BasicShirt`;
- mandatory field pants chosen equally from olive, black and desert variants;
- optional field jacket with a `0.75` chance, reusing the pants variant;
- mandatory tactical boots, gloves and vest;
- optional headgear slot with a `0.6` chance;
- equal relative weights for `SG1_SGTeamFieldHelmet` and `SG1_SGTeamFieldCap`.

The field uniform is split into two real apparel pieces:

- pants: `OnSkin`, legs only;
- jacket: `Middle`, torso, shoulders and arms;
- tactical vest: patched to `Shell` so it can remain above the jacket;
- T-shirt: vanilla `OnSkin`, torso.

The SG-team cap is now the second weighted option in the existing headgear slot. With a slot chance of `0.6` and equal option weights, the theoretical distribution is `30 %` helmet, `30 %` cap and `40 %` no headgear. No scenario-specific C# is involved.

## Vanilla weapon balance

The scenario intentionally uses vanilla human firearms rather than defining redundant Tau’ri weapon copies. The loose starting set is:

```text
1 assault rifle
1 machine pistol
1 autopistol
1 pump shotgun
```

This preserves a modern military identity while reducing the early-game power of the previous three-assault-rifle set. Optional compatibility patches may later substitute equivalent firearms from other enabled weapon mods.

## Compatibility

Profiles without configured restrictions or apparel preserve their prior behavior. Custom scenario formats that bypass Def loading remain outside the current guarantee. The old combined treillis Defs remain available for save compatibility and crafting history, but the stranded-team starter no longer equips them.
