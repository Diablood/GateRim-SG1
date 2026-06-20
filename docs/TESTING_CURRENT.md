# Current milestone testing

Current milestone: `0.3.9-dev - Add configurable starter cultural profiles`.

Status: local functional validation complete on `r3`. Publication remains.

## Validated

- RimWorld loads the eight `CulturalPawnProfileDef` profiles and hidden scenario part without relevant XML, cross-reference or patch errors.
- No Harmony dependency was added.
- The project rebuild succeeds and `GateRimSG1.dll` reports version `0.3.9.0`.
- The `Équipe SG isolée` scenario always assigns one of the six configured SGC adult careers while preserving compatible ordinary childhoods.
- Jaffa starters use only configured Jaffa childhoods and Goa'uld-aligned or Free Jaffa adult careers.
- Goa'uld-host starters use only configured off-world-human childhoods and Goa'uld-host or Tok'ra adult careers.
- Mixed Jaffa and host profiles select a cultural name matching the final adulthood.
- Backstory changes apply only the expected skill-bonus difference and do not reroll the pawn's random baseline, passions or gene aptitudes.
- Ordinary human starters retain vanilla childhoods and a clear majority of vanilla adult careers.
- Tau'ri / SGC careers appear occasionally for ordinary humans through the compatible vanilla-weighted pool, without a fixed replacement percentage.
- Jaffa, Goa'uld-host and SG-team profiles remain higher priority than the ordinary-human additive profile.
- Manual name or backstory changes made after generation remain untouched in the tested flow.
- Developer-spawned pawns, raids, settlements, visitors and other world-generated pawns retain their previous cultural behavior.
- Saving, fully quitting and reloading does not reapply profile selection, names or skill adjustments.
- `Player.log` is clean for the tested scope.

## Publication identifiers

- branch: `feature/cultural-starter-profiles`;
- commit: `0.3.9-dev - add configurable starter cultural profiles`;
- final annotated tag: `v0.3.9-dev`.

The separate wiki must be synchronized because `docs/wiki/*.md` changed. `docs/TESTING.md` remains the complete historical regression archive.
