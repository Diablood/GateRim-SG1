# Persistent Goa'uld symbiote data

## Scope of 0.1.16-dev

This milestone adds the first persistent C# state for adult Goa'uld symbiotes.

## C# classes

```text
GoauldSymbioteData
HediffComp_GoauldSymbiote
HediffCompProperties_GoauldSymbiote
```

## Stored values

| Field | Purpose |
|---|---|
| `symbioteId` | Unique persistent identity |
| `symbioteName` | Reserved for named symbiotes |
| `origin` | Goa'uld or future Tok'ra origin |
| `biologicalAgeTicks` | Reserved biological age |
| `createdAtTick` | Identity creation tick |
| `implantationTick` | First implantation tick |
| `lastDetachTick` | Last detachment tick |
| `currentHostThingId` | Current host identifier |
| `previousHostThingId` | Previous host identifier |

## Carrier Hediffs

The persistence component is currently attached to:

```text
SG1_GoauldRecentImplantation
SG1_GoauldHostSymbiote
```

`SG1_GoauldHostSymbiote` is a permanent manual-test carrier for save/load validation.

## Current test flow

1. Add `adult Goa'uld symbiote` manually to a humanoid pawn.
2. Open the health-state description and record the symbiote ID.
3. Save the game.
4. Reload the save.
5. Confirm that the same symbiote ID remains visible.
6. Inspect `Player.log` for the matching `Attached` and `Loaded` messages.

## Future work

- transfer the same data object from recent implantation to active possession;
- store the free-symbiote identity before implantation;
- survive host death and extraction;
- add named System Lords;
- add Tok'ra origin and voluntary symbiosis.


## Free-symbiote transfer prototype

Since `0.1.17-dev`, a free symbiote pawn carries its own persistent data through:

```text
Comp_GoauldForcedImplantation
```

When the manual forced-implantation action succeeds, the same `GoauldSymbioteData` object is injected into `SG1_GoauldRecentImplantation` before the free pawn is destroyed.


## Active-host conversion

Since `0.1.18-dev`, `SG1_GoauldRecentImplantation` automatically transfers its `GoauldSymbioteData` object into `SG1_GoauldHostSymbiote` immediately before expiry.

The temporary carrier marks its data as transferred-out so its removal does not clear the active identity.


## Reverse transfer during emergency extraction

Since `0.1.19-dev`, a recent-implantation host can return its persistent
`GoauldSymbioteData` object into a newly generated free symbiote pawn through the
manual `Emergency extraction` command.

The same ID should survive:

```text
free pawn
recent implantation
emergency extraction
free pawn again
save and reload
```
