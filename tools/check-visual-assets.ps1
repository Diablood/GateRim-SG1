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

function New-WikiMapping {
    param(
        [string]$Source,
        [string]$Wiki,
        [string]$Page,
        [string]$Reference
    )

    return @{
        Source = $Source
        Wiki = $Wiki
        Page = $Page
        Reference = $Reference
    }
}

Write-Host "GateRim SG-1 visual asset check"
Write-Host "Repository: $RepositoryRoot"
Write-Host ""

$registerPath = Join-Path $RepositoryRoot "docs/VISUAL_ASSET_REGISTER.md"
$texturesRoot = Join-Path $RepositoryRoot "Textures"
$modIconPath = Join-Path $RepositoryRoot "About/ModIcon.png"
$visualReferencePagePath = Join-Path $RepositoryRoot "docs/wiki/Visual-Assets.md"
$xmlRoot = Join-Path $RepositoryRoot "1.6"
$sourceRoot = Join-Path $RepositoryRoot "Source"

$wikiIconMappings = @(
    (New-WikiMapping "Textures/UI/Xenotypes/SG1_Jaffa.png" "docs/wiki/images/SG1_Jaffa.png" "docs/wiki/Jaffa.md" "images/SG1_Jaffa.png"),
    (New-WikiMapping "Textures/UI/Xenotypes/SG1_GoauldHost.png" "docs/wiki/images/SG1_GoauldHost.png" "docs/wiki/Active-Goauld-Host.md" "images/SG1_GoauldHost.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_JaffaLineage.png" "docs/wiki/images/SG1_JaffaLineage.png" "docs/wiki/Visual-Assets.md" "images/SG1_JaffaLineage.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_JaffaPhysiology.png" "docs/wiki/images/SG1_JaffaPhysiology.png" "docs/wiki/Visual-Assets.md" "images/SG1_JaffaPhysiology.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_JaffaPouchPotential.png" "docs/wiki/images/SG1_JaffaPouchPotential.png" "docs/wiki/Visual-Assets.md" "images/SG1_JaffaPouchPotential.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_JaffaSymbioteCompatibility.png" "docs/wiki/images/SG1_JaffaSymbioteCompatibility.png" "docs/wiki/Visual-Assets.md" "images/SG1_JaffaSymbioteCompatibility.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_GoauldLongevity.png" "docs/wiki/images/SG1_GoauldLongevity.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldLongevity.png"),
    (New-WikiMapping "Textures/UI/Genes/SG1_NaquadahBlood.png" "docs/wiki/images/SG1_NaquadahBlood.png" "docs/wiki/Visual-Assets.md" "images/SG1_NaquadahBlood.png"),
    (New-WikiMapping "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark_south.png" "docs/wiki/images/GenericJaffaForeheadMark_south.png" "docs/wiki/Visual-Assets.md" "images/GenericJaffaForeheadMark_south.png"),
    (New-WikiMapping "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark_south.png" "docs/wiki/images/GenericSilverJaffaForeheadMark_south.png" "docs/wiki/Visual-Assets.md" "images/GenericSilverJaffaForeheadMark_south.png"),
    (New-WikiMapping "Textures/Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark_south.png" "docs/wiki/images/GenericGoldJaffaForeheadMark_south.png" "docs/wiki/Visual-Assets.md" "images/GenericGoldJaffaForeheadMark_south.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_GoauldRitualBasin.png" "docs/wiki/images/SG1_GoauldRitualBasin.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldRitualBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaIncubationBasin.png" "docs/wiki/images/SG1_PrimtaIncubationBasin.png" "docs/wiki/Visual-Assets.md" "images/SG1_PrimtaIncubationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Visual-Assets.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_GoauldRitualBasin.png" "docs/wiki/images/SG1_GoauldRitualBasin.png" "docs/wiki/Ritual-Basin.md" "images/SG1_GoauldRitualBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_GoauldRitualBasin.png" "docs/wiki/images/SG1_GoauldRitualBasin.png" "docs/wiki/Ritual-Implantation.md" "images/SG1_GoauldRitualBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_GoauldRitualBasin.png" "docs/wiki/images/SG1_GoauldRitualBasin.png" "docs/wiki/Primta-Formal-Ceremony.md" "images/SG1_GoauldRitualBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaIncubationBasin.png" "docs/wiki/images/SG1_PrimtaIncubationBasin.png" "docs/wiki/Primta-Incubation.md" "images/SG1_PrimtaIncubationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaIncubationBasin.png" "docs/wiki/images/SG1_PrimtaIncubationBasin.png" "docs/wiki/Goauld-Queen-Assisted-Maturation.md" "images/SG1_PrimtaIncubationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaIncubationBasin.png" "docs/wiki/images/SG1_PrimtaIncubationBasin.png" "docs/wiki/Primta-Larva.md" "images/SG1_PrimtaIncubationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Primta-Preservation-Basin.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Primta-Deep-Freezing.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Primta-Temperature.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Goauld-Queen-Assisted-Maturation.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_PrimtaPreservationBasin.png" "docs/wiki/images/SG1_PrimtaPreservationBasin.png" "docs/wiki/Primta-Larva.md" "images/SG1_PrimtaPreservationBasin.png"),
    (New-WikiMapping "Textures/UI/Commands/SG1_AutonomousHunt.png" "docs/wiki/images/SG1_AutonomousHunt.png" "docs/wiki/Autonomous-Hunt.md" "images/SG1_AutonomousHunt.png"),
    (New-WikiMapping "Textures/UI/Commands/SG1_EmergencyExtraction.png" "docs/wiki/images/SG1_EmergencyExtraction.png" "docs/wiki/Emergency-Extraction.md" "images/SG1_EmergencyExtraction.png"),
    (New-WikiMapping "Textures/UI/Commands/SG1_ForcedImplantation.png" "docs/wiki/images/SG1_ForcedImplantation.png" "docs/wiki/Forced-Implantation.md" "images/SG1_ForcedImplantation.png"),
    (New-WikiMapping "Textures/UI/Commands/SG1_RitualImplantation.png" "docs/wiki/images/SG1_RitualImplantation.png" "docs/wiki/Ritual-Implantation.md" "images/SG1_RitualImplantation.png"),
    (New-WikiMapping "Textures/UI/Commands/SG1_RitualImplantation.png" "docs/wiki/images/SG1_RitualImplantation.png" "docs/wiki/Primta-Formal-Ceremony.md" "images/SG1_RitualImplantation.png"),
    (New-WikiMapping "Textures/Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh.png" "docs/wiki/images/KaraKesh.png" "docs/wiki/Kara-Kesh.md" "images/KaraKesh.png"),
    (New-WikiMapping "Textures/Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet.png" "docs/wiki/images/GoauldHealingBracelet.png" "docs/wiki/Goauld-Healing-Bracelet.md" "images/GoauldHealingBracelet.png"),
    (New-WikiMapping "Textures/Things/Item/SG1_TokraIntroductionArtifact.png" "docs/wiki/images/SG1_TokraIntroductionArtifact.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraIntroductionArtifact.png"),
    (New-WikiMapping "Textures/Things/Item/SG1_TokraMissionIntelPacket.png" "docs/wiki/images/SG1_TokraMissionIntelPacket.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraMissionIntelPacket.png"),
    (New-WikiMapping "Textures/Things/Item/SG1_TokraObservationDevice.png" "docs/wiki/images/SG1_TokraObservationDevice.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraObservationDevice.png"),
    (New-WikiMapping "Textures/Things/Item/SG1_TokraOrganicDeadDrop.png" "docs/wiki/images/SG1_TokraOrganicDeadDrop.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraOrganicDeadDrop.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_TokraRelaySabotageDevice.png" "docs/wiki/images/SG1_TokraRelaySabotageDevice.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraRelaySabotageDevice.png"),
    (New-WikiMapping "Textures/Things/Building/SG1_TokraSecureCommunicator.png" "docs/wiki/images/SG1_TokraSecureCommunicator.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraSecureCommunicator.png"),
    (New-WikiMapping "Textures/Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot.png" "docs/wiki/images/SG1_TokraDeliveryDropSpot.png" "docs/wiki/Tokra-Mission-Objects.md" "images/SG1_TokraDeliveryDropSpot.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/SG1_FreeJaffa.png" "docs/wiki/images/SG1_FreeJaffa.png" "docs/wiki/Visual-Assets.md" "images/SG1_FreeJaffa.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/SG1_GoauldSystemLords.png" "docs/wiki/images/SG1_GoauldSystemLords.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldSystemLords.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/SG1_SGCExpedition.png" "docs/wiki/images/SG1_SGCExpedition.png" "docs/wiki/Visual-Assets.md" "images/SG1_SGCExpedition.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/SG1_Tokra.png" "docs/wiki/images/SG1_Tokra.png" "docs/wiki/Visual-Assets.md" "images/SG1_Tokra.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective.png" "docs/wiki/images/SG1_GoauldEncryptedObjective.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldEncryptedObjective.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield.png" "docs/wiki/images/SG1_GoauldOpenConflictBattlefield.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldOpenConflictBattlefield.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage.png" "docs/wiki/images/SG1_GoauldRelaySabotage.png" "docs/wiki/Visual-Assets.md" "images/SG1_GoauldRelaySabotage.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition.png" "docs/wiki/images/SG1_JaffaOfficerFieldPosition.png" "docs/wiki/Visual-Assets.md" "images/SG1_JaffaOfficerFieldPosition.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact.png" "docs/wiki/images/SG1_TokraClandestineContact.png" "docs/wiki/Visual-Assets.md" "images/SG1_TokraClandestineContact.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal.png" "docs/wiki/images/SG1_TokraDistressSignal.png" "docs/wiki/Visual-Assets.md" "images/SG1_TokraDistressSignal.png"),
    (New-WikiMapping "Textures/World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous.png" "docs/wiki/images/SG1_TokraLogisticsRendezvous.png" "docs/wiki/Visual-Assets.md" "images/SG1_TokraLogisticsRendezvous.png")
)

