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
   <color=#D9B44A>[GateRim SG-1]</color> Version 0.1.42.0 loaded.
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


## Ritual Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Disable autonomous hunt.
4. Place one compatible humanoid within 12 cells but not adjacent.
5. Select the symbiote and record its persistent ID.
6. Click `Ritual implantation`.
7. Confirm that the nearest valid humanoid receives recent implantation.
8. Confirm that the free pawn disappears.
9. Confirm identity persistence.
10. Save and reload.
11. Confirm the same ID remains visible.
12. Repeat without a valid target in range and confirm rejection.


## Explicit ritual map target selection

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Place two compatible humanoids within `12` cells.
4. Disable autonomous hunt for a controlled test.
5. Select the symbiote and click `Ritual implantation`.
6. Confirm that a map-targeting cursor appears.
7. Click the farther valid humanoid.
8. Confirm that the clicked pawn, not the nearest pawn, receives recent implantation.
9. Confirm persistent identity transfer.
10. Repeat with an invalid pawn, an out-of-range pawn and an unreachable pawn.
11. Confirm rejection without consuming the free symbiote.
12. Save and reload after a valid ritual.
13. Confirm identity persistence.


## Timed ritual ceremony and cancellation

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote and disable autonomous hunt.
3. Start `Ritual implantation`.
4. Select one compatible reachable humanoid within `12` cells.
5. Confirm that implantation is not immediate.
6. Confirm the inspection panel displays the target and remaining ticks.
7. Save and reload during the ceremony.
8. Confirm the countdown resumes.
9. Let the countdown reach zero.
10. Confirm recent implantation and persistent identity transfer.
11. Start another ritual, click `Cancel ritual`, and confirm no transfer occurs.
12. Start another ritual and move the target out of range.
13. Confirm automatic cancellation without consuming the free symbiote.


## Goa'uld ritual basin requirement

1. Build with `build.cmd`.
2. Build or spawn `Goa'uld ritual basin`.
3. Spawn a free symbiote and disable autonomous hunt.
4. Keep the symbiote and one compatible target within `6` cells of the basin.
5. Start ritual implantation and select the target.
6. Confirm the inspection panel displays the ritual basin.
7. Save and reload during the ceremony.
8. Confirm the basin reference and countdown persist.
9. Complete the ritual and confirm identity transfer.
10. Start another ritual, destroy the basin, and confirm automatic cancellation.
11. Start another ritual and move the target beyond `6` cells from the basin.
12. Confirm cancellation without consuming the free symbiote.
13. Try starting without a nearby basin and confirm rejection.


## Jaffa Prim'ta implantation procedure

1. Build with `build.cmd`.
2. Spawn a newly generated Jaffa.
3. Open the health-tab operation menu.
4. Confirm `implant Jaffa Prim'ta` is available.
5. Schedule the procedure.
6. Provide one medicine and a doctor with Medicine `4+`.
7. Let the operation complete.
8. Confirm `Prim'ta symbiote` appears.
9. Confirm the expected biological modifiers.
10. Save and reload.
11. Confirm persistence and the `Loaded Jaffa Prim'ta symbiote` log.
12. Confirm the implantation operation is hidden while Prim'ta is present.
13. Select a baseliner and confirm the operation is unavailable.
14. Remove the Hediff in developer mode and confirm the removal log.


## Physical Prim'ta larva resource

1. Build with `build.cmd`.
2. Spawn one `Prim'ta larva` through developer tools.
3. Confirm the physical item can be hauled and stored.
4. Spawn a compatible Jaffa.
5. Schedule `implant Jaffa Prim'ta`.
6. Confirm the operation requires one medicine and one larva.
7. Let the surgery complete.
8. Confirm the larva is consumed.
9. Confirm `Prim'ta symbiote` appears.
10. Save and reload.
11. Confirm persistence.
12. Try the same workflow without an available larva.
13. Confirm the bill waits for the missing ingredient.


## Prim'ta larva acquisition prototype

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Open its Bills tab.
4. Add `incubate Prim'ta larva`.
5. Let a colonist complete the Intellectual work.
6. Confirm one physical `Prim'ta larva` appears.
7. Confirm hauling, storage and stacking still work.
8. Use the produced larva in `implant Jaffa Prim'ta`.
9. Confirm the surgery consumes it and adds `Prim'ta symbiote`.
10. Save and reload after production and after implantation.


## Prim'ta incubation work-giver fix

