# Testing workflow

## Minimal isolated test

Use this active mod list first:

```text
Core
Biotech
GateRim SG-1
```

This isolates GateRim definitions from unrelated third-party gene categories and patches.

## Jaffa foundation checklist

1. Start RimWorld with the minimal isolated mod list.
2. Open the xenotype editor.
3. Confirm that the editor opens without exceptions.
4. Load the premade `Jaffa` xenotype.
5. Confirm that `Jaffa physiology` displays a texture.
6. Confirm that `Jaffa longevity` displays a `150%` lifespan factor.
7. Switch to French and verify the translated labels and descriptions.
8. Close the game and inspect `Player.log`.

## Interpreting the first external test log

The first external log contained two categories of issues:

### GateRim issues corrected in 0.1.5-dev
- Leading and trailing whitespace in the Jaffa xenotype description.
- French translation values formatted across multiple lines.
- Missing `UI/Icons/Genes/Gene_Robust` texture.

### Third-party compatibility issue to isolate separately
- `KeyNotFoundException` for a gene category named `Ability`.

The `Ability` category is not declared by the current GateRim definitions. Re-run the minimal isolated test before investigating loaded third-party mods.


## Free Goa'uld symbiote prototype

Use developer mode to spawn:

```text
SG1_GoauldSymbiote
```

Checklist:

1. Confirm the pawn appears with its temporary sprite.
2. Confirm movement and a weak bite attack.
3. Confirm that no natural biome spawn occurs.
4. Switch to French and verify the translated label and description.
5. Check `Player.log` for `SG1_GoauldSymbiote` errors.


## 0.1.9-dev XML regression check

After applying the free-symbiote XML correction:

1. Launch with `Core`, `Biotech`, and `GateRim SG-1`.
2. Confirm that `Player.log` no longer reports:
   ```text
   XML error: <wildness>1</wildness> doesn't correspond to any field in type RaceProperties.
   ```
3. Spawn `SG1_GoauldSymbiote` through developer mode.
4. Confirm that movement, the weak bite, and the temporary sprite still work.


## Recent Goa'uld implantation prototype

Add the following Hediff through developer mode to a humanoid pawn:

```text
SG1_GoauldRecentImplantation
```

Checklist:

1. Confirm the health tab displays `recent Goa'uld implantation`.
2. Confirm the remaining-time countdown appears.
3. Confirm the pawn receives additional pain.
4. Wait one in-game day and confirm the Hediff disappears.
5. Switch to French and verify the translated label, description and stage.
6. Check `Player.log` for `SG1_GoauldRecentImplantation` errors.


## Jaffa Prim'ta split

Use newly generated pawns after applying `0.1.13-dev`.

1. Generate a Jaffa pawn.
2. Confirm the germline gene list contains:
   ```text
   SG1_JaffaLineage
   SG1_JaffaPouchPotential
   SG1_JaffaSymbioteCompatibility
   SG1_JaffaPhysiology
   ```
3. Confirm `SG1_JaffaPrimta` is absent at birth or initial generation.
4. Add `SG1_JaffaPrimta` through developer mode.
5. Confirm immunity, healing, pain, damage and lifespan modifiers.
6. Remove the Hediff and confirm the modifiers disappear.
7. Check `Player.log` for `SG1_JaffaPrimta` errors.


## C# logging scaffold smoke test

1. Build the mod assembly with `build.ps1` or `build.sh`.
2. Confirm that `1.6/Assemblies/GateRimSG1.dll` exists locally.
3. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
4. Close the game after the main menu appears.
5. Inspect `Player.log`.
6. Confirm the presence of:
   ```text
   [GateRim SG-1] Version 0.1.14.0 loaded.
   ```
