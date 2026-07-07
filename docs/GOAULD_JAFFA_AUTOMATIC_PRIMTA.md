# Automatic Prim'ta provisioning for Goa'uld-aligned Jaffa

Version: `0.1.63-dev`

## Scope

This milestone gives one initial `SG1_JaffaPrimta` Hediff automatically to
generated Goa'uld-aligned Jaffa servants:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`

The behavior is implemented by
`GameComponent_GoauldJaffaPrimtaInitializer`.

## Design rules

- Only the two System Lord prototype pawn kinds are targeted.
- Each generated pawn is initialized once and recorded by persistent
  `ThingID`.
- A pawn that already has a Prim'ta is registered without receiving a
  duplicate.
- Removing a Prim'ta later does not create an artificial replacement.
- Existing medical implantation, dependency and tretonin systems remain
  unchanged.
- At the `0.1.63-dev` milestone, natural Goa'uld raids, settlements and traders
  were still disabled. Later milestone documents own their current state.

## Historical manual test checklist

1. Build the mod and start RimWorld with developer mode enabled.
2. Confirm that no new C# or DefOf initialization error is reported.
3. Spawn `SG1_GoauldJaffaWarrior`.
4. Wait briefly, then confirm that the pawn has the inherited Jaffa xenotype
   and the `Prim'ta symbiote` health state.
5. Spawn `SG1_GoauldJaffaGuard` and confirm the same behavior.
6. Save and reload, then verify that both Prim'ta states remain present.
7. Remove the Prim'ta manually from one generated servant with developer
   tools.
8. Wait several seconds and verify that the Prim'ta is not recreated.
9. Advance at least one in-game hour and confirm that the ordinary Jaffa
   dependency system can start for the servant that remains without a
   Prim'ta.
10. For the original milestone only, confirm that no natural Goa'uld raid,
    settlement or trader was enabled by Prim'ta initialization itself.
## Player-starter path since 0.3.87-dev

A Jaffa selected through the vanilla starting-pawn xenotype control uses an
ordinary player PawnKind and therefore does not match the historical generated
Jaffa PawnKind list. The hidden cultural starter scenario part now adds one
`SG1_JaffaPrimta` during `PawnGenerationContext.PlayerStarter`, before the pawn
is displayed.

The existing age, compatibility and duplicate checks are reused. The hidden
scenario part resets its non-serialized ThingID guard at the beginning of each
starter-generation cycle and arms it only after the Jaffa path is resolved.
This covers a reroll even if RimWorld reuses the same pawn identifier, while
deliberately removing the Prim'ta from an already displayed pawn with another
mod is not reversed at game start or on load.
