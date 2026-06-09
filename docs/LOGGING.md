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


## Emergency-extraction diagnostics

`0.1.19-dev` adds reverse-transfer logs:

```text
Prepared Goa'uld symbiote <id> for transfer from host state on <pawn>.
Emergency extraction returned Goa'uld symbiote <id> from host <pawn> to a free pawn.
Removed transferred Goa'uld symbiote state <id> from host <pawn> without detaching the active symbiote.
```

The free pawn created after extraction must display the same ID.


## Extraction-surgery diagnostics

`0.1.20-dev` adds surgery-specific logs:

```text
Emergency extraction surgery failed for <pawn>.
Emergency extraction surgery returned Goa'uld symbiote <id> from patient <pawn> with surgeon <pawn>.
```

A successful operation must preserve the same persistent parasite ID.


## Autonomous-hunt diagnostics

`0.1.21-dev` adds pursuit logs:

```text
Free Goa'uld symbiote <id> started autonomous pursuit of <pawn>.
Autonomous implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
Autonomous hunting for Goa'uld symbiote <id> set to <true|false>.
```

After extraction, the free pawn inspection panel displays the remaining
autonomous cooldown ticks.


## Ritual-implantation diagnostics

`0.1.22-dev` adds the controlled ritual entry log:

```text
Ritual implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
```

The same ID must appear before the ritual, during recent implantation and after
save/reload.


## Ritual map-targeting diagnostics

`0.1.23-dev` changes only target selection. Successful ritual transfers still
use:

```text
Ritual implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
```


## Ritual-ceremony diagnostics

`0.1.24-dev` adds core ceremony lifecycle logs:

```text
Started ritual implantation ceremony for Goa'uld symbiote <id> and target <pawn> for <ticks> ticks.
Cancelled ritual implantation ceremony for Goa'uld symbiote <id> and target <pawn>.
Ritual implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
```


## Ritual-basin diagnostics

`0.1.25-dev` adds basin context to ceremony logs:

```text
Started ritual implantation ceremony for Goa'uld symbiote <id> and target <pawn> near basin <thing> for <ticks> ticks.
Cancelled ritual implantation ceremony for Goa'uld symbiote <id> and target <pawn> near basin <thing>.
```


## Jaffa Prim'ta diagnostics

`0.1.26-dev` adds Jaffa Prim'ta lifecycle logs:

```text
Attached Jaffa Prim'ta symbiote to <pawn>.
Loaded Jaffa Prim'ta symbiote for <pawn>.
Removed Jaffa Prim'ta symbiote from <pawn>.
Jaffa Prim'ta implantation surgery completed for <pawn> with surgeon <pawn>.
Jaffa Prim'ta implantation surgery failed for <pawn>.
```


## Physical Prim'ta-larva diagnostics

`0.1.27-dev` adds a defensive ingredient check to Jaffa implantation:

```text
Skipped Jaffa Prim'ta implantation for <pawn> because the physical Prim'ta larva ingredient is missing.
Jaffa Prim'ta implantation surgery completed for <pawn> using physical Prim'ta larva with surgeon <pawn>.
```

Normal bill execution should consume one `SG1_PrimtaLarva`.


## Jaffa puberty-dependency diagnostics

`0.1.34-dev` adds dependency lifecycle logs:

```text
Started Jaffa Prim'ta dependency for <pawn> at biological age <age>.
Removed Jaffa Prim'ta dependency from <pawn>.
```


## Jaffa Prim'ta cultural-thought diagnostics

`0.1.35-dev` logs the first rite-of-passage memory:

```text
Granted first Prim'ta cultural memory to <pawn>.
```

The log appears only once per pawn, even after removal and reimplantation.


## Tretonin-substitution diagnostics

`0.1.36-dev` adds:

```text
Started tretonin substitution for <pawn>.
Loaded tretonin substitution for <pawn>.
Ended tretonin substitution for <pawn>.
Tretonin dose administered to <pawn> by <pawn>.
```


## Formal Jaffa Prim'ta-ceremony diagnostics

`0.1.38-dev` adds:

```text
Started formal Jaffa Prim'ta ceremony for <pawn> near basin <thing> using larva <thing> for <ticks> ticks.
Loaded formal Jaffa Prim'ta ceremony for <pawn> near basin <thing> with <remaining> / <total> ticks remaining.
Completed formal Jaffa Prim'ta ceremony for <pawn> near basin <thing>.
Cancelled formal Jaffa Prim'ta ceremony for <pawn> near basin <thing> using larva <thing>.
```


## Tok'ra-foundation diagnostics

`0.1.39-dev` reuses the adult-symbiote lifecycle logs with origin-sensitive
inspection data.

The voluntary implantation log uses:

```text
Voluntary Tok'ra implantation transferred Goa'uld symbiote <id> from free pawn <pawn> into host <pawn>.
```

After extraction, the free pawn should remain the non-hunting Tok'ra variant.


## Tok'ra voluntary-host prototype diagnostics

`0.1.40-dev` adds:

```text
Initialized Tok'ra voluntary-host prototype <pawn> with symbiote <id>.
Registered existing Tok'ra voluntary-host prototype <pawn> without creating a duplicate symbiote.
```
