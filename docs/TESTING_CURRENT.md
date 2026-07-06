# Current milestone test procedure

Jalon : `0.3.83-dev - Add Goa'uld territorial safeguards and diplomatic coherence`

Revision to test: `r3`

Version de DLL attendue : `0.3.83.0`

Status: **focused functional validation completed successfully on final
revision `r3`, then published as `v0.3.83-dev`**.

Revision `r1` did not build because of `CS0165`; revision `r2` corrected that
compile failure. The first `r2` launch then reached the player map but repeated
RimWorld's `SetRelationDirect` error for goodwill-based factions. Revision `r3`
uses selective permanent hostility plus vanilla goodwill reconciliation between
Goa'uld instances and passed the focused procedure below. Territorial rules and
dry-run expectations remain unchanged. Optional boundary cases are not claimed
unless tested separately.

## Purpose

This procedure validates the strategic safety foundation only. The milestone
must reconcile inter-domain diplomacy and complete one persistent territorial
dry run, but it must never transfer, create or destroy a settlement.

## Automated checks

From the repository root:

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Expected version:

```text
Assembly: 0.3.83.0
```

Also run:

```powershell
git diff --check
```

Do not continue if the build or a consistency check fails.

## Required focused test

Use developer mode and start with the **Commandement SG-1** storyteller.

### 1. Verify the world-faction default

Open the vanilla world-faction configuration during world creation.

Expected result:

- three `Domaines des Grands Maîtres Goa'uld` entries are proposed by default;
- the player may reduce the count through the ordinary vanilla controls;
- no hidden replacement domain is added;
- the game remains startable with the reduced configuration allowed by the Def.

For the remaining focused path, keep three Goa'uld factions and generate a world
with their permanent settlements.

### 2. Verify diplomatic coherence

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

For each action below, use `Show relation report` and confirm the stored GateRim
state, actual vanilla relation, expected vanilla relation and `coherent: yes`:

| Debug relation | Expected vanilla relation |
|---|---|
| `Set first pair: Neutral` | `Neutral` |
| `Set first pair: Rivalry` | `Neutral` |
| `Set first pair: Open conflict` | `Hostile` |
| `Set first pair: Truce` | `Neutral` |
| `Set first pair: Alliance` | `Ally` |

Critical observation:

- confirm that `Alliance -> Ally` persists for two Goa'uld instances;
- confirm every Goa'uld domain remains hostile to the player and to outside
  factions before and after all five debug transitions;
- confirm no relation or goodwill involving the player or another faction
  changes during the inter-domain synchronization;
- no vanilla goodwill-change message or hostility letter should appear for this
  technical sync;
- `Player.log` must not contain `Tried to use SetRelationDirect for factions
  which use goodwill`.

Finish with `Set all pairs: Open conflict`.

### 3. Inspect territorial safeguards

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
> Territorial strategy...
> Show territorial safeguard report
```

Expected report:

- storyteller active: yes;
- the tracker-initialization count and current existing-domain count are shown;
- at least two active territorial Goa'uld domains;
- only permanent Goa'uld settlements of non-defeated domains are counted;
- minimum transfer threshold equals `active domains + 2`;
- each domain displays its settlement count;
- a one-settlement domain reports final-settlement protection;
- expansion weights are `1.00`, `0.50`, `0.25`, then `0.10`;
- delay multipliers are `1`, `2`, `4`, then `8`;
- the projected share ceiling is `50%`;
- every stored pair reports coherent GateRim and vanilla relations;
- no reservation is pending after reset.

### 4. Create one dry-run reservation

In `Territorial strategy...` use, in order:

```text
Reset territorial safeguard state
Create pending territorial reservation
Show territorial safeguard report
```

Expected result:

- exactly one reservation is pending globally;
- the exact gaining and losing domains are shown;
- the exact permanent settlement ID is shown;
- required relation is `OpenConflict`;
- creation-time domain and settlement counts are stored;
- a short positive debug deadline is present;
- no settlement changes owner, appears or disappears;
- a second `Create pending territorial reservation` is rejected.

If no candidate is eligible, use the report's exact refusal reason. The focused
happy path requires a world with enough settlements and a losing domain owning
at least two settlements while the projected winner remains at or below `50%`.

### 5. Verify save and reload

- Save the game.
- Reload the save.
- Open `Show territorial safeguard report` again.

Expected result:

- the same exact pair and settlement ID remain pending;
- creation counts remain unchanged;
- the deadline does not reset to a fresh full delay;
- diplomatic coherence remains `yes`.

### 6. Complete the dry run

Use:

```text
Trigger pending territorial reservation now
Show territorial safeguard report
```

Expected result:

- outcome is `CompletedDryRun`;
- no reservation remains pending;
- global cooldown is active;
- both involved domains have an active cooldown;
- the exact pair has an active cooldown;
- the gaining-domain cooldown reflects its size multiplier;
- no letter, raid, reward or incident is created;
- settlement ownership, settlement count, faction count and world tiles are
  unchanged.

### 7. Required regression observations

- Player goodwill and relations are unchanged.
- Relations with Tok'ra, Free Jaffa and vanilla factions are unchanged.
- Permanent Goa'uld doctrines are unchanged.
- Raid points, doctrine weights, frequency and refire delay are unchanged.
- Existing alliance rupture actions retain their prior behavior.
- `Player.log` contains no new relevant error, especially no
  `SetRelationDirect` rejection, failed selective-hostility reconciliation or
  repeated `Alliance -> Ally` loop.

## Optional boundary tests

These tests are durable coverage but are not required for the first focused
acceptance unless the main path reveals a related defect.

### One-domain suspension

Generate or reduce the world to one active territorial domain. The report must
show territorial simulation suspended while unrelated Goa'uld content remains
available. No replacement faction may be generated.

### Sparse-world rejection

With fewer than `active domains + 2` permanent Goa'uld settlements, creation of
a reservation must be rejected as `SparseWorld`.

### Final-settlement protection

A candidate that would remove a domain's only settlement must be rejected as
`LastSettlementProtected`.

### Hegemony rejection

A candidate that would make the gaining domain own more than `50%` of permanent
Goa'uld settlements must be rejected as `HegemonyLimit`.

### Incompatible transition

Create a pending alliance rupture for the same pair, then attempt a territorial
reservation involving that pair. It must be rejected without blocking an
unrelated pair.

### Storyteller suspension

Create a pending reservation, switch away from `Commandement SG-1`, let its
short delay pass, then switch back. The reservation must remain pending while
suspended and its deadline plus cooldown clocks must be shifted forward instead
of consumed as backlog.
