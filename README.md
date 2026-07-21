# GateRim SG-1

GateRim SG-1 is an early-development Stargate SG-1 content mod for RimWorld 1.6.
It builds a playable foundation around the SGC, Goa'uld, Jaffa, Free Jaffa and
Tok'ra before the later introduction of a functional Stargate and full
off-world progression.

## Current status

- Development version: `0.3.104-dev`
- RimWorld version: `1.6`
- Required DLC: `Biotech`
- Required mod: `Harmony`
- Package ID: `diablood.gaterimsg1`
- Author: `Diablood`

The functional Stargate is not yet included. The current build focuses on the
people, factions, technologies, biology and conflicts surrounding it.

`0.3.104-dev` finalizes one shared directional mobile appearance for adult
Goa'uld and Tok'ra symbiotes. Both PawnKinds now use a dedicated
`Graphic_Multi` family with validated north, south, east and mirrored-west
presentation, a crawling silhouette faithful to the adult Stargate symbiote
reference, strong black outlines and `drawSize = 0.65`.

The Goa'uld and Tok'ra remain mechanically distinct: hostile hunting, forced
implantation and ritual behavior are unchanged, as are Tok'ra voluntary
implantation and persistent identity. The Goa'uld queen deliberately retains
the previous single-image placeholder until its own later visual milestone.

`0.3.103-dev` adds a dedicated Jaffa knife as the mod's first Jaffa melee
weapon. It uses the accepted compact forked-blade texture, `drawSize = 0.65` and
a gladius-equivalent melee profile. Colonies can reproduce it at the machining
table after Jaffa weaponry research, and Free Jaffa supply convoys can carry a
small recovered stock.

Goa'uld-aligned and Free Jaffa warriors, guards, officers and traders can now
select the knife as their primary weapon, adding melee diversity to settlements,
raids, caravans and missions that reuse those PawnKinds. The dedicated Tok'ra
diversion breacher remains explicitly Ma'Tok-only so its structural role is not
weakened.

`0.3.102-dev` finalizes the remaining non-directional weapon, projectile and
Jaffa-equipment visuals. Bolas, Ma'Tok staff and Zat'nik'tel families receive
dedicated transparent art, together with their projectiles and the Tok'ra
hypodermic energy dart. Bolas are reusable, the Ma'Tok projectile uses its
validated speed, and the hypodermic rifle uses an alien energy-shot sound.

Jaffa map and inventory art now covers standard light and heavy armor, officer
armor, standard and officer deployed helmets, gauntlets, reinforced boots,
under-armor clothing, trousers and an armor belt. Directional worn variants,
automatic world and mission loadouts, and the retractable-helmet refactor remain
separate later work.

`0.3.101-dev` finalizes the Jaffa helmet-mode gizmo with one transparent
`64×64` cobra-helmet icon and simplifies the control to a direct manual toggle.
The command now reads `Deploy helmet` or `Retract helmet` according to the
current position; drafting no longer changes it automatically. Legacy automatic
save values preserve their stored physical position before becoming manual.
Armor values, deployed and retracted coverage, standard/officer pair isolation
and stable save identifiers remain unchanged.

`0.3.100-dev` restores missing published documentation and adds automatic
safeguards against the same drift recurring. The changelog and durable testing
history are reconciled, the visual register is checked against its own table,
the roadmap is returned to decided future work, and the final publication
procedure now has a blocking documentation-ready mode. No gameplay, Def,
translation, visual asset or save identifier changes in this milestone.
`0.3.99-dev` replaces the blurred `64×64` Goa'uld open-conflict
battlefield site icon with a crisp flat `128×128` version. Two opposed
Goa'uld staff-weapon silhouettes and a central orange impact remain readable
at world-map scale while matching the limited-detail, no-pseudo-3D style of
the other validated mission icons. Site generation, duration, faction
relations and optional player intervention remain unchanged.

`0.3.98-dev` finalizes the physical tretonin dose with a dedicated
transparent `128×128` medical ampoule. The compact high-contrast vial contains
a luminous cyan treatment and uses a stronger outline for clear map and
inventory readability, while its small in-game presentation remains controlled
by the existing `drawSize`. Tretonin production, stacking, administration and
one-day substitution behavior remain unchanged.

