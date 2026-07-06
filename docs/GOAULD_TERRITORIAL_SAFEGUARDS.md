# Goa'uld territorial strategy and safeguards

## Current milestone

- Version: `0.3.84-dev`
- Branch: `feature/goauld-bounded-territorial-takeover`
- Target assembly: `0.3.84.0`
- Local revision: `r1`
- Status: final revision `r1`, validated and published as `v0.3.84-dev`.
- Foundation: `0.3.83-dev`, final revision `r3`, published as `v0.3.83-dev`.

## Purpose

`0.3.83-dev` established the reusable safety evaluator, persistent reservation,
cooldowns and GateRim-to-vanilla diplomatic coherence without mutating the
world. `0.3.84-dev` applies the first real consequence through that exact
boundary: one existing permanent Goa'uld settlement may change from one System
Lord domain to another.

The result is intentionally narrow. It is not a general conquest engine, does
not simulate armies between settlements and does not create a chain reaction of
territorial changes.

## World-generation and activation baseline

`SG1_GoauldSystemLordPrototype` proposes three faction instances in the editable
vanilla world-faction list while keeping only one required baseline. The player
may reduce the count; the mod never recreates a removed domain.

Territorial strategy is available only when:

- `Commandement SG-1` is active;
- at least two non-defeated Goa'uld domains each own a permanent vanilla
  `Settlement`.

With zero or one territorial domain, only the territorial layer is suspended.
Other Goa'uld raids, missions and systems keep their own contracts.

## Exact transferable object

Only a permanent vanilla `Settlement` world object owned by a Goa'uld System
Lord faction is transferable. Player settlements, non-Goa'uld settlements,
mission sites, battlefields, caravans and temporary world objects are excluded
by type and faction.

A successful takeover preserves:

- the same `Settlement` instance and world-object ID;
- the same label and name;
- the same world tile;
- the same total number of settlements;
- the same number of factions.

Only `Settlement.SetFaction` changes the owner. The implementation then
invalidates the cached faction-colored material, destroys stale trader stock,
clears cached former inhabitants and refreshes static world rendering so future
visits and trade resolve from the new domain rather than the former owner.

No special rollback state is created. A settlement can later become an eligible
candidate in the opposite direction only through another fully guarded natural
occurrence after all cooldowns and strategic conditions permit it.

## Published strategic safeguards

Every candidate is checked when scheduled and checked again immediately before
mutation.

### Minimum domains and sparse worlds

- at least two active territorial domains are required;
- the world must contain at least `active domains + 2` permanent Goa'uld
  settlements;
- the losing domain must retain at least one permanent settlement.

Examples:

- two domains require at least four settlements;
- three domains require at least five;
- four domains require at least six.

### Minor expansion and hegemony ceiling

The gaining domain's weight and post-transfer cooldown decrease its ability to
expand repeatedly:

| Current or resulting settlements | Expansion weight | Domain-delay multiplier |
|---:|---:|---:|
| 1 | `1.00` | `1` |
| 2 | `0.50` | `2` |
| 3 | `0.25` | `4` |
| 4+ | `0.10` | `8` |

The hegemony ceiling is `75%` when exactly two active domains remain and `50%`
when three or more domains are active. The two-domain exception is necessary
because any visible transfer between only two owners creates a majority; it
still permits at most a bounded imbalance while final-settlement protection
preserves the other domain. Existing imbalance is not rewritten: a domain above
the current ceiling is simply blocked from further automatic gain.

### Exact diplomatic and compatibility conditions

The gaining and losing domains must be the exact stored pair in
`OpenConflict`. The relation tracker remains authoritative and vanilla must
therefore report the pair as `Hostile`.

A pending alliance rupture for the same exact pair is incompatible. Unrelated
pairs do not block each other, although the single global territorial slot still
prevents two simultaneous takeovers.

### Map, player and quest protection

The exact settlement is rejected or a pending reservation is cancelled when:

- its map is currently loaded;
- a player-owned world object is present on its tile;
- an active, non-historical, non-dismissed quest references it.

These protections prevent strategic background simulation from changing a
colony while the player is interacting with it or while another system owns its
narrative state.

## Natural cadence and selection

Under `Commandement SG-1`, the tracker performs one natural scheduling attempt
every `45–90` RimWorld days. A failed attempt simply arms the next interval; it
does not retry continuously.

Candidates are built from both directions of every active open-conflict pair.
Each eligible losing settlement is a candidate. The gaining domain's expansion
weight is applied to each candidate, so small domains are favored as winners
while a domain with more transferable settlements exposes more possible losses.

