# GateRim SG-1 — Current project state

## Latest published milestone

`v0.2.8-dev-r1 - Require adult stranded SG-team candidates`

The main GitHub repository and the separate wiki repository are synchronized.

## Current validated behavior

The `Équipe SG isolée` starting scenario now rejects candidates below `20`
biological years through the existing C# starting-pawn filter. This threshold
ensures that every candidate has an adulthood backstory.

Preserved behavior:
- exactly four candidates for four starting slots;
- regeneration of every candidate slot;
- rejection of candidates incapable of violence;
- automatic SG-team field equipment.

## Validation status

- Forced C# rebuild: passed with no warnings or errors.
- Four initial candidates aged `20+`: passed.
- Adulthood backstories present: passed.
- Repeated regeneration of all four slots: passed.
- Violence capability and SG-team equipment preserved: passed.

## Next focus

- Continue validation of the GateRim SG-1 research tab.
