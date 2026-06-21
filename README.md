# GateRim SG-1

GateRim SG-1 is an early-development Stargate SG-1 content mod for RimWorld 1.6.
It builds a playable foundation around the SGC, Goa'uld, Jaffa, Free Jaffa and
Tok'ra before the later introduction of a functional Stargate and full
off-world progression.

## Current status

- Development version: `0.3.25-dev`
- RimWorld version: `1.6`
- Required DLC: `Biotech`
- Package ID: `diablood.gaterimsg1`
- Author: `Diablood`

The functional Stargate is not yet included. The current build focuses on the
people, factions, technologies, biology and conflicts surrounding it.

## Playable content

- A stranded four-person SG-team starting scenario with dedicated field gear.
- Goa'uld System Lord domains, Goa'uld-aligned Jaffa, Free Jaffa and a hidden
  Tok'ra world presence.
- Persistent Goa'uld and Tok'ra symbiote identities, implantation, extraction
  and player-controlled Tok'ra host/symbiote personality switching.
- Jaffa physiology, Prim'ta implantation, incubation, preservation, tretonin
  dependency and formal ceremonies.
- Ma'Tok staffs, Zat'nik'tels, modular Jaffa armor and varied SGC field equipment.
- Tok'ra trust, safehouses, medical support, secure communications, recurring
  organic operations and a sabotage mission on a temporary map.
- A reusable, Def-driven mission framework foundation with persistent runtime
  state, RP text variants, recurrence controls and RimWorld threat snapshots;
  the Tok'ra observation and intelligence-recovery operations now read their mission data and balance from XML, including an adaptive threat snapshot for accelerated intelligence analysis.
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
