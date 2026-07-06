# GateRim SG-1 storyteller

## Published strategic milestones

- Foundation: `0.3.65-dev`
- Persistent inter-domain relations: `0.3.66-dev`
- Open-conflict natural-raid reduction: `0.3.68-dev`
- Local battlefield: `0.3.69-dev`
- World-site battlefield extension: `0.3.70-dev`
- Alliance-strength extension: `0.3.73-dev`
- Delayed allied reinforcement: `0.3.78-dev`
- Coordinated joint raid outcomes: `0.3.79-dev`
- Relation-influenced doctrine weights: `0.3.80-dev`
- Target assembly: `0.3.80.0`
- Current local revision: `r1`
- Status: final revision `r1` validated and published.

## Purpose

`SG1_GateRimStoryteller` provides an optional, explicit orchestration boundary
for strategic systems that should belong to GateRim SG-1 rather than silently
modifying Cassandra, Phoebe, Randy or a modded storyteller.

Selecting `SG-1 Command` preserves the currently resolved Cassandra Classic
incident cadence while activating persistent Goa'uld inter-domain relations and
their separately validated strategic consequences.

## Player-facing description

The selection-panel description remains the concise `0.3.65-dev-r2` behavioral
summary. Later strategic systems do not lengthen this text or restore a French
scrollbar.

French:

```text
Le Commandement SG-1 suit un rythme classique : il augmente progressivement la
pression, puis vous accorde un répit. Il coordonne aussi les futurs événements
propres à GateRim.
```

English:

```text
SG-1 Command follows a classic rhythm: it steadily raises the pressure, then
gives you time to recover. It also coordinates future GateRim events.
```

## Cassandra baseline architecture

At static startup, `GateRimStorytellerBootstrap` reads the currently resolved
`Cassandra` Def and copies its public difficulty curves, adaptation settings and
component list into `SG1_GateRimStoryteller`. It then appends exactly one
`StorytellerComp_GateRimOrchestrator`.

This avoids embedding a frozen copy of Core XML. RimWorld 1.6, active DLCs and
compatible updates remain authoritative for Cassandra's ordinary incident
contracts.

The appended component emits no independent incident. Strategic relations and
their consequences are maintained by dedicated systems that check the active
storyteller explicitly.

## Activation contract

All automatic strategic systems must use:

```csharp
GateRimStorytellerUtility.IsGateRimStorytellerActive
```

The check compares the active `StorytellerDef` with
`SG1_GateRimStoryteller`. It never infers activation from difficulty,
storyteller labels or incident history.

When another storyteller is selected:

- no inter-domain transition is rolled;
- no RP relation report is emitted;
- all pair and global relation deadlines are shifted forward by the suspension
  duration when SG-1 Command becomes active again;
- every relation-derived natural-raid pressure factor resolves to `1.00`;
- every relation-derived doctrine-weight modifier is disabled;
- existing published GateRim incidents retain their ordinary contracts.

This is a true suspension rather than a backlog. Returning to SG-1 Command does
not immediately consume transitions that would have become due under Cassandra,
Phoebe, Randy or a modded storyteller.

## Persistent storyteller lifecycle

`GameComponent_GateRimStorytellerOrchestrator` stores:

- schema version;
- whether SG-1 Command was active at the previous observation;
- activation count;
- last activation and deactivation ticks;
- last observation tick;
- last observed storyteller Def name.

It observes changes every `250` ticks and on new game, load and final
initialization.

Its report embeds relation-tracker availability, active pair count, automatic
activation state, next strategic deadline and the count of domains whose
natural-raid pressure is currently reduced. Relation-pressure diagnostics list
both open-conflict and alliance participation with the resolved final factor.
The battlefield tracker exposes its own persistent cadence and active-map report
through the relation debug menu.

## Persistent relation model

`GameComponent_GoauldInterDomainRelationTracker` stores one state for every
unordered pair of Goa'uld System Lord faction instances.

Each `GoauldInterDomainRelationState` stores:

- canonical first and second faction references ordered by `loadID`;
- current and previous relation;
- establishment tick;
- last and next transition ticks;
- transition count.

The relation belongs to the factions, not their current leaders. Replacing a
System Lord therefore does not reset diplomacy. A defeated domain remains safe
to deserialize and inspect, but its pairs are inactive and cannot transition.

New worlds and older saves are reconciled automatically. Every active pair that
does not already exist begins in neutrality.

## Relation states and transitions

The five persistent states are neutral, rivalry, open conflict, truce and
alliance.

```text
neutral -> rivalry | alliance
rivalry -> open conflict | neutral
open conflict -> truce
truce -> neutral | rivalry | alliance
alliance -> neutral | rivalry
```

Cadence remains:

- first transition for a new pair: `8–16` days;
- pair cooldown after a transition: `12–24` days;
- global spacing between RP reports: `5–10` days;
- runtime observation interval: `250` ticks;
- at most one automatic pair transition per global window.

Pair and text anti-repetition remain unchanged.

## Relation-derived doctrine influence

`0.3.80-dev` adds a second narrow relation consequence before doctrine selection.
It remains distinct from the later point-pressure factor.

For ordinary natural Goa'uld raids under `Commandement SG-1`, one XML Def can
multiply one already eligible doctrine weight by `1.25`:

- open conflict favors destruction, priority `300`;
- otherwise alliance favors direct assault, priority `200`;
- otherwise rivalry favors abduction, priority `100`;
- neutrality and truce have neutral `1.00` multipliers.

The domain's persistent profile is applied first. Existing point, colonist and
building-wealth thresholds are applied next. Only then can the relation modifier
multiply a positive weight. Several relations never stack, and open conflict
therefore overrides alliance and rivalry for doctrine weighting just as it
already overrides alliance for raid-point pressure.

