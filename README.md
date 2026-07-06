# GateRim SG-1

GateRim SG-1 is an early-development Stargate SG-1 content mod for RimWorld 1.6.
It builds a playable foundation around the SGC, Goa'uld, Jaffa, Free Jaffa and
Tok'ra before the later introduction of a functional Stargate and full
off-world progression.

## Current status

- Development version: `0.3.79-dev`
- RimWorld version: `1.6`
- Required DLC: `Biotech`
- Required mod: `Harmony`
- Package ID: `diablood.gaterimsg1`
- Author: `Diablood`

The functional Stargate is not yet included. The current build focuses on the
people, factions, technologies, biology and conflicts surrounding it.

`0.3.79-dev` adds coordinated joint assaults as a second possible alliance
manifestation under SG-1 Command. An eligible alliance-context raid now remains
standard half the time. Cooperative direct raids divide the other half between
the published delayed reinforcement and a simultaneous two-domain assault from
opposite map edges. The existing `110%` budget, incident frequency and RP
relation reports remain authoritative; a diplomatic announcement never launches
or guarantees a raid.

## Playable content

- A stranded four-person SG-team starting scenario with dedicated field gear.
- An optional **SG-1 Command** storyteller that preserves Cassandra Classic's
  ordinary incident rhythm while adding a persistent GateRim-only orchestration
  channel. It advances persistent neutrality, rivalry, open conflict, truce and
  alliance states between Goa'uld domain pairs. Under SG-1 Command, a domain in
  open conflict uses `75%` of its ordinary points for natural Jaffa raids, while
  a domain participating only in alliances uses a non-stacking `110%`. On an
  eligible alliance raid of at least `800` final points, half of the outcomes
  remain standard. Cooperative direct outcomes are split between a delayed
  `75/25` reinforcement and a simultaneous `60/40` joint assault from opposite
  edges. Doctrine selection still uses the
  original vanilla points, open conflict overrides alliance, and raid frequency
  remains unchanged. Open conflicts can
  also produce rare battlefields in two alternating forms: a local engagement
  near a colony or a temporary world site that a caravan may visit or ignore.
  Both forms use the same exact domain pair, map-edge arrival, rally, announced
  assault, ranged pursuit, morale break, bounded retaliation and withdrawal
  rules. Ignored world sites expire without failure or diplomatic consequence.
  Selecting another storyteller freezes future strategic opportunities and
  disables both relation-derived raid modifiers.
- Goa'uld System Lord domains, Goa'uld-aligned Jaffa, Free Jaffa and an
  optional non-territorial Tok'ra faction. The Tok'ra are selected once by
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
