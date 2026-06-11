# Controlled Goa'uld Jaffa abduction raid

Version: `0.1.71-dev`

## Scope

This milestone adds a second developer-only incident:

```text
SG1_GoauldJaffaControlledAbductionRaid
```

Its storyteller base chance is exactly `0`. Natural Goa'uld raids,
settlements and traders remain disabled.

## Doctrine

The raid uses the dedicated strategy:

```text
SG1_GoauldJaffaAbductionAssault
```

and the custom lord job:

```text
LordJob_GoauldJaffaAbductionAssault
```

## Assault and capture windows

The group begins in a vanilla `LordToil_AssaultColony` state.

A Goa'uld-specific trigger checks every `30` ticks for a nearby admissible
downed colonist that an available Jaffa can safely recover. When the first
opportunity appears, the group enters a vanilla `LordToil_KidnapCover`
state and starts a `2400`-tick capture window.

During that capture window:

- Jaffa with nearby admissible downed colonists receive the vanilla
  `Kidnap` duty;
- other Jaffa keep the vanilla `AssaultColony` duty;
- capture opportunities are reevaluated periodically by the vanilla toil.

At the capture deadline, the group switches to a second
`LordToil_KidnapCover` with `cover = false`. Pawns then extract victims
where possible and leave the map otherwise.

If no victim ever becomes available, a separate `12000`-tick deadline
forces extraction without captives and prevents an endless assault.

## Why reuse vanilla kidnap toils

Vanilla already provides the desired division of labor:

- opportunistic kidnappers take downed humanlike player pawns;
- remaining raiders cover the extraction by continuing the assault;
- target assignment is reevaluated over time;
- extraction duties leave the map when no victim is available.

The mod adds only the Goa'uld-specific strategy selection and the explicit
extraction deadline.

## Direct-assault separation

The existing controlled direct-assault incident now sets:

```csharp
parms.canSteal = false;
parms.canKidnap = false;
```

It remains a pure military baseline.

## Manual test checklist

1. Build the mod and start RimWorld with developer mode enabled.
2. Confirm that no new XML, DefOf or C# loading error appears.
3. Trigger:
   `Do incident (Map) > controlled Goa'uld Jaffa abduction test raid`.
4. Confirm that hostile Jaffa arrive from the map edge and attack normally.
5. Down at least one player colonist near an available Jaffa.
6. Confirm that one Jaffa attempts to carry the downed colonist away while
   unassigned Jaffa continue fighting.
7. Down a second colonist during the capture window and confirm that another
   available Jaffa may attempt a second kidnapping.
8. Let `2400` ticks pass after the first admissible victim appears and
   confirm that surviving Jaffa retreat with or without victims.
9. Run another abduction raid without downing any colonist. Confirm that the
   Jaffa continue fighting substantially longer and retreat only after the
   separate `12000`-tick deadline.
10. Trigger the points variant and repeat the same validation with a larger
    group.
11. Trigger the original controlled direct-assault incident and confirm that
    it no longer kidnaps or steals.
12. Confirm that natural Goa'uld raids remain disabled.

## r2 capture-window trigger correction

The capture-window trigger no longer requires the probing Jaffa to already be
outside `GenAI.InDangerousCombat`.

That safety check belongs to vanilla `LordToil_KidnapCover`, which applies it
when assigning an individual pawn to the `Kidnap` duty. Keeping the same check
in the transition trigger prevented the raid from ever entering its capture
phase during an active firefight.

The corrected sequence is:

1. detect a reachable nearby downed colonist;
2. enter the cover-and-capture phase immediately;
3. let vanilla `LordToil_KidnapCover` assign only safe available kidnappers;
4. keep the other Jaffa on `AssaultColony`.
