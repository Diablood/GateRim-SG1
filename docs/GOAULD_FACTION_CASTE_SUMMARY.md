# Goa'uld faction caste summary

## Goal

RimWorld's Create World faction tooltip derives its member summary from
`FactionDef.xenotypeSet`. GateRim correctly declares the dominant Jaffa
xenotype there, but Goa'uld possession is an acquired persistent parasitic
state and must not be added as a germline xenotype merely for display.

## Implementation

`GoauldFactionCasteSummaryPatch` adds a localized qualitative section to the
calculated `FactionDef.Description` only when the definition is
`SG1_GoauldSystemLordPrototype`. It leaves the cached vanilla description and
the generation data untouched.

The section identifies:

- Jaffa servants as the dominant population;
- Goa'uld hosts as a settlement minority;
- the System Lord host as the faction leader;
- possession as an acquired state absent from xenotype percentages.

This patch changes no faction count, settlement option, pawn kind, raid,
leader, name, host identity or save data.

## Mandatory r1 test

1. Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1`.
2. Open `Nouvelle colonie` > `Équipe SG isolée` > `Créer le monde` > `Factions`.
3. Hover `Domaines des Grands Maîtres Goa'uld` and verify the vanilla description and xenotype section followed by `Castes Goa'uld`.
4. Hover `Jaffa libres` and verify that no Goa'uld caste section appears.
5. Generate the world and inspect `Player.log`.

Expected result: the localized caste summary appears only for the Goa'uld
faction, world generation succeeds and no new Harmony, translation or C# error
is logged.
