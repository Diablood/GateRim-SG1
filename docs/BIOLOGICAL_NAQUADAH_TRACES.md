# Persistent biological naquadah traces

## Milestone

`0.3.58-dev - Add persistent biological naquadah traces`

## Design contract

Biological naquadah eligibility is an acquired and persistent state. It must not
be inferred from faction, hostility, PawnKind or current social allegiance.

A humanoid receives `SG1_NaquadahBlood` when carrying either:

- a recent or active adult Goa'uld-family symbiote, including Goa'uld and
  Tok'ra origins;
- an implanted Jaffa Prim'ta.

Once granted, the marker remains after adult symbiote extraction or Prim'ta
removal. This covers former hosts and prevents a technology from becoming
unusable merely because the organism that created the trace has left.

The legacy `SG1_GoauldHost` xenotype already contains the same Def and therefore
remains compatible without duplicate genes.

## Architecture

`NaquadahTraceUtility` is the authoritative service:

- `HasPersistentTrace` reads the stable gene marker;
- `HasActiveAdultSymbiote` and `HasActivePrimta` identify current biological
  sources;
- `EnsurePersistentTrace` grants the acquired xenogene idempotently;
- `CanActivateNaquadahTechnology` is the reusable technology-access contract.

Lifecycle hooks apply the marker immediately in
`HediffComp_GoauldSymbiote` and `HediffComp_JaffaPrimta`, including immediately
before removal. `GameComponent_NaquadahTraceReconciler` is the migration and
recovery layer for older saves, map pawns, player caravans, faction leaders and
world pawns.

Normal gameplay has no removal path. The developer-only removal action exists
only to test reconciliation and inactive technology behavior.

## Kara kesh integration

The kara kesh remains ordinary apparel for hauling, storage, capture and
wearing. Its `Comp_KaraKeshShield` checks the shared service before delegating
to native `CompShield` behavior.

Without traces:

- incoming damage is not absorbed;
- the shield bubble and energy gizmo are hidden;
- recharge and reset progression are paused;
- outgoing ranged weapons are not blocked.

With traces, the complete validated `0.3.57-dev` shield behavior is unchanged.
A device may retain stored charge while inactive; it resumes native behavior
when an eligible wearer activates it.

## Save compatibility

The stable Def name remains `SG1_NaquadahBlood`. Existing pawns already carrying
that gene are immediately eligible. Existing adult hosts and Prim'ta carriers
without the old prototype gene receive it during load reconciliation.

No serialized parallel list is introduced. The gene itself is the durable save
state, while the game component is stateless and idempotent.

## Validation result

Use exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

Final local revision `r1` passed the complete targeted checklist: an untraced
kara kesh wearer remained inactive, manual acquisition activated the shield,
adult-host and Prim'ta traces persisted after removal and save/reload, and
`Player.log` remained clean. The durable procedure remains in
`docs/TESTING_CURRENT.md` and `docs/TESTING.md`. The validated revision is published on
`feature/persistent-naquadah-biological-traces` with annotated tag
`v0.3.58-dev`, and the separate wiki is synchronized.

## Deferred work

This milestone does not add nearby-symbiote sensing, naquadah poisoning,
quantity or decay of traces, offensive kara kesh functions, or gene-extractor
restrictions. The visible acquired xenogene remains the compatibility choice
for this slice and should be audited separately if Biotech gene manipulation
creates an exploitable duplication path.
