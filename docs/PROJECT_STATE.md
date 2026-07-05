# Project state

Current milestone: `0.3.69-dev - Add open-conflict Goa'uld battlefield incident`
- corrected in cumulative local revision `r6`; build and bounded-retaliation retest pending.

## Repository state

- Starting published integration state: `develop` at annotated tag
  `v0.3.68-dev`.
- Active milestone branch:
  `feature/goauld-open-conflict-battlefield-incident`.
- Current public development version: `0.3.69-dev`.
- Technical assembly version: `0.3.69.0`.
- Current local revision: `r6`.
- Local validation is complete; fast-forward integration, tag and publication are
  pending.
- `main` remains reserved for the future stable `1.0.0` line.

## Implemented scope

- Add a persistent SG-1-Command-only scheduler for rare local battlefields.
- Select an exact active domain pair whose relation is `open conflict`.
- Preserve one active battlefield slot, hidden initial and recurrence delays,
  save/reload state, last-pair anti-repetition and RP-text anti-repetition.
- Select only player-home maps with free colonists and no existing hostile
  force for natural opportunities.
- Generate two Jaffa detachments belonging to the two exact domain factions.
- Scale each side from the current vanilla storyteller points with a bounded
  `0.35` factor and `250–1800` point range.
- Spawn both groups from valid cells within four cells of the map edge.
- Move each detachment to an opposing rally point, hold briefly, then announce
  and start the mutual assault.
- Keep both detachments focused on one another instead of launching an initial
  organized assault against colony structures.
- Send one bilingual RP letter naming both domains and explaining optional
  intervention.
- Begin withdrawal when one side is eliminated, exactly one side falls to
  `30%` or less of its initial mobile force, or the two-day limit expires;
  preserve downed pawns and loot, then force remaining mobile non-prisoners to
  leave after a fixed grace period.
- Add deterministic diagnostics and state-changing developer actions.
- Correct the first functional-test defects: the letter jumps to a pawn on the
  colony map, both detachments receive forced AI attack jobs, each camp persists
  exact attacking colonists, and stalled withdrawal paths are refreshed.
- Correct the second functional-test defects: troops now enter from the map edge,
  rally before an announced assault, and a victorious withdrawing camp cancels
  its exit behavior to retaliate when attacked by the player.
- Correct the third functional-test defects: ranged Jaffa now pursue until they
  reach weapon range and line of sight instead of receiving an immobile distant
  `AttackStatic` job, while post-victory injury changes infer a player provoker
  when the drafted attack job itself cannot be read reliably.
- Correct the fourth functional-design issue: retaliation now expires after
  `1800` quiet ticks, cannot pursue farther than `35` cells from its recorded
  origin, and can delay withdrawal for at most `6000` ticks without extending
  the fixed forced-exit deadline.

## Persistence model

The global tracker serializes only orchestration data:

- next check and opportunity ticks;
- active map ID;
- last selected domain-pair IDs;
- last RP-letter variant;
- storyteller suspension state.

The map component serializes the exact factions, generated pawns, initial
combatant counts, player provocateurs per camp, last provocation ticks,
provocation origins, withdrawal-retaliation starts, edge entries, rally anchors,
rally/assault timing, threat snapshot, start tick and withdrawal state. Existing
relation data is reused without changing the relation schema.

## Guardrails

- Automatic opportunities exist only under `Commandement SG-1`.
- An already active battlefield completes if the storyteller changes.
- Only one battlefield exists at a time.
- No natural battlefield is added over another active hostile threat.
- The event changes no strategic relation, faction goodwill, world settlement
  or natural-raid pressure factor.
- It creates no mission success, failure penalty or artificial reward.

## Validation result

Final local revision `r6` is validated:

- forced build succeeds with assembly `0.3.69.0`;
- both forces enter from the map edge and rally correctly;
- the assault announcement and mutual combat work;
- ranged Jaffa pursue and fire normally;
- player retaliation is temporary and spatially bounded;
- morale break, two-day limit and fixed withdrawal deadline work;
- save/reload and existing Goa'uld regressions pass;
- `Player.log` is clean.

## Next planned layer

After publication of `0.3.69-dev`, the selected follow-up is:

`0.3.70-dev - Add open-conflict Goa'uld world battlefield site`

Planned branch:

`feature/goauld-open-conflict-world-battlefield-site`

It must reuse the battle-generation contract and a shared active slot rather
than duplicating local and world battlefield orchestration.