$requiredFiles = @($registerPath, $modIconPath, $visualReferencePagePath)
foreach ($requiredFile in $requiredFiles) {
    if (-not (Test-Path -LiteralPath $requiredFile -PathType Leaf)) {
        Add-Failure "Missing required visual-audit file: $requiredFile"
    }
}

foreach ($requiredDirectory in @($texturesRoot, $xmlRoot, $sourceRoot)) {
    if (-not (Test-Path -LiteralPath $requiredDirectory -PathType Container)) {
        Add-Failure "Missing required visual-audit directory: $requiredDirectory"
    }
}

foreach ($mapping in $wikiIconMappings) {
    foreach ($path in @($mapping.Source, $mapping.Wiki, $mapping.Page)) {
        $fullPath = Join-Path $RepositoryRoot $path
        if (-not (Test-Path -LiteralPath $fullPath -PathType Leaf)) {
            Add-Failure "Missing approved visual reference file: $path"
        }
    }
}

if ($failures.Count -gt 0) {
    Write-Host ""
    Write-Host "Visual asset check failed before inventory comparison." -ForegroundColor Red
    exit 1
}

Add-Pass "Preserved public mod icon exists."

$visualReferenceText = Get-Content -LiteralPath $visualReferencePagePath -Raw -Encoding UTF8
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

    if ($visualReferenceText -notlike "*$($mapping.Reference)*") {
        Add-Failure "Visual reference page does not display approved icon: $($mapping.Reference)"
    }
}

