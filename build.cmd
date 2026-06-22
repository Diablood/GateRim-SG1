@echo off
setlocal
set "RIMWORLD_MANAGED_DIR=%~1"
if "%RIMWORLD_MANAGED_DIR%"=="" (
    set "RIMWORLD_MANAGED_DIR=D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
)

dotnet build "%~dp0Source\GateRimSG1\GateRimSG1.csproj" ^
    --configuration Release ^
    --no-incremental ^
    -p:RimWorldManagedDir="%RIMWORLD_MANAGED_DIR%"

endlocal
