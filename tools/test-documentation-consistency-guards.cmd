@echo off
setlocal
powershell.exe -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0test-documentation-consistency-guards.ps1" %*
exit /b %ERRORLEVEL%