if ($failures.Count -eq 0) {
    Add-Pass "Progressive visual reference page displays every approved gameplay icon copy."
}

$registerText = Get-Content -LiteralPath $registerPath -Raw -Encoding UTF8
$localBlock = Get-MarkedBlock $registerText "<!-- LOCAL_ASSET_TABLE_START -->" "<!-- LOCAL_ASSET_TABLE_END -->" "local asset table"
$externalBlock = Get-MarkedBlock $registerText "<!-- EXTERNAL_ASSET_TABLE_START -->" "<!-- EXTERNAL_ASSET_TABLE_END -->" "external asset table"

$registeredLocalCounts = @{}
$registeredLocalStatuses = @{}
$registeredLocalPaths = New-Object System.Collections.Generic.List[string]

if ($null -ne $localBlock) {
    foreach ($line in ($localBlock -split "`r?`n")) {
        $match = [regex]::Match($line, '^\|\s*`(?<path>[^`]+)`\s*\|\s*(?<count>\d+)\s*\|[^|]*\|[^|]*\|\s*`(?<status>[^`]+)`\s*\|')
        if (-not $match.Success) { continue }
        $path = $match.Groups["path"].Value
        if ($registeredLocalPaths -ccontains $path) {
            Add-Failure "Duplicate local asset family in register: $path"
            continue
        }
        [void]$registeredLocalPaths.Add($path)
        $registeredLocalCounts[$path] = [int]$match.Groups["count"].Value
        $registeredLocalStatuses[$path] = $match.Groups["status"].Value
    }
}

$registeredExternalPaths = New-Object System.Collections.Generic.List[string]
if ($null -ne $externalBlock) {
    foreach ($line in ($externalBlock -split "`r?`n")) {
        $match = [regex]::Match($line, '^\|\s*`(?<path>[^`]+)`\s*\|')
        if (-not $match.Success) { continue }
        $path = $match.Groups["path"].Value
        if ($registeredExternalPaths -ccontains $path) {
            Add-Failure "Duplicate external asset path in register: $path"
            continue
        }
        [void]$registeredExternalPaths.Add($path)
    }
}

