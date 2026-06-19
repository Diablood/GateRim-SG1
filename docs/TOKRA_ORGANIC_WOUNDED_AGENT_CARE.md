# Organic Tok'ra wounded-agent care

## Purpose

This recurring preliminary operation asks the colony to shelter and treat a seriously wounded or sick Tok'ra agent. It uses normal RimWorld medical gameplay rather than exchanging medicine through a special container.

## Flow

1. The shared organic-operation scheduler creates the offer.
2. Ignoring the offer has no consequence.
3. A colon accepts through a powered Tok'ra secure communicator.
4. A generated Tok'ra voluntary host reaches a reachable map edge, is seriously injured, may also carry a significant illness and receives the temporary `SG1_TokraWoundedAgentSymbioteShock` hediff.
5. The shock sets Moving to zero, suppresses vanilla Injury Healing Factor and Immunity Gain Speed, and completely suspends the direct Tok'ra therapeutic injury-regeneration scan until the player intervenes.
6. The player rescues the agent into a player-owned medical bed and tends the symbiote shock itself. The shock carries a standard tending component, so a doctor can treat it even if every ordinary injury or illness has already healed. The tracker follows the patient through `MapHeld`, so the temporary carried state used by vanilla rescue is not treated as disappearance from the active map.
7. The tracker confirms that the shock itself was tended in the medical bed, records initial care, removes the acute shock and adds `SG1_TokraWoundedAgentPostShockRecovery`.
8. Acute shock suspends the direct Tok'ra therapeutic injury regeneration. After emergency treatment, the post-shock condition resumes that direct regeneration at only `25%` of its normal rate and also multiplies vanilla Injury Healing Factor by `0.25`. Ordinary tending, bleeding control and medical rest therefore remain important.
9. The tracker then evaluates travel fitness every shared state-check interval.
10. After a short stable period, the agent's visitor lord receives a departure memo.
11. Success is resolved only after the living agent leaves the map.

## Fit-to-travel criteria

The patient does not require complete healing. Departure requires:

- alive, spawned and not imprisoned;
- conscious and not downed;
- Moving and Consciousness capacities at or above the configured minimums;
- no dangerous active bleeding;
- sufficient summary health;
- no urgent medical-rest need;
- no condition close to lethal severity.

The condition must remain stable for a short interval to avoid departure during a transient health fluctuation.

## Failure rules

An accepted operation fails once if the agent:

- dies;
- is captured by the colony;
- disappears before departure is confirmed;
- remains unfit when the secure care window closes.

A dead corpse remains in the game. A living patient is removed only when an unresolved operation is explicitly reset or cleaned after failure.

## Persistence

The tracker saves:

- the active patient reference;
- whether the first colony treatment has been confirmed;
- the first stable tick;
- whether departure has been ordered;
- the departure grace deadline.

The archetype is appended as enum value `3`; all previous values and save keys are preserved.

## Recurrence

Resolution returns the archetype to the shared pool. Hidden delays and the last-archetype weight reduction prevent a fixed cadence or immediate repetition without permanently closing the event.

## Post-shock recovery balance

The new recovery condition has no fixed countdown and does not create an artificial mandatory wait. It remains only while the accepted operation is unresolved and is removed during resolution cleanup. The patient may leave as soon as the unchanged travel-fitness checks are satisfied, but injuries no longer disappear at the full Tok'ra accelerated rate immediately after first aid.

The condition is also restored automatically for a loaded `0.3.2-dev` patient whose initial-care flag was already recorded, so saves from the framework compatibility baseline continue cleanly.
