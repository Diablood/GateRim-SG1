# GateRim SG-1 — Current project state

## Latest published milestone

`v0.2.9-dev - Add natural Zat'nik'tel acquisition baseline`

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

## Validated acquisition baseline

Validated branch:

```text
feature/stargate-resource-acquisition-baseline
```

Validated behavior:
- keep Goa'uld Jaffa warriors restricted to Ma'Tok staff weapons;
- allow Goa'uld Jaffa guards to generate with either a Ma'Tok staff or a
  Zat'nik'tel;
- reuse existing natural Goa'uld raids and settlements as the acquisition
  source;
- preserve immediate use of captured weapons before research;
- keep local manufacturing gated by `SG1_JaffaWeaponry`.

This acquisition milestone is XML-only, so no C# rebuild was required.

Validation completed:
- multiple Goa'uld Jaffa guards generated with both weapon outcomes: passed;
- ordinary and settlement warriors restricted to Ma'Tok staff weapons: passed;
- settlement guards generated with both weapon outcomes: passed;
- natural Goa'uld raids produced rare Zat'nik'tel carriers: passed;
- recovered Zat'nik'tel weapons remained usable before research: passed;
- local crafting remained unavailable until `SG1_JaffaWeaponry`: passed;
- no XML or pawn-generation errors observed in `Player.log`: passed;
- all `70` gameplay Def XML files parsed successfully.

## Next focus

Prepare `0.2.10-dev` on branch:

```text
feature/goauld-queen-natural-acquisition
```

Current scope:
- add a rare escaped-queen storyteller incident after day `30`;
- enforce a minimum `60`-day refire delay;
- suppress the incident while any living player-controlled queen exists on a
  map or in a caravan;
- make immature-symbiote extraction available to player-controlled queens;
- preserve developer-mode extraction for isolated tests;
- preserve the persistent one-day cooldown and assisted-maturation recipe;
- keep local maturation gated by `SG1_GoauldBiotechnology`.

This milestone requires C# for deterministic queen generation, player-control
checks and duplicate prevention.

Validation required:
- force `SG1_GoauldQueenArrival` and confirm one player-controlled queen enters
  from a reachable map edge;
- confirm the extraction command is visible without developer mode;
- extract one immature symbiote and confirm the one-day cooldown;
- save and reload during the cooldown and confirm it persists;
- confirm a second forced incident is refused while the queen is alive;
- move the queen into a caravan and confirm duplicate prevention still works;
- mature the resource with `10` raw meat after `SG1_GoauldBiotechnology`;
- confirm no XML, incident or pawn-generation errors appear in `Player.log`.