`0.3.97-dev` finalizes the experimental Tok'ra hypodermic rifle with a
dedicated high-contrast `128×128` weapon texture. The simplified horizontal
silhouette preserves its cyan sealed-charge modules and specialized
non-lethal identity while remaining readable when held, dropped and viewed
under stronger camera zoom. The existing twelve-charge behavior, projectile,
neutralization effect and self-disposal rules remain unchanged.

`0.3.96-dev` gives the two inert Prim'ta biological resources distinct final
art. The extracted immature symbiote is small, tightly curled and embryonic;
the incubated Prim'ta larva is longer, more developed and ready for preservation
or implantation. Both remain pale ivory-pink organisms inspired by the larval
appearance shown in *Bloodlines*, without adopting the armored anatomy of an
adult Goa'uld. Their established Defs, storage, incubation, deterioration and
implantation behavior remain unchanged.

`0.3.95-dev` finalizes the Tok'ra mission-object visual set: introduction
cipher module, encoded intelligence packet, portable observation device and
deployed point, organic dead drop, Goa'uld relay control node, secure
communicator and priority delivery marker. The established Def names, save
identifiers, mission behavior, trust gating, research and balance remain
unchanged. All world-event site icons under
`Textures/World/WorldObjects/Expanding/Sites` remain validated final references.

`0.3.94-dev` replaces the shared temporary Zat'nik'tel art used by the kara kesh
and Goa'uld healing bracelet with two dedicated transparent `128×128` item
textures approved by the maintainer. Both devices keep their stable apparel
texture paths, ThingDefs, save identifiers, research, recipes, System Lord
assignment and gameplay behavior.

`0.3.93-dev` finalizes the Goa'uld ritual basin and both Prim'ta basin visuals. The ritual basin now offers save-compatible `2×2` and `3×3` placement variants through one Architect dropdown; both use the same `2×2` visual and common UI icon so players can center the apparatus in rooms with either even or odd dimensions. The Prim'ta incubation and preservation basins use distinct validated green and blue one-cell textures, while the preservation basin remains specialized powered storage under Furniture.

`0.3.91-dev` replaces the temporary intrinsic Jaffa forehead-mark overlays
with the final compact symbol of Apophis. Ordinary, elite and First Prime marks
share one small forehead-sized geometry in black, silver and gold. Only the
`South` texture is visible; the `North`, `East` and `West` files remain fully
transparent so the mark never floats beside or above the head. The existing
intrinsic pawn data, assignment rules, developer actions, persistence and manual
removal remain unchanged.

`0.3.90-dev` removed the three obsolete technical Jaffa forehead-mark migration
genes and intentionally discontinued compatibility with private saves that
still contained those early prototypes.


`0.3.87-dev` corrected the vanilla starter-generation path for GateRim
xenotypes. An eligible adult Jaffa receives its Prim'ta before appearing on the
starting-pawn page. Every generated `SG1_GoauldHost` candidate receives a real
persistent adult symbiote before the player accepts or rerolls the pawn: Tok'ra
careers receive Tok'ra origin, a distinct human-host identity and dual
personality, while Goa'uld careers receive Goa'uld origin without Tok'ra
personality switching. The correction runs only during generation, so a
deliberate removal performed with another editor mod before starting the game is
respected.

## Playable content