A successful scheduling attempt creates one persistent reservation with an
ordinary `1–2` day deadline. It stores:

- exact gaining and losing faction references;
- exact settlement world-object ID;
- required relation;
- natural or developer source;
- creation and resolution ticks;
- domain and settlement counts at creation;
- pending, completed or cancellation outcome.

At most one territorial reservation exists globally.

## Cooldowns

After a successful transfer:

- global spacing: `15` days;
- both involved domains: base `30` days;
- exact pair: `60` days;
- gaining domain: base domain delay multiplied by its post-transfer size factor.

The natural-attempt deadline, pending resolution deadline and all cooldowns are
suspended outside `Commandement SG-1` and shifted forward when it returns. No
backlog is consumed under another storyteller.

## Resolution and player report

At the deadline, every safeguard and cooldown is evaluated again. If all remain
valid, the owner changes once and the outcome becomes `CompletedTransfer`.

One neutral letter is selected from three localized RP variants with local
anti-repetition. It names the settlement, losing domain and gaining domain and
targets the unchanged settlement tile.

The pair remains in `OpenConflict`; the takeover itself does not alter GateRim
or vanilla diplomacy.

## Save compatibility

Schema `2` persists the natural scheduler and real transfer outcomes.

Schema-`1` global, domain and pair cooldowns from `0.3.83-dev` represented
only developer dry runs and are cleared during migration. A still-pending dry
run becomes `CancelledLegacyDryRun` without changing ownership. Older saves
receive a newly armed `45–90` day natural deadline rather than an immediate
attempt.

Completed world ownership is already serialized by RimWorld through the
settlement's faction reference. The tracker preserves the completed outcome and
cooldowns for diagnostics.

## Diplomatic coherence retained from 0.3.83-dev

`GameComponent_GoauldInterDomainRelationTracker` maps each stored pair state to
vanilla:

| GateRim state | Vanilla relation |
|---|---|
| `Neutral` | `Neutral` |
| `Rivalry` | `Neutral` |
| `OpenConflict` | `Hostile` |
| `Truce` | `Neutral` |
| `Alliance` | `Ally` |

Only two instances of the Goa'uld System Lord faction Def may change goodwill
with one another. Goa'uld domains remain permanently hostile to the player and
all outside factions. Reconciliation sends no goodwill message or hostility
letter.

## Developer tools

Relation preparation:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
> Set all pairs: Open conflict
```

Territorial path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
> Territorial strategy...
```

Exact actions:

```text
Show territorial strategy report
Reconcile territorial and diplomatic state
Create pending territorial takeover
Run natural territorial attempt now
Trigger pending territorial takeover now
Cancel pending territorial takeover
Reset territorial strategy state
```

The report shows storyteller state, natural cadence, domains, settlement counts,
sparse threshold, final-settlement protection, expansion weights, delay
multipliers, share ceiling, cooldowns, diplomatic coherence and the exact
reservation. A reservation also shows its source, current owner, map-loaded
state, player presence and active-quest protection.

## Explicit non-effects

`0.3.84-dev` does not:

- create, destroy or move a settlement;
- change a settlement name or tile;
- transfer player or non-Goa'uld territory;
- defeat or create a faction;
- change the exact pair's relation;
- change player or outside-faction goodwill;
- change doctrines, raid points, raid frequency or refire delay;
- create a raid, mission, reward or battle site;
- alter the storyteller's ordinary incident cadence.

## Validation result

The maintainer reports the focused final-`r1` procedure as successful.

Confirmed results:

- assembly `0.3.84.0`, local build and consistency checks succeeded;
- one natural-source reservation stored the exact open-conflict pair,
  settlement ID, creation snapshot and ordinary `1–2` day deadline;
- the pending state survived save and reload without changing ownership;
- immediate resolution completed as `CompletedTransfer` and changed only the
  settlement owner;
- settlement name, ID, tile and total count remained unchanged;
- the losing domain retained territory and the gaining domain stayed within the
  required three-domain `50%` ceiling;
- the world icon, inspect string and targeted three-actor RP letter reflected
  the new owner immediately;
- global, domain and pair cooldowns used the winner's post-transfer size and
  survived save and reload with the completed outcome;
- the exact pair remained `OpenConflict` / vanilla `Hostile` and no player or
  outside goodwill, raid, doctrine, mission, reward or storyteller-frequency
  state changed;
- the accepted `Player.log` contained no new relevant error.

Optional legacy migration, suspension and refusal cases remain durable coverage
but are not claimed separately by this focused acceptance.
