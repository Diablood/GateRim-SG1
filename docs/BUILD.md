# C# build workflow

## Requirements

- RimWorld 1.6 installed locally.
- Harmony mod installed locally for the compile-time `0Harmony.dll` reference.
- A .NET SDK capable of targeting .NET Framework 4.7.2.
- The RimWorld managed assembly directory.

## Windows PowerShell

From the mod root:

```powershell
.\build.ps1 -RimWorldManagedDir "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

The resulting DLL is written to:

```text
1.6/Assemblies/GateRimSG1.dll
```

## Bash

```bash
./build.sh "/path/to/RimWorldLinux_Data/Managed"
```

## Test

After building, launch RimWorld with:

```text
Core
Harmony
Biotech
GateRim SG-1
```

Then inspect `Player.log` for:

```text
[GateRim SG-1] Version 0.3.58.0 loaded.
```

The DLL is a local build output and remains ignored by Git.

GateRim SG-1 references Harmony with `Private=false`. The required `brrainz.harmony` mod supplies `0Harmony.dll` at runtime; do not copy `0Harmony.dll` into `1.6/Assemblies`.

If Harmony is installed outside the default Workshop path, pass its assemblies directory explicitly:

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj -t:Rebuild -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed" -p:HarmonyAssemblyDir="D:\SteamLibrary\steamapps\workshop\content\294100\2009463077\Current\Assemblies"
```


## Windows CMD wrapper

If PowerShell script execution is blocked, use:

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

The wrapper calls `dotnet build` directly and does not depend on PowerShell script execution policies.

If your RimWorld installation matches the default path currently configured in the wrapper, the argument can be omitted:

```powershell
.\build.cmd
```
