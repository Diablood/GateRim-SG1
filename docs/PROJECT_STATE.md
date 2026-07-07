# Project state

Current milestone: `0.3.87-dev - Fix Jaffa and Tok'ra starter symbiotes`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.86-dev` at commit
  `bbbb981fb54131964a05b6f7626b5472cbc399dd`.
- Final branch: `fix/jaffa-tokra-starter-symbiotes`.
- Final local revision: `r3`.
- Published assembly version: `0.3.87.0`.
- Final annotated tag: `v0.3.87-dev`.
- Integration target: `develop`, by fast-forward from the validated fix branch.

## Corrected defect

The vanilla new-game configuration can replace a generated starter with a
GateRim xenotype and reroll the pawn while preserving that xenotype. This path
previously produced culturally valid but biologically incomplete starters:

- an adult `SG1_Jaffa` could appear without `SG1_JaffaPrimta` and enter Prim'ta
  dependency shortly after the game began;
- an `SG1_GoauldHost` could appear without any persistent adult symbiote,
  leaving only the enhanced xenotype;
- Tok'ra identity switching worked correctly only when a symbiote happened to
  be present.

The older automatic initializers covered dedicated non-player PawnKinds after
spawning, not ordinary player starters constrained only by xenotype.

## Final behavior

- Reuse the hidden `ScenPart_CulturalStarterProfiles` already injected into
  Def-based scenarios.
- Run only from `Notify_PawnGenerated` under
  `PawnGenerationContext.PlayerStarter`.
- Attach the biological state before the pawn is displayed on the vanilla
  starter page, so it is visible in the health preview.
- Cover the first pawn generated after xenotype selection and every later
  xenotype-constrained reroll.
- Give every eligible adult `SG1_Jaffa` starter exactly one
  `SG1_JaffaPrimta`.
- Give every `SG1_GoauldHost` starter exactly one persistent
  `SG1_GoauldHostSymbiote`:
  - Tok'ra cultural career: Tok'ra origin, a distinct generated historical
    human-host identity, dual personality and the existing switch behavior;
  - Goa'uld cultural career: Goa'uld origin, the generated Goa'uld name used
    as the symbiote identity and no Tok'ra personality switch.
- Treat Tok'ra as the same Goa'uld-family biological symbiote model while
  preserving its distinct cultural allegiance and voluntary identity behavior.
- Reject duplicates when the required Prim'ta or any persistent adult-symbiote
  component already exists.
- Keep only a non-serialized one-generation guard inside the starter scenario
  part.
- Add no first-tick, periodic, load-time or save-migration reconciliation.
- Respect a deliberate removal performed with another mod after the pawn has
  been displayed.
- Leave ordinary humans, underage Jaffa, non-player generation, implantation,
  extraction, xenotype definitions, existing saves and visuals unchanged.

## Revision history

- `r1` validated the Jaffa path and the Tok'ra dual-identity path whenever the
  symbiote was created, but some host-xenotype rerolls still lacked one.
- `r2` corrected the transient generation guard, but the same symptom remained.
- `r3` fixed the actual cause: the mixed `SG1_GoauldHost` starter profile can
  resolve to either a Tok'ra or Goa'uld career, so every result now receives a
  real adult symbiote and the final career determines its origin and controls.

## Validation result

The maintainer reports final local revision `r3` as successful.

Validated results include:

- successful build of assembly `0.3.87.0` and project checks;
- Prim'ta visible on eligible adult Jaffa starters before game confirmation;
- exactly one adult symbiote on every generated `SG1_GoauldHost` starter;
- complete first-generation and reroll behavior;
- functional Tok'ra dual identity and personality switching;
- Goa'uld-origin starters without Tok'ra personality controls;
- no duplicate biological state and no regression of the validated Jaffa path;
- no runtime or load-time reimplantation added by this milestone.

## Publication state

The final milestone state is published through one fix-branch commit,
fast-forward integration into `develop`, push of `develop`, annotated tag
`v0.3.87-dev` and synchronization of the separate wiki.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.87-dev` point to
   the same integrated commit;
2. review the remaining observed defects before selecting an improvement;
3. read `docs/ROADMAP.md` and reserve one distinct milestone;
4. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch.
