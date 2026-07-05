# Goa'uld gameplay direction

Status: approved baseline. Threat progression, natural doctrines, the first
extraction ultimatum, persistent domain doctrines, the SG-1 storyteller,
persistent inter-domain relation states and the first bounded open-conflict
pressure consequence are published. The first local battlefield consequence is
implemented in `0.3.69-dev-r6` for validation. Larger world and territorial
consequences remain deferred.

## Purpose

Goa'uld content must not reproduce the Tok'ra operation framework. The Tok'ra
act as discreet partners who contact the player, offer work and maintain a
relationship of trust. The Goa'uld are hostile powers: they impose pressure,
exploit weakness and react to interference.

The next Goa'uld milestone must be selected from this direction before any new
incident, quest or world object is implemented.

## Existing playable foundation

The mod already provides:

- visible hostile Goa'uld domains, settlements and System Lord leaders;
- Jaffa settlement defenders and natural direct, abduction and destruction
  raid doctrines;
- controlled prototypes for direct, abduction and destruction raid doctrines;
- free-symbiote incursions, autonomous implantation and hostile host takeover;
- emergency and active-host extraction with persistent symbiote identity;
- Goa'uld host castes, Jaffa ranks, equipment and cultural identities;
- persistent relations between domain pairs under Commandement SG-1;
- a bounded `75%` natural-raid pressure factor for domains in open conflict;
- a bounded local battlefield between the two exact domains, pending validation;
- several Goa'uld objectives used inside Tok'ra operations.

Future work should deepen these systems instead of creating a second mission
catalogue around them.

## Core identity

Goa'uld gameplay is built on four pillars.

### Domain pressure

Each domain is a hostile political and military presence. Its actions should
feel like orders, raids, reprisals, territorial pressure or exploitation, not
voluntary assignments offered to the colony.

### Hierarchy

System Lords, Goa'uld hosts and Jaffa servants must have distinct functions.
The System Lord embodies the domain, Goa'uld hosts are rare authority figures,
and Jaffa remain the visible military majority.

### Biological threat

Symbiotes, implantation, host capture and extraction are part of the same
hostile ecosystem as the faction. Biological incidents should preserve the
existing persistent identity and faction rules rather than introduce a second
infection model.

### Consequences

Player actions against a domain may provoke an appropriate response. The
response should follow from visible events such as destroying an installation,
capturing a ranked servant, extracting a Goa'uld or attacking a settlement.
It must not rely on a hidden mission list presented as Goa'uld content.

## Interaction rules

- No Goa'uld communicator, trust meter or recurring offer slot.
- No copy of the Tok'ra accept, travel, objective and debrief loop.
- Goa'uld incidents remain compatible with vanilla and compatible modded
  storytellers.
- World sites are used only when a real location or strategic objective needs
  to exist, not as a mandatory wrapper for every event.
- New consequences must expose an understandable cause to the player even when
  their timing or exact strength remains hidden.
- Existing saves and faction identities must remain valid.
- Texture finalization remains part of the later complete visual pass.
- Existing and future combat systems use RimWorld's vanilla threat points so
  colony wealth, pawn strength and the active storyteller remain authoritative.
- Existing GateRim incidents keep shared contracts compatible with vanilla and
  modded storytellers.
- Automatic inter-domain relation transitions and their strategic frequency or
  threat modifiers belong exclusively to the optional GateRim SG-1 storyteller.
  Other storytellers receive no hidden pacing changes.

## Ranked equipment direction

Goa'uld authority should eventually be visible through rare equipment as well
as titles and followers. The first candidate is a personal System Lord shield,
followed only after lore review by hand devices, control technology or other
rank-linked tools that offer distinct gameplay.

Lore accuracy does not justify unrestricted power. Every item must have a
RimWorld-readable energy limit, recharge or cooldown, vulnerability and
counterplay. Its actual strength must be reflected in pawn `combatPower`, raid
budgets, availability and loot value. High-rank technology must not become a
routine drop from every Jaffa raid.

Mechanical prototypes may use stable placeholder paths; final art remains part
of the later complete texture pass.

## Candidate implementation order

### 1. Vanilla threat progression audit

Before activating another doctrine, audit every existing Goa'uld raid,
reprisal, hostile mission site and settlement defense. Remove fixed early-game
forces and unjustified ceilings, preserve deliberate encounter factors, and
scale constructed defenses when a site contains an enemy installation.

This slice was published in `0.3.53-dev`.

### 2. Natural assault doctrines

Audit the existing controlled abduction and destruction raid prototypes, then
decide whether they can become rare natural Goa'uld doctrines alongside the
validated direct assault. Selection must depend on clear colony context and
must preserve a readable distinction between capture, destruction and a normal
raid.

This slice was published in `0.3.54-dev`. It retains one low-frequency incident,
uses weights `2/1/1`, gates abduction on threat plus available colonists and
gates destruction on threat plus building wealth.

### 3. Domain reprisals and demands

Create a shared reaction layer for significant hostile actions committed by
the player. Reprisals should reuse suitable raid or biological systems and
identify the offended domain whenever that information is available.

