# Contextual social baseline

Version: `0.2.5-dev`

## Purpose

This milestone adds the first lightweight social layer without introducing a
large relationship framework or an Ideology dependency.

The effects are intentionally modest and contextual.

## Central identity utility

```text
GateRimSG1.Social.ContextualSocialIdentityUtility
```

The utility recognizes:

```text
Free Jaffa
Goa'uld-domain Jaffa
intrinsically marked Jaffa
active Goa'uld hosts
active Tok'ra hosts
real Goa'uld System Lord hosts
nearby same-faction System Lords
```

### Free Jaffa persistence

Free Jaffa identity is recognized from either:

```text
current faction = SG1_FreeJaffa
```

or the generated PawnKindDef:

```text
SG1_FreeJaffaWarrior
SG1_FreeJaffaGuard
```

This allows a generated Free Jaffa to retain a cultural background even if
faction allegiance changes later.

### Adult-symbiote identity

Goa'uld and Tok'ra identity is read from:

```text
SG1_GoauldHostSymbiote
+
HediffComp_GoauldSymbiote
+
GoauldSymbioteData.Origin
```

The thoughts therefore follow the persistent symbiote rather than a temporary
xenotype approximation.

## Opinion thoughts

### Free Jaffa toward active Goa'uld hosts

```text
SG1_FreeJaffaDistrustsGoauldHost
-30 opinion
```

### Tok'ra toward active Goa'uld hosts

```text
SG1_TokraSeesGoauldEnemy
-40 opinion
```

### Free Jaffa toward marked Jaffa

```text
SG1_FreeJaffaWaryOfMarkedJaffa
-8 opinion
```

The marked-Jaffa reaction stays mild because the mark can represent former
service, coercion, a chosen scar, pride or infiltration.

## Mood thought

### Goa'uld-domain Jaffa near their own System Lord

```text
SG1_DomainJaffaUnderSystemLordGaze
+2 mood
radius: 12 cells
```

The effect only activates when:

```text
observer is a Goa'uld-domain Jaffa
candidate is SG1_GoauldSystemLordHost
candidate carries an active Goa'uld symbiote
candidate belongs to the same faction
candidate is spawned within 12 cells
```

The mood value represents imposed discipline and composure, not genuine joy.

## Deferred work

- voluntary Tok'ra-host positive memories;
- more nuanced thoughts for former servants;
- reactions to prisoners and captured hosts;
- faction diplomacy events;
- optional Ideology integration.

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Spawn or obtain a Free Jaffa and an active Goa'uld host on the same map.
3. Inspect the Free Jaffa social opinion of the Goa'uld host.
4. Confirm `se méfie d'un Goa'uld` with `-30`.
5. Spawn or obtain an active Tok'ra host.
6. Inspect the Tok'ra social opinion of the Goa'uld host.
7. Confirm `voit un ennemi Goa'uld` with `-40`.
8. Place a marked Goa'uld-domain Jaffa near a Free Jaffa.
9. Confirm `se méfie d'un Jaffa marqué par les Goa'uld` with `-8`.
10. Place a Goa'uld-domain Jaffa within 12 cells of its faction's Grand Maître.
11. Confirm `sous le regard d'un Grand Maître` with `+2` mood.
12. Move the Jaffa farther than 12 cells away.
13. Confirm that the mood thought disappears after situational-thought refresh.
14. Save and reload.
15. Confirm that the same reactions remain available.
