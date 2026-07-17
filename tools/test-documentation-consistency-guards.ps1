[CmdletBinding()]
param()

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

$checkerPath = Join-Path $PSScriptRoot "check-documentation-consistency.ps1"
if (-not (Test-Path -LiteralPath $checkerPath -PathType Leaf)) {
    Write-Host "[FAIL] Missing checker: $checkerPath" -ForegroundColor Red
    exit 1
}

$failures = New-Object System.Collections.Generic.List[string]
$tempRoot = Join-Path ([IO.Path]::GetTempPath()) ("GateRim-DocumentationGuards-" + [guid]::NewGuid().ToString("N"))

function Add-Failure {
    param([string]$Message)

    [void]$script:failures.Add($Message)
    Write-Host "[FAIL] $Message" -ForegroundColor Red
}

function Add-Pass {
    param([string]$Message)

    Write-Host "[PASS] $Message" -ForegroundColor Green
}

function Write-Utf8File {
    param(
        [string]$Path,
        [string]$Content
    )

    $directory = Split-Path -Parent $Path
    if (-not (Test-Path -LiteralPath $directory -PathType Container)) {
        [void](New-Item -ItemType Directory -Path $directory -Force)
    }
    [IO.File]::WriteAllText($Path, $Content, (New-Object System.Text.UTF8Encoding($false)))
}

function New-TestFixture {
    param([string]$Name)

    $root = Join-Path $tempRoot $Name
    [void](New-Item -ItemType Directory -Path $root -Force)

    Write-Utf8File (Join-Path $root "About/About.xml") @'
<?xml version="1.0" encoding="utf-8"?>
<ModMetaData>
    <name>GateRim SG-1</name>
    <modVersion>0.3.100-dev</modVersion>
</ModMetaData>
'@

    Write-Utf8File (Join-Path $root "Source/GateRimSG1/GateRimSG1.csproj") @'
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <Version>0.3.100.0</Version>
        <AssemblyVersion>0.3.100.0</AssemblyVersion>
        <FileVersion>0.3.100.0</FileVersion>
    </PropertyGroup>
</Project>
'@

    Write-Utf8File (Join-Path $root "README.md") @'
# GateRim SG-1

- Development version: `0.3.100-dev`
'@

    Write-Utf8File (Join-Path $root "docs/PROJECT_STATE.md") @'
# Project state

Current milestone: `0.3.100-dev - Restore documentation consistency and add publication safeguards`

Status: validated and ready for publication.

- Final annotated tag: `v0.3.100-dev`.
'@

    Write-Utf8File (Join-Path $root "docs/TESTING_CURRENT.md") @'
# Current testing

Jalon : `0.3.100-dev`
Version de DLL validée : `0.3.100.0`

All documentation guards pass.
'@

    Write-Utf8File (Join-Path $root "docs/CHANGELOG.md") @'
# Changelog

## 0.3.100-dev - Restore documentation consistency and add publication safeguards

- Add documentation guards.

## 0.3.99-dev - Published fixture milestone

- Published fixture entry.

## 0.3.98-dev - Published fixture milestone

- Published fixture entry.

## 0.3.97-dev - Published fixture below durable threshold

- Changelog coverage remains mandatory, but no durable-test section is required.

## 0.3.50-dev - Historical duplicate fixture

- Historical duplicate outside the guarded publication range.

## 0.3.50-dev - Historical duplicate fixture

- Historical duplicate outside the guarded publication range.
'@

    Write-Utf8File (Join-Path $root "docs/VISUAL_ASSET_REGISTER.md") @'
# Visual asset register

## Milestone

- Version: `0.3.100-dev`
- Target assembly: `0.3.100.0`
- Status: current repository inventory validated for `0.3.100-dev`.

## Approved final references

The table below is authoritative.

## Audit summary

- Local PNG files: `2`.
- Local texture families: `2`.
- Accepted final local families: `2` (`2` command icons).
- Temporary original families: `0`.
- Temporary recolor families: `0`.
- Temporary reuse families: `0`.
- Project-icon placeholder families: `0`.
- Priorities: `0` P0, `0` P1, `0` P2, `2` done.

<!-- LOCAL_ASSET_TABLE_START -->
| Canonical path under `Textures/` | Files | Base size | Surface | Status | Priority | Referenced by | Audit note |
|---|---:|---:|---|---|---|---|---|
| `UI/Commands/FixtureOne` | 1 | 64×64 | Command UI | `final` | `done` | FixtureOne | Fixture. |
| `UI/Commands/FixtureTwo` | 1 | 64×64 | Command UI | `final` | `done` | FixtureTwo | Fixture. |
<!-- LOCAL_ASSET_TABLE_END -->

<!-- EXTERNAL_ASSET_TABLE_START -->
| External texture path | Source | Status |
|---|---|---|
| `UI/Commands/Cancel` | RimWorld | `accepted-vanilla` |
<!-- EXTERNAL_ASSET_TABLE_END -->
'@

    Write-Utf8File (Join-Path $root "docs/wiki/Home.md") @'
# GateRim SG-1

> Version du mod documentée : `0.3.100-dev`
'@

    Write-Utf8File (Join-Path $root "docs/wiki/Content-Status.md") @'
# État du contenu

> Dernière révision : `0.3.100-dev`
'@


    Write-Utf8File (Join-Path $root "docs/TESTING.md") @'
# Durable testing

## Documentation consistency safeguards (`0.3.100-dev`)

- Fixture current coverage.

## Published fixture coverage (`0.3.99-dev`)

- Fixture coverage.

## Published fixture coverage (`0.3.98-dev`)

- Fixture coverage.
'@

    Write-Utf8File (Join-Path $root "docs/ROADMAP.md") @'
# Roadmap

## Prochain jalon décidé

Finalize the next dedicated command icon.

## Jalons différés décidés

- Remaining temporary visual families.
'@

    Write-Utf8File (Join-Path $root "docs/MILESTONE_PUBLICATION.md") @'
# Publication

Aucun script PowerShell temporaire ne doit être placé à la racine.

```powershell
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-project-consistency.cmd -RequirePublicationReady
```
'@

    Write-Utf8File (Join-Path $root "docs/PROJECT_CONSISTENCY_CHECKS.md") @'
# Project consistency checks

Version: `0.3.100-dev`

The main checker launches `tools/check-documentation-consistency.ps1`.
'@

    Write-Utf8File (Join-Path $root "docs/DOCUMENTATION_CONSISTENCY_GUARDS.md") @'
# Documentation consistency guards

Version: `0.3.100-dev`
'@

    & git -C $root init --quiet
    if ($LASTEXITCODE -ne 0) { throw "Could not initialize fixture Git repository." }
    & git -C $root config user.email "documentation-guards@example.invalid"
    & git -C $root config user.name "Documentation Guards"
    & git -C $root config core.autocrlf false
    & git -C $root add .
    & git -C $root commit --quiet -m "fixture"
    if ($LASTEXITCODE -ne 0) { throw "Could not commit fixture repository." }
    & git -C $root tag v0.3.97-dev
    & git -C $root tag v0.3.98-dev
    & git -C $root tag v0.3.99-dev

    return $root
}

