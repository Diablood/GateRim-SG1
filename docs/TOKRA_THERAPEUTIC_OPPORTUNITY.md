# Tok'ra therapeutic-opportunity incident

## Status

Prototype introduced in `0.1.47-dev`, with a lightweight peaceful escort added
in `0.1.48-dev` and a persistent time-limited lifecycle added in `0.1.49-dev`.

## Purpose

Provide a rare natural narrative entry point for voluntary therapeutic Tok'ra
hosting without automatically transforming a colonist.

## Trigger

The incident can be selected after day `30` when a compatible
player-controlled humanoid has at least one non-traumatic biological condition
accepted by the RimWorld-oriented Tok'ra healing filter. Recent injuries alone
do not qualify.

Initial storyteller tuning:

```text
baseChance = 0.035
earliestDay = 30
minRefireDays = 60
```

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

## Current limits

The escort is intentionally lightweight. Quests, medical envoys, remembered
refusals and diplomatic consequences remain separate future milestones.
