# GateRim SG-1

GateRim SG-1 is an early-development Stargate SG-1 content mod for RimWorld 1.6.
It builds a playable foundation around the SGC, Goa'uld, Jaffa, Free Jaffa and
Tok'ra before the later introduction of a functional Stargate and full
off-world progression.

## Current status

- Development version: `0.3.51-dev`
- RimWorld version: `1.6`
- Required DLC: `Biotech`
- Required mod: `Harmony`
- Package ID: `diablood.gaterimsg1`
- Author: `Diablood`

The functional Stargate is not yet included. The current build focuses on the
people, factions, technologies, biology and conflicts surrounding it.

## Playable content

- A stranded four-person SG-team starting scenario with dedicated field gear.
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
  color variations when several copies of a faction are added.
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
- Persistent Goa'uld and Tok'ra symbiote identities, implantation, extraction
  and player-controlled Tok'ra host/symbiote personality switching. Emergency
  extraction before conversion preserves the host and the symbiote's original
  allegiance; established Goa'uld hosts require the riskier active-host surgery.
- Jaffa physiology, Prim'ta implantation, incubation, preservation, tretonin
  dependency and formal ceremonies.
- Ma'Tok staffs, Zat'nik'tels, modular Jaffa armor and varied SGC field equipment.
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
  from XML. Their
  shared persistent scheduler filters temporarily
  unavailable missions before weighted selection and exposes long-term
  recurrence diagnostics.
- Configurable cultural profiles, persistent cultural names and 83 cultural
  backstories integrated with starting pawns and world generation.

GateRim SG-1 content remains compatible with normal colonies and compatible
vanilla or modded storytellers. The dedicated scenario is optional.

## Documentation

- [Player wiki](https://github.com/Diablood/GateRim-SG1/wiki)
- [Current content status](docs/wiki/Content-Status.md)
- [Development roadmap](docs/ROADMAP.md)
- [Changelog](docs/CHANGELOG.md)
- [Build instructions](docs/BUILD.md)
- [Current validation plan](docs/TESTING_CURRENT.md)

## Development workflow

Development proceeds through small, testable milestones on dedicated branches
created from the latest validated `v...-dev` tag. Repository rules and context
recovery instructions are maintained in [AGENTS.md](AGENTS.md), while the
current handoff is maintained in [docs/PROJECT_STATE.md](docs/PROJECT_STATE.md).

Root ZIP archives are local delivery artifacts and must not be committed.
`About/ModIcon.png` must be preserved.
