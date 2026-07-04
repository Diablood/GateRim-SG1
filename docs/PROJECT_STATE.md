# Project state

Current milestone: `0.3.66-dev - Add persistent Goa'uld inter-domain relations`
- closed after final local revision `r1`.

## Repository state

- Starting tag: `v0.3.65-dev`.
- Published branch: `feature/goauld-inter-domain-relations`.
- Published version and final unique tag: `0.3.66-dev` / `v0.3.66-dev`.
- Technical assembly version: `0.3.66.0`.
- Final validated local revision: `r1`.
- Forced rebuild and focused in-game validation: completed successfully.
- Main repository publication: completed.
- Separate wiki synchronization and publication: completed.

## Published scope

- Store one persistent relation for every unordered pair of Goa'uld System Lord
  faction instances.
- Support five states: neutral, rivalry, open conflict, truce and alliance.
- Keep the relation attached to faction instances rather than current leaders.
- Reconcile new games, older saves, multiple domains and newly created domains.
- Preserve defeated-domain records safely while excluding them from automatic
  transitions.
- Advance relations automatically only while `SG1_GateRimStoryteller` is active.
- Freeze all pair and global transition clocks while another storyteller is
  selected, then resume without applying an accumulated backlog.
- Apply slow bounded state transitions with one global report at a time.
- Avoid selecting the same pair twice in succession when another eligible pair
  exists.
- Add three RP text variants for each resulting state and avoid repeating the
  exact same text variant immediately.
- Add English and French relation labels and reports.
- Extend the storyteller orchestration report with relation-system
  availability, pair counts, activation and next-transition state.
- Add a dedicated developer menu for pair creation, reports, reconciliation,
  forced transitions, direct state setters and reset.

## Validated transition model

- neutral -> rivalry or alliance;
- rivalry -> open conflict or neutral;
- open conflict -> truce;
- truce -> neutral, rivalry or alliance;
- alliance -> neutral or rivalry.

New pairs begin neutral. Their first transition is scheduled after `8` to `16`
days. A transitioned pair waits `12` to `24` days before becoming eligible
again, and RP reports are globally spaced by `5` to `10` days.

## Final validation

The final `r1` validation confirmed:

- successful forced build of assembly `0.3.66.0`;
- canonical pair creation and all five translated states;
- RP letters naming both domains;
- persistence across save and reload;
- real suspension under Cassandra and clean resumption under Commandement SG-1;
- pair anti-repetition when several alternatives exist;
- preservation of existing raids, doctrines and extraction reprisals;
- no new C#, XML, translation, Scribe, faction, letter or storyteller error in
  `Player.log`.

## Deliberately inactive consequences

`0.3.66-dev` does not modify:

- incident frequency or refire delays;
- threat points or raid composition;
- domain doctrine weights;
- extraction ultimatums or reprisals;
- faction goodwill toward the player;
- map battles between Goa'uld forces;
- alliance reinforcements or joint raids;
- territorial expansion or settlement destruction.

The relation states and reports remain strategic data only in this milestone.

## Next step

No numbered successor milestone is selected yet. The next branch must start
from published tag `v0.3.66-dev` and remain limited to one separately validated
consequence or another roadmap item chosen by the maintainer.
