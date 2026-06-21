[CmdletBinding()]
param(
    [string]$RepositoryRoot,
    [string]$ExpectedVersion,
    [int]$ExpectedBackstoryCount = -1
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

function Get-RepositoryPath {
    param([string]$RelativePath)

    return Join-Path $RepositoryRoot $RelativePath
}

function Read-RequiredText {
    param([string]$RelativePath)

    $path = Get-RepositoryPath $RelativePath
    if (-not (Test-Path -LiteralPath $path -PathType Leaf)) {
        Add-Failure "Missing required file: $RelativePath"
        return $null
    }

    try {
        return Get-Content -LiteralPath $path -Raw -Encoding UTF8
    }
    catch {
        Add-Failure ("Could not read {0}: {1}" -f $RelativePath, $_.Exception.Message)
        return $null
    }
}

function Get-CapturedValue {
    param(
        [string]$Text,
        [string]$Pattern,
        [string]$Description,
        [System.Text.RegularExpressions.RegexOptions]$Options = [System.Text.RegularExpressions.RegexOptions]::None
    )

    if ($null -eq $Text) {
        return $null
    }

    $match = [regex]::Match($Text, $Pattern, $Options)
    if (-not $match.Success) {
        Add-Failure "Could not find $Description."
        return $null
    }

    return $match.Groups["value"].Value
}

function Assert-Equal {
    param(
        [string]$Actual,
        [string]$Expected,
        [string]$Description
    )

    if ([string]::IsNullOrWhiteSpace($Actual)) {
        return
    }

    if ($Actual -ne $Expected) {
        Add-Failure "$Description is '$Actual'; expected '$Expected'."
        return
    }

    Add-Pass "$Description matches '$Expected'."
}

function Assert-Count {
    param(
        [int]$Actual,
        [int]$Expected,
        [string]$Description
    )

    if ($Actual -ne $Expected) {
        Add-Failure "$Description is $Actual; expected $Expected."
        return
    }

    Add-Pass "$Description matches $Expected."
}

Write-Host "GateRim SG-1 project consistency check"
Write-Host "Repository: $RepositoryRoot"
Write-Host ""

$markdownFiles = @()
$readmePath = Get-RepositoryPath "README.md"
if (Test-Path -LiteralPath $readmePath -PathType Leaf) {
    $markdownFiles += Get-Item -LiteralPath $readmePath
}

$docsPath = Get-RepositoryPath "docs"
if (Test-Path -LiteralPath $docsPath -PathType Container) {
    $markdownFiles += Get-ChildItem -LiteralPath $docsPath -Filter "*.md" -File -Recurse
}

$markdownTabLocations = @()
foreach ($file in $markdownFiles) {
    $lineNumber = 0
    foreach ($line in Get-Content -LiteralPath $file.FullName -Encoding UTF8) {
        $lineNumber++
        if ($line.Contains("`t")) {
            $markdownTabLocations += ("{0}:{1}" -f $file.FullName, $lineNumber)
        }
    }
}

if ($markdownTabLocations.Count -gt 0) {
    Add-Failure ("Literal tab character(s) found in Markdown files: {0}" -f ($markdownTabLocations -join ", "))
}
else {
    Add-Pass "Markdown files contain no literal tab characters."
}



$aboutPath = Get-RepositoryPath "About/About.xml"
$aboutText = Read-RequiredText "About/About.xml"
$modVersion = $null

if ($null -ne $aboutText) {
    try {
        [xml]$aboutXml = $aboutText
        $modVersion = [string]$aboutXml.ModMetaData.modVersion
    }
    catch {
        Add-Failure "About/About.xml is not valid XML: $($_.Exception.Message)"
    }
}

if ([string]::IsNullOrWhiteSpace($modVersion)) {
    Add-Failure "About/About.xml does not define ModMetaData/modVersion."
}
elseif ($modVersion -notmatch '^\d+\.\d+\.\d+-dev$') {
    Add-Failure "Mod version '$modVersion' does not follow the expected x.y.z-dev format."
}
else {
    Add-Pass "About mod version is '$modVersion'."
}

if (-not [string]::IsNullOrWhiteSpace($ExpectedVersion) -and -not [string]::IsNullOrWhiteSpace($modVersion)) {
    Assert-Equal $modVersion $ExpectedVersion "Requested milestone version"
}

$assemblyVersion = $null
if (-not [string]::IsNullOrWhiteSpace($modVersion) -and $modVersion -match '^(?<numeric>\d+\.\d+\.\d+)-dev$') {
    $assemblyVersion = "$($Matches["numeric"]).0"
}

$projectText = Read-RequiredText "Source/GateRimSG1/GateRimSG1.csproj"
if ($null -ne $projectText -and $null -ne $assemblyVersion) {
    try {
        [xml]$projectXml = $projectText
        $propertyGroups = @($projectXml.Project.PropertyGroup)
        $versionValues = @{
            "Project Version" = [string](($propertyGroups | Where-Object { $_.Version } | Select-Object -First 1).Version)
            "AssemblyVersion" = [string](($propertyGroups | Where-Object { $_.AssemblyVersion } | Select-Object -First 1).AssemblyVersion)
            "FileVersion" = [string](($propertyGroups | Where-Object { $_.FileVersion } | Select-Object -First 1).FileVersion)
        }

        foreach ($entry in $versionValues.GetEnumerator()) {
            Assert-Equal $entry.Value $assemblyVersion $entry.Key
        }
    }
    catch {
        Add-Failure "Source/GateRimSG1/GateRimSG1.csproj is not valid XML: $($_.Exception.Message)"
    }
}

$readmeText = Read-RequiredText "README.md"
$homeText = Read-RequiredText "docs/wiki/Home.md"
$contentStatusText = Read-RequiredText "docs/wiki/Content-Status.md"
$projectStateText = Read-RequiredText "docs/PROJECT_STATE.md"
$testingCurrentText = Read-RequiredText "docs/TESTING_CURRENT.md"
$changelogText = Read-RequiredText "docs/CHANGELOG.md"

if ($null -ne $modVersion) {
    $readmeVersion = Get-CapturedValue $readmeText '(?m)^- Development version: `(?<value>[^`]+)`\s*$' "the README development version"
    $homeVersion = Get-CapturedValue $homeText '(?m)^> Version du mod document.e : `(?<value>[^`]+)`\s*$' "the wiki-home documented version"
    $contentStatusVersion = Get-CapturedValue $contentStatusText '(?m)^> Derni.re r.vision : `(?<value>[^`]+)`\s*$' "the content-status revision"
    $projectStateVersion = Get-CapturedValue $projectStateText '(?m)^Current milestone: `(?<value>\d+\.\d+\.\d+-dev)\b' "the project-state milestone version"
    $testingVersion = Get-CapturedValue $testingCurrentText '(?m)^Jalon\s*:\s*`(?<value>\d+\.\d+\.\d+-dev)\b' "the current-test milestone version"
    $changelogVersion = Get-CapturedValue $changelogText '(?m)^## (?<value>\d+\.\d+\.\d+-dev)\b' "the newest changelog version"

    Assert-Equal $readmeVersion $modVersion "README development version"
    Assert-Equal $homeVersion $modVersion "Wiki home documented version"
    Assert-Equal $contentStatusVersion $modVersion "Content-status revision"
    Assert-Equal $projectStateVersion $modVersion "Project-state milestone version"
    Assert-Equal $testingVersion $modVersion "Current-test milestone version"
    Assert-Equal $changelogVersion $modVersion "Newest changelog version"
}

if ($null -ne $assemblyVersion) {
    $testingAssemblyVersion = Get-CapturedValue $testingCurrentText '(?m)^Version de DLL (?:attendue|validée)\s*:\s*`(?<value>[^`]+)`\s*$' "the documented DLL version"
    Assert-Equal $testingAssemblyVersion $assemblyVersion "Documented DLL version"
}

$backstoryDirectory = Get-RepositoryPath "1.6/Defs/BackstoryDefs"
$backstoryCount = 0
$seenDefNames = @{}

if (-not (Test-Path -LiteralPath $backstoryDirectory -PathType Container)) {
    Add-Failure "Missing backstory directory: 1.6/Defs/BackstoryDefs"
}
else {
    $backstoryFiles = @(Get-ChildItem -LiteralPath $backstoryDirectory -Filter "*.xml" -File -Recurse | Sort-Object FullName)
    if ($backstoryFiles.Count -eq 0) {
        Add-Failure "No BackstoryDef XML file was found."
    }

    foreach ($file in $backstoryFiles) {
        try {
            [xml]$xml = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
            $nodes = @($xml.SelectNodes('/Defs/BackstoryDef'))
            $backstoryCount += $nodes.Count

            foreach ($node in $nodes) {
                $defNameNode = $node.SelectSingleNode('defName')
                if ($null -eq $defNameNode -or [string]::IsNullOrWhiteSpace($defNameNode.InnerText)) {
                    Add-Failure "BackstoryDef without defName in $($file.FullName)."
                    continue
                }

                $defName = $defNameNode.InnerText.Trim()
                if ($seenDefNames.ContainsKey($defName)) {
                    Add-Failure "Duplicate BackstoryDef '$defName' in '$($file.Name)' and '$($seenDefNames[$defName])'."
                }
                else {
                    $seenDefNames[$defName] = $file.Name
                }
            }
        }
        catch {
            Add-Failure "Could not parse backstory file '$($file.FullName)': $($_.Exception.Message)"
        }
    }
}

if ($backstoryCount -gt 0) {
    Add-Pass "Loaded $backstoryCount BackstoryDef entries with $($seenDefNames.Count) unique defNames."
}

if ($ExpectedBackstoryCount -ge 0) {
    Assert-Count $backstoryCount $ExpectedBackstoryCount "Requested backstory count"
}

$culturalBackstoriesText = Read-RequiredText "docs/CULTURAL_BACKSTORIES.md"
$wikiCatalogueText = Read-RequiredText "docs/wiki/Cultural-Backstories.md"

$readmeCount = Get-CapturedValue $readmeText 'persistent cultural names and\s+(?<value>\d+)\s+cultural\s+backstories' "the README backstory count" ([System.Text.RegularExpressions.RegexOptions]::IgnoreCase -bor [System.Text.RegularExpressions.RegexOptions]::Singleline)
$homeCount = Get-CapturedValue $homeText '\[Histoires culturelles\]\(Cultural-Backstories\)\s*:\s*(?<value>\d+)\s+enfances' "the wiki-home backstory count" ([System.Text.RegularExpressions.RegexOptions]::IgnoreCase -bor [System.Text.RegularExpressions.RegexOptions]::Singleline)
$contentStatusCount = Get-CapturedValue $contentStatusText '\|\s*Histoires culturelles\s*\|\s*Catalogue natif de\s+(?<value>\d+)\s+enfances' "the content-status backstory count" ([System.Text.RegularExpressions.RegexOptions]::IgnoreCase -bor [System.Text.RegularExpressions.RegexOptions]::Singleline)
$technicalCount = Get-CapturedValue $culturalBackstoriesText '(?m)^Status:\s*`(?<value>\d+)`\s+cultural backstories' "the technical backstory count"
$wikiDeclaredCount = Get-CapturedValue $wikiCatalogueText '(?m)^Le catalogue compte d.sormais\s*`(?<value>\d+)`\s+histoires\.' "the wiki-catalogue declared count"

foreach ($item in @(
    @{ Value = $readmeCount; Description = "README backstory count" },
    @{ Value = $homeCount; Description = "Wiki home backstory count" },
    @{ Value = $contentStatusCount; Description = "Content-status backstory count" },
    @{ Value = $technicalCount; Description = "Technical backstory count" },
    @{ Value = $wikiDeclaredCount; Description = "Wiki catalogue declared count" }
)) {
    if ($null -ne $item.Value -and $item.Value -match '^\d+$') {
        Assert-Count ([int]$item.Value) $backstoryCount $item.Description
    }
}

if ($null -ne $wikiCatalogueText) {
    $catalogueMatch = [regex]::Match(
        $wikiCatalogueText,
        '(?s)<!-- BACKSTORY_TABLES_START -->(?<value>.*?)<!-- BACKSTORY_TABLES_END -->'
    )

    if (-not $catalogueMatch.Success) {
        Add-Failure "Could not find the wiki backstory table markers."
    }
    else {
        $catalogueRows = [regex]::Matches(
            $catalogueMatch.Groups["value"].Value,
            '(?m)^\|\s*(?:Enfance|.ge adulte)\s*\|'
        ).Count
        Assert-Count $catalogueRows $backstoryCount "Wiki catalogue table-row count"
    }
}

Write-Host ""
if ($failures.Count -gt 0) {
    Write-Host "Project consistency check failed with $($failures.Count) issue(s)." -ForegroundColor Red
    exit 1
}

Write-Host "Project consistency check passed." -ForegroundColor Green
Write-Host "Version: $modVersion"
Write-Host "Assembly: $assemblyVersion"
Write-Host "Backstories: $backstoryCount"
exit 0
