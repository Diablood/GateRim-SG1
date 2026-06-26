# Faction world icons

Version: `0.3.50-dev`

## Scope

This milestone gives the visible GateRim SG-1 factions dedicated world-faction icons instead of relying on vanilla settlement-house or pirate-outpost silhouettes.

The current icon paths are:

```text
SG1_FreeJaffa                  -> World/WorldObjects/Expanding/SG1_FreeJaffa
SG1_GoauldSystemLordPrototype  -> World/WorldObjects/Expanding/SG1_GoauldSystemLords
SG1_Tokra                      -> World/WorldObjects/Expanding/SG1_Tokra
SG1_PlayerSGCExpedition        -> World/WorldObjects/Expanding/SG1_SGCExpedition
```

## Visual direction

- Free Jaffa: Jaffa spear and broken chains, to read as liberated warriors rather than a generic settlement.
- Goa'uld System Lords: pyramid, throne/crown geometry and serpent silhouette, to read as imperial Goa'uld power.
- Tok'ra: existing circular Tok'ra icon from `0.3.49-dev`, kept unchanged.
- SGC expedition: shield, team and Stargate-ring silhouette, to read as a stranded player expedition.

The first local revision used detailed white/alpha silhouettes. In-game testing confirmed that faction-color tinting worked, but the icons were too detailed and too thin at the final UI size.

Revision `r2` keeps the same 128x128 PNG size and the same tinting principle, but uses fewer internal details, larger masses and thick dark outlines. The fill remains light so RimWorld can tint it; the dark outline stays dark and keeps the icon readable on the world-faction UI.

In-game validation accepts `r2`. A slight color variation remains visible on repeated faction copies, but that variation is produced by RimWorld's vanilla faction-tint handling and is intentional for this milestone.

## Implementation

No C# change is required. RimWorld 1.6 already reads `FactionDef.factionIconPath` for faction UI icons.

The XML-only path changes are:

```text
1.6/Defs/FactionDefs/SG1_FreeJaffa.xml
1.6/Defs/FactionDefs/SG1_GoauldSystemLordPrototype.xml
1.6/Defs/FactionDefs/SG1_PlayerSGCExpedition.xml
```

The Tok'ra path remains:

```text
World/WorldObjects/Expanding/SG1_Tokra
```

## Manual test checklist

1. Load `Core`, `Harmony`, `Biotech`, then `GateRim SG-1`.
2. Open `New colony`.
3. Select `Équipe SG isolée` / `Stranded SG team`.
4. Go to `Create world`.
5. In `Factions`, verify the simplified Free Jaffa, Goa'uld and existing Tok'ra icons.
6. Use `Add...` to add several Free Jaffa and Goa'uld copies.
7. Confirm that copies share the same silhouette but still differ by vanilla faction tint.
8. Start the scenario and inspect the `Factions` tab for the SGC expedition icon.
9. Check `Player.log` for texture, XML and Harmony errors.
