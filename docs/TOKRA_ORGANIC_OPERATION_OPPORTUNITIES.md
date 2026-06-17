# Tok'ra organic operation opportunities

## Purpose

The organic-operation system creates a progression path that begins before Trusted Tok'ra confidence. It separates two concepts:

- incoming low-sensitivity opportunities initiated by the Tok'ra;
- outgoing manual support requests initiated by the player through the trusted channel.

This prevents the colony from being required to reach Trusted trust before it can perform the very operations intended to build that trust.

## Persistent state machine

The current scheduler has three states:

1. `None`: no active opportunity; a hidden future check is scheduled.
2. `Offered`: the Tok'ra request is open on one player home map.
3. `Accepted`: monitoring is underway or the report is ready for transmission.

The active archetype, map identifier, creation time, expiry, acceptance time, ready time, deadline, notification state, last offered/completed archetypes and outcome counters are saved through `ExposeData()`.

## First archetype: Goa'uld observation

The cell asks the colony to discreetly monitor unusual Goa'uld movement. It is intentionally low-sensitivity and can appear at every current trust tier.

Player flow:

1. An RP letter announces a narrow Tok'ra channel.
2. The player selects an Intellectual-capable colon.
3. The player right-clicks the powered Tok'ra communicator and accepts the request.
4. After about six in-game hours, a message announces that the report is ready.
5. The player again right-clicks the communicator and transmits the report before the secure window closes.

Outcome:

- success: `+3` trust and `250` Intellectual XP;
- ignored offer: no trust change;
- accepted but missed report: `-1` trust.

## Scheduling

All scheduling is hidden from the player and uses randomized ranges.

| Trust tier | Recurrence range | Observation selection weight |
|---|---:|---:|
| Wary | 6-12 days | 0.60 |
| Neutral | 4-8 days | 1.00 |
| Cooperative | 3-7 days | 0.85 |
| Trusted | 6-12 days | 0.35 |

The first opportunity is delayed roughly 3-6 days after tracker initialization. If no eligible home map with a powered communicator exists, the scheduler waits and retries without creating a broken offer.

## Anti-repetition architecture

The tracker persists the last offered archetype. Once multiple compatible archetypes exist, repeating the same archetype applies a `0.25` weight factor before weighted selection. With only one archetype in `0.2.48-dev`, the system does not suppress the sole valid progression route.

## Compatibility and non-goals

- Existing save games instantiate the new GameComponent with safe defaults.
- Existing manual communicator actions retain their current Trusted-tier checks and cooldowns.
- Relay sabotage remains sensitive and Trusted-tier gated.
- The first archetype does not create a world site, quest, raid, reward cache, trade, recruitment, healing or military aid.
- No player-visible fixed cycle is added.

## Extension points

Future archetypes should provide:

- an eligibility predicate based on trust and colony/world state;
- a trust-dependent weight;
- an operation-specific state payload if needed;
- success, failure and refusal/expiry consequences;
- a short RP status line for the communicator report;
- an anti-repetition identity through `TokraOrganicOperationArchetype`.
