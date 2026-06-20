# Unified cultural identity diagnostics

Version: `0.3.15-dev`

## Purpose

Culture-dependent information is currently resolved by several authoritative systems: cultural profiles, cultural names, active backstories, faction allegiance, Jaffa physiology and marks, contextual social identity, and persistent symbiote data.

The unified report makes those decisions visible in one place for development and testing. It does not introduce a second identity model and does not cache or rewrite any result.

Status: functionally validated and published in `v0.3.15-dev`.

## Access

The report is available through:

- one RimWorld developer action under `GateRim SG-1`;
- one button in the existing GateRim SG-1 advanced-debug settings section.

Both paths inspect the currently selected pawn and call the same report builder. The settings entry is visible only when RimWorld developer mode or the explicit advanced-debug option is enabled.

## Report sections

The report includes:

1. current pawn identity: name, ID, race, xenotype, `PawnKindDef`, faction and active backstories;
2. cultural profile matches, selected profile and name group for `NonPlayer` and `PlayerStarter` contexts;
3. Jaffa physiology, Prim'ta and forehead-mark state;
4. contextual social flags already used by gameplay;
5. persistent adult-symbiote data when the pawn carries the host Hediff.

The two generation contexts are reported separately because a starter-only profile can legitimately differ from the normal world-pawn profile.

## Read-only boundary

The diagnostic must never:

- assign or reserve a cultural name;
- replace a backstory;
- change a faction or `PawnKindDef`;
- add or remove a Jaffa mark, Prim'ta or dependency;
- initialize, implant, extract or migrate a symbiote identity;
- toggle a Tok'ra personality;
- alter save data merely because the report was opened.

All displayed values are obtained at report time from the existing services. A discrepancy therefore indicates either a real cross-system inconsistency or an incomplete diagnostic field, not a hidden synchronization layer.

## Future extension rule

New culture-dependent systems may append a concise section when they have an authoritative service worth auditing. Do not turn the report into a player-facing catalogue or duplicate technical histories already available elsewhere.

## Validated coverage

The final `0.3.15-dev` matrix confirmed:

- correct reporting for ordinary humans, Free Jaffa, Goa'uld-aligned Jaffa, Goa'uld hosts and pre-joined Tok'ra;
- stable persistent Tok'ra data across an active-personality switch;
- identical output through the developer action and advanced settings access;
- complete hiding outside authorized technical access;
- no mutation after repeated inspection or save/load;
- a clean `Player.log` for the tested scope.
