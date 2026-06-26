# Tok'ra optional world-faction baseline

## Purpose

`SG1_Tokra` is a selectable non-territorial faction. It behaves like a world-generation content toggle rather than a territorial settlement faction.

## World-generation contract

- shown in the configurable faction list;
- selected once by default;
- configurable count limited to `0` or `1`;
- displayed with the dedicated `World/WorldObjects/Expanding/SG1_Tokra` faction icon;
- hidden from the ordinary in-game diplomacy list;
- no settlements because the generated faction is hidden and its settlement weight is `0`;
- no mandatory fallback count outside the player's selection.

## Disabled-game contract

When the player removes the Tok'ra before generating the world:

- no `SG1_Tokra` faction instance exists;
- the world-generation screen displays a yellow warning before the world is generated;
- no runtime component or utility recreates one;
- Tok'ra storyteller incidents are ineligible;
- the introduction arc and recurrent-operation scheduler remain dormant;
- secure-communicator Tok'ra interactions are hidden;
- save/reload preserves the disabled state.

The rest of GateRim SG-1 remains available.

## Enabled-game contract

When the default Tok'ra entry is retained:

- exactly one hidden faction instance is generated;
- no Tok'ra settlement appears;
- all existing Tok'ra incidents, missions and operations resolve that same instance;
- duplicate saved instances are reported but never deleted automatically.

## Compatibility

The historical methods `GetOrCreatePersistentFaction` and `GetOrCreateHiddenFaction` remain available for source compatibility, but both are resolver-only from `0.3.49-dev` onward. Their names must not be interpreted as permission to recreate a faction the player removed.

`GameComponent_TokraWorldPresenceInitializer` remains as an empty compatibility shell so older saves can deserialize safely without restoring mandatory behavior.

## Warning implementation

Vanilla displays dedicated yellow warnings when mechanoids or insects are removed, but RimWorld 1.6 exposes no generic warning-text field on `FactionDef`.

`0.3.49-dev-r5` therefore uses the declared Harmony mod dependency for one narrow UI integration: `TokraWorldFactionSelectionWarningPatch` appends the Tok'ra warning line to the existing `WorldFactionsUIUtility.DoWindowContents` warning buffer after vanilla resets `warningHeight` and before RimWorld calculates and draws the yellow warning block. The `r5` placement avoids the branch-target skip observed in `r4`.

The patch does not create a faction, change the selected count or override the player's choice. It only explains the consequence before world generation.
