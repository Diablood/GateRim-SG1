# Project state

Current milestone: `0.3.75-dev - Add Jaffa officers to eligible Goa'uld forces`

- Final local revision: `r2`.
- Revision `r1` failed to compile because two raid workers attempted to override
  a non-overridable `IncidentWorker_RaidEnemy.GeneratePawns` path.
- Revision `r2` moves those replacements to the existing narrowly scoped
  `PawnGroupMakerUtility.GeneratePawns` Harmony postfix and is functionally
  validated.
- The milestone branch is integrated by fast-forward into `develop`; annotated
  tag `v0.3.75-dev` and the updated wiki sources are published.

## Repository state

- Published starting point: `develop` exactly aligned with annotated tag
  `v0.3.74-dev` at commit
  `5f5315416a3ec4c740ff15b39bf1143ff11fef4c`.
- Final milestone branch: `feature/jaffa-officers-in-goauld-forces`.
- Published development version: `0.3.75-dev`.
- Technical assembly version: `0.3.75.0`.
- `develop` and annotated tag `v0.3.75-dev` identify the same final commit.
- `main` remains reserved for the first stable `1.0.0` line.

## Published scope

- Extend the published red-armored officer rank beyond the capture target.
- Require at least five eligible Jaffa in one generated force before an officer
  can appear.
- Replace at most one budget-equivalent guard; never add an extra pawn.
- Preserve the capture target `SG1_GoauldJaffaOfficer` at its historical `165`
  combat power.
- Add `SG1_GoauldJaffaFieldOfficer` at `145` combat power for combat and mission
  groups, matching the ordinary combat guard it replaces.
- Add `SG1_GoauldSettlementJaffaOfficer` at `130` combat power for settlement
  groups, matching the settlement guard it replaces.
- Cover ordinary natural Goa'uld raids, Goa'uld settlement defense groups,
  Tok'ra introduction defenders, distress-call hostile forces, relay defenders
  and reinforcements, delivery interceptions and diversion assaults.
- Scope combat-group replacement only while the natural-raid or diversion
  worker holds an explicit generation context for the expected pawn-group kind.
- Restore the previous context after every generation attempt so unrelated pawn
  generation cannot inherit the replacement.
- Keep the capture operation at exactly one dedicated target officer and do not
  add another officer to its escort.
- Keep controlled raids, extraction reprisals and open-conflict battlefields
  unchanged.
- Guarantee the officer torso armor, retractable helmet, elite silver mark,
  Prim'ta and Goa'uld Jaffa cultural profile.
- Add no save field, incident frequency, doctrine weight, mission phase,
  additional pawn or free threat budget.

## Validation result

Final cumulative revision `r2` is validated:

- forced build succeeds with assembly `0.3.75.0`;
- duration audit still passes with `104` unique compatibility keys;
- global project-consistency check passes;
- an eligible natural raid can replace exactly one `145`-point guard with one
  `145`-point field officer without changing group size;
- groups below five eligible Jaffa remain unchanged;
- an eligible Goa'uld settlement group can replace at most one `130`-point
  settlement guard with one `130`-point settlement officer;
- adapted hostile missions use the same threshold and one-officer cap;
- the capture operation retains only its historical `165`-point target officer
  and adds no officer to the escort;
- controlled raids, extraction reprisals and inter-domain battlefields remain
  outside the replacement layer;
- save/reload preserves the PawnKind, Prim'ta, silver mark, red apparel and
  retractable-helmet mode;
- `Player.log` contains no new C#, Harmony, XML, DefOf, pawn-group, apparel,
  rendering, Scribe, raid or mission error.

## Next step

No `0.3.76-dev` gameplay milestone or branch is reserved. Select the next
explicitly decided item from `docs/ROADMAP.md`, create its dedicated branch from
`develop` aligned with `v0.3.75-dev`, and update this handoff before
implementation.
