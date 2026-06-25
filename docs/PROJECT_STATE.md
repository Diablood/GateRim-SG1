# Current project state

Current milestone: `0.3.44-dev - Add Free Jaffa military aid` — final local revision `r1` validated and published.

## Repository state

- Starting tag: `v0.3.43-dev`.
- Published branch: `feature/free-jaffa-military-aid`.
- Final tag: `v0.3.44-dev`.
- Published versions: `0.3.44-dev` and `0.3.44.0`.
- Final local revision: `r1`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

The visible Free Jaffa faction can answer RimWorld's ordinary military-aid request once relations reach the allied threshold.

The feature deliberately reuses:

- the powered communications-console interaction;
- vanilla faction-relation requirements;
- vanilla goodwill cost and request cooldown;
- vanilla arrival, combat and departure behavior;
- the existing Free Jaffa `Combat` pawn-group profile.

The generated force uses `SG1_FreeJaffaWarrior` and `SG1_FreeJaffaGuard`, with their existing Jaffa xenotype, Prim'ta, Ma'Tok equipment, modular armor and Free Jaffa cultural identity.

## Deliberate limits

This milestone does not add:

- a custom aid incident or mission;
- a dedicated aid currency or trust system;
- quest sites or faction quests;
- natural Free Jaffa raids;
- sieges or staged attacks;
- new PawnKinds, weapons, armor, textures or persistent save data.

The exact force size, arrival method, goodwill cost and cooldown remain governed by RimWorld's ordinary military-aid flow rather than hard-coded GateRim SG-1 values.

## Final validation

- `check-project-consistency.cmd` passed and the rebuilt assembly reported version `0.3.44.0`;
- the request remained unavailable at neutral or hostile relations and appeared at allied status through a powered communications console;
- goodwill cost, repeated-request restrictions and refusal messages followed the vanilla diplomatic flow;
- the arriving force contained coherent Free Jaffa warriors and guards with Jaffa biology, culture and equipment;
- arrival, combat, losses and departure remained under vanilla control without GateRim-specific orders or persistent state;
- save/reload during the intervention preserved faction, pawns and behavior;
- quest sites, natural raids, sieges and staged attacks remained disabled;
- the existing Free Jaffa trade network and peaceful visitors remained functional;
- `Player.log` contained no new GateRim SG-1 error.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.44-dev` on a new dedicated branch.
