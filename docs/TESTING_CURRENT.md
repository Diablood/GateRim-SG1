# Current milestone test procedure

Jalon : `0.3.82-dev - Add Goa'uld alliance rupture after major failure`

Révision finale validée : `r1`

Version de DLL validée : `0.3.82.0`


## Final result

Status: **validated by the maintainer**.

The required focused path passed on revision `r1`:

- expected assembly and automated checks;
- pending exact pair and `1/6` debug survivor state;
- save/reload persistence without deadline reset;
- one targetless diplomatic letter naming both domains;
- exact `Alliance -> Rivalry` transition;
- final `Completed` outcome with no pending rupture;
- no raid, reward, goodwill or territorial side effect;
- no new relevant error in the accepted `Player.log`.

The optional cancellation and storyteller-suspension procedures below remain
durable regression tests; they are not claimed as part of the focused acceptance.

## Automated checks

From the repository root:

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Expected results:

```text
Assembly: 0.3.82.0
Duration keys: 104
BackstoryDefs: 83
Project consistency: passed
```

## Required focused test

Use a player home map, developer mode and the **Commandement SG-1** storyteller.

### 1. Prepare one exact alliance

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

- Use `Create additional test domain` until at least two active domains exist.
- Use `Set all pairs: Alliance`.
- Use `Show relation report` and confirm the selected test pair is in
  `Alliance`.

### 2. Create the major failure

Open:

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Domain reactions...
```

- Use `Reset alliance rupture state`.
- Use `Create major alliance failure`.
- Use `Show domain reaction state`.

Expected report:

- one pending alliance rupture;
- the exact two allied domains;
- failure count `1/6`;
- a short positive deadline;
- outcome `Pending`.

No raid or letter should be created at this scheduling stage.

### 3. Verify persistence

- Save the game.
- Reload the save.
- Use `Show domain reaction state` again.

The same pair, failure count and pending rupture must remain present. The deadline
must not reset to a fresh full delay.

### 4. Resolve the rupture

In `Domain reactions...`, use:

```text
Trigger pending alliance rupture now
```

Expected result:

- exactly one neutral diplomatic letter appears;
- the text names both domains;
- the text explicitly states that their alliance is dissolved and replaced by
  rivalry;
- the letter has no `Se rendre sur les lieux` action and no pawn/map target;
- no raid, reward or site is created.

Open `Goa'uld inter-domain relations... > Show relation report`:

- the exact pair is now `Rivalry`;
- its previous relation is `Alliance`.

Open `Goa'uld... > Domain reactions... > Show domain reaction state`:

- the rupture outcome is `Completed`;
- no rupture remains pending.

## Required regression observations

- Existing shared-reprisal actions keep their exact labels and behavior.
- The rupture does not modify vanilla goodwill toward the player.
- No settlement or territory changes owner or disappears.
- No extra natural raid is started.
- `Player.log` contains no new relevant error.

## Optional cancellation test

1. Set all pairs to `Alliance`.
2. Use `Reset alliance rupture state`.
3. Use `Create major alliance failure`.
4. In the relation menu use `Set first pair: Neutral` before the rupture fires.
5. Return to `Domain reactions...` and use `Show domain reaction state` after
   at least `250` game ticks.

Expected result: the rupture is no longer pending and reports
`CancelledNoLongerAllied`; no rupture letter is emitted.

## Optional storyteller suspension test

1. Schedule a major failure under `Commandement SG-1`.
2. Switch to another storyteller before the deadline.
3. Let more than the short debug delay pass.
4. Confirm no rupture occurs.
5. Return to `Commandement SG-1` and confirm the pending deadline was shifted
   forward rather than consumed as backlog.
