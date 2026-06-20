# Cultural backstories

Version: `0.3.18-dev`

Status: `72` cultural backstories validated and published through `0.3.18-dev`.

## Scope

GateRim SG-1 now prepares `72` native RimWorld `BackstoryDef` entries.

The catalogue consists of:

- the `52` entries reworked in `0.3.8-dev`;
- six adult off-world-human careers added in `0.3.13-dev` for generated historical Tok'ra hosts;
- twelve measured additions in `0.3.16-dev` for cultures already implemented in game;
- two modern-Earth Tau'ri childhoods in `0.3.18-dev` for generated historical Tok'ra hosts.

The `0.3.16-dev` expansion adds exactly two entries to each targeted area:

- Tau'ri / SGC adult careers;
- shared Jaffa childhoods;
- Goa'uld-aligned Jaffa adult careers;
- Free Jaffa adult careers;
- ordinary Goa'uld-host adult careers;
- Tok'ra adult careers.

No Asgard, Nox or Unas backstory is added before those cultures exist as playable or generated content.

## Data rules

Every dedicated entry keeps the stable structure:

```text
spawnCategories
requiresSpawnCategory = true
```

Existing `defName` values, slots and categories are preserved for save and mod compatibility. New entries use new identifiers and never replace an existing Def.

## Editorial rules

Every English and French description contains two complementary ideas:

- the cultural or professional origin of the pawn;
- the practical habits and skills produced by that life.

Descriptions remain player-facing and avoid debug terminology, implementation details or claims that exceed the pawn's actual background.

## Skill bonuses

- childhoods provide modest foundations, generally `+1` to `+3`;
- adult careers provide clearer specialization, generally `+1` to `+4` in this expansion;
- no backstory adds passions, forced traits, work incapabilities or direct stat multipliers;
- combined childhood and adulthood bonuses remain useful without defining an entire pawn by themselves.

## Cultural groups after `0.3.16-dev`

### Tau'ri / SGC

Two dedicated childhoods now cover a modern-Earth science-fair upbringing and life in a military family. They are used only by the minority Tau'ri origin for historical hosts generated already joined with a Tok'ra.

Eight optional adult careers cover special operations, medicine, research, linguistics, engineering, liaison work, survival preparation and expedition logistics.

### Jaffa

Ten shared childhoods cover village, temple, warrior-household, pastoral, artisan, fortress, sanctuary, training-camp, naquadah-mine and Chappa'ai-settlement origins.

Ten Goa'uld-aligned adult careers and ten Free Jaffa careers now provide equivalent catalogue depth while retaining distinct military, political and civilian identities.

### Off-world humans and Goa'uld hosts

Six off-world childhoods and six civilian off-world adult careers remain dedicated to generated historical hosts. Ordinary Goa'uld hosts now have eight administrative, technical or court careers.

System Lords retain four dedicated rulership profiles; this measured expansion does not broaden that exceptional caste.

### Tok'ra

Eight agent careers now cover infiltration, medicine, diplomacy, scouting, analysis, covert delivery, sabotage engineering and safehouse coordination.

Existing colonists who later accept a Tok'ra symbiote keep their original childhood and adulthood.

## Cultural-profile integration

The new Defs are integrated without a C# change:

- spawn categories preserve normal world-generation filtering;
- an XML patch appends the two new SGC careers to the additive ordinary-human starter pool and the SG-team scenario pool;
- the same patch appends the new Jaffa and Goa'uld-host careers to the explicit starter lists;
- mixed starter profiles receive matching name-rule additions so a selected adulthood still determines the correct cultural name group.

This keeps the framework generic and allows future content additions to remain data-driven while the current rule schema is sufficient.

## Compatibility

- Existing pawns are not globally rerolled, renamed or reassigned.
- Existing saves keep their assigned backstories.
- Manual editor choices made after generation remain untouched.
- The `0.3.0-dev` framework remains the save-compatibility baseline.
- The six off-world-human adult careers from `0.3.13-dev` remain outside ordinary Tau'ri starter randomization.
- The two Tau'ri childhoods from `0.3.18-dev` are likewise reserved for generated historical hosts and do not alter ordinary or SG-team starter childhoods.

## Wiki catalogue maintenance

`docs/wiki/Cultural-Backstories.md` contains a player-facing row for every current backstory, grouped by culture and showing the localized name, description and skill bonuses.

Any future backstory addition, removal, presentation change or skill change must update the corresponding wiki table in the same milestone.

## Validated `0.3.18-dev` coverage

The final `0.3.18-dev` matrix confirmed:

- both weighted historical-host origins generate correctly, with the off-world origin remaining predominant;
- the two Tau'ri childhoods load in English and French and are selected only by `SG1_GeneratedHost_TauriSGCVolunteer`;
- ordinary human and stranded-SG-team starter childhood pools remain unchanged;
- host and symbiote identities, backstories and shared progression remain stable through switching and save/load;
- identities saved before the new origin remain unchanged;
- extraction followed by real reimplantation clears the generated origin and preserves the actual new host;
- the player wiki contains all `72` entries with matching descriptions and bonuses;
- no additional C# or Def correction was required after local revision `r1`.

## Previously validated coverage

The final `0.3.16-dev` matrix confirmed:

- all twelve Defs and their French presentation load without XML, patch or translation errors;
- Jaffa and Goa'uld-host starter pools select the expected cultural name groups;
- the stranded SG-team scenario remains exclusive to the eight SGC adult careers;
- the ordinary-human additive profile keeps vanilla adulthoods clearly predominant;
- normal pawn, raid and world generation continue to use compatible spawn categories;
- assigned backstories, manual names and Tok'ra shared progression persist through save/load;
- the player wiki contained all `70` entries then present, with matching descriptions and bonuses;
- no additional C# or Def correction was required after local revision `r1`.

## Future skill-coverage audit

A later dedicated milestone must compare the skill coverage of each cultural catalogue rather than expanding it by volume alone. The audit must identify absent or under-represented RimWorld skills for existing cultures, then add only the backstories needed to close culturally credible gaps. The same rule will apply when Asgard, Nox, Unas and other future cultures receive their own catalogues.

## Manual validation

1. Randomize Jaffa, Goa'uld-host, ordinary-human and stranded-SG-team starters.
2. Use representative GateRim SG-1 PawnKinds through `Debug actions menu` → `Spawn pawn`.
3. Compare visible titles, descriptions and bonuses with the XML and wiki catalogue.
4. Verify mixed-profile cultural names with the unified diagnostic introduced in `0.3.15-dev`.
5. Save, quit completely and reload; confirm histories and skill levels remain stable.
6. Check `Player.log` for XML, patch, translation, BackstoryDef or shuffled-backstory errors.
