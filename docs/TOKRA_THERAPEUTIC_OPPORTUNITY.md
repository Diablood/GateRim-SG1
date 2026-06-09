# Tok'ra therapeutic-opportunity incident prototype

## Milestone

`0.1.47-dev — Add Tok'ra therapeutic-opportunity incident prototype`

## Narrative flow

```text
compatible sick colony pawn
    ↓
rare storyteller-selected opportunity after day 30
    ↓
one free Tok'ra symbiote arrives at a reachable map edge
    ↓
targeted letter naming the pawn and detected conditions
    ↓
player selects the symbiote
    ↓
existing therapeutic implantation command
    ↓
explicit consent confirmation
    ↓
active Tok'ra host and biological healing
```

## Eligibility

The incident targets a spawned player-controlled humanlike pawn aged at least
`13` biological years, without a recent or active adult symbiote state.

It requires at least one non-traumatic biological condition accepted by the
shared RimWorld-oriented Tok'ra healing filter. Injuries remain healable after
implantation but do not trigger the narrative incident on their own. This
avoids generating a rare Tok'ra opportunity because of a minor cut.

When several colonists are eligible, the incident selects the pawn with the
highest aggregated therapeutic-need severity.

## Frequency

```text
base chance: 0.035
earliest day: 30
minimum refire delay: 60 days
```

## Current limits

The first prototype spawns an unescorted free Tok'ra symbiote and reuses the
existing manual command. Escorted envoys, quests, diplomacy consequences,
time-limited decisions and automatic departure remain separate future work.
