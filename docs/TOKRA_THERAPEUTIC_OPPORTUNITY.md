# Tok'ra therapeutic-opportunity incident

## Status

Prototype introduced in `0.1.47-dev`, with a lightweight peaceful escort added
in `0.1.48-dev`, a persistent time-limited lifecycle added in `0.1.49-dev`,
a lightweight Tok'ra trust foundation added in `0.1.50-dev`, trust tiers added
in `0.1.51-dev`, tretonin-support gifts added in `0.1.52-dev` and
storyteller trust weighting added in `0.1.54-dev`.

## Purpose

Provide a rare natural narrative entry point for voluntary therapeutic Tok'ra
hosting without automatically transforming a colonist.

## Trigger

The incident can be selected after day `30` when a compatible
player-controlled humanoid has at least one non-traumatic biological condition
accepted by the RimWorld-oriented Tok'ra healing filter. Recent injuries alone
do not qualify.

Storyteller tuning:

```text
XML baseline chance = 0.035
earliest day        = 30
minimum refire      = 60 days
```

Since `0.1.54-dev`, the runtime worker multiplies the XML baseline by the
current trust tier:

| Tier | Factor | Effective base chance |
|---|---:|---:|
| Wary | ×0.50 | 0.0175 |
| Neutral | ×1.00 | 0.0350 |
| Cooperative | ×1.25 | 0.04375 |
| Trusted | ×1.50 | 0.0525 |

## Escorted arrival

The worker spawns:

```text
1 free Tok'ra symbiote
1 to 2 SG1_TokraVoluntaryHost escort pawns
```

The escort belongs to the lazily created hidden Tok'ra faction and reuses the
vanilla peaceful colony-visit lord behavior. The faction is shared with the
existing peaceful-visitor incident through `TokraFactionUtility`.

## Player choice

The letter targets the free symbiote and names the sick pawn and detected
conditions. The player must still select the symbiote and use the dedicated
therapeutic-implantation command. The explicit consent dialog remains
mandatory.

## Temporary offer lifecycle

Since `0.1.49-dev`, the opportunity remains active for two RimWorld days. The
free symbiote displays the remaining duration in its inspection text and gains
an explicit refusal command.

The persistent `GameComponent_TokraTherapeuticOpportunityTracker` records the
free symbiote, escort pawns and expiration tick. Expiration, refusal and
successful implantation all close the record and ask the escort to leave
through vanilla `LordJob_TravelAndExit` behavior. Save and reload preserve the
remaining duration.

## Lightweight trust foundation

Since `0.1.50-dev`, therapeutic-offer outcomes update a persistent mod-owned
Tok'ra trust score while the runtime faction remains hidden from normal world
diplomacy:

```text
accepted offer = +5 trust
explicit refusal = -1 trust
unanswered expiration = -2 trust
```

Tracked free symbiotes display the current score and localized tier below the
remaining offer duration. The score is clamped between `-100` and `100` and
persists across save and reload.

Since `0.1.51-dev`, the tier is read once when an offer is created. Wary offers
last `1` day with `1` escort, neutral offers last `2` days with `1` to `2`
escorts, cooperative offers last `3` days with `2` escorts, and trusted offers
last `4` days with `2` to `3` escorts.

## Tretonin-support gifts

Since `0.1.52-dev`, cooperative teams bring `1` physical `SG1_TretoninDose`
and trusted teams bring `2`. Wary and neutral teams bring no material support.
The supplies are placed near the arrival point and remain on the map regardless
of the final response. Refusal and expiration still reduce trust, limiting
repeat harvesting naturally.

## Current limits

The escort and trust layer remain intentionally lightweight. Quests, broader
medical envoys and vanilla-goodwill integration remain separate future
milestones.
