# Goa'uld host foundation

## Prototype definitions

```text
SG1_GoauldHost
SG1_NaquadahBlood
SG1_GoauldLongevity
```

## Scope of 0.1.7-dev

This milestone introduces an XML-only xenotype for a humanoid already possessed by an adult Goa'uld symbiote.

It intentionally does not yet simulate:

- a free symbiote creature;
- forced implantation;
- ritual implantation;
- extraction;
- transfer between hosts;
- host identity suppression;
- named System Lords;
- sarcophagus addiction or degradation.

## Current prototype effects

| Gene | Effect | Purpose |
|---|---:|---|
| `SG1_NaquadahBlood` | Persistent acquired marker | Shared biological eligibility for naquadah-reactive technology; retained after extraction |
| `SG1_GoauldLongevity` | `LifespanFactor ×5` | Provisional 500% lifespan expectancy |
| `Immunity_Strong` | Vanilla gene | Improved disease resistance |
| `WoundHealing_Fast` | Vanilla gene | Accelerated recovery |
| `Pain_Reduced` | Vanilla gene | Better combat endurance |
| `Robust` | Vanilla gene | Improved resilience |
| `MeleeDamage_Strong` | Vanilla gene | Physical enhancement |
| `StrongStomach` | Vanilla gene | Improved digestive resilience |

## Balance note

`LifespanFactor ×5` is a provisional value for a standard implanted host. It represents a much longer life without treating every host as effectively immortal. Sarcophagus use and exceptional named Goa'uld can be balanced separately later.

## Current architecture note

Since later milestones, the functional possession architecture is carried by
the persistent `SG1_GoauldHostSymbiote` Hediff rather than by forcing this
legacy xenotype onto every active host.

Since `0.2.3-dev`, naturally generated Goa'uld host castes also use that
persistent Hediff architecture. The xenotype remains available as an early
prototype and manual-test artifact.

## Future architecture

The final system should follow this cycle:

```text
Free symbiote
    ↓ forced attack or controlled ritual
Recent implantation
    ↓ critical period
Active Goa'uld host
    ↓ extraction, host death or transfer
Free symbiote or new host
```

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Open the xenotype editor.
3. Confirm that `Goa'uld host` appears.
4. Confirm that `naquadah in the blood` and `Goa'uld host longevity` appear.
5. Confirm that lifespan expectancy displays as `500%`.
6. Switch to French.
7. Confirm that the new xenotype and genes are translated.
8. Check `Player.log` for `SG1_GoauldHost`, `SG1_NaquadahBlood` or `SG1_GoauldLongevity` errors.


## Final xenotype artwork

Since `0.3.86-dev`, `SG1_GoauldHost` uses the maintainer-approved symbolic
Goa'uld symbiote icon at the stable path:

```text
Textures/UI/Xenotypes/SG1_GoauldHost.png
```

The icon represents the defining organism rather than a human forehead mark:
once the symbiote is removed, the former host returns to ordinary human
identity. The two custom Goa'uld gene icons remain temporary under:

```text
Textures/UI/Genes/
```

## Persistent biological traces since 0.3.58-dev

The legacy xenotype Def still contains `SG1_NaquadahBlood`, but the same marker
is now granted by adult-symbiote and Prim'ta lifecycle code. It remains after
extraction and is used by the kara kesh through `NaquadahTraceUtility`.
