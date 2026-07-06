# Current milestone test procedure

Jalon : `0.3.84-dev - Add first bounded Goa'uld territorial takeover`

Revision to test: `r1`

Version de DLL attendue : `0.3.84.0`

Status: **focused functional validation completed successfully on final
revision `r1`; validated and published as `v0.3.84-dev`**.

The maintainer accepted the required path: build and consistency checks,
natural-source reservation, pre-resolution save/reload, one bounded
`CompletedTransfer`, immediate world presentation and targeted letter,
post-resolution save/reload, cooldown persistence and required regressions.
Optional boundary and migration cases are not claimed unless tested separately.

## Purpose

Validate one real but bounded transfer of an existing Goa'uld settlement. The
same world object, ID, name and tile must remain while only its owning Goa'uld
domain changes. No settlement or faction may be created or destroyed.

## Automated checks

From the repository root:

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
git diff --check
```

Expected version:

```text
Assembly: 0.3.84.0
```

Do not continue if the build or a consistency check fails.

## Required focused test

Use developer mode, the **Commandement SG-1** storyteller and a world containing
at least three Goa'uld domains and enough permanent settlements for the
`active domains + 2` threshold. For three active domains, at least five permanent
Goa'uld settlements are required.

### 1. Prepare exact open conflicts

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
> Set all pairs: Open conflict
```

Then use `Show relation report` and confirm every active pair reports GateRim
`OpenConflict`, vanilla `Hostile` and `coherent: yes`.

### 2. Reset and inspect territorial strategy

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
> Territorial strategy...
```

Use, in order:

```text
Reset territorial strategy state
Show territorial strategy report
```

Expected report:

- schema `2`;
- storyteller active and territorial simulation available;
- at least two active territorial domains;
- exact permanent settlement counts and threshold `active domains + 2`;
- expansion weights `1.00 / 0.50 / 0.25 / 0.10`;
- delay multipliers `1 / 2 / 4 / 8`;
- automatic share ceiling `50%` for this three-domain test; the report also documents `75%` for an exactly two-domain world;
- natural cadence `45–90` days with a future next-attempt deadline;
- no pending reservation;
- diplomatic coherence remains `yes`.

### 3. Exercise the natural scheduler

Use:

```text
Run natural territorial attempt now
Show territorial strategy report
```

Expected result:

- exactly one takeover is pending globally;
- source is `natural scheduler`;
- exact gaining and losing domains are shown;
- the exact settlement label and world-object ID are shown;
- its current owner is the losing domain;
- required relation is `OpenConflict`;
- the ordinary deadline is between `1` and `2` RimWorld days;
- `map loaded`, `player present on tile` and `active quest target` are all `no`;
- creation-time domain and settlement counts are stored.

Record the target settlement's name, ID, tile, current owner, the total Goa'uld
settlement count and both involved domains' settlement counts.

A second `Create pending territorial takeover` must be rejected while this slot
is occupied.

### 4. Verify pending-state persistence

- Save the game.
- Reload the save.
- Open `Show territorial strategy report` again.

Expected result:

- the same pair, settlement ID, source and creation counts remain pending;
- the deadline does not reset;
- the settlement still belongs to the losing domain before resolution;
- diplomatic coherence remains `yes`.

### 5. Resolve the bounded takeover

Use:

```text
Trigger pending territorial takeover now
Show territorial strategy report
```

Expected result:

- outcome is `CompletedTransfer` and no reservation remains pending;
- the exact same settlement object keeps its name, ID and tile;
- only its owner changes from the losing domain to the gaining domain;
- the total permanent Goa'uld settlement count is unchanged;
- the losing domain still owns at least one settlement;
- the gaining domain stays within the current ceiling (`50%` in this required
  three-domain path);
- the world icon color and inspect-string faction reflect the new owner without
  reopening the world screen or reloading;
- global, both involved-domain and exact-pair cooldowns are active;
- the gaining-domain cooldown reflects its post-transfer size multiplier;
- one neutral RP letter names the settlement and both domains, and its target
  jumps to the unchanged settlement tile;
- the exact pair remains GateRim `OpenConflict` and vanilla `Hostile`.

### 6. Verify completed-state persistence

- Save after the transfer.
- Reload.
- Reopen the territorial strategy report and inspect the settlement.

Expected result:

- the new owner persists;
- settlement name, ID, tile and total settlement count remain unchanged;
- `CompletedTransfer` and cooldown state remain readable;
- no duplicate letter or second immediate takeover occurs after loading.

### 7. Required regressions

- Every Goa'uld domain remains hostile to the player and outside factions.
- Player, Tok'ra, Free Jaffa and vanilla-faction goodwill is unchanged.
- No faction becomes defeated.
- No settlement, site, tile, raid, mission, reward or pawn is created or
  destroyed by the takeover.
- Permanent doctrines, raid points, doctrine weights, incident frequency and
  refire delay are unchanged.
- Existing alliance rupture and shared-reprisal state is unchanged unless the
  exact pair was already incompatible, in which case scheduling must have been
  refused.
- `Player.log` contains no new relevant error, especially no settlement material
  reflection error, ownership mismatch, quest-target error, relation loop or
  repeated exception.

## Optional boundary tests

These cases are durable regression coverage but are not required for the first
focused acceptance unless the main path reveals a related defect.

### Legacy dry-run migration

Load a `0.3.83-dev` save containing a pending dry-run reservation and dry-run
cooldowns. It must become `CancelledLegacyDryRun`, preserve the original owner,
clear the legacy global/domain/pair cooldowns and arm a future natural attempt
rather than performing a takeover on load.

### Storyteller suspension

Create a pending takeover, switch away from **Commandement SG-1**, let the
visible deadline pass and switch back. The reservation, natural-attempt deadline
and cooldown clocks must all shift forward by the suspended duration, with no
backlog resolution.

### Loaded-map protection

Generate or enter the candidate settlement map before resolution. The takeover
must be cancelled as `CancelledSettlementMapLoaded` and ownership must remain
unchanged.

### Player-presence protection

Place a player caravan or other player world object on the candidate tile before
resolution. The takeover must be cancelled as `CancelledPlayerPresent`.

### Active-quest protection

Use a settlement currently referenced by an active quest. It must not be
scheduled, or an already pending takeover must cancel as
`CancelledQuestTargetProtected` if the quest begins before resolution.

### Published strategic limits

Confirm separately that sparse worlds, the losing domain's final settlement, a
projected share above the dynamic `75%`/`50%` ceiling, a changed relation, an
inactive domain and an
incompatible exact-pair alliance rupture all refuse or cancel without ownership
change.
