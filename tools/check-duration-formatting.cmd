@echo off
setlocal
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0check-duration-formatting.ps1" -RepositoryRoot "%~dp0.."
exit /b %ERRORLEVEL%
