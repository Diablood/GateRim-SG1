# Cultural backstories

Version: `0.3.19-dev`

Status: `83` cultural backstories validated and published through `0.3.19-dev`.

## Scope

GateRim SG-1 now prepares `83` native RimWorld `BackstoryDef` entries.

The catalogue consists of:

- the `52` entries reworked in `0.3.8-dev`;
- six adult off-world-human careers added in `0.3.13-dev` for generated historical Tok'ra hosts;
- twelve measured additions in `0.3.16-dev` for cultures already implemented in game;
- two modern-Earth Tau'ri childhoods in `0.3.18-dev` for generated historical Tok'ra hosts;
- eleven targeted adult careers in `0.3.19-dev` to close demonstrated zero-coverage skill gaps.

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
- adult careers provide clear but bounded specialization, generally `+1` to `+4`;
- no backstory adds passions, forced traits, work incapabilities or direct stat multipliers;
- combined childhood and adulthood bonuses remain useful without defining an entire pawn by themselves.

## Cultural groups after `0.3.19-dev`

### Tau'ri / SGC

Two dedicated childhoods cover a modern-Earth science-fair upbringing and life in a military family for historical hosts generated already joined with a Tok'ra.

Eleven adult careers now include special operations, medicine, research, linguistics, engineering, liaison work, survival preparation, expedition logistics, planetary geology, expedition cooking and field archaeology.

### Jaffa

Ten shared childhoods combine with ten Goa'uld-aligned adult careers or ten Free Jaffa careers. The `0.3.19-dev` audit confirmed that both Jaffa profiles already covered all twelve skills, so no artificial entry was added.

### Off-world humans and Goa'uld hosts

Six off-world childhoods now combine with seven civilian off-world adult careers, including quarry work for Mining and Melee coverage.

Ordinary Goa'uld hosts now have ten administrative, technical, medical or court careers. System Lords now have six dedicated rulership profiles, including domain architecture and biomedical experimentation.

### Tok'ra

Eleven agent careers now cover infiltration, medicine, diplomacy, scouting, analysis, covert delivery, sabotage engineering, safehouse coordination, field technology, close-quarters operations and cultural adaptation.

Existing colonists who later accept a Tok'ra symbiote keep their original childhood and adulthood.

## Skill-coverage rule

`docs/CULTURAL_SKILL_COVERAGE.md` audits RimWorld's twelve standard skills against the actual childhood-and-adulthood pools available to each profile.

A profile is considered covered when at least one credible path grants each skill. Low redundancy is documented, but it is not an automatic reason to add content. This keeps the catalogue culturally coherent instead of numerically symmetrical.

The `0.3.19-dev` audit confirms complete coverage for:

- Tau'ri / SGC;
- Goa'uld-aligned Jaffa;
- Free Jaffa;
- off-world humans;
- ordinary Goa'uld hosts;
- Goa'uld System Lords;
- Tok'ra adulthood independently of host origin.

## Cultural-profile integration

The new Defs remain integrated without a C# change:

- spawn categories preserve normal world-generation filtering;
- XML patches extend the explicit starter and generated-host pools;
- mixed starter profiles receive matching name-rule additions where required;
- generated-host origin weights remain unchanged;
- existing pawns are not rerolled when new entries become available.

## Compatibility

- Existing pawns are not globally rerolled, renamed or reassigned.
- Existing saves keep their assigned backstories.
- Manual editor choices made after generation remain untouched.
- The `0.3.0-dev` framework remains the save-compatibility baseline.
- Dedicated generated-host childhoods and adult careers remain limited to their intended categories.
- Tok'ra active-identity switching continues to apply only the difference between stored histories without duplicating progression.

## Wiki catalogue maintenance

`docs/wiki/Cultural-Backstories.md` contains a player-facing row for every current backstory, grouped by culture and showing the localized name, description and skill bonuses.

Any future backstory addition, removal, presentation change or skill change must update the corresponding wiki table in the same milestone.

## Validated `0.3.19-dev` coverage

The final matrix confirmed:

- all eleven new Defs and French texts load without XML, patch or translation errors;
- each modified cultural pool produces representative new entries;
- the stranded SG-team scenario remains exclusive to SGC adult careers;
- ordinary-human starters remain vanilla-majority;
- Jaffa pools remain unchanged;
- Goa'uld, System Lord and Tok'ra name groups remain coherent;
- both generated-host origins retain their weights and persistent identities;
- assigned histories and skill effects persist through save/load;
- ten Tok'ra personality switches produce no skill drift;
- the player wiki contains all `83` entries with matching descriptions and bonuses;
- no corrective C# or Def revision was required after local revision `r1`.

## Future cultures

Asgard, Nox, Unas and every later culture must receive the same coverage audit when their first meaningful childhood and adulthood pools exist. Missing skills should be closed with credible cultural lives rather than generic filler.

## Manual validation

1. Randomize Jaffa, Goa'uld-host, ordinary-human and stranded-SG-team starters.
2. Use representative GateRim SG-1 PawnKinds through `Debug actions menu` → `Spawn pawn`.
3. Compare visible titles, descriptions and bonuses with the XML and wiki catalogue.
4. Verify mixed-profile cultural names with the unified diagnostic introduced in `0.3.15-dev`.
5. Save, quit completely and reload; confirm histories and skill levels remain stable.
6. Check `Player.log` for XML, patch, translation, BackstoryDef or shuffled-backstory errors.