function Invoke-Checker {
    param(
        [string]$RepositoryRoot,
        [switch]$PublicationReady
    )

    $arguments = @(
        "-NoLogo",
        "-NoProfile",
        "-ExecutionPolicy", "Bypass",
        "-File", $checkerPath,
        "-RepositoryRoot", $RepositoryRoot,
        "-MinimumPublishedVersion", "0.3.97-dev",
        "-MinimumDurableTestingVersion", "0.3.98-dev"
    )
    if ($PublicationReady) {
        $arguments += "-RequirePublicationReady"
    }

    $output = @(& powershell.exe @arguments 2>&1)
    return @{
        ExitCode = $LASTEXITCODE
        Text = ($output -join [Environment]::NewLine)
    }
}

function Assert-CheckerPasses {
    param(
        [string]$Name,
        [string]$RepositoryRoot,
        [switch]$PublicationReady
    )

    $result = Invoke-Checker -RepositoryRoot $RepositoryRoot -PublicationReady:$PublicationReady
    if ($result.ExitCode -ne 0) {
        Add-Failure "$Name should pass, but exited with $($result.ExitCode).`n$($result.Text)"
    }
    else {
        Add-Pass $Name
    }
}

function Assert-CheckerFails {
    param(
        [string]$Name,
        [string]$RepositoryRoot,
        [string]$ExpectedText,
        [switch]$PublicationReady
    )

    $result = Invoke-Checker -RepositoryRoot $RepositoryRoot -PublicationReady:$PublicationReady
    if ($result.ExitCode -eq 0) {
        Add-Failure "$Name should fail, but passed."
        return
    }
    if ($result.Text -notlike "*$ExpectedText*") {
        Add-Failure "$Name failed without the expected diagnostic '$ExpectedText'.`n$($result.Text)"
        return
    }
    Add-Pass $Name
}

