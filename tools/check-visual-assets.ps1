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

function Normalize-TextureFamily {
    param([string]$Path)

    if ([string]::IsNullOrWhiteSpace($Path)) {
        return ""
    }

    $normalized = $Path.Replace("\", "/").Trim()
    if ($normalized.EndsWith(".png", [StringComparison]::OrdinalIgnoreCase)) {
        $normalized = $normalized.Substring(0, $normalized.Length - 4)
    }

    $slashIndex = $normalized.LastIndexOf("/")
    if ($slashIndex -ge 0) {
        $directory = $normalized.Substring(0, $slashIndex)
        $name = $normalized.Substring($slashIndex + 1)
    }
    else {
        $directory = ""
        $name = $normalized
    }

    $name = [regex]::Replace(
        $name,
        '_(Child|Fat|Female|Hulk|Male|Thin)_(east|north|south|west)$',
        "")
    $name = [regex]::Replace(
        $name,
        '_(east|north|south|west)$',
        "")

    if ([string]::IsNullOrWhiteSpace($directory)) {
        return $name
    }

    return "$directory/$name"
}

function Get-MarkedBlock {
    param(
        [string]$Text,
        [string]$StartMarker,
        [string]$EndMarker,
        [string]$Description
    )

    $pattern = '(?s)' + [regex]::Escape($StartMarker) +
        '(?<value>.*?)' + [regex]::Escape($EndMarker)
    $match = [regex]::Match($Text, $pattern)

    if (-not $match.Success) {
        Add-Failure "Missing $Description markers in the visual asset register."
        return $null
    }

    return $match.Groups["value"].Value
}

Write-Host "GateRim SG-1 visual asset check"
Write-Host "Repository: $RepositoryRoot"
Write-Host ""

$registerPath = Join-Path $RepositoryRoot "docs/VISUAL_ASSET_REGISTER.md"
$texturesRoot = Join-Path $RepositoryRoot "Textures"
$modIconPath = Join-Path $RepositoryRoot "About/ModIcon.png"
$visualReferencePagePath = Join-Path $RepositoryRoot "docs/wiki/Visual-Assets.md"
$wikiIconMappings = @(
    @{
        Source = "Textures/UI/Xenotypes/SG1_Jaffa.png"
        Wiki = "docs/wiki/images/SG1_Jaffa.png"
        Page = "docs/wiki/Jaffa.md"
        Reference = "images/SG1_Jaffa.png"
    },
    @{
        Source = "Textures/UI/Xenotypes/SG1_GoauldHost.png"
        Wiki = "docs/wiki/images/SG1_GoauldHost.png"
        Page = "docs/wiki/Active-Goauld-Host.md"
        Reference = "images/SG1_GoauldHost.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_JaffaLineage.png"
        Wiki = "docs/wiki/images/SG1_JaffaLineage.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_JaffaLineage.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_JaffaPhysiology.png"
        Wiki = "docs/wiki/images/SG1_JaffaPhysiology.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_JaffaPhysiology.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_JaffaPouchPotential.png"
        Wiki = "docs/wiki/images/SG1_JaffaPouchPotential.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_JaffaPouchPotential.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_JaffaSymbioteCompatibility.png"
        Wiki = "docs/wiki/images/SG1_JaffaSymbioteCompatibility.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_JaffaSymbioteCompatibility.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_GoauldLongevity.png"
        Wiki = "docs/wiki/images/SG1_GoauldLongevity.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldLongevity.png"
    },
    @{
        Source = "Textures/UI/Genes/SG1_NaquadahBlood.png"
        Wiki = "docs/wiki/images/SG1_NaquadahBlood.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_NaquadahBlood.png"
    },
    @{
        Source = "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark_south.png"
        Wiki = "docs/wiki/images/GenericJaffaForeheadMark_south.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/GenericJaffaForeheadMark_south.png"
    },
    @{
        Source = "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark_south.png"
        Wiki = "docs/wiki/images/GenericSilverJaffaForeheadMark_south.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/GenericSilverJaffaForeheadMark_south.png"
    },
    @{
        Source = "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark_south.png"
        Wiki = "docs/wiki/images/GenericGoldJaffaForeheadMark_south.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/GenericGoldJaffaForeheadMark_south.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_GoauldRitualBasin.png"
        Wiki = "docs/wiki/images/SG1_GoauldRitualBasin.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldRitualBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaIncubationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaIncubationBasin.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_PrimtaIncubationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_GoauldRitualBasin.png"
        Wiki = "docs/wiki/images/SG1_GoauldRitualBasin.png"
        Page = "docs/wiki/Ritual-Basin.md"
        Reference = "images/SG1_GoauldRitualBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_GoauldRitualBasin.png"
        Wiki = "docs/wiki/images/SG1_GoauldRitualBasin.png"
        Page = "docs/wiki/Ritual-Implantation.md"
        Reference = "images/SG1_GoauldRitualBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_GoauldRitualBasin.png"
        Wiki = "docs/wiki/images/SG1_GoauldRitualBasin.png"
        Page = "docs/wiki/Primta-Formal-Ceremony.md"
        Reference = "images/SG1_GoauldRitualBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaIncubationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaIncubationBasin.png"
        Page = "docs/wiki/Primta-Incubation.md"
        Reference = "images/SG1_PrimtaIncubationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaIncubationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaIncubationBasin.png"
        Page = "docs/wiki/Goauld-Queen-Assisted-Maturation.md"
        Reference = "images/SG1_PrimtaIncubationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaIncubationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaIncubationBasin.png"
        Page = "docs/wiki/Primta-Larva.md"
        Reference = "images/SG1_PrimtaIncubationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Primta-Preservation-Basin.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Primta-Deep-Freezing.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Primta-Temperature.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Goauld-Queen-Assisted-Maturation.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/Things/Building/SG1_PrimtaPreservationBasin.png"
        Wiki = "docs/wiki/images/SG1_PrimtaPreservationBasin.png"
        Page = "docs/wiki/Primta-Larva.md"
        Reference = "images/SG1_PrimtaPreservationBasin.png"
    },
    @{
        Source = "Textures/UI/Commands/SG1_AutonomousHunt.png"
        Wiki = "docs/wiki/images/SG1_AutonomousHunt.png"
        Page = "docs/wiki/Autonomous-Hunt.md"
        Reference = "images/SG1_AutonomousHunt.png"
    },
    @{
        Source = "Textures/UI/Commands/SG1_EmergencyExtraction.png"
        Wiki = "docs/wiki/images/SG1_EmergencyExtraction.png"
        Page = "docs/wiki/Emergency-Extraction.md"
        Reference = "images/SG1_EmergencyExtraction.png"
    },
    @{
        Source = "Textures/UI/Commands/SG1_ForcedImplantation.png"
        Wiki = "docs/wiki/images/SG1_ForcedImplantation.png"
        Page = "docs/wiki/Forced-Implantation.md"
        Reference = "images/SG1_ForcedImplantation.png"
    },
    @{
        Source = "Textures/UI/Commands/SG1_RitualImplantation.png"
        Wiki = "docs/wiki/images/SG1_RitualImplantation.png"
        Page = "docs/wiki/Ritual-Implantation.md"
        Reference = "images/SG1_RitualImplantation.png"
    },
    @{
        Source = "Textures/UI/Commands/SG1_RitualImplantation.png"
        Wiki = "docs/wiki/images/SG1_RitualImplantation.png"
        Page = "docs/wiki/Primta-Formal-Ceremony.md"
        Reference = "images/SG1_RitualImplantation.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/SG1_FreeJaffa.png"
        Wiki = "docs/wiki/images/SG1_FreeJaffa.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_FreeJaffa.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/SG1_GoauldSystemLords.png"
        Wiki = "docs/wiki/images/SG1_GoauldSystemLords.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldSystemLords.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/SG1_SGCExpedition.png"
        Wiki = "docs/wiki/images/SG1_SGCExpedition.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_SGCExpedition.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/SG1_Tokra.png"
        Wiki = "docs/wiki/images/SG1_Tokra.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_Tokra.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective.png"
        Wiki = "docs/wiki/images/SG1_GoauldEncryptedObjective.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldEncryptedObjective.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield.png"
        Wiki = "docs/wiki/images/SG1_GoauldOpenConflictBattlefield.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldOpenConflictBattlefield.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage.png"
        Wiki = "docs/wiki/images/SG1_GoauldRelaySabotage.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_GoauldRelaySabotage.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition.png"
        Wiki = "docs/wiki/images/SG1_JaffaOfficerFieldPosition.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_JaffaOfficerFieldPosition.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact.png"
        Wiki = "docs/wiki/images/SG1_TokraClandestineContact.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_TokraClandestineContact.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal.png"
        Wiki = "docs/wiki/images/SG1_TokraDistressSignal.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_TokraDistressSignal.png"
    },
    @{
        Source = "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous.png"
        Wiki = "docs/wiki/images/SG1_TokraLogisticsRendezvous.png"
        Page = "docs/wiki/Visual-Assets.md"
        Reference = "images/SG1_TokraLogisticsRendezvous.png"
    }
)

if (-not (Test-Path -LiteralPath $registerPath -PathType Leaf)) {
    Add-Failure "Missing visual asset register: docs/VISUAL_ASSET_REGISTER.md"
}

if (-not (Test-Path -LiteralPath $texturesRoot -PathType Container)) {
    Add-Failure "Missing texture directory: Textures"
}

if (-not (Test-Path -LiteralPath $modIconPath -PathType Leaf)) {
    Add-Failure "Missing preserved public mod icon: About/ModIcon.png"
}
else {
    Add-Pass "Preserved public mod icon exists."
}

if (-not (Test-Path -LiteralPath $visualReferencePagePath -PathType Leaf)) {
    Add-Failure "Missing progressive visual reference page: docs/wiki/Visual-Assets.md"
}

foreach ($mapping in $wikiIconMappings) {
    $sourcePath = Join-Path $RepositoryRoot $mapping.Source
    $wikiPath = Join-Path $RepositoryRoot $mapping.Wiki
    $pagePath = Join-Path $RepositoryRoot $mapping.Page

    if (-not (Test-Path -LiteralPath $sourcePath -PathType Leaf)) {
        Add-Failure "Missing approved gameplay icon: $($mapping.Source)"
    }

    if (-not (Test-Path -LiteralPath $wikiPath -PathType Leaf)) {
        Add-Failure "Missing approved wiki icon copy: $($mapping.Wiki)"
    }

    if (-not (Test-Path -LiteralPath $pagePath -PathType Leaf)) {
        Add-Failure "Missing wiki page: $($mapping.Page)"
    }
}

if ($failures.Count -gt 0) {
    Write-Host ""
    Write-Host "Visual asset check failed before inventory comparison." -ForegroundColor Red
    exit 1
}

foreach ($mapping in $wikiIconMappings) {
    $sourcePath = Join-Path $RepositoryRoot $mapping.Source
    $wikiPath = Join-Path $RepositoryRoot $mapping.Wiki
    $pagePath = Join-Path $RepositoryRoot $mapping.Page

    $sourceHash = (Get-FileHash -LiteralPath $sourcePath -Algorithm SHA256).Hash
    $wikiHash = (Get-FileHash -LiteralPath $wikiPath -Algorithm SHA256).Hash

    if ($sourceHash -cne $wikiHash) {
        Add-Failure "Wiki icon differs from approved gameplay icon: $($mapping.Wiki)"
    }
    else {
        Add-Pass "Wiki icon matches approved gameplay icon: $($mapping.Wiki)"
    }

    $pageText = Get-Content -LiteralPath $pagePath -Raw -Encoding UTF8
    if ($pageText -notlike "*$($mapping.Reference)*") {
        Add-Failure "Wiki page does not display approved icon: $($mapping.Page)"
    }
    else {
        Add-Pass "Wiki page displays approved icon: $($mapping.Page)"
    }
}

$visualReferenceText = Get-Content -LiteralPath $visualReferencePagePath -Raw -Encoding UTF8
foreach ($mapping in $wikiIconMappings) {
    if ($visualReferenceText -notlike "*$($mapping.Reference)*") {
        Add-Failure "Visual reference page does not display approved icon: $($mapping.Reference)"
    }
}
if ($failures.Count -eq 0) {
    Add-Pass "Progressive visual reference page displays every approved gameplay icon copy."
}

$registerText = Get-Content -LiteralPath $registerPath -Raw -Encoding UTF8
$localBlock = Get-MarkedBlock `
    $registerText `
    "<!-- LOCAL_ASSET_TABLE_START -->" `
    "<!-- LOCAL_ASSET_TABLE_END -->" `
    "local asset table"
$externalBlock = Get-MarkedBlock `
    $registerText `
    "<!-- EXTERNAL_ASSET_TABLE_START -->" `
    "<!-- EXTERNAL_ASSET_TABLE_END -->" `
    "external asset table"

$registeredLocalCounts = @{}
$registeredLocalStatuses = @{}
$registeredLocalPaths = New-Object System.Collections.Generic.List[string]

if ($null -ne $localBlock) {
    foreach ($line in ($localBlock -split "`r?`n")) {
        $match = [regex]::Match(
            $line,
            '^\|\s*`(?<path>[^`]+)`\s*\|\s*(?<count>\d+)\s*\|[^|]*\|[^|]*\|\s*`(?<status>[^`]+)`\s*\|')

        if (-not $match.Success) {
            continue
        }

        $path = $match.Groups["path"].Value
        $count = [int]$match.Groups["count"].Value
        $status = $match.Groups["status"].Value

        if ($registeredLocalPaths -ccontains $path) {
            Add-Failure "Duplicate local asset family in register: $path"
            continue
        }

        [void]$registeredLocalPaths.Add($path)
        $registeredLocalCounts[$path] = $count
        $registeredLocalStatuses[$path] = $status
    }
}

$registeredExternalPaths = New-Object System.Collections.Generic.List[string]

if ($null -ne $externalBlock) {
    foreach ($line in ($externalBlock -split "`r?`n")) {
        $match = [regex]::Match(
            $line,
            '^\|\s*`(?<path>[^`]+)`\s*\|')

        if (-not $match.Success) {
            continue
        }

        $path = $match.Groups["path"].Value

        if ($registeredExternalPaths -ccontains $path) {
            Add-Failure "Duplicate external asset path in register: $path"
            continue
        }

        [void]$registeredExternalPaths.Add($path)
    }
}

if ($registeredLocalPaths.Count -eq 0) {
    Add-Failure "No local asset family was parsed from the register."
}

if ($registeredExternalPaths.Count -eq 0) {
    Add-Failure "No external asset path was parsed from the register."
}

$expectedFinalLocalPaths = @(
    "Storytellers/SG1_Command",
    "Storytellers/SG1_Command_Tiny",
    "Things/Building/SG1_GoauldRitualBasin",
    "Things/Building/SG1_PrimtaIncubationBasin",
    "Things/Building/SG1_PrimtaPreservationBasin",
    "UI/Xenotypes/SG1_GoauldHost",
    "UI/Xenotypes/SG1_Jaffa",
    "UI/Genes/SG1_GoauldLongevity",
    "UI/Genes/SG1_JaffaLineage",
    "UI/Genes/SG1_JaffaPhysiology",
    "UI/Genes/SG1_JaffaPouchPotential",
    "UI/Genes/SG1_JaffaSymbioteCompatibility",
    "UI/Genes/SG1_NaquadahBlood",
    "Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark",
    "Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark",
    "Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark",
    "UI/Commands/SG1_AutonomousHunt",
    "UI/Commands/SG1_EmergencyExtraction",
    "UI/Commands/SG1_ForcedImplantation",
    "UI/Commands/SG1_RitualImplantation",
    "World/WorldObjects/Expanding/SG1_FreeJaffa",
    "World/WorldObjects/Expanding/SG1_GoauldSystemLords",
    "World/WorldObjects/Expanding/SG1_SGCExpedition",
    "World/WorldObjects/Expanding/SG1_Tokra",
    "World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective",
    "World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield",
    "World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage",
    "World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition",
    "World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact",
    "World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal",
    "World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous"
)
$actualFinalLocalPaths = @(
    $registeredLocalPaths |
        Where-Object { $registeredLocalStatuses[$_] -ceq "final" } |
        Sort-Object
)
$finalPathDifferences = @(
    Compare-Object `
        -ReferenceObject @($expectedFinalLocalPaths | Sort-Object) `
        -DifferenceObject $actualFinalLocalPaths `
        -CaseSensitive
)

if ($finalPathDifferences.Count -gt 0) {
    Add-Failure (
        "Final local asset whitelist differs from the thirty-one approved storyteller, building, xenotype, gameplay-gene, intrinsic Jaffa pawn-overlay, command, faction and event-site families: {0}" -f
        (($finalPathDifferences | ForEach-Object {
            "{0} {1}" -f $_.SideIndicator, $_.InputObject
        }) -join ", "))
}
else {
    Add-Pass "Final local asset whitelist matches the thirty-one approved storyteller, building, xenotype, gameplay-gene, intrinsic Jaffa pawn-overlay, command, faction and event-site families."
}

if ($registerText -notmatch '(?m)^- `About/ModIcon\.png`: `final` public mod identity\.') {
    Add-Failure "The register does not explicitly classify About/ModIcon.png as final public mod identity."
}
else {
    Add-Pass "About/ModIcon.png is explicitly registered as final public mod identity."
}

$pngFiles = @(
    Get-ChildItem -LiteralPath $texturesRoot -Filter "*.png" -File -Recurse |
        Sort-Object FullName
)

$actualFamilyCounts = @{}
$actualFamilyPaths = New-Object System.Collections.Generic.List[string]
$invalidPngFiles = New-Object System.Collections.Generic.List[string]
$pngSignature = @(137, 80, 78, 71, 13, 10, 26, 10)

foreach ($file in $pngFiles) {
    $relative = $file.FullName.Substring($texturesRoot.Length)
    $relative = $relative.TrimStart(
        [char[]]@(
            [IO.Path]::DirectorySeparatorChar,
            [IO.Path]::AltDirectorySeparatorChar))
    # Normalize the original PNG path directly. Windows PowerShell 5.1 can
    # bind Path.ChangeExtension($relative, $null) as an empty extension and
    # leave a trailing dot, which would split every physical file into its own
    # false family.
    $family = Normalize-TextureFamily $relative

    if (-not $actualFamilyCounts.ContainsKey($family)) {
        $actualFamilyCounts[$family] = 0
        [void]$actualFamilyPaths.Add($family)
    }

    $actualFamilyCounts[$family]++

    try {
        $bytes = [IO.File]::ReadAllBytes($file.FullName)
        $validSignature = $bytes.Length -ge 8

        if ($validSignature) {
            for ($index = 0; $index -lt 8; $index++) {
                if ($bytes[$index] -ne $pngSignature[$index]) {
                    $validSignature = $false
                    break
                }
            }
        }

        if (-not $validSignature) {
            [void]$invalidPngFiles.Add($relative.Replace("\", "/"))
        }
    }
    catch {
        [void]$invalidPngFiles.Add($relative.Replace("\", "/"))
    }
}

if ($invalidPngFiles.Count -gt 0) {
    Add-Failure ("Invalid or unreadable PNG file(s): {0}" -f
        ($invalidPngFiles -join ", "))
}
else {
    Add-Pass "All local PNG files have a valid PNG signature."
}

$familyDifferences = @(
    Compare-Object `
        -ReferenceObject @($registeredLocalPaths | Sort-Object) `
        -DifferenceObject @($actualFamilyPaths | Sort-Object) `
        -CaseSensitive
)

$missingRegisteredFamilies = @(
    $familyDifferences |
        Where-Object { $_.SideIndicator -eq "<=" } |
        ForEach-Object { [string]$_.InputObject }
)
$unregisteredActualFamilies = @(
    $familyDifferences |
        Where-Object { $_.SideIndicator -eq "=>" } |
        ForEach-Object { [string]$_.InputObject }
)

if ($missingRegisteredFamilies.Count -gt 0) {
    Add-Failure ("Registered family without matching PNG files: {0}" -f
        ($missingRegisteredFamilies -join ", "))
}
else {
    Add-Pass "Every registered local family resolves to PNG files."
}

if ($unregisteredActualFamilies.Count -gt 0) {
    Add-Failure ("Unregistered local texture family: {0}" -f
        ($unregisteredActualFamilies -join ", "))
}
else {
    Add-Pass "Every local texture family is registered."
}

$expectedPngCount = 0

foreach ($path in $registeredLocalPaths) {
    $expectedPngCount += [int]$registeredLocalCounts[$path]

    $actualPath = @(
        $actualFamilyPaths |
            Where-Object { $_ -ceq $path }
    ) | Select-Object -First 1

    if ($null -eq $actualPath) {
        continue
    }

    $actualCount = [int]$actualFamilyCounts[$actualPath]
    $expectedCount = [int]$registeredLocalCounts[$path]

    if ($actualCount -ne $expectedCount) {
        Add-Failure (
            "Family '{0}' contains {1} PNG file(s); register expects {2}." -f
            $path,
            $actualCount,
            $expectedCount)
    }
}

if ($expectedPngCount -ne $pngFiles.Count) {
    Add-Failure (
        "Registered PNG total is {0}; repository contains {1}." -f
        $expectedPngCount,
        $pngFiles.Count)
}
else {
    Add-Pass "Registered PNG counts match the repository."
}

$localReferences = New-Object System.Collections.Generic.List[string]
$externalReferences = New-Object System.Collections.Generic.List[string]
$textureNodeNames = @(
    "activateTexPath",
    "commandIconPath",
    "expandingIconTexture",
    "factionIconPath",
    "iconPath",
    "portraitLarge",
    "portraitTiny",
    "settlementTexturePath",
    "siteTexture",
    "texPath",
    "texture",
    "uiIconPath",
    "wornGraphicPath"
)

$xmlRoot = Join-Path $RepositoryRoot "1.6"
$xmlFiles = @(
    Get-ChildItem -LiteralPath $xmlRoot -Filter "*.xml" -File -Recurse |
        Sort-Object FullName
)

foreach ($file in $xmlFiles) {
    try {
        [xml]$xml = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8

        foreach ($node in @($xml.SelectNodes("//*"))) {
            if ($textureNodeNames -notcontains $node.Name) {
                continue
            }

            $path = [string]$node.InnerText
            $path = $path.Trim()

            if ([string]::IsNullOrWhiteSpace($path) -or
                -not $path.Contains("/")) {
                continue
            }

            $family = Normalize-TextureFamily $path

            if ($actualFamilyPaths -ccontains $family) {
                if ($localReferences -cnotcontains $family) {
                    [void]$localReferences.Add($family)
                }
            }
            elseif ($externalReferences -cnotcontains $path) {
                [void]$externalReferences.Add($path)
            }
        }
    }
    catch {
        Add-Failure ("Could not parse XML while checking texture paths: {0}: {1}" -f
            $file.FullName,
            $_.Exception.Message)
    }
}

$sourceRoot = Join-Path $RepositoryRoot "Source"
$csharpFiles = @(
    Get-ChildItem -LiteralPath $sourceRoot -Filter "*.cs" -File -Recurse |
        Sort-Object FullName
)
$csharpPatterns = @(
    'ContentFinder<Texture2D>\.Get\("(?<path>[^"]+)"',
    'commandIconPath\s*=\s*"(?<path>[^"]+)"'
)

foreach ($file in $csharpFiles) {
    $text = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8

    foreach ($pattern in $csharpPatterns) {
        foreach ($match in [regex]::Matches($text, $pattern)) {
            $path = $match.Groups["path"].Value
            $family = Normalize-TextureFamily $path

            if ($actualFamilyPaths -ccontains $family) {
                if ($localReferences -cnotcontains $family) {
                    [void]$localReferences.Add($family)
                }
            }
            elseif ($externalReferences -cnotcontains $path) {
                [void]$externalReferences.Add($path)
            }
        }
    }
}

$referenceDifferences = @(
    Compare-Object `
        -ReferenceObject @($actualFamilyPaths | Sort-Object) `
        -DifferenceObject @($localReferences | Sort-Object) `
        -CaseSensitive
)
$unreferencedFamilies = @(
    $referenceDifferences |
        Where-Object { $_.SideIndicator -eq "<=" } |
        ForEach-Object { [string]$_.InputObject }
)
$missingLocalReferences = @(
    $referenceDifferences |
        Where-Object { $_.SideIndicator -eq "=>" } |
        ForEach-Object { [string]$_.InputObject }
)

if ($unreferencedFamilies.Count -gt 0) {
    Add-Failure ("Local family has no direct XML or C# reference: {0}" -f
        ($unreferencedFamilies -join ", "))
}
else {
    Add-Pass "Every local texture family has a direct XML or C# reference."
}

if ($missingLocalReferences.Count -gt 0) {
    Add-Failure ("Source references a missing local family: {0}" -f
        ($missingLocalReferences -join ", "))
}
else {
    Add-Pass "No direct local texture reference is missing."
}

$externalDifferences = @(
    Compare-Object `
        -ReferenceObject @($registeredExternalPaths | Sort-Object) `
        -DifferenceObject @($externalReferences | Sort-Object) `
        -CaseSensitive
)
$unusedExternalRegistrations = @(
    $externalDifferences |
        Where-Object { $_.SideIndicator -eq "<=" } |
        ForEach-Object { [string]$_.InputObject }
)
$unregisteredExternalReferences = @(
    $externalDifferences |
        Where-Object { $_.SideIndicator -eq "=>" } |
        ForEach-Object { [string]$_.InputObject }
)

if ($unusedExternalRegistrations.Count -gt 0) {
    Add-Failure ("Registered external path is no longer referenced: {0}" -f
        ($unusedExternalRegistrations -join ", "))
}
else {
    Add-Pass "Every registered external texture path remains referenced."
}

if ($unregisteredExternalReferences.Count -gt 0) {
    Add-Failure ("Unregistered external texture path: {0}" -f
        ($unregisteredExternalReferences -join ", "))
}
else {
    Add-Pass "Every direct external texture path is registered."
}

Write-Host ""
Write-Host "Final local texture families: $($actualFinalLocalPaths.Count)"
Write-Host "Local PNG files: $($pngFiles.Count)"
Write-Host "Local texture families: $($actualFamilyPaths.Count)"
Write-Host "Direct external texture paths: $($externalReferences.Count)"
Write-Host "Missing local references: $($missingLocalReferences.Count)"
Write-Host "Unregistered local families: $($unregisteredActualFamilies.Count)"
Write-Host ""

if ($failures.Count -gt 0) {
    Write-Host "Visual asset check failed with $($failures.Count) issue(s)." -ForegroundColor Red
    exit 1
}

Write-Host "Visual asset check passed." -ForegroundColor Green
exit 0