Other storytellers and all historical externally forced doctrine commands bypass
this layer. It changes no incident chance, refire delay, point budget, alliance
split, officer eligibility or raid strategy definition.

## Relation-derived natural-raid pressure

`0.3.68-dev` adds the first mechanical consequence of a relation state.
`0.3.73-dev` extends the same narrow point-modifier path to alliances.

While SG-1 Command is active, an ordinary natural Goa'uld Jaffa raid resolves:

- `0.75` when the attacking domain participates in at least one open conflict;
- `1.10` when it has no open conflict and participates in at least one alliance;
- `1.00` otherwise.

The effect is intentionally narrow:

- several conflicts or alliances never stack;
- open conflict overrides alliance when both affect the domain;
- doctrine eligibility and weighting use the original vanilla points;
- the factor is applied after doctrine selection;
- incident chance, earliest day and refire delay remain unchanged;
- every external forced path remains excluded by default, including extraction
  reprisals and deterministic regression raids;
- the dedicated relation-pressure command enables the factor for one forced
  test;
- the factor itself remains derived; only a pending or active cooperative raid
  serializes its exact pair and participants.

The shared raid worker internally marks every generated raid as forced. The
natural worker now records whether the caller was already forced before entering
that shared path. This lets ordinary storyteller raids receive the relation
factor without exposing reprisals or exact developer regressions to it.

Changing storyteller or relation state changes the derived factor immediately.

## Alliance-context raid outcomes

At `800+` final points, an eligible alliance-context raid remains standard half
the time. This preserves uncertainty after an alliance report and keeps the
entire `1.10` budget on one domain.

The cooperative half resolves after doctrine selection. Direct raids divide it
equally between delayed `75/25` reinforcement and simultaneous `60/40` joint
assault. Abduction and destruction can use delayed support but never the joint
mode. No branch creates another incident roll or modifies the shared refire
delay.

Joint direct forces retain exact faction colors, use opposite map edges and
share one RP letter. The temporary cooperation layer changes no persistent
goodwill. A retreat or bounded break by either detachment orders both forces to
leave.

## Open-conflict battlefields

`0.3.69-dev` publishes the first visible battle caused by a relation state.
`0.3.70-dev` extends the same subsystem with an optional world-map site.

The persistent tracker owns one shared slot and cadence for both forms. It
selects one exact open-conflict pair, stores the previous pair and form, and
alternates local and world occurrences when both are possible. A world marker
and a local battle can never coexist in this slot.

Each camp belongs to its stored domain faction and receives `35%` of the
vanilla threat snapshot, clamped to `250–1800` points. Both forms reuse the same
map component: edge arrival, rally, announced assault, movement into weapon
range, bounded player retaliation, one-sided `30%` morale break, two-day limit
and fixed withdrawal.

The local form appears only on a suitable home map without another active
hostile force. The world form appears `6–18` tiles from a home map, lasts eight
days and generates its `140 × 140` encounter map only when a player caravan
arrives through the normal RimWorld world-path action.

Ignoring a world site has no failure consequence. It disappears without
changing goodwill, relation state, territory or settlements. After entry, the
site remains in the shared slot until the battle resolves and all player pawns
have left through ordinary caravan reformation.

## RP reports

Every real state change produces one neutral-event letter naming both domains.
The reports describe political or military intelligence without exposing hidden
weights. Open-conflict and alliance texts remain RP-facing and do not reveal the
exact derived factor outside developer diagnostics.

A report changes eligibility only. It never launches a raid or battlefield,
does not consume a storyteller opportunity and does not guarantee that the next
eligible raid will use a relation-specific form.

## Developer diagnostics

Relation path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

`Show natural raid relation-pressure report` exposes open-conflict state,
alliance state and the final natural-raid factor for every active domain.

Threat path:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Threat progression...
```

`Show current progression` displays vanilla points, effective natural points,
pressure factor, unchanged intercepted points and doctrine context.

`Domain doctrines... > Show domain doctrine report` displays the permanent
profile, eligible pre-relation weights, selected relation Def and priority,
`x1.25` multipliers and final percentages for every domain.

`Force current natural raid (relation pressure applied)` is available in the
relation menu and exercises the ordinary relation-aware worker while temporarily enabling
the factor for that forced validation. The historical forced-doctrine commands
remain exact and bypass the factor.

The same menu provides deterministic standard, delayed and joint
alliance-context raids. `Show allied raid cooperation report` identifies the
active manifestation, while `Order joint primary force withdrawal` validates
the bilateral exit response.

## Still inactive consequences

The published relation and alliance layers still add no:

- alliance frequency increase;
- shared reprisals;
- territorial expansion or settlement destruction;
- change to faction goodwill toward the player.

These effects require separate balancing and validation milestones.

## Published validation

Final local revision `r1` of `0.3.73-dev` is validated and published. The
focused relation-pressure procedure confirms alliance `110%`, open-conflict
`75%`, mixed-state precedence, non-stacking, other-storyteller `100%`, natural
versus forced execution, save/reload and a clean accepted `Player.log`.

Final revision `r1` of `0.3.78-dev` validates delayed allied reinforcement.
Final revision `r1` of `0.3.79-dev` is validated and published for standard and simultaneous joint
outcomes, opposite-edge faction colors, one shared letter, mutual cooperation,
bilateral withdrawal and a clean accepted `Player.log`. The lack of an officer
in the forced `1200`-point joint test is expected from the forced-path rules and
the `792`-point primary budget, not a regression.

Final revision `r1` of `0.3.80-dev` is validated and published. The focused
procedure confirms XML-driven `x1.25` doctrine modifiers, non-stacking priority,
threshold preservation, storyteller exclusion, deterministic forced-command
behavior, unchanged alliance manifestations and a clean accepted `Player.log`.