if ($registeredLocalPaths.Count -eq 0) { Add-Failure "No local asset family was parsed from the register." }
if ($registeredExternalPaths.Count -eq 0) { Add-Failure "No external asset path was parsed from the register." }

$expectedFinalLocalPaths = @(
    "Storytellers/SG1_Command",
    "Storytellers/SG1_Command_Tiny",
    "Things/Building/SG1_GoauldRitualBasin",
    "Things/Building/SG1_PrimtaIncubationBasin",
    "Things/Building/SG1_PrimtaPreservationBasin",
    "Things/Building/SG1_TokraRelaySabotageDevice",
    "Things/Building/SG1_TokraSecureCommunicator",
    "Things/Building/TokraDeliveryDropSpot/TokraDeliveryDropSpot",
    "Things/Item/SG1_TokraIntroductionArtifact",
    "Things/Item/SG1_TokraMissionIntelPacket",
    "Things/Item/SG1_TokraObservationDevice",
    "Things/Item/SG1_TokraOrganicDeadDrop",
    "Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet",
    "Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh",
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

$actualFinalLocalPaths = @($registeredLocalPaths | Where-Object { $registeredLocalStatuses[$_] -ceq "final" } | Sort-Object)
$finalPathDifferences = @(Compare-Object -ReferenceObject @($expectedFinalLocalPaths | Sort-Object) -DifferenceObject $actualFinalLocalPaths -CaseSensitive)
if ($finalPathDifferences.Count -gt 0) {
    Add-Failure ("Final local asset whitelist differs from the forty approved families: {0}" -f (($finalPathDifferences | ForEach-Object { "{0} {1}" -f $_.SideIndicator, $_.InputObject }) -join ", "))
}
else {
    Add-Pass "Final local asset whitelist matches the forty approved families."
}

if ($registerText -notmatch '(?m)^- `About/ModIcon\.png`: `final` public mod identity\.') {
    Add-Failure "The register does not explicitly classify About/ModIcon.png as final public mod identity."
}
else {
    Add-Pass "About/ModIcon.png is explicitly registered as final public mod identity."
}

$pngFiles = @(Get-ChildItem -LiteralPath $texturesRoot -Filter "*.png" -File -Recurse | Sort-Object FullName)
$actualFamilyCounts = @{}
$actualFamilyPaths = New-Object System.Collections.Generic.List[string]
$invalidPngFiles = New-Object System.Collections.Generic.List[string]
$pngSignature = @(137, 80, 78, 71, 13, 10, 26, 10)

foreach ($file in $pngFiles) {
    $relative = $file.FullName.Substring($texturesRoot.Length)
    $relative = $relative.TrimStart([char[]]@([IO.Path]::DirectorySeparatorChar, [IO.Path]::AltDirectorySeparatorChar))
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
        if (-not $validSignature) { [void]$invalidPngFiles.Add($relative.Replace("\", "/")) }
    }
    catch {
        [void]$invalidPngFiles.Add($relative.Replace("\", "/"))
    }
}

if ($invalidPngFiles.Count -gt 0) {
    Add-Failure ("Invalid or unreadable PNG file(s): {0}" -f ($invalidPngFiles -join ", "))
}
else {
    Add-Pass "All local PNG files have a valid PNG signature."
}

$familyDifferences = @(Compare-Object -ReferenceObject @($registeredLocalPaths | Sort-Object) -DifferenceObject @($actualFamilyPaths | Sort-Object) -CaseSensitive)
$missingRegisteredFamilies = @($familyDifferences | Where-Object { $_.SideIndicator -eq "<=" } | ForEach-Object { [string]$_.InputObject })
$unregisteredActualFamilies = @($familyDifferences | Where-Object { $_.SideIndicator -eq "=>" } | ForEach-Object { [string]$_.InputObject })

if ($missingRegisteredFamilies.Count -gt 0) { Add-Failure ("Registered family without matching PNG files: {0}" -f ($missingRegisteredFamilies -join ", ")) }
else { Add-Pass "Every registered local family resolves to PNG files." }
if ($unregisteredActualFamilies.Count -gt 0) { Add-Failure ("Unregistered local texture family: {0}" -f ($unregisteredActualFamilies -join ", ")) }
else { Add-Pass "Every local texture family is registered." }

$expectedPngCount = 0
foreach ($path in $registeredLocalPaths) {
    $expectedPngCount += [int]$registeredLocalCounts[$path]
    if (-not $actualFamilyCounts.ContainsKey($path)) { continue }
    if ([int]$actualFamilyCounts[$path] -ne [int]$registeredLocalCounts[$path]) {
        Add-Failure ("Family '{0}' contains {1} PNG file(s); register expects {2}." -f $path, $actualFamilyCounts[$path], $registeredLocalCounts[$path])
    }
}
if ($expectedPngCount -ne $pngFiles.Count) { Add-Failure ("Registered PNG total is {0}; repository contains {1}." -f $expectedPngCount, $pngFiles.Count) }
else { Add-Pass "Registered PNG counts match the repository." }

$localReferences = New-Object System.Collections.Generic.List[string]
$externalReferences = New-Object System.Collections.Generic.List[string]
$textureNodeNames = @("activateTexPath", "commandIconPath", "expandingIconTexture", "factionIconPath", "iconPath", "portraitLarge", "portraitTiny", "settlementTexturePath", "siteTexture", "texPath", "texture", "uiIconPath", "wornGraphicPath")

$xmlFiles = @(Get-ChildItem -LiteralPath $xmlRoot -Filter "*.xml" -File -Recurse | Sort-Object FullName)
foreach ($file in $xmlFiles) {
    try {
        [xml]$xml = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
        foreach ($node in @($xml.SelectNodes("//*"))) {
            if ($textureNodeNames -notcontains $node.Name) { continue }
            $path = ([string]$node.InnerText).Trim()
            if ([string]::IsNullOrWhiteSpace($path) -or -not $path.Contains("/")) { continue }
            $family = Normalize-TextureFamily $path
            if ($actualFamilyPaths -ccontains $family) {
                if ($localReferences -cnotcontains $family) { [void]$localReferences.Add($family) }
            }
            elseif ($externalReferences -cnotcontains $path) {
                [void]$externalReferences.Add($path)
            }
        }
    }
    catch {
        Add-Failure ("Could not parse XML while checking texture paths: {0}: {1}" -f $file.FullName, $_.Exception.Message)
    }
}

$csharpPatterns = @('ContentFinder<Texture2D>\.Get\("(?<path>[^"]+)"', 'commandIconPath\s*=\s*"(?<path>[^"]+)"')
$csharpFiles = @(Get-ChildItem -LiteralPath $sourceRoot -Filter "*.cs" -File -Recurse | Sort-Object FullName)
foreach ($file in $csharpFiles) {
    $text = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
    foreach ($pattern in $csharpPatterns) {
        foreach ($match in [regex]::Matches($text, $pattern)) {
            $path = $match.Groups["path"].Value
            $family = Normalize-TextureFamily $path
            if ($actualFamilyPaths -ccontains $family) {
                if ($localReferences -cnotcontains $family) { [void]$localReferences.Add($family) }
            }
            elseif ($externalReferences -cnotcontains $path) {
                [void]$externalReferences.Add($path)
            }
        }
    }
}

$referenceDifferences = @(Compare-Object -ReferenceObject @($actualFamilyPaths | Sort-Object) -DifferenceObject @($localReferences | Sort-Object) -CaseSensitive)
$unreferencedFamilies = @($referenceDifferences | Where-Object { $_.SideIndicator -eq "<=" } | ForEach-Object { [string]$_.InputObject })
$missingLocalReferences = @($referenceDifferences | Where-Object { $_.SideIndicator -eq "=>" } | ForEach-Object { [string]$_.InputObject })
if ($unreferencedFamilies.Count -gt 0) { Add-Failure ("Local family has no direct XML or C# reference: {0}" -f ($unreferencedFamilies -join ", ")) }
else { Add-Pass "Every local texture family has a direct XML or C# reference." }
if ($missingLocalReferences.Count -gt 0) { Add-Failure ("Source references a missing local family: {0}" -f ($missingLocalReferences -join ", ")) }
else { Add-Pass "No direct local texture reference is missing." }

$externalDifferences = @(Compare-Object -ReferenceObject @($registeredExternalPaths | Sort-Object) -DifferenceObject @($externalReferences | Sort-Object) -CaseSensitive)
$unusedExternalRegistrations = @($externalDifferences | Where-Object { $_.SideIndicator -eq "<=" } | ForEach-Object { [string]$_.InputObject })
$unregisteredExternalReferences = @($externalDifferences | Where-Object { $_.SideIndicator -eq "=>" } | ForEach-Object { [string]$_.InputObject })
if ($unusedExternalRegistrations.Count -gt 0) { Add-Failure ("Registered external path is no longer referenced: {0}" -f ($unusedExternalRegistrations -join ", ")) }
else { Add-Pass "Every registered external texture path remains referenced." }
if ($unregisteredExternalReferences.Count -gt 0) { Add-Failure ("Unregistered external texture path: {0}" -f ($unregisteredExternalReferences -join ", ")) }
else { Add-Pass "Every direct external texture path is registered." }

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
