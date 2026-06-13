# GateRim SG-1 — Current project state

## Latest published milestone

`v0.2.8-dev-r1 - Require adult stranded SG-team candidates`

The main GitHub repository and the separate wiki repository are synchronized.

## Current validated behavior

The `Équipe SG isolée` starting scenario now rejects candidates below `20`
biological years through the existing C# starting-pawn filter. This threshold
ensures that every candidate has an adulthood backstory.

Preserved behavior:
- exactly four candidates for four starting slots;
- regeneration of every candidate slot;
- rejection of candidates incapable of violence;
- automatic SG-team field equipment.

## Validation status

- Forced C# rebuild: passed with no warnings or errors.
- Four initial candidates aged `20+`: passed.
- Adulthood backstories present: passed.
- Repeated regeneration of all four slots: passed.
- Violence capability and SG-team equipment preserved: passed.

## Validated research baseline

Validation branch:

```text
validation/stargate-crafting-research
```

Static validation completed:
- all four vanilla prerequisites exist in RimWorld 1.6;
- all four GateRim research projects use the expected tab and prerequisite;
- all `19` intended recipes, apparel items, weapons and buildings are gated by
  the expected GateRim project;
- no Stargate weapon or apparel recipe in the audited scope remains ungated;
- all `70` gameplay Def XML files parse successfully;
- all `89` French translation XML files parse successfully;
- the latest `Player.log` contains no unresolved GateRim research reference or
  GateRim XML loading error.

Manual validation completed with the isolated active mod list:

```text
Core
Biotech
GateRim SG-1
```

- dedicated research tab and all four projects: passed;
- four vanilla prerequisites: passed;
- production and construction unavailable before research: passed;
- all expected unlocks after completing each project: passed;
- captured, existing and scenario-supplied items remain usable: passed;
- no unresolved GateRim research reference in `Player.log`: passed.

Environment note: an earlier `Player.log` detected the sibling
`GateRim-SG1.wiki` directory as an invalid mod because both repositories are
stored under `RimWorld/Mods`; `CharacterEditor` was active during that launch.
Those unrelated warnings were excluded from the isolated validation result.

## Next focus

- Add normal acquisition sources for rare Stargate resources in a separate
  milestone.
