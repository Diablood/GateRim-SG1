# Current project state

Current milestone: `0.3.58-dev - Add persistent biological naquadah traces` -
validated and published after local revision `r1`.

## Repository state

- Starting tag: `v0.3.57-dev`.
- Active branch: `feature/persistent-naquadah-biological-traces`.
- Published branch: `feature/persistent-naquadah-biological-traces`.
- Last published version: `0.3.58-dev`.
- Last published tag: `v0.3.58-dev`.
- Technical assembly version: `0.3.58.0`.
- Final local revision: `r1`.
- Publication status: validated, committed, tagged and published; the separate
  wiki is synchronized.
- Next milestone: not selected. It must start from `v0.3.58-dev` on a dedicated
  branch.

## Current scope

This milestone promotes the existing `SG1_NaquadahBlood` prototype into the
shared persistent biological marker used by naquadah-reactive technology.

A humanoid gains the marker when any of these acquired states is present:

- recent or active adult Goa'uld-family symbiote hosting, including Goa'uld and
  Tok'ra identities;
- an implanted Jaffa Prim'ta;
- an existing legacy `SG1_NaquadahBlood` gene from an older prototype save.

The marker is deliberately never removed by normal gameplay. Adult symbiote
extraction and Prim'ta removal therefore leave the former host biologically
eligible. A reconciliation component scans map pawns, player caravans, faction
leaders and world pawns on load and periodically, while lifecycle hooks apply
the marker immediately to newly attached symbiotes.

`NaquadahTraceUtility` is the single gameplay service for checking eligibility.
The kara kesh now activates only for a wearer carrying the persistent marker.
An ineligible pawn may transport or wear the item, but receives no shield,
energy gizmo or outgoing-fire restriction until biological eligibility exists.

The existing gene Def and texture remain stable for save compatibility. This
revision does not add offensive kara kesh modes, nearby-symbiote detection,
naquadah poisoning, gene-extractor restrictions or new factions.

## Files and architecture

- `Source/GateRimSG1/Goauld/NaquadahTraceUtility.cs` owns marker detection,
  acquisition and technology eligibility.
- `Source/GateRimSG1/Goauld/GameComponent_NaquadahTraceReconciler.cs` migrates
  existing saves and reconciles known pawns.
- adult symbiote and Prim'ta Hediff lifecycle hooks grant the marker before
  removal can create a former host;
- `Comp_KaraKeshShield` suppresses native shield behavior for ineligible
  wearers without blocking inventory or apparel handling;
- deterministic developer actions expose inspection, marker manipulation,
  adult-symbiote and Prim'ta test states, and global reconciliation.

## Final r1 validation

Load exactly:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Use a player colonist without `naquadah in the blood`. Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

1. Run `Equip kara kesh on target` on that colonist, then run `Inspect pawn trace state` and
   confirm `persistentTrace=False` and `karaKeshEligible=False`. The vanilla
   shield-energy gizmo must be absent.
2. Open:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh shield...
```

   Run `Apply ranged test hit` on the colonist. The hit must not be absorbed.
3. Return to `Biological naquadah traces...`, run `Apply persistent trace` on
   the same colonist, then inspect again. Confirm the gene appears and the
   report shows `persistentTrace=True` and `karaKeshEligible=True`.
4. Keep the colonist selected until the vanilla `Énergie du bouclier` /
   `Shield energy` gizmo appears, then apply another ranged test hit. It must be
   absorbed without injury.
5. Run `Apply adult symbiote test state` on a second baseliner, inspect the
   marker, then run `Remove adult symbiote test state`. The marker must remain
   after removal and after save/reload.
6. Spawn or select a compatible Jaffa, run `Apply Prim'ta test state`, inspect
   the marker, then run `Remove Prim'ta test state`. The marker must remain.
7. Check `Player.log` for new XML, Def, gene, Scribe or C# errors.

Validation result: passed. Biological state, not faction or PawnKind, controls
the stable marker and kara kesh activation. The untraced wearer remained
ineligible, acquired traces activated the shield, adult-host and Prim'ta traces
persisted after removal and save/reload, and `Player.log` remained clean.

## Optional regression checks

- Spawn a hostile System Lord with the existing kara kesh action and confirm it
  still receives the marker, active shield and all `0.3.57-dev` counterplay.
- Generate or encounter a Tok'ra voluntary host and confirm reconciliation
  grants the same marker.
- Save a traced pawn in a caravan, reload and inspect the marker.
- Remove the marker from an active host with the developer action, run
  `Reconcile all known pawns` and confirm it is restored.

## Remaining uncertainty

The implementation was rebuilt against RimWorld 1.6 and validated in game on
local revision `r1`. The acquired marker remains a visible xenogene for
compatibility with the existing prototype; interaction with Biotech gene
extraction is outside this milestone and must be audited later if it becomes a
practical exploit.

## Next action

No gameplay revision `r2` is required and no `0.3.59-dev` scope is imposed by
this closure. Select the next milestone only after rereading the durable
roadmap, then create its dedicated branch directly from `v0.3.58-dev`.
