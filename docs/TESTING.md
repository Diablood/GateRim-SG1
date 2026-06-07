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
   <color=#D9B44A>[GateRim SG-1]</color> Version 0.1.21.0 loaded.
   ```


## Colored logging prefix regression check

1. Build with:
   ```powershell
   .\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
   ```
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Inspect `Player.log`.
4. Confirm the bootstrap line contains:
   ```text
   <color=#D9B44A>[GateRim SG-1]</color>
   ```


## Persistent Goa'uld symbiote identity

1. Build the assembly with `build.cmd`.
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Select a humanoid pawn.
4. Add the health state:
   ```text
   adult Goa'uld symbiote
   ```
5. Open the health-state description and copy the displayed symbiote ID.
6. Save the game.
7. Reload the save.
8. Confirm that the displayed symbiote ID is unchanged.
9. Inspect `Player.log`.
10. Confirm that `Attached` and `Loaded` messages use the same symbiote ID.
11. Remove the Hediff and confirm a `Detached` message appears.

Optional regression check:

1. Add `recent Goa'uld implantation`.
2. Confirm that it also receives a persistent symbiote ID.
3. Confirm that the temporary state still disappears after one in-game day.


## Forced Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free `Goa'uld symbiote`.
3. Place one adult humanoid pawn in an adjacent cell.
4. Select the symbiote and record its free-symbiote ID.
5. Click `Forced implantation`.
6. Confirm that the free symbiote disappears.
7. Confirm that the target receives `recent Goa'uld implantation`.
8. Confirm that the ID displayed on the Hediff matches the former free-symbiote ID.
9. Save and reload.
10. Confirm that the transferred ID remains unchanged.
11. Inspect `Player.log` for the transfer lifecycle.

Negative checks:

- no adjacent compatible humanoid;
- child under 13;
- animal target;
- pawn already implanted;
- pawn already carrying an adult Goa'uld symbiote state.


## Active Goa'uld host conversion

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through the manual forced-implantation command.
3. Record the persistent symbiote ID.
4. Save and reload during `recent Goa'uld implantation`.
5. Let the one-day countdown expire.
6. Confirm that the recent state disappears.
7. Confirm that `adult Goa'uld symbiote` appears.
8. Confirm that the ID is unchanged.
9. Confirm active-host modifiers and preservation of the original germline xenotype.
10. Save and reload after conversion.
11. Confirm that the ID remains unchanged.
12. Inspect `Player.log` for preparation, conversion and safe-removal logs.


## Emergency Goa'uld extraction prototype

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through `Forced implantation`.
3. Record the persistent symbiote ID.
4. Select the implanted host before the critical countdown expires.
5. Click `Emergency extraction`.
6. Confirm that recent implantation disappears.
7. Confirm that a free Goa'uld symbiote pawn appears nearby.
8. Confirm that the free pawn displays the same ID.
9. Re-implant the extracted pawn and confirm the ID remains unchanged.
10. Save and reload after extraction.
11. Confirm that the free pawn ID remains unchanged.
12. Inspect `Player.log` for reverse-transfer lifecycle logs.


## Emergency Goa'uld extraction surgery

1. Build with `build.cmd`.
2. Implant an adult humanoid through `Forced implantation`.
3. Open the health tab and schedule:
   ```text
   emergency Goa'uld extraction
   ```
4. Provide a doctor with Medicine `6+`, a bed and medicine.
5. Let the bill complete.
6. On success, confirm that recent implantation disappears.
7. Confirm that a nearby free symbiote displays the same ID.
8. Re-implant the extracted pawn and confirm the ID remains unchanged.
9. Save and reload after extraction.
10. Confirm persistence.
11. Test a lower-quality medical setup.
12. Confirm that a failed surgery leaves recent implantation in place.
13. Inspect `Player.log`.


## Autonomous free-symbiote hunt

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote several cells away from an adult humanoid.
3. Confirm `Autonomous hunt` is enabled.
4. Wait for the pursuit job to start.
5. Confirm movement toward the target.
6. Confirm automatic implantation on contact.
7. Confirm the persistent ID transfer.
8. Extract the parasite through surgery.
9. Confirm the free pawn returns with a non-zero cooldown.
10. Confirm it does not immediately re-implant the patient.
11. Wait for cooldown expiry and confirm pursuit resumes.
12. Toggle autonomous hunt off and on.
13. Confirm the toggle interrupts and restores the autonomous behavior.
14. Inspect `Player.log`.
