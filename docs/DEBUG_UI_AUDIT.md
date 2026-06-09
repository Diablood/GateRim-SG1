# GateRim SG-1 — Debug UI and conditional-gizmo audit

## Purpose

Before the `0.2.x` Stargate chapter, review every piece of player-facing technical information and every command added during prototyping.

The goal is to keep the normal player experience readable while preserving advanced diagnostics for development and troubleshooting.

## Visibility classes

| Class | Meaning | Typical examples |
|---|---|---|
| Always visible | Useful for normal gameplay decisions | readable remaining offer duration, relevant biological condition, trust tier when it affects an offer |
| Contextual | Visible only while the related mechanic is active | refuse a temporary Tok'ra offer, cancel an active ritual, ritual progress |
| GateRim debug | Advanced diagnostic shown only with a future mod option | persistent symbiote ID, raw expiration ticks, tracker state, runtime counters |
| RimWorld dev mode | Development or regression-test command | manual prototype spawning, unrestricted test actions |
| Remove from player UI | Internal implementation detail with no player value | raw object references, Lord references, low-level scan counters |

## Information inventory checklist

### Symbiotes and hosts

- [ ] Persistent symbiote ID
- [ ] Symbiote origin
- [ ] Host history
- [ ] Raw age placeholders
- [ ] Implantation countdown
- [ ] Offer expiration ticks
- [ ] Human-readable remaining days
- [ ] Autonomous-hunt cooldown
- [ ] Ritual target and ritual-basin references

### Tok'ra diplomacy

- [ ] Trust score
- [ ] Trust tier
- [ ] Wary cooldown remaining time
- [ ] Storyteller chance factors
- [ ] Effective runtime base chances
- [ ] Tracked-offer internal state
- [ ] Escort references

### Jaffa biology

- [ ] Prim'ta dependency severity
- [ ] Tretonin substitution time remaining
- [ ] Larva temperature
- [ ] Larva effective deterioration rate
- [ ] Internal temperature-band counters

## Gizmo inventory checklist

| Gizmo or command | Initial audit direction |
|---|---|
| Forced Goa'uld implantation | likely contextual gameplay action or dev-only until faction content exists |
| Ritual Goa'uld implantation | contextual near valid ritual requirements |
| Cancel ritual | contextual during active ritual only |
| Emergency extraction | audit after surgery path is fully established; likely dev-only or narrowly contextual |
| Autonomous hunt toggle | likely dev-only unless exposed through a deliberate gameplay rule |
| Tok'ra voluntary implantation | contextual for free Tok'ra symbiotes |
| Tok'ra therapeutic implantation | contextual when compatible sick targets exist |
| Refuse Tok'ra offer | contextual only for tracked temporary offers |
| Manual incubation actions | audit against the normal RimWorld bill workflow |

## Future mod setting

Add a GateRim SG-1 option similar to:

```text
Show advanced GateRim SG-1 debug information
```

Recommended display rule:

```text
GateRim debug option enabled
    OR
RimWorld developer mode enabled
    ↓
show advanced diagnostics
```

## Audit output expected before 0.2.x

Produce a final table listing every visible information line, gizmo, button and diagnostic log with one of the five visibility classes.
