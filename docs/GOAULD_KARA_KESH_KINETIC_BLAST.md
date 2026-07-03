# Kara kesh kinetic blast

## Milestone

`0.3.59-dev - Add kara kesh kinetic blast`

## Design contract

The kinetic blast is the second implemented kara kesh mode after the personal
shield. It is intentionally a focused single-target action rather than a new
weapon, area explosion or generic psychic ability.

Only a wearer accepted by `NaquadahTraceUtility` can activate it. The player
receives a targeted gizmo; hostile non-player wearers use the same method
automatically against the closest valid hostile pawn.

Balance values are Def-driven through `CompProperties_KaraKeshShield`:

| Value | Current setting |
|---|---:|
| Range | `10.9` cells |
| Shared shield-energy cost | `1.25` |
| Cooldown | `900` ticks |
| Blunt damage | `12` |
| Armor penetration | `0.25` |
| Stun | `120` ticks |
| Maximum knockback | `2` cells |
| Hostile AI check interval | `60` ticks |

The four-point shield can therefore power three blasts from full charge only if
it does not absorb incoming damage or recharge between activations. Every blast
also starts the existing `300`-tick recharge pause. Attack and defense are thus
a shared resource rather than two independent advantages.

## Targeting and knockback

A target must be:

- a living spawned pawn;
- hostile to the wearer;
- on the same map;
- within configured range;
- in direct line of sight.

The blast applies direct blunt damage, then attempts to move the target directly
away from the wearer. Each candidate cell must remain inside the map, walkable
and free of another pawn. The search stops at the first invalid cell. The target
is never forced through walls, doors that are not walkable, occupied cells or
the map edge.

A target that survives is stunned after relocation. The flash and floating text
make the otherwise instantaneous direct effect readable in combat.

## Shield integration

`Comp_KaraKeshShield` remains the single component for both modes. The blast
checks `ShieldState.Active`, current public shield energy and the persistent
cooldown. Energy is subtracted from the protected native `CompShield.energy` field and
`KeepDisplaying()` refreshes the vanilla indicator. The native energy gizmo,
collapse behavior and recharge remain authoritative.

The kinetic mode is not an ordinary weapon verb, so it can fire from inside the
wearer's own active field. Ordinary ranged weapons remain blocked by native
`CompShield.CompAllowVerbCast`.

The cooldown is serialized as `karaKeshLastKineticBlastTick`. Existing saves
without the value default to ready.

## AI use

A hostile non-player wearer checks every `60` ticks. It selects the nearest pawn
that passes the exact player targeting rules and invokes the same blast method.
No separate damage or balance path exists for AI.

The System Lord profile rises from `400` to `450` `combatPower` to account for
the new short-range control and damage while preserving the energy tradeoff.

## Debug support

Use exactly:

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Available actions:

- `Inspect kinetic blast state`;
- `Prepare kinetic blast test state` (full energy, active shield, no cooldown);
- `Use selected wearer's kinetic blast`;
- `Reset kinetic blast cooldown`;
- the existing kara kesh spawn, hostile System Lord and shield-impact actions.

For `Use selected wearer's kinetic blast`, first select the pawn wearing the
kara kesh, then activate the debug tool and click the hostile target.

## Deferred functions

This milestone did not add neural pain, prolonged paralysis, torture,
telekinesis against objects or buildings, remote commands, area damage or a
separate research project. Temporary neural agony is implemented separately in
`0.3.60-dev`; the remaining functions still require independent gameplay slices.

## Validation and publication result

The first `r1` rebuild exposed a compile-only API mismatch: RimWorld 1.6
returns `IReadOnlyList<Pawn>` from `MapPawns.AllPawnsSpawned`. Final local
revision `r2` uses the actual interface type for the hostile AI scan without
changing behavior, balance or save data.

After a forced `0.3.59.0` rebuild, `r2` passed the complete targeted checklist:
player targeting, the shared `1.25` energy cost, the `900`-tick cooldown,
knockback in open and obstructed terrain, autonomous hostile AI use and
save/reload persistence all behaved as documented. Biological refusal remained
intact and `Player.log` contained no new XML, Def, Scribe or C# error.

The validated `r2` state was published on
`feature/kara-kesh-kinetic-blast` with annotated tag `v0.3.59-dev`; the
separate player wiki was synchronized in the same publication.