try {
    [void](New-Item -ItemType Directory -Path $tempRoot -Force)

    $baseline = New-TestFixture "baseline"
    Assert-CheckerPasses "Baseline fixture passes" $baseline -PublicationReady

    $missingHistory = New-TestFixture "missing-history"
    $path = Join-Path $missingHistory "docs/CHANGELOG.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace("## 0.3.99-dev - Published fixture milestone", "### 0.3.99-dev - Published fixture milestone")
    Write-Utf8File $path $text
    Assert-CheckerFails "Missing published changelog milestone is rejected" $missingHistory "missing published milestone '0.3.99-dev'"


    $duplicateCurrentHistory = New-TestFixture "duplicate-current-history"
    $path = Join-Path $duplicateCurrentHistory "docs/CHANGELOG.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $duplicateSection = @'

## 0.3.99-dev - Duplicate published fixture milestone

- Invalid duplicate inside the guarded publication range.
'@
    $text = $text.Replace("## 0.3.98-dev - Published fixture milestone", $duplicateSection + [Environment]::NewLine + "## 0.3.98-dev - Published fixture milestone")
    Write-Utf8File $path $text
    Assert-CheckerFails "Duplicate guarded changelog milestone is rejected" $duplicateCurrentHistory "milestone '0.3.99-dev' appears 2 times"

    $volatileBranch = New-TestFixture "volatile-branch"
    $path = Join-Path $volatileBranch "docs/VISUAL_ASSET_REGISTER.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace('- Version: `0.3.100-dev`', '- Version: `0.3.100-dev`' + [Environment]::NewLine + '- Branch: `fix/fixture`')
    Write-Utf8File $path $text
    Assert-CheckerFails "Volatile visual-register branch metadata is rejected" $volatileBranch "contains volatile branch metadata"

    $staleRegister = New-TestFixture "stale-register"
    $path = Join-Path $staleRegister "docs/VISUAL_ASSET_REGISTER.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace('- Version: `0.3.100-dev`', '- Version: `0.3.95-dev`')
    Write-Utf8File $path $text
    Assert-CheckerFails "Stale visual-register version is rejected" $staleRegister "Visual-register version is '0.3.95-dev'"

    $badBreakdown = New-TestFixture "bad-breakdown"
    $path = Join-Path $badBreakdown "docs/VISUAL_ASSET_REGISTER.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace('(`2` command icons)', '(`1` command icon)')
    Write-Utf8File $path $text
    Assert-CheckerFails "Incorrect final-family category sum is rejected" $badBreakdown "category breakdown totals 1"

    $strayRow = New-TestFixture "stray-row"
    $path = Join-Path $strayRow "docs/VISUAL_ASSET_REGISTER.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $replacement = '| `UI/Commands/Stray` | 1 | 64×64 | Command UI | `final` | `done` | Stray | Invalid duplicate summary row. |' + [Environment]::NewLine + [Environment]::NewLine + '## Audit summary'
    $text = $text.Replace('## Audit summary', $replacement)
    Write-Utf8File $path $text
    Assert-CheckerFails "Asset row outside the authoritative table is rejected" $strayRow "asset-table row(s) outside"


    $missingDurableTesting = New-TestFixture "missing-durable-testing"
    $path = Join-Path $missingDurableTesting "docs/TESTING.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace('## Published fixture coverage (`0.3.99-dev`)', '### Published fixture coverage (`0.3.99-dev`)')
    Write-Utf8File $path $text
    Assert-CheckerFails "Missing durable testing milestone is rejected" $missingDurableTesting "missing published milestone '0.3.99-dev'"

    $historicalRoadmap = New-TestFixture "historical-roadmap"
    $path = Join-Path $historicalRoadmap "docs/ROADMAP.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text += [Environment]::NewLine + "## Dernier jalon visuel validé et publié" + [Environment]::NewLine
    Write-Utf8File $path $text
    Assert-CheckerFails "Published history in roadmap is rejected" $historicalRoadmap "published-history heading"

    $missingPublicationCommand = New-TestFixture "missing-publication-command"
    $path = Join-Path $missingPublicationCommand "docs/MILESTONE_PUBLICATION.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace(".\tools\check-project-consistency.cmd -RequirePublicationReady", ".\tools\check-project-consistency.cmd")
    Write-Utf8File $path $text
    Assert-CheckerFails "Missing publication-ready gate is rejected" $missingPublicationCommand "missing required command"

    $unfinished = New-TestFixture "unfinished-publication"
    $path = Join-Path $unfinished "docs/TESTING_CURRENT.md"
    $text = Get-Content -LiteralPath $path -Raw -Encoding UTF8
    $text = $text.Replace("Version de DLL validée", "Version de DLL attendue")
    Write-Utf8File $path $text
    Assert-CheckerFails "Publication-ready mode rejects unfinished wording" $unfinished "Version de DLL attendue" -PublicationReady
}
catch {
    Add-Failure ("Unexpected test error: {0}" -f $_.Exception.Message)
}
finally {
    if (Test-Path -LiteralPath $tempRoot) {
        Remove-Item -LiteralPath $tempRoot -Recurse -Force -ErrorAction SilentlyContinue
    }
}

Write-Host ""
if ($failures.Count -gt 0) {
    Write-Host "Documentation guard tests failed with $($failures.Count) issue(s)." -ForegroundColor Red
    exit 1
}

Write-Host "Documentation guard tests passed." -ForegroundColor Green
exit 0