- A stranded four-person SG-team starting scenario with dedicated field gear.
- An optional **SG-1 Command** storyteller that preserves Cassandra Classic's
  ordinary incident rhythm while adding a persistent GateRim-only orchestration
  channel. It advances persistent neutrality, rivalry, open conflict, truce and
  alliance states between Goa'uld domain pairs. Under SG-1 Command, a domain in
  open conflict uses `75%` of its ordinary points for natural Jaffa raids, while
  a domain participating only in alliances uses a non-stacking `110%`. Before
  those point factors are applied, one non-stacking relation modifier can
  multiply an already eligible doctrine weight by `1.25`: alliance favors direct
  assault, rivalry favors abduction and open conflict favors destruction, with
  priority `open conflict > alliance > rivalry`. On an eligible alliance raid of
  at least `800` final points, half of the outcomes remain standard. Cooperative
  direct outcomes are split between a delayed `75/25` reinforcement and a
  simultaneous `60/40` joint assault from opposite edges. Raid frequency remains
  unchanged. Open conflicts can
  also produce rare battlefields in two alternating forms: a local engagement
  near a colony or a temporary world site that a caravan may visit or ignore.
  Both forms use the same exact domain pair, map-edge arrival, rally, announced
  assault, ranged pursuit, morale break, bounded retaliation and withdrawal
  rules. Ignored world sites expire without failure or diplomatic consequence.
  A natural shared reprisal that begins with at least six combined Jaffa can,
  after collapsing to `20%` or fewer active survivors, dissolve the exact
  alliance into rivalry after a suspended `1–2` day diplomatic delay. Selecting
  another storyteller freezes future strategic opportunities and disables both
  relation-derived raid modifiers.
- Three Goa'uld System Lord domains are proposed by default, while the player
  may reduce their count through the vanilla world-faction list. Territorial
  strategy requires two active domains. In open conflict, rare bounded takeovers
  may transfer one eligible permanent Goa'uld settlement while protecting the
  final colony of every domain, sparse worlds, loaded maps, player presence,
  active quest targets and the dynamic automatic-hegemony ceiling: `75%` with exactly two domains, `50%` from three domains onward.
  Goa'uld-aligned Jaffa, Free Jaffa and an optional non-territorial
  Tok'ra faction are also included. The Tok'ra are selected once by
  default in the world-faction list with a dedicated icon, create no
  settlements and may be removed to disable their contacts, questline and
  recurrent operations for that game; the world-generation screen now shows a
  yellow warning before that choice is confirmed. Newly generated Goa'uld domains and settlements use
  dedicated combinatorial naming grammars instead of a shared fixed faction
  name or vanilla pirate settlement names. Newly generated System Lord leaders
  also receive a formal Goa'uld symbiote identity before the world-creation
  interface displays them, while a distinct human host name is preserved.
- Dedicated tintable world-faction icons distinguish the Free Jaffa, Goa'uld
  System Lord domains, Tok'ra and SGC expedition while preserving RimWorld's
  color variations when several copies of a faction are added. The Goa'uld
  faction tooltip also distinguishes its dominant Jaffa population, minority
  host caste and System Lord leader without misrepresenting possession as a
  germline xenotype.
- Newly generated Free Jaffa factions and settlements use dedicated combinatorial
  liberation- and clan-themed grammars instead of a shared fixed faction name
  or vanilla outlander town names; French compound names use natural
  capitalization and settlement ordinals avoid technical numeric suffixes.
  Their newly generated faction leaders receive formal two-part Free Jaffa
  cultural names directly during PawnKind generation, so the correct identity
  is already visible in the world-creation interface without vanilla names.
- A specialized Free Jaffa clan-supply convoy that trades provisions, strategic materials and limited military equipment through ordinary RimWorld caravan commerce.
- Allied Free Jaffa can answer an ordinary military-aid request through a powered communications console, using RimWorld goodwill costs, cooldowns and arrival behavior.
- Rare Goa'uld free-symbiote incursions that scale from storyteller threat
  points and reuse the autonomous implantation system as a biological hazard.
  A hostile symbiote that survives the one-day intervention window can seize
  its host's faction allegiance and turn the former colonist hostile. A
  captured active host can now undergo a difficult extraction surgery that
  restores the displaced host and removes the same symbiote alive.
- Goa'uld raids, intercepted threats and hostile mission sites that consume
  RimWorld's vanilla threat progression; relay installations grow from a
  bunker into larger fortified layouts as the defender budget increases.
- Persistent strategic doctrines differentiate each Goa'uld domain: conquest
  favors direct assault, enslavement favors living captives and scorched earth
  favors destructive strikes. The preference belongs to the faction, survives
  leader replacement and changes only the relative choice between the three
  existing natural raid doctrines.
