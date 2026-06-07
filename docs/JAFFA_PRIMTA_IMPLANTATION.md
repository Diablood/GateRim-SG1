# Jaffa Prim'ta implantation prototype

## Scope of 0.1.26-dev

This milestone adds the first planifiable Jaffa Prim'ta implantation procedure.

## Operation

```text
SG1_ImplantJaffaPrimta
```

Player-facing label:

```text
implant Jaffa Prim'ta
```

French label:

```text
implanter un Prim'ta jaffa
```

## Worker

```text
Recipe_ImplantJaffaPrimta
```

The worker derives from vanilla:

```text
Recipe_Surgery
```

and uses the normal surgery-failure path before adding:

```text
SG1_JaffaPrimta
```

## Eligibility

The patient must carry all three active inherited compatibility genes:

```text
SG1_JaffaLineage
SG1_JaffaPouchPotential
SG1_JaffaSymbioteCompatibility
```

The operation is unavailable when `SG1_JaffaPrimta` is already present.

## Initial balance

| Property | Value |
|---|---:|
| Medicine skill | `4` |
| Medicine | `1` unit |
| Work amount | `900` |
| Surgery success factor | `1` |
| Death chance on failure | `0.005` |
| Specific body part | None |

## Prototype limitation

A dedicated physical immature-symbiote resource does not exist yet.

The current medicine requirement represents the controlled procedure only.
Future milestones will add:

```text
Prim'ta larva resource
age or life-stage ceremony
removal consequences
dependency
tretonin substitution
Goa'uld-controlled and Free Jaffa variations
```

## Logs

```text
Attached Jaffa Prim'ta symbiote to <pawn>.
Loaded Jaffa Prim'ta symbiote for <pawn>.
Removed Jaffa Prim'ta symbiote from <pawn>.
Jaffa Prim'ta implantation surgery completed for <pawn> with surgeon <pawn>.
Jaffa Prim'ta implantation surgery failed for <pawn>.
```

## Test checklist

1. Build with `build.cmd`.
2. Spawn a newly generated Jaffa.
3. Open the health tab.
4. Schedule:
   ```text
   implant Jaffa Prim'ta
   ```
5. Provide one medicine and a doctor with Medicine `4+`.
6. Let the operation complete.
7. Confirm `Prim'ta symbiote` appears.
8. Confirm immunity, healing, resistance, lifespan and pain modifiers.
9. Save and reload.
10. Confirm the Hediff remains present and the load log appears.
11. Confirm the operation no longer appears while Prim'ta is present.
12. Try a baseliner and confirm the operation is unavailable.
13. Remove the Hediff in developer mode and confirm the removal log.
