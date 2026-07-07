@echo off
setlocal
powershell.exe -NoLogo -NoProfile -ExecutionPolicy Bypass -File "%~dp0check-visual-assets.ps1" -RepositoryRoot "%~dp0.." %*
exit /b %ERRORLEVEL%