- Persistent Goa'uld and Tok'ra symbiote identities, implantation, extraction
  and player-controlled Tok'ra host/symbiote personality switching. Emergency
  extraction before conversion preserves the host and the symbiote's original
  allegiance; established Goa'uld hosts require the riskier active-host surgery.
- Jaffa physiology, Prim'ta implantation, incubation, preservation, tretonin
  dependency and formal ceremonies.
- Ma'Tok staffs, Zat'nik'tels, modular Jaffa armor and varied SGC field equipment.
- Kara kesh hand devices reserved for Goa'uld System Lords, combining a
  powerful personal shield, a focused short-range kinetic blast, a temporary
  neural-agony attack and a maintained single-target paralysis hold. Every mode
  draws from the same energy reserve, retains melee, range, line-of-sight and
  EMP counterplay, and requires persistent biological naquadah traces rather
  than faction identity.
- Separate Goa'uld healing bracelets carried by System Lords stabilize an
  adjacent humanoid's bleeding wounds and repair only a limited amount of
  recent injury. They require the same biological naquadah eligibility but use
  their own fatigue and cooldown instead of kara kesh shield energy.
- Persistent naquadah traces acquired from an adult Goa'uld/Tok'ra symbiote or
  a Prim'ta and retained by former hosts after extraction.
- Early non-lethal capture tools: craftable single-use bolas and an experimental
  twelve-charge Tok'ra hypodermic rifle supplied by the first recurrent
  living-target capture operation; captured mission targets remain bound during
  travel and are collected visibly from a colony by a called Tok'ra team.
- Tok'ra trust, safehouses, medical support and secure communications; new
  recurring organic operations require a powered player communicator, while
  active operations survive channel loss. Distress-call sites, craft-and-deliver
  contracts, a Goa'uld/Jaffa diversion assault on the colony, a living Jaffa
  officer capture operation and a sabotage mission use colony, temporary-map
  and world-destination flows.
- A pre-communicator Tok'ra introduction mission with a natural encrypted
  offer, adaptive Goa'uld/Jaffa recovery site, physical cipher-module objective,
  long hidden retries until recovery and a three-session analysis that unlocks
  the dedicated research required for new communicator construction.
- A reusable, Def-driven mission framework foundation with persistent runtime
  state, RP text variants, recurrence controls and RimWorld threat snapshots;
  the Tok'ra observation, intelligence-recovery, wounded-agent care,
  medical-handoff, distress-call, temporary-base delivery, diversion-assault
  and Jaffa-officer capture operations now read their mission data and balance
  from XML. Their shared persistent scheduler filters temporarily unavailable
  missions before weighted selection and exposes long-term recurrence diagnostics.
- Configurable cultural profiles, persistent cultural names and 83 cultural
  backstories integrated with starting pawns and world generation.

GateRim SG-1 content remains compatible with normal colonies and compatible
vanilla or modded storytellers. The dedicated scenario is optional.

## Documentation

- [Player wiki](https://github.com/Diablood/GateRim-SG1/wiki)
- [Current content status](docs/wiki/Content-Status.md)
- [Documentation map](docs/README.md)
- [Branching workflow](docs/BRANCHING_WORKFLOW.md)
- [Development roadmap](docs/ROADMAP.md)
- [Changelog](docs/CHANGELOG.md)
- [Build instructions](docs/BUILD.md)
- [Current validation plan](docs/TESTING_CURRENT.md)

## Development workflow

`develop` is the canonical integration branch for validated development
milestones. Each `feature/*` or `fix/*` branch starts from an up-to-date
`develop`, is validated independently and is then fast-forwarded back into
`develop`. The annotated `v...-dev` tag is created on that integrated commit.
`main` remains reserved for the first stable `1.0.0` line and later stable
releases.

Repository rules and context recovery instructions are maintained in
[AGENTS.md](AGENTS.md), the complete branch policy in
[docs/BRANCHING_WORKFLOW.md](docs/BRANCHING_WORKFLOW.md), and the current
handoff in [docs/PROJECT_STATE.md](docs/PROJECT_STATE.md).

Root ZIP archives are local delivery artifacts and must not be committed.
`About/ModIcon.png` must be preserved.
