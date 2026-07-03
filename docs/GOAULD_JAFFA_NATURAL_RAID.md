# Natural Goa'uld Jaffa direct-assault raid

Version: `0.2.1-dev`

## Purpose

This milestone enables the first natural Goa'uld hostile encounter in normal
play without activating every validated raid doctrine at once.

```text
SG1_GoauldJaffaNaturalRaid
```

## Storyteller tuning

```text
baseChance: 0.08
earliestDay: 12
minRefireDays: 18
category: ThreatBig
target: Map_PlayerHome
```

The incident remains deliberately uncommon.

## Reused direct-assault path

```text
GateRimSG1.Goauld.IncidentWorker_GoauldJaffaNaturalRaid
```

The worker subclasses the controlled direct-assault worker and keeps:

```text
ImmediateAttack
canSteal = false
canKidnap = false
canTimeoutOrFlee = true
```

The faction is set explicitly before delegating to vanilla
`IncidentWorker_RaidEnemy`.

Since `0.3.53-dev-r2`, the shared worker also forces vanilla `EdgeWalkIn`.
Higher threat budgets can increase the force but never switch Goa'uld/Jaffa
raids to transport-pod arrivals.

## Why generic vanilla faction selection stays blocked

The Goa'uld faction keeps:

```xml
<raidsForbidden>true</raidsForbidden>
```

This is intentional. World settlements should be visible, but the first
playable baseline should not allow vanilla faction selection to choose
unreviewed raid strategies or opportunistic behaviors.

The dedicated incident is the only natural Goa'uld raid route in this
milestone.

## Deferred doctrines

Still developer-only:

```text
SG1_GoauldJaffaControlledAbductionRaid
SG1_GoauldJaffaControlledDestructionRaid
```

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Generate a new world with the `Équipe SG isolée` scenario.
3. Confirm that one hostile Goa'uld world faction exists with visible
   settlements.
4. Confirm that its French label is `Domaines des Grands Maîtres Goa'uld`.
5. Trigger `raid de Jaffa Goa'uld` through developer incident tools for a fast
   validation, or let the storyteller fire it naturally after day 12.
6. Confirm that the arriving Jaffa use Ma'Tok staffs and modular armor.
7. Confirm that the raid remains a direct assault without opportunistic
   stealing or kidnapping.
8. Recover a Ma'Tok from a defeated Jaffa.
9. Trigger the three controlled developer raids and confirm no regression.
10. Save and reload the colony.
