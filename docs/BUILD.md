# C# build workflow

## Requirements

- RimWorld 1.6 installed locally.
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
Biotech
GateRim SG-1
```

Then inspect `Player.log` for:

```text
[GateRim SG-1] Version 0.1.14.0 loaded.
```

The DLL is a local build output and remains ignored by Git.
