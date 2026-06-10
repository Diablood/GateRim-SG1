# Goa'uld-aligned Jaffa pawn kinds

Version: `0.1.62-dev`

## Scope

This milestone introduces two first Goa'uld-aligned Jaffa `PawnKindDef`
prototypes:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`

Both pawn kinds force the existing inheritable `SG1_Jaffa` xenotype and are
exposed through the hidden `SG1_GoauldSystemLordPrototype` faction's nested
`Combat` pawn-group profile.

## Intentionally deferred

The faction still does not generate naturally. Settlements, raids and
traders remain disabled. The first Jaffa servants also do not yet receive
automatic Prim'ta provisioning, faction-specific visual markings, dedicated
armor or Ma'Tok equipment.

## Manual test checklist

1. Start RimWorld with developer mode enabled and verify that no new XML
   loading error is reported.
2. Spawn `SG1_GoauldJaffaWarrior` from developer tools.
3. Spawn `SG1_GoauldJaffaGuard` from developer tools.
4. Verify that both generated pawns are humanlike Jaffa and expose the
   expected inherited Jaffa xenotype.
5. When a developer tool permits group generation for the hidden faction,
   optionally generate a `Combat` group and confirm that it is made only of
   the two new Jaffa pawn kinds.
6. Confirm that no natural Goa'uld raid, settlement or trader has been
   enabled by this milestone.
