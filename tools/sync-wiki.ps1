param(
    [string]$WikiRoot
)

$ErrorActionPreference = "Stop"

$MainRoot = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path

if ([string]::IsNullOrWhiteSpace($WikiRoot)) {
    $WikiRoot = Join-Path (Split-Path $MainRoot -Parent) "GateRim-SG1.wiki"
}

$WikiRoot = [System.IO.Path]::GetFullPath($WikiRoot)
$WikiGit = Join-Path $WikiRoot ".git"
$SourceRoot = Join-Path $MainRoot "docs\wiki"

if (-not (Test-Path $WikiGit)) {
    Write-Error "Wiki repository not found: $WikiRoot`nExpected a sibling clone named GateRim-SG1.wiki."
}

if (-not (Test-Path $SourceRoot)) {
    Write-Error "Wiki drafts not found: $SourceRoot"
}

Get-ChildItem -Path $SourceRoot -Force | Copy-Item -Destination $WikiRoot -Recurse -Force

Write-Host "Wiki drafts copied directly to: $WikiRoot"
Write-Host "Review changes with:"
Write-Host "  cd `"$WikiRoot`""
Write-Host "  git status"
