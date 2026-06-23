# Goa'uld-aligned Jaffa pawn kinds

Version: `0.1.62-dev`

## Scope

This milestone introduces two first Goa'uld-aligned Jaffa `PawnKindDef`
prototypes:

- `SG1_GoauldJaffaWarrior`
- `SG1_GoauldJaffaGuard`

Both pawn kinds force the existing inheritable `SG1_Jaffa` xenotype and are
exposed through the visible `SG1_GoauldSystemLordPrototype` faction's
`Combat` and `Settlement` pawn-group profiles.

## Current integration

Since later milestones, the Goa'uld world faction generates visible
settlements and rare natural direct-assault raids. Its servants receive:

```text
automatic Prim'ta
Ma'Tok
modular armor
retractable helmet
automatic Goa'uld-domain forehead mark
```

Free Jaffa use separate PawnKindDefs since `0.2.2-dev` and intentionally
remain without an automatic forehead mark.

## Mission-only breacher (`0.3.34-dev`)

The Tok'ra diversion assault adds `SG1_GoauldJaffaBreacher` as a specialized
child of `SG1_GoauldJaffaWarriorBase`. It is marked `canBeSapper` and
`isGoodBreacher`, keeps the inherited Ma'Tok-only weapon tag and is limited to
four pawns per generated group.

This pawn kind appears only in the explicit `SG1_TokraDiversionAssault`
pawn-group kind. The normal `Combat` and `Settlement` profiles remain
unchanged, so ordinary Goa'uld raids and domain populations do not gain the
specialized breacher.

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
