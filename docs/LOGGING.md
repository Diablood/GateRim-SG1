# C# logging workflow

## Purpose

All GateRim SG-1 C# diagnostics must go through:

```text
GR_Log
```

This keeps `Player.log` entries easy to filter and identify.

## Prefix

```text
[GateRim SG-1]
```

## Available methods

```csharp
GR_Log.Message("...");
GR_Log.Warning("...");
GR_Log.Error("...");
GR_Log.WarningOnce("...", key);
GR_Log.ErrorOnce("...", key);
```

## Rules

- Use `Message` for important lifecycle events and useful state transitions.
- Use `Warning` for recoverable inconsistencies.
- Use `Error` for failures that prevent the intended behavior.
- Prefer `WarningOnce` and `ErrorOnce` inside repeated tick or event paths.
- Do not log every tick.
- Include pawn identifiers, symbiote identifiers and relevant Def names when diagnosing future possession mechanics.

## Current smoke test

The bootstrap emits one message when the assembly loads:

```text
[GateRim SG-1] Version 0.1.14.0 loaded.
```

## Future usage examples

```csharp
GR_Log.Message($"Created Goa'uld symbiote {symbioteId}.");
GR_Log.WarningOnce($"Unable to implant incompatible pawn {pawn.ThingID}.", pawn.thingIDNumber);
GR_Log.Error($"Missing host state for symbiote {symbioteId}.");
```
