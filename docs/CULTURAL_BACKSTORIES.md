# Cultural backstories

Version: `0.3.9-dev-r2`

## Scope

GateRim SG-1 currently defines `52` native RimWorld `BackstoryDef` entries. This milestone reworks the existing set without adding or deleting any backstory.

The stable structure remains:

```text
spawnCategories
requiresSpawnCategory = true
```

Off-world `PawnKindDef` filters continue to select the appropriate childhood and adulthood categories. Existing `defName` values, slots and categories are preserved for compatibility.

## Editorial pass

Every English and French description now contains two complementary ideas:

- the cultural or professional origin of the pawn;
- the practical habits and skills produced by that life.

Descriptions remain player-facing and avoid debug terminology, implementation details or claims that exceed the pawn's actual background.

## Skill bonuses

Every dedicated backstory now grants a small set of coherent `skillGains`.

Design rules:

- childhoods provide modest foundations, generally `+1` to `+3`;
- adult careers provide clearer specialization, generally `+1` to `+5`;
- no backstory adds passions, forced traits, work incapabilities or direct stat multipliers;
- military histories do not all use the same distribution;
- civilian Jaffa, off-world humans, Goa'uld administrators and Tok'ra support roles retain distinct gameplay identities;
- combined childhood and adulthood bonuses remain useful without defining an entire pawn by themselves.

## Cultural groups

### Tau'ri / SGC

Six optional adult careers cover special operations, medicine, research, linguistics, engineering and liaison work. The stranded SG-team scenario remains compatible with broader vanilla Earth childhoods.

### Jaffa

Eight shared Jaffa childhoods establish village, temple, warrior-household, pastoral, artisan, fortress, sanctuary and training-camp origins.

Eight Goa'uld-aligned adult careers emphasize military service, ritual security, garrison command and Chappa'ai defense. Eight Free Jaffa careers distinguish liberated fighters, community defenders, scouts, civilians, healers and former deserters.

### Off-world humans and Goa'uld hosts

Six off-world childhoods represent tributary villages, palaces, markets, temples, caravans and isolated farms.

Ordinary Goa'uld hosts use six administrative or court careers. System Lords retain four dedicated rulership profiles with stronger social, intellectual or military emphasis.

### Tok'ra

Six agent careers cover infiltration, medicine, diplomacy, scouting, intelligence analysis and covert delivery. Existing colonists who later accept a Tok'ra symbiote keep their original childhood and adulthood.

## Compatibility

- No `defName`, slot, category or body-type default is changed.
- No pawn is rerolled or renamed.
- Existing saves keep their assigned backstories; the newly defined skill bonuses are read from those same Defs when the pawn's skill offsets are evaluated.
- The `0.3.0-dev` framework remains the save-compatibility baseline.

## Integration with the cultural framework

These `BackstoryDef` entries remain stable content data. Starting with `0.3.9-dev`, the shared GateRim SG-1 cultural framework consumes them for configurable starter profiles instead of adding culture-specific hardcoded branches.

A configurable cultural profile can reference:

- compatible childhood and adulthood categories or explicit Defs;
- the associated cultural name generator;
- identification criteria and profile priority;
- scenario-specific starting-pawn restrictions;
- optional data used by other culture-aware systems.

The first consumers are cultural name resolution and culture-aware starting-pawn randomization. The starter behavior remains restricted to newly generated player starters; world pawn generation and manual editor assignments remain unchanged.

## Deferred expansion

The number of backstories is not increased in this milestone. A later discussion will decide whether additional entries are useful, while keeping the set readable and maintainable.

Future cultures such as Asgard, Nox and Unas must receive coherent name generators and backstories when the race or faction is introduced, or in an immediately following milestone.

The separate question of preserving and switching a player-controlled Tok'ra host / symbiote identity is intentionally deferred. Its complete design record is maintained in `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`; it must not be implemented as a hidden side effect of backstory data.

## Wiki catalogue maintenance

`docs/wiki/Cultural-Backstories.md` contains a player-facing table for every current backstory, grouped by culture and showing the localized name, description and skill bonuses. Any future backstory addition, removal, renamed presentation or skill change must update the corresponding wiki table in the same milestone.

## Manual validation

1. Start several `Équipe SG isolée` games and inspect the four candidates before launch.
2. Use `Debug actions menu` → `Spawn pawn` with representative GateRim SG-1 PawnKinds.
3. Inspect childhood and adulthood descriptions and compare the visible skill bonuses with the selected histories.
4. Generate Goa'uld-aligned Jaffa, Free Jaffa, Goa'uld hosts, a System Lord and Tok'ra agents through their normal contexts when available.
5. Save, quit completely and reload; confirm histories and skill levels remain stable.
6. Check `Player.log` for XML, translation, BackstoryDef, skill or shuffled-backstory errors.

## Starter cultural profiles

Starting-pawn restrictions are now configured by `CulturalPawnProfileDef` rather than by changing the global backstory categories. This keeps normal world generation on the existing PawnKind and faction filters.

- Jaffa starters use the Jaffa childhood set and either Goa'uld-aligned or Free Jaffa adulthoods.
- Goa'uld-host starters use off-world-human childhoods and either Goa'uld-host or Tok'ra adulthoods.
- Ordinary human starters in normal scenarios retain vanilla childhoods and adult careers, while the six Tau'ri / SGC careers participate as additional weighted adult choices instead of using a fixed replacement chance.
- The stranded SG-team scenario restricts only adulthood to the current six SGC careers; a dedicated Tau'ri childhood set remains deferred.
- Manual editor choices made after generation are not validated or replaced.

The profile schema and extension rules are documented in `docs/CULTURAL_FRAMEWORK.md`.
