# Cultural backstory skill coverage

Version: `0.3.19-dev`

Status: validated and published through `0.3.19-dev`.

## Audit rule

The audit uses RimWorld's twelve standard skills:

`Shooting`, `Melee`, `Construction`, `Mining`, `Cooking`, `Plants`, `Animals`, `Crafting`, `Artistic`, `Medicine`, `Social` and `Intellectual`.

A cultural profile is considered covered when its actual childhood-and-adulthood pool contains at least one backstory granting each skill. The Tok'ra adulthood pool is audited independently because it can be paired with multiple generated-host origins and with pre-existing player hosts.

A skill represented by only one backstory is recorded as low redundancy, but it does not automatically justify another entry. This prevents catalogue growth based only on numerical symmetry.

## Coverage before `0.3.19-dev`

| Cultural pool | Missing skills | Decision |
|---|---|---|
| Tau'ri / SGC | Mining, Cooking, Artistic | Add three distinct SGC careers. |
| Goa'uld-aligned Jaffa | None | No new entry. |
| Free Jaffa | None | No new entry. |
| Off-world humans | Mining, Melee | Add one quarry career covering both credibly. |
| Ordinary Goa'uld hosts | Medicine, Melee | Add a physician and a duelist. |
| Goa'uld System Lords | Construction, Mining, Crafting, Medicine | Add an architect and a biomedical experimenter. |
| Tok'ra adulthood | Melee, Cooking, Crafting, Artistic | Add three operationally distinct careers. |

The two Jaffa profiles already reach complete coverage through their ten shared childhoods plus their separate adult pools. Adding Jaffa entries only to make counts resemble other cultures would therefore violate the measured-expansion rule.

## Added backstories

| Culture | Def | Skills |
|---|---|---|
| Tau'ri / SGC | `SG1_TauriSGC_PlanetaryGeologist` | Mining +4, Intellectual +2, Construction +1 |
| Tau'ri / SGC | `SG1_TauriSGC_ExpeditionCook` | Cooking +4, Social +2, Plants +1 |
| Tau'ri / SGC | `SG1_TauriSGC_FieldArchaeologist` | Artistic +4, Intellectual +3, Social +1 |
| Off-world human | `SG1_OffworldHuman_QuarryWorker` | Mining +4, Melee +2, Construction +1 |
| Goa'uld host | `SG1_GoauldHost_PalacePhysician` | Medicine +4, Intellectual +2, Social +1 |
| Goa'uld host | `SG1_GoauldHost_PalaceDuelist` | Melee +4, Social +2, Shooting +1 |
| System Lord | `SG1_SystemLord_DomainArchitect` | Construction +4, Mining +2, Crafting +2 |
| System Lord | `SG1_SystemLord_BiomedicalExperimenter` | Medicine +4, Intellectual +3, Crafting +1 |
| Tok'ra | `SG1_Tokra_FieldTechnician` | Crafting +4, Intellectual +2, Construction +1 |
| Tok'ra | `SG1_Tokra_CloseQuartersOperative` | Melee +4, Shooting +2, Medicine +1 |
| Tok'ra | `SG1_Tokra_CulturalAdaptationSpecialist` | Artistic +3, Cooking +3, Social +2 |

## Expected coverage after the change

All audited implemented pools cover all twelve skills:

- Tau'ri / SGC;
- Goa'uld-aligned Jaffa;
- Free Jaffa;
- off-world humans;
- ordinary Goa'uld hosts;
- Goa'uld System Lords;
- Tok'ra adulthood, independently of host origin.

The catalogue grows from `72` to `83` entries. No skill bonus exceeds the established moderate ranges and no entry adds a passion, trait, incapability or direct stat multiplier.

## Future cultures

Asgard, Nox, Unas and every later culture must receive the same audit when their first meaningful childhood and adulthood pools exist. The audit should happen after the culture has real gameplay roles, so missing skills can be closed with credible lives rather than generic filler.


## Validated `0.3.19-dev` result

The full matrix was validated on local revision `r1`:

- all eleven Defs and French translations load cleanly;
- every modified pool produces representative new entries;
- cultural name groups remain coherent;
- both generated-host origins keep their existing weights and persistent identities;
- Jaffa pools remain unchanged;
- ordinary-human starters remain vanilla-majority;
- save/load and Tok'ra personality switching remain stable;
- `Player.log` is clean.

No corrective C# or Def revision was required after the audit implementation.
