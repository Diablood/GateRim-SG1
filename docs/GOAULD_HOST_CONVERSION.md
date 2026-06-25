# Active Goa'uld host conversion

## Current conversion loop

```text
Free symbiote
    ↓ implantation
Recent implantation
    ↓ one-day intervention window
Active Goa'uld host
```

`HediffComp_GoauldImplantationConversion` moves the same persistent `GoauldSymbioteData` instance from `SG1_GoauldRecentImplantation` into `SG1_GoauldHostSymbiote`. The temporary state is then removed without detaching the transferred identity.

## Hostile takeover in `0.3.40-dev`

A free symbiote now records its current faction allegiance before implantation. A pending hostile takeover is armed only when all of these conditions are true:

- the symbiote origin is Goa'uld, not Tok'ra;
- the host currently belongs to the player;
- the recorded symbiote faction is not the player faction;
- that faction is hostile to the player.

The persistent data stores:

| Field | Purpose |
|---|---|
| `allegianceFaction` | faction followed by the Goa'uld symbiote |
| `displacedHostFaction` | host faction to restore if the symbiote is released |
| `hostControlState` | `None`, `Pending` or `Active` |

When the one-day recent state converts, a pending takeover changes the existing host pawn to the recorded Goa'uld faction. The pawn is not recreated and retains its body, age, injuries, equipment, relationships, xenotype and symbiote ID.

The transition also undrafts the pawn, stops its current job, refreshes player pawn tables and sends a threat letter.

Live validation showed that faction reassignment alone makes the former colonist seek a map exit rather than attack. Local revision `r3` introduced a dedicated no-retreat assault and confirmed real hostile attacks, but a prolonged abandoned-map test showed that this version could remain indefinitely after all meaningful objectives disappeared.

Local revision `r4` assigns the converted host to `LordJob_GoauldHostTakeoverRaidAssault`, a persisted vanilla colony assault with kidnapping disabled but vanilla timeout and retreat enabled. The active host Hediff rechecks that assignment every `30` ticks. A legacy `LordJob_GoauldHostTakeoverAssault` is retained only for save loading and is automatically replaced, so existing `r3` development saves migrate without restarting the implantation.

## Extraction and recovery

Emergency extraction before conversion clears the pending takeover and leaves the host in the player faction. The extracted free symbiote retains its recorded Goa'uld allegiance.

If an active hostile host state is removed through a supported transfer or developer recovery path, the dedicated takeover assault is removed before the displaced faction is restored and the symbiote detaches. Free-symbiote placement remains transactional: a failed placement cancels transfer-out and does not silently clear the pending takeover.

## Protected paths

No hostile takeover is armed for:

- Tok'ra-origin symbiotes;
- Goa'uld symbiotes controlled by the player;
- factionless or non-hostile symbiotes;
- a host that no longer belongs to the originally displaced faction before conversion.

This logic is attached to persistent symbiote data rather than the `0.3.39-dev` incident, so other future hostile implantation routes can reuse it without duplicating the conversion system.

## Active-host biological effects

| Modifier | Current value |
|---|---:|
| `ImmunityGainSpeed` | `×1.75` |
| `InjuryHealingFactor` | `×1.75` |
| `IncomingDamageFactor` | `×0.8` |
| `LifespanFactor` | `×5` |
| Pain factor | `×0.7` |

Adult possession does not replace the host's germline xenotype.

## Final `0.3.40-dev` validation

Final local revision `r4` validated the complete hostile-takeover loop:

1. A hostile incident symbiote continuously pursued and implanted a compatible player pawn without yielding to animal flight.
2. `Pending` control preserved the same symbiote ID, hostile allegiance and displaced player faction through save/reload.
3. Active conversion removed player control, transferred the existing pawn to the Goa'uld faction and produced one threat letter.
4. The converted host attacked nearby colonists and colony property instead of seeking an immediate map exit.
5. The dedicated assault remained stable after save/reload.
6. An `r3` development save automatically migrated from the legacy no-retreat Lord to the current raid-like takeover Lord.
7. After every player colonist left the map, the host continued attacking valid remaining targets and then eventually withdrew through vanilla raid logic instead of remaining indefinitely.
8. Emergency extraction during the recent phase preserved the player host and returned the same hostile-allegiance symbiote.
9. Tok'ra conversion and player-controlled or factionless Goa'uld conversion remained excluded from hostile takeover.
10. Supported recovery removed the takeover Lord before restoring the displaced faction.
11. No new GateRim SG-1 error was reported in `Player.log`.

The published milestone is tagged `v0.3.40-dev`. Re-run the durable matrix in `docs/TESTING.md` whenever symbiote identity, faction transfer, extraction or hostile Lord behavior changes.
