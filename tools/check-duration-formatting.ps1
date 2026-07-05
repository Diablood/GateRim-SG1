[CmdletBinding()]
param(
    [string]$RepositoryRoot
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($RepositoryRoot)) {
    $RepositoryRoot = Join-Path $PSScriptRoot ".."
}

try {
    $RepositoryRoot = (Resolve-Path -LiteralPath $RepositoryRoot).Path
}
catch {
    Write-Host "[FAIL] Repository root not found: $RepositoryRoot" -ForegroundColor Red
    exit 1
}

$failures = New-Object System.Collections.Generic.List[string]

function Add-Failure {
    param([string]$Message)

    [void]$script:failures.Add($Message)
    Write-Host "[FAIL] $Message" -ForegroundColor Red
}

function Add-Pass {
    param([string]$Message)

    Write-Host "[PASS] $Message" -ForegroundColor Green
}

function Get-RelativePath {
    param([string]$FullPath)

    if ($FullPath.StartsWith($RepositoryRoot, [StringComparison]::OrdinalIgnoreCase)) {
        return $FullPath.Substring($RepositoryRoot.Length).TrimStart('\', '/')
    }

    return $FullPath
}

Write-Host "GateRim SG-1 player-facing duration audit"
Write-Host "Repository: $RepositoryRoot"
Write-Host ""

$bridgeRelativePath = "Source/GateRimSG1/Goauld/PlayerFacingDurationTranslationPatches.cs"
$bridgePath = Join-Path $RepositoryRoot $bridgeRelativePath
$directPatchRelativePath = "Source/GateRimSG1/Goauld/PlayerFacingDurationHarmonyPatches.cs"
$directPatchPath = Join-Path $RepositoryRoot $directPatchRelativePath
$utilityRelativePath = "Source/GateRimSG1/GR_PlayerFacingDurationUtility.cs"
$utilityPath = Join-Path $RepositoryRoot $utilityRelativePath

foreach ($requiredPath in @($bridgePath, $directPatchPath, $utilityPath)) {
    if (-not (Test-Path -LiteralPath $requiredPath -PathType Leaf)) {
        Add-Failure "Missing duration-formatting source file: $(Get-RelativePath $requiredPath)"
    }
}

$allowlistedKeys = @{}

if (Test-Path -LiteralPath $bridgePath -PathType Leaf) {
    $bridgeText = Get-Content -LiteralPath $bridgePath -Raw -Encoding UTF8
    $allowlistPattern = '\{\s*"(?<key>GR_[^"]+)"\s*,\s*(?:Hours|Days)\('

    foreach ($match in [regex]::Matches($bridgeText, $allowlistPattern)) {
        $key = $match.Groups["key"].Value

        if ($allowlistedKeys.ContainsKey($key)) {
            Add-Failure "Duplicate duration bridge key '$key'."
        }
        else {
            $allowlistedKeys[$key] = $true
        }
    }

    if ($allowlistedKeys.Count -eq 0) {
        Add-Failure "No duration translation keys were found in the compatibility bridge."
    }
    else {
        Add-Pass "Loaded $($allowlistedKeys.Count) explicit duration translation keys."
    }
}

$languageRoot = Join-Path $RepositoryRoot "Languages"
$translationFiles = @()

if (-not (Test-Path -LiteralPath $languageRoot -PathType Container)) {
    Add-Failure "Missing Languages directory."
}
else {
    $translationFiles = @(
        Get-ChildItem -LiteralPath $languageRoot -Filter "*.xml" -File -Recurse |
            Where-Object { $_.FullName -match '[\\/]Keyed[\\/]' } |
            Sort-Object FullName
    )
}

$legacyUnitPattern = '(?i)\{\d+\}\s*(?:RimWorld\s+)?(?:hour\(s\)|hours?|day\(s\)|days?|heure\(s\)|heures?|jour\(s\)|jours?|h|d|j)(?:\s+RimWorld)?(?=\s|[.,;:)\]\-]|$)'
$uncoveredTranslationKeys = New-Object System.Collections.Generic.List[string]
$legacyTranslationKeys = @{}
$translationElementCount = 0

foreach ($file in $translationFiles) {
    try {
        [xml]$xml = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
    }
    catch {
        Add-Failure "Could not parse translation file '$(Get-RelativePath $file.FullName)': $($_.Exception.Message)"
        continue
    }

    $root = $xml.DocumentElement

    if ($null -eq $root) {
        continue
    }

    foreach ($node in $root.ChildNodes) {
        if ($node.NodeType -ne [System.Xml.XmlNodeType]::Element) {
            continue
        }

        $translationElementCount++
        $text = [string]$node.InnerText

        if ($text -notmatch $legacyUnitPattern) {
            continue
        }

        $key = [string]$node.Name
        $legacyTranslationKeys[$key] = $true

        if (-not $allowlistedKeys.ContainsKey($key)) {
            [void]$uncoveredTranslationKeys.Add(
                "$(Get-RelativePath $file.FullName):$key"
            )
        }
    }
}

if ($translationFiles.Count -eq 0) {
    Add-Failure "No keyed translation XML files were found."
}
elseif ($uncoveredTranslationKeys.Count -gt 0) {
    Add-Failure ("Player-facing translation key(s) still append a fixed duration unit without a migration entry: {0}" -f ($uncoveredTranslationKeys -join ", "))
}
else {
    Add-Pass ("Scanned {0} keyed translations across {1} files; all {2} legacy duration keys are explicitly migrated." -f $translationElementCount, $translationFiles.Count, $legacyTranslationKeys.Count)
}

$sourceRoot = Join-Path $RepositoryRoot "Source/GateRimSG1"
$sourceFiles = @()

if (-not (Test-Path -LiteralPath $sourceRoot -PathType Container)) {
    Add-Failure "Missing Source/GateRimSG1 directory."
}
else {
    $sourceFiles = @(
        Get-ChildItem -LiteralPath $sourceRoot -Filter "*.cs" -File -Recurse |
            Sort-Object FullName
    )
}

$approvedLegacyConverterFiles = @{
    "Source/GateRimSG1/Goauld/Comp_GoauldQueenImmatureSymbioteSource.cs" = $true
    "Source/GateRimSG1/Goauld/Comp_TokraSecureCommunicator.cs" = $true
    "Source/GateRimSG1/Goauld/GameComponent_TokraOrganicOperationManager.cs" = $true
    "Source/GateRimSG1/Goauld/GameComponent_TokraTherapeuticOpportunityTracker.cs" = $true
    "Source/GateRimSG1/Goauld/GameComponent_TokraTrustTracker.cs" = $true
    "Source/GateRimSG1/Goauld/GameComponent_TokraInterceptedThreatTracker.cs" = $true
    "Source/GateRimSG1/Goauld/GameComponent_TokraIntroductionArc.cs" = $true
    "Source/GateRimSG1/Goauld/MapComponent_TokraRelaySabotageMission.cs" = $true
    "Source/GateRimSG1/Goauld/PlayerFacingDurationTranslationPatches.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_GoauldOpenConflictBattlefieldSite.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraDecodedMissionSite.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraDistressCallSite.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraHiddenSafehouseMarker.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraIntroductionArtifactSite.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraJaffaOfficerCaptureSite.cs" = $true
    "Source/GateRimSG1/Goauld/WorldObject_TokraTemporaryBaseDeliverySite.cs" = $true
    "Source/GateRimSG1/Jaffa/GameComponent_JaffaPrimtaDependency.cs" = $true
}

$converterPattern = '(?i)(Math\.Ceiling\s*\([^\r\n]{0,180}/\s*(?:2500f|60000f|GenDate\.TicksPer(?:Hour|Day)|TicksPer(?:Hour|Day))|/\s*(?:2500f|60000f|GenDate\.TicksPer(?:Hour|Day)|TicksPer(?:Hour|Day)))'
$playerFacingContextPattern = '(?i)(\.Translate\s*\(|Messages\.Message|Dialog_MessageBox|CompInspectStringExtra|GetInspectString|Command_Action|Gizmo|LetterMaker|ReceiveLetter)'
$developerContextPattern = '(?i)(GR_Log|GetDebug|StringBuilder)'
$converterCandidates = New-Object System.Collections.Generic.List[string]

foreach ($file in $sourceFiles) {
    $relativePath = (Get-RelativePath $file.FullName) -replace '\\', '/'

    if ($approvedLegacyConverterFiles.ContainsKey($relativePath) -or
        $relativePath -match '(?i)(^|/)[^/]*Debug[^/]*\.cs$') {
        continue
    }

    $lines = @(Get-Content -LiteralPath $file.FullName -Encoding UTF8)

    for ($index = 0; $index -lt $lines.Count; $index++) {
        if ($lines[$index] -notmatch $converterPattern) {
            continue
        }

        $start = [Math]::Max(0, $index - 12)
        $end = [Math]::Min($lines.Count - 1, $index + 12)
        $window = ($lines[$start..$end] -join "`n")

        if ($window -match $playerFacingContextPattern -and
            $window -notmatch $developerContextPattern) {
            [void]$converterCandidates.Add(
                "${relativePath}:$($index + 1)"
            )
        }
    }
}

if ($converterCandidates.Count -gt 0) {
    Add-Failure ("Possible player-facing manual duration converter(s) require review: {0}" -f ($converterCandidates -join ", "))
}
else {
    Add-Pass ("No unapproved player-facing manual duration converter was found across {0} C# files." -f $sourceFiles.Count)
}

Write-Host ""

if ($failures.Count -gt 0) {
    Write-Host "Duration-formatting audit failed with $($failures.Count) issue(s)." -ForegroundColor Red
    exit 1
}

Write-Host "Duration-formatting audit passed." -ForegroundColor Green
Write-Host "Allowlisted translation keys: $($allowlistedKeys.Count)"
Write-Host "Legacy keyed surfaces found: $($legacyTranslationKeys.Count)"
Write-Host "C# files scanned: $($sourceFiles.Count)"
exit 0
