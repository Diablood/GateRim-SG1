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
