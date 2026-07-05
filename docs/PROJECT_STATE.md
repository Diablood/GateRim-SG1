# Project state

Current milestone: `0.3.74-dev - Add distinctive Jaffa capture-officer appearance`

- Final local revision: `r2`.
- Forced build `0.3.74.0`, static checks and focused functional validation are
  complete.
- The milestone branch is integrated by fast-forward into `develop`; annotated
  tag `v0.3.74-dev` and the updated wiki sources are published.

## Repository state

- Published starting point: `develop` exactly aligned with annotated tag
  `v0.3.73-dev` at commit
  `a2bfd3a96b7cc1a12f0b985ea34f3fafb2efbe5b`.
- Final milestone branch:
  `feature/distinctive-jaffa-capture-officer-appearance`.
- Published development version: `0.3.74-dev`.
- Technical assembly version: `0.3.74.0`.
- `develop` and annotated tag `v0.3.74-dev` identify the same final commit.
- `main` remains reserved for the first stable `1.0.0` line.

## Published scope

- Add dedicated final Def names and texture paths for the capture officer's
  torso armor and deployed/retracted helmet pair.
- Use temporary red-tinted copies of the current heavy Jaffa armor and
  retractable helmet silhouettes; a later art pass may replace only the PNG
  contents without changing Defs, paths, saves or generation logic.
- Reserve the distinctive set to `SG1_GoauldJaffaOfficer`; ordinary warriors,
  guards and escorts retain their standard brown-and-gold equipment.
- Preserve heavy-armor protection, movement offset, helmet armor, coverage and
  automatic/manual retraction behavior.
- Add `SocialImpact +0.10` only to `SG1_JaffaOfficerArmor`.
- Gate the two craftable visible pieces behind `SG1_JaffaArmor`; keep the
  retracted state internal and non-craftable.
- Keep the officer pieces at `generateCommonality = 0` and guarantee them
  explicitly immediately after mission-target generation.
- Abort encounter initialization cleanly if either required distinctive piece
  cannot be equipped.
- Generalize the retractable-helmet component through per-comp deployed and
  retracted Def references while retaining the original standard pair as the
  fallback.
- Correct the roadmap debt for the faction and operation world-icon overhauls
  already published in `0.3.50-dev` and `0.3.51-dev`.
- Record a future separate expansion for officers in suitable natural Goa'uld
  raids, settlement defenses and missions: at least five Jaffa, zero or one
  officer, replacement within the existing threat budget.
- Add no new save field, mission phase, combat bonus, extraction rule, faction
  behavior or world-site behavior.

## Validation result

Final cumulative revision `r2` is validated:

- forced build succeeds with assembly `0.3.74.0`;
- duration audit still passes with `104` unique compatibility keys;
- global project-consistency check passes;
- `SG1_JaffaArmor` unlocks the officer armor and deployed helmet bills while
  the retracted state remains non-craftable;
- the mission-generated officer receives his weapon, red torso armor, red
  retractable helmet, gauntlets and reinforced boots;
- the escort retains standard brown-and-gold Jaffa equipment;
- the torso armor displays `Social impact +10%` and the helmet adds no duplicate
  social offset;
- automatic, always-deployed and always-retracted modes remain inside the
  officer helmet pair, while standard helmets retain their historical pair;
- save/reload preserves the exact apparel and helmet mode;
- capture, caravan transport and Tok'ra extraction remain functional;
- `Player.log` contains no new C#, Harmony, XML, DefOf, texture, rendering,
  apparel-generation, Scribe, caravan or mission error.

## Next step

No `0.3.75-dev` gameplay milestone or branch is reserved. Select the next
explicitly decided item from `docs/ROADMAP.md`, create its dedicated branch from
`develop` aligned with `v0.3.74-dev`, and update this handoff before
implementation.