1. Restart RimWorld so XML Defs are reloaded.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` can prioritize the basin manually.
5. Confirm the same pawn starts the bill automatically when Handling work is enabled.
6. Confirm a pawn below Animals `4` is rejected with a minimum-skill message.
7. Let the work complete and confirm one physical larva appears.


## Prim'ta incubation nutrient requirements

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` is eligible.
5. Leave the map without raw meat and confirm the bill waits.
6. Add fewer than `10` raw-meat units and confirm the bill still waits.
7. Add at least `10` raw-meat units.
8. Confirm the pawn hauls the meat and completes the bill.
9. Confirm `10` units of raw meat are consumed.
10. Confirm one physical `Prim'ta larva` appears.
11. Complete the existing Jaffa implantation workflow.


## Prim'ta larva preservation prototype

1. Build with `build.cmd`.
2. Produce or spawn one `Prim'ta larva`.
3. Select the item and confirm rotting/spoilage information appears.
4. Store one larva at room temperature.
5. Confirm rot progresses.
6. Store one larva in a cold room or freezer.
7. Confirm it is preserved better than the room-temperature larva.
8. Let a warm larva fully rot.
9. Confirm it is destroyed.
10. Implant a fresh larva into a compatible Jaffa and confirm the medical loop still works.


## Prim'ta larva biological storage category

1. Build with `build.cmd`.
2. Start RimWorld and inspect the log for XML errors.
3. Produce or spawn one `Prim'ta larva`.
4. Open a stockpile storage filter.
5. Confirm the larva appears under:
   ```text
   raw resources
       ↓
   Goa'uld biological products
   ```
6. Confirm it no longer appears under `manufactured`.
7. Confirm it is not presented as raw food or an animal food product.
8. Confirm hauling, stacking, rotting and Jaffa implantation still work.


## Prim'ta larva temperature tuning

1. Build with `build.cmd`.
2. Spawn or incubate several `Prim'ta larva` items.
3. Select one larva and confirm the thermal inspection lines appear.
4. Store larvae below `0 °C`, around `5 °C`, around `20 °C`, above `25 °C`
   and above `40 °C`.
5. Confirm the displayed effective rates are respectively approximately:
   ```text
   ×0
   ×0.5
   ×1
   ×2
   ×3
   ```
6. Confirm hot larvae deteriorate faster than room-temperature larvae.
7. Confirm frozen larvae stop deteriorating for this prototype.
8. Confirm storage category, hauling, stacking, incubation and implantation
   regressions remain valid.


## Jaffa Prim'ta implantation age eligibility

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa below `10` biological years.
3. Confirm `implant Jaffa Prim'ta` is absent from the operations list.
4. Spawn a compatible Jaffa aged exactly `10` biological years.
5. Confirm the operation appears.
6. Complete the normal surgery with one medicine and one larva.
7. Confirm `Prim'ta symbiote` is attached.
8. Confirm the duplicate-operation guard still works.
9. Save and reload.
10. Confirm persistence.


## Jaffa puberty dependency prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `11` without Prim'ta.
3. Wait at least one in-game hour and confirm no dependency appears.
4. Spawn a compatible Jaffa aged `12` without Prim'ta.
5. Wait up to one in-game hour.
6. Confirm `Prim'ta deficiency` appears.
7. Accelerate time and confirm progressive severity stages.
8. Confirm immunity and healing modifiers worsen.
9. Save and reload during progression.
10. Confirm severity persists.
11. Implant a physical larva with the existing medical procedure.
12. Confirm the dependency disappears immediately.


## Jaffa Prim'ta cultural thoughts

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `9` without Prim'ta.
3. Confirm `awaiting Prim'ta` is absent.
4. Spawn a compatible Jaffa aged `10` without Prim'ta.
5. Confirm `awaiting Prim'ta` appears with mood `-1`.
6. Implant a physical larva with the existing surgery.
7. Confirm `awaiting Prim'ta` disappears.
8. Confirm `received Prim'ta` appears with mood `+3`.
9. Remove the Prim'ta in developer mode.
10. Confirm `awaiting Prim'ta` returns.
11. Save and reload.
12. Reimplant a larva.
13. Confirm the `received Prim'ta` memory is not granted again.
14. Confirm the puberty dependency remains separate and still works from age `12`.


## Jaffa tretonin substitution prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `12+` without Prim'ta.
3. Wait for `Prim'ta deficiency`.
4. Spawn one `tretonin dose`.
5. Schedule `administer tretonin` from the health tab.
6. Confirm the dose is consumed.
7. Confirm the dependency disappears immediately.
8. Confirm `tretonin substitution` appears with remaining time.
9. Save and reload.
10. Confirm remaining duration persistence.
11. Let one day expire.
12. Confirm substitution disappears.
13. Wait for the next hourly dependency scan.
14. Confirm dependency returns.
15. Confirm Jaffa with implanted Prim'ta cannot receive tretonin.
16. Confirm waiting-thought behavior remains separate.


