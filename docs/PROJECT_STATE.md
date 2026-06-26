# Current project state

Current milestone: `0.3.49-dev - Add optional Tok'ra world-faction selection` - published as the latest milestone.

## Repository state

- Starting tag: `v0.3.48-dev`.
- Active branch: `feature/tokra-world-faction-selection-audit`.
- Published version: `0.3.49-dev`.
- Published tag: `v0.3.49-dev`.
- Technical assembly version: `0.3.49.0`.
- Functional validation is complete. The main repository and wiki are published for this milestone.

## Published Scope

The initial `r1/r2` interpretation was reversed after comparison with the vanilla mechanoid and insect faction controls. Revision `r3` functionally validated the core behavior, but tester feedback made the missing warning and faction icon part of the same milestone.

Revision `r4` validated the dedicated icon but did not display the warning. The Harmony insertion was placed before a vanilla branch target, so normal warning paths skipped the Tok'ra append call.

Final revision `r5`:

- displays `SG1_Tokra` in the world-faction selection;
- selects one Tok'ra faction by default and limits the configurable count to `0` or `1`;
- keeps the generated faction hidden from the ordinary diplomacy list and gives it no settlements;
- removes the mandatory count and all runtime recreation of a missing faction;
- treats absence as an intentional player choice that disables Tok'ra-generated content for that save;
- makes the introduction scheduler, recurrent-operation scheduler, storyteller incidents and communicator interactions ineligible when no Tok'ra faction exists;
- keeps the existing resolver method names for source compatibility, but they now resolve only and never create a replacement;
- retains a developer audit report without any command that overrides the player's selection;
- adds a dedicated Tok'ra faction icon through `FactionDef.factionIconPath`;
- adds a yellow world-generation warning when the Tok'ra entry is removed;
- declares the Harmony mod dependency and references `0Harmony.dll` only for compilation, with `Private=false` so GateRim SG-1 does not bundle the DLL.

## Technical approach

- XML is sufficient for the faction icon: `SG1_Tokra` points to `World/WorldObjects/Expanding/SG1_Tokra`.
- C# is required for the yellow warning. RimWorld 1.6 has no generic warning field on `FactionDef`; vanilla warnings are assembled inside `WorldFactionsUIUtility.DoWindowContents`.
- The Harmony patch is limited to a transpiler that appends the Tok'ra line to the existing vanilla warning text buffer before RimWorld calculates and draws the yellow warning block.
- Revision `r5` injects the append call after vanilla resets `WorldFactionsUIUtility.warningHeight` and before the text-length check, matching the immediate mechanoid/insect warning flow while avoiding the branch-target skip observed in `r4`.

## Intended player behavior

The Tok'ra now follow the same high-level opt-out principle as vanilla non-settlement factions:

- default world: one Tok'ra faction exists, without cities;
- player removes Tok'ra before world generation: a yellow warning immediately explains that Tok'ra content will be disabled;
- re-adding Tok'ra removes that warning;
- the rest of GateRim SG-1 remains available;
- Tok'ra visitors, incidents, introduction questline, communicator requests and recurrent operations do not occur when removed;
- save/reload does not recreate the removed faction.

## Deliberate limits

This milestone does not:

- add Tok'ra settlements, traders, military aid, raids or territorial diplomacy;
- add a second Tok'ra faction or allow a count above one;
- alter existing saves that already contain a Tok'ra faction;
- implement the future GateRim-only world preset;
- start the global faction-icon overhaul beyond the single Tok'ra world-selection icon required by this validation.

## Validation status

Mandatory r5 happy path reported OK by the tester:

- `Core`, `Harmony`, `Biotech`, then `GateRim SG-1` load order;
- `New colony` > `Create world` > `Factions`;
- `Tok'ra` row visible, selected once by default and using the dedicated icon;
- removing `Tok'ra` displays the yellow `Warning:` line immediately;
- `Add...` > `Tok'ra` removes the warning again;
- `Player.log` accepted by the tester as part of the test pass.

Optional regression coverage:

- generate one world with Tok'ra enabled and confirm one hidden instance, zero settlements and working Tok'ra content;
- generate another world after removing Tok'ra and confirm zero instances and zero settlements;
- in the Tok'ra-disabled game, wait through storyteller checks and verify that no Tok'ra offer, visitor, support incident or recurrent operation appears;
- verify that right-clicking a secure communicator exposes no Tok'ra actions when the faction is absent;
- save and reload the disabled game and confirm the faction is not recreated;
- inspect the developer audit through `GateRim SG-1 > Tok'ra... > World selection... > Show audit` in both configurations.
