# Test plan - 0.2.48-dev

## Preparation

1. Checkout `feature/tokra-organic-operation-opportunities` created from `v0.2.47-dev`.
2. Extract the `0.2.48-dev-r1` archive at the repository root.
3. Delete the previous `1.6/Assemblies/GateRimSG1.dll` or run a clean build.
4. Build against the RimWorld 1.6 managed directory.
5. Start RimWorld with developer mode enabled and confirm the loaded assembly version is `0.2.48.0`.

## Test 1 - Startup and save compatibility

- Load the mod and inspect the startup log.
- Load an existing `v0.2.47-dev` save.
- Expected: no XML, missing method, missing Def, scribing or type-load error.

## Test 2 - Progression at trust 0

- Ensure Tok'ra trust is Neutral at score `0`.
- Build and power a Tok'ra secure communicator.
- Use `Force Tok'ra organic observation opportunity`.
- Expected: the RP offer appears even though manual support requests remain locked by insufficient Tok'ra trust.

## Test 3 - Ignore without penalty

- Record current trust.
- Force an offer and do not accept it.
- Let approximately two days pass, or advance time until the offer closes.
- Expected: neutral closure message; trust unchanged; no recurring error.

## Test 4 - Accept operation

- Force an offer.
- Select a colon capable of Intellectual work.
- Right-click the powered communicator and choose the observation request.
- Expected: the pawn walks to and briefly operates the communicator; the state changes to observation underway.
- Immediately right-click again.
- Expected: the transmit action is disabled with the remaining observation time.

## Test 5 - Successful report

- Use `Make Tok'ra organic observation report ready`.
- Select an Intellectual-capable colon and transmit the report.
- Expected: success letter; `+3` trust; `250` Intellectual XP for the transmitting colon; active request cleared.

## Test 6 - Accepted failure

- Force and accept another request.
- Let the secure reporting deadline expire without transmitting.
- Expected: failure letter; `-1` trust; active request cleared; future opportunity rescheduled.

## Test 7 - Persistence

Repeat save/reload checks in both states:

- offer waiting for acceptance;
- accepted observation waiting for readiness or transmission.

Expected: map association, remaining times, action label and final outcome remain coherent after reload.

## Test 8 - Communicator UI

- Inspect the communicator during no request, offered, observing and ready states.
- Open `Consult Tok'ra channel status` in each state.
- Expected: concise RP state line; no raw trust score; no duplicated power line.

## Test 9 - Operator and power restrictions

- Try with a pawn incapable of Intellectual work.
- Try with the communicator switched off.
- Try with an unreachable or reserved communicator.
- Expected: concise disabled reasons; no job starts; no state change.

## Test 10 - Regression checks

At Trusted trust, verify existing actions still behave as before:

- defensive diversion;
- medical guidance;
- emergency medical cache;
- tactical threat assessment;
- status report;
- existing mission-chain state reporting.

Expected: no manual request has lost its existing trust, power, threat, patient or cooldown requirement.