## Tretonin acquisition prototype

1. Build with `build.cmd`.
2. Build or spawn the vanilla `DrugLab`.
3. Confirm `prepare tretonin doses` appears in its Bills tab.
4. Confirm the minimum Intellectual skill is `6`.
5. Test with no larva and confirm the bill waits.
6. Test with no medicine and confirm the bill waits.
7. Supply one `Prim'ta larva` and one medicine unit.
8. Complete the bill.
9. Confirm both inputs are consumed.
10. Confirm exactly five `tretonin dose` items appear.
11. Confirm storage and stacking.
12. Administer one dose to an eligible Jaffa.
13. Confirm the existing one-day substitution workflow remains valid.


## Formal Jaffa Prim'ta ceremony prototype

1. Build with `build.cmd`.
2. Construct or spawn one `Goa'uld ritual basin`.
3. Place one physical `Prim'ta larva` within `6` cells.
4. Place one compatible Jaffa aged `10+` within `6` cells.
5. Select the basin and start `Formal Prim'ta ceremony`.
6. Target the Jaffa.
7. Confirm the inspection panel shows target, reserved larva and progress.
8. Save and reload during the rite.
9. Confirm progress persists.
10. Let the rite complete.
11. Confirm one larva is consumed and `Prim'ta symbiote` appears.
12. Confirm dependency relief and cultural-memory behavior.
13. Test manual cancellation.
14. Test automatic cancellation after moving the larva away.
15. Confirm cancelled ceremonies do not consume the larva.
16. Confirm the medical implantation operation still works independently.


## Formal Jaffa Prim'ta ceremony ticker regression

1. Restart RimWorld after applying the XML fix.
2. Construct or reuse one `Goa'uld ritual basin`.
3. Place one eligible Jaffa and one larva within `6` cells.
4. Start `Formal Prim'ta ceremony`.
5. Select the basin.
6. Confirm the remaining duration decreases from `600 / 600`.
7. Let the ceremony complete and confirm one larva is consumed.
8. Confirm the Prim'ta Hediff is attached.


## Tok'ra foundation prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra symbiote` through developer tools.
3. Confirm the inspection panel displays origin `Tok'ra` and autonomous hunt `disabled`.
4. Confirm only `Voluntary Tok'ra implantation` is available.
5. Confirm forced implantation, Goa'uld ritual implantation and autonomous hunt are absent.
6. Place a player-controlled compatible adult within `12` cells.
7. Start voluntary implantation and target the colonist.
8. Confirm recent implantation uses the same persistent ID.
9. Save and reload.
10. Wait one day and confirm active Tok'ra symbiosis.
11. Repeat and extract during recent implantation.
12. Confirm the free pawn returns as `Tok'ra symbiote`.
13. Confirm origin remains `Tok'ra` and hunt remains disabled.
14. Spawn a normal `Goa'uld symbiote`.
15. Confirm previous Goa'uld forced, ritual and autonomous workflows remain available.


## Tok'ra FactionDef loading regression

1. Apply the `0.1.39-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following errors no longer appear:
   ```text
   hairTags doesn't correspond to any field in type FactionDef
   startingGoodwill doesn't correspond to any field in type FactionDef
   naturalColonyGoodwill doesn't correspond to any field in type FactionDef
   raidLootValueFromPointsCurve must be defined
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.39.0 loaded.
   ```
6. Continue the Tok'ra voluntary-implantation regression tests.


## Tok'ra voluntary-host pawn prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra voluntary host` through developer tools.
3. Confirm the pawn is player-controlled.
4. Wait up to `60` ticks.
5. Confirm `adult Goa'uld-family symbiote` appears in the Health tab.
6. Confirm the persistent origin is `Tok'ra`.
7. Save and reload.
8. Confirm the same symbiote identity persists.
9. Spawn a second prototype host.
10. Confirm the second pawn receives a distinct identity.
11. Confirm free Tok'ra voluntary implantation still works.
12. Confirm normal Goa'uld workflows remain available.


## Tok'ra voluntary-host resistance-range regression

