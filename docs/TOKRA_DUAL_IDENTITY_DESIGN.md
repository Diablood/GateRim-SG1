# Tok'ra host / symbiote dual identity — phased design

Status: phase 1 implemented and validated in `0.3.10-dev`. Persistence and player-facing inspection are complete; the personality-switch gizmo and skill-offset logic remain deferred to a later dedicated milestone.

## Phased implementation

### Phase 1 — `0.3.10-dev`

Implemented and validated:

- store the current host name, childhood and adulthood in the existing persistent symbiote data;
- assign and store a Tok'ra adulthood from the shared cultural profile;
- preserve the same records through implantation, active-host conversion, save/load, extraction and reimplantation;
- show both identities in the health description only when the Tok'ra is a directly player-controlled colon;
- retain the technical transfer history in advanced debug information;
- do not alter the pawn's active name, active backstories or skills;
- expose no personality-switch gizmo.

No dedicated Tok'ra childhood set currently exists. Phase 1 therefore stores an optional symbiote childhood field for future use but configures only the six existing adult Tok'ra careers.

### Later phase — personality switching

Still deferred:

- player-only gizmo;
- active name and displayed-backstory switching;
- safe shared skill baseline and backstory-only offsets;
- active-personality persistence;
- detailed compatibility tests with social, quest and editor interfaces.

## Purpose

After a Tok'ra symbiote is implanted, the host must remain identifiable as the original person while the symbiote's own name and identity must not disappear. The resulting pawn represents two conscious identities sharing one body.

The feature is useful only when the Tok'ra pawn is genuinely controlled by the player. AI-managed visitors, allies, enemies, faction members and non-controlled quest pawns keep the current behavior and receive no manual personality switch.

## Player-facing concept

A player-controlled Tok'ra pawn may receive one gizmo that switches the active personality:

- host active → let the named symbiote take control;
- symbiote active → return control to the named host.

The active personality may change:

- the displayed primary name;
- the displayed childhood and adulthood;
- the title derived from those backstories;
- only the skill offsets attributable to the active backstories.

The switch must not change:

- the body, genes or xenotype;
- faction membership;
- relations;
- equipment or inventory;
- traits;
- health state, implants or injuries;
- progression earned during play;
- the permanent stored identities of host and symbiote.

The inspection interface must always expose both identities, even when one is inactive. A presentation such as `Jacob Carter — host of Selmak — active personality: Selmak` is the intended direction, not a final label.

## Persistent data

Use one pawn with a dedicated persistent component or equivalent save data containing at least:

- host name;
- host childhood and adulthood;
- symbiote name;
- symbiote childhood and adulthood;
- active personality;
- any baseline data required to calculate backstory-only skill differences safely.

Do not create two pawns. Do not overwrite the original identity irreversibly.

The symbiote identity may still be stored for AI-managed Tok'ra when useful for save consistency, extraction or later implantation, but no switch gizmo or manual personality mechanics are exposed to them.

## Skill handling requirement

The difficult part is not the gizmo or the displayed name. RimWorld does not automatically regenerate a pawn's skills when its `BackstoryDef` references are replaced.

The implementation must therefore preserve a shared progression baseline and apply only the difference produced by the active personality's backstories. Repeated switches must never duplicate, erase or permanently stack skill gains.

Conceptually:

`effective skill = shared earned baseline + active backstory offset`

Any XP earned while either personality is active remains common to the pawn unless a later design explicitly and safely introduces separate memories. Separate skill progression is not part of the current design.

Before implementation, audit all other backstory effects, including work restrictions and titles. A simple assignment of different `BackstoryDef` values is acceptable only if every resulting effect is intentionally handled.

## Eligibility

Default eligibility:

- pawn contains a valid Tok'ra host / symbiote identity pair;
- pawn belongs to the player faction;
- pawn is directly player-controlled;
- pawn is not merely an AI visitor, ally, enemy or uncontrolled quest pawn.

Temporary guests or quest pawns that become controllable require an explicit decision and dedicated tests. The safe default is to exclude them until they permanently join the colony.

## Vanilla reuse audit

Before adding a custom interface or data model, inspect reusable vanilla mechanisms for:

- pawn names and titles;
- story tracker and backstory display;
- skill offsets and cached values;
- relations and social inspection;
- Hediffs or components with persistent saved data;
- gizmo eligibility and player control checks;
- extraction, death and reimplantation flows.

The final implementation should reuse vanilla presentation where safe, but must not depend on a vanilla mechanism that loses one identity or recalculates skills destructively.

## Required prototype and tests

A dedicated milestone must prototype and validate at least:

1. voluntary implantation stores the symbiote name and histories;
2. the host identity remains unchanged and recoverable;
3. only a player-controlled Tok'ra receives the gizmo;
4. switching updates visible identity without changing faction, relations, body, equipment, traits or health;
5. backstory-derived skill differences switch exactly once and never stack;
6. XP gained before and after switches remains persistent and shared;
7. save, full quit and reload preserve both identities and the active state;
8. death, resurrection if supported, extraction and reimplantation preserve or transfer identity correctly;
9. AI-managed Tok'ra continue to use the current classic behavior;
10. letters, social tabs, health tabs, inspection strings and compatible pawn editors do not expose contradictory identities.

## Framework integration

This feature should consume the shared `0.3.x` cultural and identity framework rather than introduce Tok'ra-only duplicate logic where a reusable service is appropriate.

Potential reusable data includes:

- cultural profile references;
- name generators;
- host and symbiote identity records;
- compatible backstory sets;
- player-control eligibility;
- debug reports for ambiguous or incomplete identity data.

The dedicated milestone must remain free to revise this design after the vanilla API audit and prototype results.
