# C# logging workflow

## Purpose

All GateRim SG-1 C# diagnostics must go through:

```text
GR_Log
```

This keeps `Player.log` entries easy to filter and identify.

## Prefix

```text
<color=#D9B44A>[GateRim SG-1]</color>
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
<color=#D9B44A>[GateRim SG-1]</color> Version 0.1.14.0 loaded.
```

## Future usage examples

```csharp
GR_Log.Message($"Created Goa'uld symbiote {symbioteId}.");
GR_Log.WarningOnce($"Unable to implant incompatible pawn {pawn.ThingID}.", pawn.thingIDNumber);
GR_Log.Error($"Missing host state for symbiote {symbioteId}.");
```


## Color choice

The current GateRim SG-1 log prefix uses:

```text
#D9B44A
```

This gold tone keeps the mod visually distinct in `Player.log` and fits the Goa'uld / Stargate theme.


## Persistent symbiote diagnostics

`0.1.16-dev` adds lifecycle logs for adult Goa'uld symbiote data:

```text
Attached Goa'uld symbiote <id> to host <pawn>.
Loaded Goa'uld symbiote <id> for host <pawn>.
Detached Goa'uld symbiote <id> from host <pawn>.
```

The ID should remain identical after saving and reloading.


## Forced-implantation diagnostics

`0.1.17-dev` adds free-symbiote and transfer logs:

```text
Created free Goa'uld symbiote <id> as pawn <pawn>.
Loaded free Goa'uld symbiote <id> as pawn <pawn>.
Forced implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
Consumed free Goa'uld symbiote <id> during forced implantation.
```


## Active-host conversion diagnostics

`0.1.18-dev` adds conversion logs:

```text
Prepared Goa'uld symbiote <id> for transfer from host state on <pawn>.
Converted recent Goa'uld implantation <id> into active host state on <pawn>.
Removed transferred Goa'uld symbiote state <id> from host <pawn> without detaching the active symbiote.
```

The same ID must remain visible before conversion, after conversion and after save/reload.