1. Apply the `0.1.40-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following error no longer appears:
   ```text
   Config error in SG1_TokraVoluntaryHost: initial resistance range is undefined for humanlike pawn kind.
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.40.0 loaded.
   ```
6. Repeat the developer-spawn and save/reload regression tests.


## Tok'ra pawn-group foundation

1. Restart RimWorld completely.
2. Open `Player.log`.
3. Confirm no XML or Def-validation errors reference:
   ```text
   SG1_TokraSmallTeam
   SG1_TokraVisitorPrototype
   ```
4. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
5. Confirm the Tok'ra faction remains hidden and non-generated.
6. Spawn `Tok'ra voluntary host` and confirm one-time Tok'ra initialization.
7. Spawn `Tok'ra symbiote` and confirm voluntary implantation only.
8. Spawn `Goa'uld symbiote` and confirm previous hostile workflows.


## Tok'ra pawn-group nested-profile regression

1. Apply the `0.1.41-dev-r1` patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm this error no longer appears:
   ```text
   Type PawnGroupMakerDef is not a Def type or could not be found
   ```
5. Confirm no new `SG1_Tokra`, `pawnGroupMakers` or
   `maxPawnCostPerTotalPointsCurve` error appears.
6. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
7. Repeat Tok'ra-host, free-Tok'ra and Goa'uld regression tests.


## Tok'ra peaceful visitor prototype

1. Build with `build.cmd`.
2. Restart RimWorld completely.
3. Open developer tools.
4. Run:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
5. Confirm a neutral letter appears.
6. Confirm `1` to `3` Tok'ra hosts enter from the map edge.
7. Wait up to `60` ticks.
8. Confirm each visitor receives active Tok'ra symbiosis.
9. Confirm visitors are not player-controlled.
10. Confirm automatic departure after the visit.
11. Save and reload after the first visit.
12. Trigger the incident again.
13. Confirm the same hidden Tok'ra faction instance is reused.
14. Confirm no random storyteller visits, traders or settlements are enabled.
15. Repeat free-Tok'ra, Tok'ra-host and Goa'uld regression tests.


## Tok'ra peaceful-visitor faction-generator build regression

1. Apply the `0.1.42-dev-r1` patch.
2. Rebuild with `build.cmd`.
3. Confirm the compiler no longer reports:
   ```text
   CS1503: cannot convert from 'RimWorld.FactionDef' to 'RimWorld.FactionGeneratorParms'
   ```
4. Restart RimWorld completely.
5. Trigger:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
6. Confirm the hidden Tok'ra faction is created and the peaceful visit starts.

## 0.2.24-dev - Tok'ra safehouse follow-up lead

Suggested validation:

1. Build with a forced C# rebuild.
2. Prepare a safehouse test and keep Tok'ra trust neutral.
3. Enter the safehouse and exchange with the contact: the dialogue should give Medicine XP, but no follow-up lead.
4. Raise Tok'ra trust to cooperative with the debug step action, prepare/create a fresh safehouse, then exchange: one follow-up safehouse lead should be stored if capacity remains.
5. Raise Tok'ra trust to trusted and repeat with a fresh contact: one follow-up lead should again be stored if capacity remains.
6. Fill the lead registry to the cap and repeat: the briefing should report that stored lead capacity is already full.
7. Confirm the contact remains non-trading, non-recruitable, non-hostile and once per generated contact.

## Tok'ra organic operation opportunities

Use a player home map with a powered Tok'ra secure communicator, one colon capable of Intellectual work and developer mode enabled.

1. At Tok'ra trust `0`, force each available preliminary archetype and confirm both can be accepted before Trusted contact.
2. Ignore an unsolicited offer and confirm it expires without changing trust.
3. Complete the Goa'uld-observation flow and confirm `+3` trust, Intellectual XP and save persistence.
4. Accept an intelligence-recovery offer and confirm the same delivery routing already used by existing Tok'ra deliveries and caches:
   - beside or on the Tok'ra delivery drop zone when one exists;
   - beside a powered Tok'ra secure communicator when no delivery zone exists;
   - a reachable, unfogged map-edge cell only when neither a delivery zone nor a communicator is available for delivery routing.
5. Confirm the GateRim log reports both the preferred delivery cell and the final module cell, then repeat the three routing cases above.
6. Secure the intelligence module and confirm `+2` trust, Intellectual XP, objective removal and no material reward.
7. Destroy or expire an accepted intelligence module and confirm `-1` trust is applied exactly once.
8. Save and reload during offered and accepted states and confirm deadlines, active objective references and anti-repetition context persist.
9. Verify both organic archetypes remain selectable, with the last offered archetype strongly discouraged but not forbidden.
10. Re-test existing manual communicator actions and confirm their Trusted-tier requirements and independent cooldowns remain unchanged.
11. Check English and French player-facing text and confirm no red errors in `Player.log`.
