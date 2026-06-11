# Explicit strategy for the controlled Goa'uld Jaffa raid

Version: `0.1.70-dev`

## Scope

The developer-only controlled Goa'uld Jaffa test raid now assigns:

```csharp
parms.raidStrategy = RaidStrategyDefOf.ImmediateAttack;
```

before delegating to `IncidentWorker_RaidEnemy`.

## Why this exists

The previous controlled raid prototype worked correctly but let the vanilla
worker resolve a missing raid strategy. RimWorld then emitted:

```text
No raid strategy found, defaulting to ImmediateAttack.
```

The fallback behavior already matched the intended prototype gameplay. This
milestone makes that choice explicit and removes the warning without adding
a custom raid-strategy worker.

## Deliberate limits

- The controlled raid remains developer-only.
- Its storyteller base chance remains exactly `0`.
- Natural Goa'uld raids remain disabled.
- Settlements and traders remain disabled.
- Custom Goa'uld tactics are deferred until the direct-assault baseline is
  fully stabilized.

## Manual test checklist

1. Build the mod and start RimWorld with developer mode enabled.
2. Trigger:
   `Do incident (Map) > controlled Goa'uld Jaffa test raid`.
3. Confirm that the group attacks normally.
4. Trigger:
   `Do incident (points) > controlled Goa'uld Jaffa test raid`.
5. Confirm that the group attacks normally.
6. Search `Player.log` for:
   `No raid strategy found`.
7. Confirm that the search returns no result.
8. Confirm that natural Goa'uld raids remain disabled.

## Temporary r3 diagnostics

While stabilizing the controlled raid, a temporary diagnostic component can
write compact post-generation pawn snapshots to:

```text
DevOutput/GateRimSG1_JaffaRaidDiagnostics.txt
```

It avoids `Verse.Log` so diagnostic data does not produce in-game popups.
