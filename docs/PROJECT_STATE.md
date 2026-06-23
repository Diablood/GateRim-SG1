# Project state

Current milestone: `0.3.32-dev - Add Tok'ra cipher-module study and research` — validated locally on revision `r2`, closed and published under final tag `v0.3.32-dev`.

## Published base

- Starting tag: `v0.3.31-dev`.
- Dedicated branch: `feature/tokra-artifact-study-research`.
- Final local revision: `0.3.32-dev-r2`.
- Final tag: `v0.3.32-dev`.
- Assembly version: `0.3.32.0`.
- Cultural backstory count remains `83`.

## Validated scope

- Analyze the exact cipher module recovered by the introduction mission through three research-bench sessions.
- Persist analysis progress through RimWorld's `AnalysisManager` across save and reload.
- Reject separately spawned copies by checking the tracked artifact identity.
- Keep the recovered module non-tradeable while analysis is incomplete.
- Preserve the module during the first two sessions, then dismantle it during the third session so no obsolete storage item remains.
- Add `SG1_TokraSecureCommunications` to the GateRim SG-1 research tab.
- Require both completed module analysis and vanilla `Electricity` before formal research can begin.
- Detect a missing tracked module while analysis is unfinished and schedule one hidden replacement after `2–8` in-game days.
- Preserve completed analysis sessions when a replacement module is issued.
- Cancel the replacement if the tracked module reappears before the hidden delay expires.
- Treat completed secure-communications research as authoritative during load and periodic reconciliation.
- Close the introduction arc, satisfy analysis and remove obsolete offers, sites or modules when that research is already completed by a scenario, edited save or developer action.
- Make `Tok'ra intro: recover key artifact` produce a coherent completed arc and genuine analyzable module when study is still required.
- Keep communicator construction and recurrent-operation gating unchanged for the next milestone.

## Final validation

Local revision `r1` validated the normal three-session study flow, exact-object identity, persistence and research lock. Local revision `r2` validated final dismantling, non-tradeability, delayed automatic replacement after loss, preserved progress, developer-state reconciliation and automatic satisfaction when secure-communications research is already complete.

The replacement delay is automatic. `Tok'ra study: make replacement due` is only a developer acceleration tool; normal play waits the persisted hidden `2–8` day delay.

The final rebuild produced `GateRimSG1.dll` version `0.3.32.0`, the consistency check passed and the focused in-game tests were reported successful.

## Deferred work

- Require `SG1_TokraSecureCommunications` for communicator construction.
- Gate the six recurrent Tok'ra operations behind an available powered communicator.
- Reconcile existing saves and custom starters when communicator construction or operation gating is introduced.
- Add the final dedicated cipher-module texture during the later global visual pass.