This layer must be driven by actual tracked actions. It must not award or
invent hidden mission progress.

Coercive demands and ultimatums are allowed when they fit the situation. They
must remain understandable player choices with visible consequences, not
disguised Tok'ra-style mission offers.

The first reaction slice was published in `0.3.55-dev`: successful extraction
of an active Goa'uld provokes one announced, delayed raid from that exact
domain. It validates attribution, persistence and anti-stacking before any
tribute or ultimatum choice is designed.

The first choice slice was published in `0.3.56-dev`. The offended domain gives
the colony one day to surrender the exact extracted symbiote. Compliance
removes that pawn and averts the attack; refusal or expiration schedules the
validated delayed reprisal. This deliberately avoids an arbitrary silver
tribute and keeps cause, demanded object and consequence visible together.
The player may postpone viewing the choice without pausing its deadline. A
failed extraction that immediately kills the host bypasses the choice and
creates the reprisal directly because no living symbiote remains to surrender.

### 4. Domain differentiation

Give multiple Goa'uld domains meaningful behavioral distinctions only after a
shared pressure loop is stable. Names and icon color variation already make
them visually distinct; doctrine weights or strategic preferences can make
them mechanically distinct without hard-coding one implementation per System
Lord.

The first differentiation slice was published in `0.3.64-dev`. Each faction
instance receives one persistent data-driven profile: conquest, enslavement or
scorched earth. The profile changes only the relative weights of the three
already validated natural assault doctrines. Incident frequency, vanilla
threat points, contextual thresholds and direct-assault fallback remain
unchanged. The faction is the persistent key, so leader replacement never
rerolls the doctrine.

### 5. Strategic world presence

Expansion, rivalry, patrols or larger world-scale consequences belong after
local pressure and reprisal rules are proven. They must be designed around the
world systems that actually exist at that time, especially before the Stargate
becomes functional.

The first relation layer was published in `0.3.66-dev`. Every unordered pair of
Goa'uld faction instances stores neutral, rivalry, open conflict, truce or
alliance independently of its current leaders. New and older saves reconcile
missing pairs in neutrality. Only the GateRim SG-1 storyteller advances the
bounded transition graph; selecting another storyteller freezes and shifts its
deadlines rather than accumulating overdue transitions.

Slow pair and global delays, previous-pair exclusion and three RP variants per
resulting state limit visible repetition. The reports identify both domains.

The first mechanical consequence was published in `0.3.68-dev`. While
Commandement SG-1 is active, each domain participating in at least one open
conflict uses `75%` of its ordinary natural Goa'uld Jaffa raid points. The
factor never stacks, doctrine selection still uses the original vanilla points,
raid frequency remains unchanged and extraction reprisals, controlled raids,
mission attacks and deterministic tests are excluded.

The local battlefield slice is implemented in `0.3.69-dev-r6`. A persistent
SG-1-Command-only scheduler selects an exact open-conflict pair, creates two
bounded Jaffa detachments near the colony, keeps their initial focus on one
another and forces mobile survivors to leave after elimination, a one-sided
`30%` morale break or the two-day limit. Player intervention triggers only a
local `1800`-tick retaliation inside a `35`-cell pursuit boundary; withdrawal
retaliation is capped at `6000` ticks and cannot postpone forced exit. The player
may remain outside the battle or attack either or both forces.

The next selected layer is `0.3.70-dev`: a temporary world-map battlefield site
that can be visited or ignored by caravan. It must reuse the same two-camp
generation and share the active slot so local and world battles cannot stack.

Alliance may later increase attack cadence or raid strength only slightly and
under a global cap. Later extensions may include second-domain reinforcements,
joint raids, doctrine interactions, shared reprisals and alliance rupture.

Rival domains may later expand their territories and destroy enemy settlements.
Those simulations require discreet safeguards against self-elimination, runaway
expansion and world imbalance before activation.

## Validated decisions

- Threat progression is audited before natural abduction or destruction.
- Natural doctrine activation preserves one shared incident frequency and
  keeps direct assault as the dominant fallback.
- Goa'uld demands and ultimatums are an accepted interaction type when tied to
  visible causes and consequences.
- Rival domains may conflict through RP information, bounded raid pressure,
  future battle incidents, territorial expansion and settlement destruction
  once each layer receives appropriate safeguards.
- Domain doctrine profiles are assigned to faction instances, not named leaders,
  and modify only existing doctrine weights.
- The `0.3.68-dev` open-conflict factor is a separate post-selection consequence
  and does not rewrite doctrine weights or thresholds.
- Automatic Goa'uld inter-domain relations and their strategic effects are
  reserved for the GateRim SG-1 storyteller; other storytellers remain
  untouched.
- Goa'uld equipment may be expanded through lore-audited, rank-limited items;
  the kara kesh personal-shield mode is the first candidate, not a promise of
  permanent invulnerability.

## Tok'ra boundary

The existing eight organic Tok'ra operations form a closed content set. Long
player games may reveal defects, balance problems or unclear instructions;
those findings become targeted future fixes. No ninth operation or general
pool expansion is currently planned.
