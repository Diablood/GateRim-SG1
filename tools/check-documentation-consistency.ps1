[CmdletBinding()]
param(
    [string]$RepositoryRoot,
    [switch]$RequirePublicationReady,
    [string]$MinimumPublishedVersion = "0.3.67-dev",
    [string]$MinimumDurableTestingVersion = "0.3.93-dev"
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

function ConvertTo-VersionNumber {
    param([string]$VersionText)

    if ([string]::IsNullOrWhiteSpace($VersionText)) {
        return $null
    }

    $normalized = $VersionText.Trim()
    if ($normalized.StartsWith("v", [StringComparison]::OrdinalIgnoreCase)) {
        $normalized = $normalized.Substring(1)
    }
    $normalized = $normalized -replace '-dev$', ''

    try {
        return [version]$normalized
    }
    catch {
        Add-Failure "Invalid milestone version: $VersionText"
        return $null
    }
}

function Assert-Equal {
    param(
        [string]$Actual,
        [string]$Expected,
        [string]$Description
    )

    if ([string]::IsNullOrWhiteSpace($Actual) -or [string]::IsNullOrWhiteSpace($Expected)) {
        return
    }

    if ($Actual -cne $Expected) {
        Add-Failure "$Description is '$Actual'; expected '$Expected'."
    }
    else {
        Add-Pass "$Description matches '$Expected'."
    }
}

function Get-PublishedTags {
    $tags = @()
    try {
        $output = @(& git -C $RepositoryRoot tag --list "v*-dev" 2>$null)
        if ($LASTEXITCODE -ne 0) {
            Add-Failure "Could not enumerate published Git tags."
            return @()
        }

        foreach ($tag in $output) {
            if ($tag -match '^v\d+\.\d+\.\d+-dev$') {
                $tags += [string]$tag
            }
        }
    }
    catch {
        Add-Failure ("Could not enumerate published Git tags: {0}" -f $_.Exception.Message)
    }
    return $tags
}

function Get-MarkedBlock {
    param(
        [string]$Text,
        [string]$StartMarker,
        [string]$EndMarker,
        [string]$Description
    )

    if ($null -eq $Text) {
        return $null
    }

    $pattern = '(?s)' + [regex]::Escape($StartMarker) + '(?<value>.*?)' + [regex]::Escape($EndMarker)
    $match = [regex]::Match($Text, $pattern)
    if (-not $match.Success) {
        Add-Failure "Missing $Description markers."
        return $null
    }

    return $match.Groups["value"].Value
}

function Assert-NoForbiddenWording {
    param(
        [string]$RelativePath,
        [string]$Text,
        [object[]]$Patterns
    )

    if ($null -eq $Text) {
        return
    }

    foreach ($entry in $Patterns) {
        if ($Text -match $entry.Pattern) {
            Add-Failure "$RelativePath still contains publication-blocking wording: $($entry.Label)"
        }
    }
}

Write-Host "GateRim SG-1 documentation consistency check"
Write-Host "Repository: $RepositoryRoot"
Write-Host "Publication-ready mode: $RequirePublicationReady"
Write-Host ""

$aboutText = Read-RequiredText "About/About.xml"
$projectText = Read-RequiredText "Source/GateRimSG1/GateRimSG1.csproj"
$readmeText = Read-RequiredText "README.md"
$projectStateText = Read-RequiredText "docs/PROJECT_STATE.md"
$testingCurrentText = Read-RequiredText "docs/TESTING_CURRENT.md"
$changelogText = Read-RequiredText "docs/CHANGELOG.md"
$visualRegisterText = Read-RequiredText "docs/VISUAL_ASSET_REGISTER.md"
$wikiHomeText = Read-RequiredText "docs/wiki/Home.md"
$contentStatusText = Read-RequiredText "docs/wiki/Content-Status.md"
$testingDurableText = Read-RequiredText "docs/TESTING.md"
$roadmapText = Read-RequiredText "docs/ROADMAP.md"
$publicationProcedureText = Read-RequiredText "docs/MILESTONE_PUBLICATION.md"
$projectChecksText = Read-RequiredText "docs/PROJECT_CONSISTENCY_CHECKS.md"
$guardDocumentationText = Read-RequiredText "docs/DOCUMENTATION_CONSISTENCY_GUARDS.md"

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
    Add-Failure "Mod version '$modVersion' does not follow x.y.z-dev."
}
else {
    Add-Pass "Authoritative mod version is '$modVersion'."
}

$assemblyVersion = $null
if (-not [string]::IsNullOrWhiteSpace($modVersion) -and $modVersion -match '^(?<numeric>\d+\.\d+\.\d+)-dev$') {
    $assemblyVersion = "$($Matches['numeric']).0"
}

if ($null -ne $projectText -and $null -ne $assemblyVersion) {
    try {
        [xml]$projectXml = $projectText
        $propertyGroups = @($projectXml.Project.PropertyGroup)
        $projectVersion = [string](($propertyGroups | Where-Object { $_.Version } | Select-Object -First 1).Version)
        $projectAssemblyVersion = [string](($propertyGroups | Where-Object { $_.AssemblyVersion } | Select-Object -First 1).AssemblyVersion)
        $projectFileVersion = [string](($propertyGroups | Where-Object { $_.FileVersion } | Select-Object -First 1).FileVersion)
        Assert-Equal $projectVersion $assemblyVersion "Project Version"
        Assert-Equal $projectAssemblyVersion $assemblyVersion "AssemblyVersion"
        Assert-Equal $projectFileVersion $assemblyVersion "FileVersion"
    }
    catch {
        Add-Failure "Source/GateRimSG1/GateRimSG1.csproj is not valid XML: $($_.Exception.Message)"
    }
}

if ($null -ne $modVersion) {
    $readmeVersion = Get-CapturedValue $readmeText '(?m)^- Development version: `(?<value>[^`]+)`\s*$' "the README development version"
    $projectStateVersion = Get-CapturedValue $projectStateText '(?m)^Current milestone: `(?<value>\d+\.\d+\.\d+-dev)\b' "the project-state milestone version"
    $testingVersion = Get-CapturedValue $testingCurrentText '(?m)^Jalon\s*:\s*`(?<value>\d+\.\d+\.\d+-dev)\b' "the current-test milestone version"
    $changelogVersion = Get-CapturedValue $changelogText '(?m)^##\s+(?<value>\d+\.\d+\.\d+-dev)\b' "the newest changelog version"
    $wikiHomeVersion = Get-CapturedValue $wikiHomeText '(?m)^> Version du mod document.e\s*:\s*`(?<value>[^`]+)`\s*$' "the wiki-home documented version"
    $contentStatusVersion = Get-CapturedValue $contentStatusText '(?m)^> Derni.re r.vision\s*:\s*`(?<value>[^`]+)`\s*$' "the content-status revision"

    Assert-Equal $readmeVersion $modVersion "README development version"
    Assert-Equal $projectStateVersion $modVersion "Project-state milestone version"
    Assert-Equal $testingVersion $modVersion "Current-test milestone version"
    Assert-Equal $changelogVersion $modVersion "Newest changelog version"
    Assert-Equal $wikiHomeVersion $modVersion "Wiki-home documented version"
    Assert-Equal $contentStatusVersion $modVersion "Content-status revision"
}

if ($null -ne $assemblyVersion) {
    $testingAssembly = Get-CapturedValue $testingCurrentText '(?m)^Version de DLL (?:attendue|validée)\s*:\s*`(?<value>[^`]+)`\s*$' "the documented DLL version"
    Assert-Equal $testingAssembly $assemblyVersion "Documented DLL version"
}

$publishedTags = @()

if ($null -ne $changelogText -and $null -ne $modVersion) {
    $headingMatches = @([regex]::Matches($changelogText, '(?m)^##\s+(?<version>\d+\.\d+\.\d+-dev)\b'))
    $headingCounts = @{}
    $headingOrder = New-Object System.Collections.Generic.List[string]

    foreach ($match in $headingMatches) {
        $version = $match.Groups['version'].Value
        [void]$headingOrder.Add($version)
        if (-not $headingCounts.ContainsKey($version)) {
            $headingCounts[$version] = 0
        }
        $headingCounts[$version]++
    }

    $minimumVersionNumber = ConvertTo-VersionNumber $MinimumPublishedVersion
    $currentVersionNumber = ConvertTo-VersionNumber $modVersion

    foreach ($version in @($headingCounts.Keys | Sort-Object)) {
        $versionNumber = ConvertTo-VersionNumber $version
        if ($null -eq $versionNumber -or $null -eq $minimumVersionNumber -or $null -eq $currentVersionNumber) {
            continue
        }
        if ($versionNumber -lt $minimumVersionNumber -or $versionNumber -gt $currentVersionNumber) {
            continue
        }
        if ($headingCounts[$version] -ne 1) {
            Add-Failure "Changelog milestone '$version' appears $($headingCounts[$version]) times; expected exactly once."
        }
    }

    $publishedTags = @(Get-PublishedTags)
    $requiredPublishedVersions = New-Object System.Collections.Generic.List[string]

    foreach ($tag in $publishedTags) {
        $tagVersionNumber = ConvertTo-VersionNumber $tag
        if ($null -eq $tagVersionNumber -or $null -eq $minimumVersionNumber -or $null -eq $currentVersionNumber) {
            continue
        }
        if ($tagVersionNumber -ge $minimumVersionNumber -and $tagVersionNumber -le $currentVersionNumber) {
            [void]$requiredPublishedVersions.Add($tag.Substring(1))
        }
    }

    foreach ($publishedVersion in @($requiredPublishedVersions | Sort-Object -Unique)) {
        if (-not $headingCounts.ContainsKey($publishedVersion)) {
            Add-Failure "Changelog is missing published milestone '$publishedVersion'."
        }
    }

    if ($headingOrder.Count -gt 0 -and $headingOrder[0] -cne $modVersion) {
        Add-Failure "The first changelog milestone is '$($headingOrder[0])'; expected '$modVersion'."
    }

    $previousNumber = $null
    foreach ($version in $headingOrder) {
        $number = ConvertTo-VersionNumber $version
        if ($null -eq $number) {
            continue
        }
        if ($number -lt $minimumVersionNumber) {
            continue
        }
        if ($null -ne $previousNumber -and $number -gt $previousNumber) {
            Add-Failure "Changelog milestones are out of descending order near '$version'."
            break
        }
        $previousNumber = $number
    }

    if ($failures.Count -eq 0) {
        Add-Pass "Published changelog coverage is complete from $MinimumPublishedVersion through $modVersion."
    }
}


if ($null -ne $testingDurableText -and $null -ne $modVersion) {
    $testingHeadingMatches = @([regex]::Matches(
        $testingDurableText,
        '(?m)^##\s+.*?(?<version>\d+\.\d+\.\d+-dev)\b.*$'
    ))
    $testingHeadingCounts = @{}
    foreach ($match in $testingHeadingMatches) {
        $version = $match.Groups['version'].Value
        if (-not $testingHeadingCounts.ContainsKey($version)) {
            $testingHeadingCounts[$version] = 0
        }
        $testingHeadingCounts[$version]++
    }

    $durableFailuresBefore = $failures.Count
    if (-not $testingHeadingCounts.ContainsKey($modVersion)) {
        Add-Failure "Durable testing is missing the current milestone '$modVersion'."
    }
    elseif ($testingHeadingCounts[$modVersion] -ne 1) {
        Add-Failure "Durable testing milestone '$modVersion' appears $($testingHeadingCounts[$modVersion]) times; expected exactly once."
    }

    $minimumDurableVersionNumber = ConvertTo-VersionNumber $MinimumDurableTestingVersion
    $currentVersionNumber = ConvertTo-VersionNumber $modVersion
    foreach ($tag in $publishedTags) {
        $tagVersionNumber = ConvertTo-VersionNumber $tag
        if ($null -eq $tagVersionNumber -or $null -eq $minimumDurableVersionNumber -or $null -eq $currentVersionNumber) {
            continue
        }
        if ($tagVersionNumber -lt $minimumDurableVersionNumber -or $tagVersionNumber -gt $currentVersionNumber) {
            continue
        }

        $publishedVersion = $tag.Substring(1)
        if (-not $testingHeadingCounts.ContainsKey($publishedVersion)) {
            Add-Failure "Durable testing is missing published milestone '$publishedVersion'."
        }
        elseif ($testingHeadingCounts[$publishedVersion] -ne 1) {
            Add-Failure "Durable testing milestone '$publishedVersion' appears $($testingHeadingCounts[$publishedVersion]) times; expected exactly once."
        }
    }

    if ($failures.Count -eq $durableFailuresBefore) {
        Add-Pass "Durable testing coverage is complete from $MinimumDurableTestingVersion through $modVersion."
    }
}

if ($null -ne $roadmapText) {
    if ($roadmapText -notmatch '(?m)^## Prochain jalon décidé\s*$') {
        Add-Failure "docs/ROADMAP.md does not define a 'Prochain jalon décidé' section."
    }

    $historicalRoadmapHeadings = @([regex]::Matches(
        $roadmapText,
        '(?mi)^##\s+(?:Dernier jalon|Jalon .*publié|Jalon précédent|Historique publié).*$'
    ))
    if ($historicalRoadmapHeadings.Count -gt 0) {
        Add-Failure "docs/ROADMAP.md still contains $($historicalRoadmapHeadings.Count) published-history heading(s); published history belongs in docs/CHANGELOG.md."
    }

    $versionedRoadmapHeadings = @([regex]::Matches(
        $roadmapText,
        '(?m)^##\s+.*\d+\.\d+\.\d+-dev.*$'
    ))
    if ($versionedRoadmapHeadings.Count -gt 0) {
        Add-Failure "docs/ROADMAP.md contains versioned milestone headings; versions are assigned when a new branch starts."
    }
}

if ($null -ne $publicationProcedureText) {
    foreach ($requiredCommand in @(
        '.\tools\test-documentation-consistency-guards.cmd',
        '.\tools\check-project-consistency.cmd -RequirePublicationReady'
    )) {
        if ($publicationProcedureText -notlike "*$requiredCommand*") {
            Add-Failure "docs/MILESTONE_PUBLICATION.md is missing required command: $requiredCommand"
        }
    }

    if ($publicationProcedureText -notmatch '(?i)aucun script PowerShell temporaire.*racine') {
        Add-Failure "docs/MILESTONE_PUBLICATION.md does not forbid temporary PowerShell scripts at repository root."
    }
}

if ($null -ne $projectChecksText -and $null -ne $modVersion) {
    $projectChecksVersion = Get-CapturedValue $projectChecksText '(?m)^Version:\s*`(?<value>[^`]+)`\s*$' "the project-consistency documentation version"
    Assert-Equal $projectChecksVersion $modVersion "Project-consistency documentation version"

    if ($projectChecksText -notlike '*tools/check-documentation-consistency.ps1*') {
        Add-Failure "docs/PROJECT_CONSISTENCY_CHECKS.md does not document the documentation-consistency subprocess."
    }
}

if ($null -ne $guardDocumentationText -and $null -ne $modVersion) {
    $guardDocumentationVersion = Get-CapturedValue $guardDocumentationText '(?m)^Version:\s*`(?<value>[^`]+)`\s*$' "the documentation-guard document version"
    Assert-Equal $guardDocumentationVersion $modVersion "Documentation-guard document version"
}

if ($null -ne $visualRegisterText -and $null -ne $modVersion -and $null -ne $assemblyVersion) {
    $registerVersion = Get-CapturedValue $visualRegisterText '(?m)^- Version:\s*`(?<value>[^`]+)`\s*$' "the visual-register version"
    $registerAssembly = Get-CapturedValue $visualRegisterText '(?m)^- Target assembly:\s*`(?<value>[^`]+)`\s*$' "the visual-register target assembly"
    Assert-Equal $registerVersion $modVersion "Visual-register version"
    Assert-Equal $registerAssembly $assemblyVersion "Visual-register target assembly"

    $branchMatch = [regex]::Match($visualRegisterText, '(?m)^- Branch:\s*`(?<value>[^`]+)`\s*$')
    if ($branchMatch.Success) {
        Add-Failure "The visual register contains volatile branch metadata; branch and local revision belong in docs/PROJECT_STATE.md."
    }
    else {
        Add-Pass "Visual register contains no volatile branch metadata."
    }

    $statusMatch = [regex]::Match($visualRegisterText, '(?m)^- Status:\s*(?<value>.+)$')
    if ($statusMatch.Success) {
        $status = $statusMatch.Groups['value'].Value
        if ($status -match '\d+\.\d+\.\d+-dev' -and $status -notlike "*$modVersion*") {
            Add-Failure "Visual-register status mentions an obsolete milestone: $status"
        }
    }

    $localBlock = Get-MarkedBlock $visualRegisterText '<!-- LOCAL_ASSET_TABLE_START -->' '<!-- LOCAL_ASSET_TABLE_END -->' "visual-register local asset table"
    if ($null -ne $localBlock) {
        $rowMatches = @([regex]::Matches($localBlock, '(?m)^\|\s*`(?<path>[^`]+)`\s*\|\s*(?<count>\d+)\s*\|[^|]*\|[^|]*\|\s*`(?<status>[^`]+)`\s*\|\s*`(?<priority>[^`]+)`\s*\|'))
        if ($rowMatches.Count -eq 0) {
            Add-Failure "No visual asset rows were parsed from the registered local table."
        }
        else {
            $statusCounts = @{}
            $priorityCounts = @{}
            $physicalFileCount = 0
            foreach ($row in $rowMatches) {
                $status = $row.Groups['status'].Value
                $priority = $row.Groups['priority'].Value
                $physicalFileCount += [int]$row.Groups['count'].Value
                if (-not $statusCounts.ContainsKey($status)) { $statusCounts[$status] = 0 }
                if (-not $priorityCounts.ContainsKey($priority)) { $priorityCounts[$priority] = 0 }
                $statusCounts[$status]++
                $priorityCounts[$priority]++
            }

            $summaryPngCount = Get-CapturedValue $visualRegisterText '(?m)^- Local PNG files:\s*`(?<value>\d+)`\.' "the visual-register PNG summary"
            $summaryFamilyCount = Get-CapturedValue $visualRegisterText '(?m)^- Local texture families:\s*`(?<value>\d+)`\.' "the visual-register family summary"
            $summaryFinalCount = Get-CapturedValue $visualRegisterText '(?m)^- Accepted final local families:\s*`(?<value>\d+)`' "the visual-register final-family summary"

            if ($null -ne $summaryPngCount -and [int]$summaryPngCount -ne $physicalFileCount) {
                Add-Failure "Visual-register PNG summary is $summaryPngCount; table rows total $physicalFileCount."
            }
            else {
                Add-Pass "Visual-register PNG summary matches the table."
            }

            if ($null -ne $summaryFamilyCount -and [int]$summaryFamilyCount -ne $rowMatches.Count) {
                Add-Failure "Visual-register family summary is $summaryFamilyCount; table contains $($rowMatches.Count) families."
            }
            else {
                Add-Pass "Visual-register family summary matches the table."
            }

            $actualFinalCount = 0
            if ($statusCounts.ContainsKey('final')) { $actualFinalCount = $statusCounts['final'] }
            if ($null -ne $summaryFinalCount -and [int]$summaryFinalCount -ne $actualFinalCount) {
                Add-Failure "Visual-register final-family summary is $summaryFinalCount; table contains $actualFinalCount final families."
            }
            else {
                Add-Pass "Visual-register final-family summary matches the table."
            }

            $finalSummaryMatch = [regex]::Match(
                $visualRegisterText,
                '(?s)^- Accepted final local families:\s*`\d+`\s*\((?<value>.*?)\)\.\s*$',
                [System.Text.RegularExpressions.RegexOptions]::Multiline
            )
            if ($finalSummaryMatch.Success) {
                $categoryNumbers = @([regex]::Matches($finalSummaryMatch.Groups['value'].Value, '`(?<value>\d+)`') | ForEach-Object { [int]$_.Groups['value'].Value })
                $categorySum = 0
                foreach ($number in $categoryNumbers) { $categorySum += $number }
                if ($categoryNumbers.Count -eq 0) {
                    Add-Failure "Visual-register final-family category breakdown contains no numeric categories."
                }
                elseif ($categorySum -ne $actualFinalCount) {
                    Add-Failure "Visual-register final-family category breakdown totals $categorySum; table contains $actualFinalCount final families."
                }
                else {
                    Add-Pass "Visual-register final-family category breakdown matches the table."
                }
            }
            else {
                Add-Failure "Could not parse the visual-register final-family category breakdown."
            }

            $summaryStatusMap = @{
                'temporary-original' = 'Temporary original families'
                'temporary-recolor' = 'Temporary recolor families'
                'temporary-reuse' = 'Temporary reuse families'
                'placeholder-personal-icon' = 'Project-icon placeholder families'
            }
            foreach ($statusName in $summaryStatusMap.Keys) {
                $label = $summaryStatusMap[$statusName]
                $pattern = '(?m)^- ' + [regex]::Escape($label) + ':\s*`(?<value>\d+)`\.'
                $summaryCount = Get-CapturedValue $visualRegisterText $pattern "the visual-register '$label' summary"
                $actualCount = 0
                if ($statusCounts.ContainsKey($statusName)) { $actualCount = $statusCounts[$statusName] }
                if ($null -ne $summaryCount -and [int]$summaryCount -ne $actualCount) {
                    Add-Failure "$label is $summaryCount; table contains $actualCount."
                }
            }

            $priorityMatch = [regex]::Match($visualRegisterText, '(?m)^- Priorities:\s*`(?<p0>\d+)` P0,\s*`(?<p1>\d+)` P1,\s*`(?<p2>\d+)` P2,\s*`(?<done>\d+)` done\.')
            if (-not $priorityMatch.Success) {
                Add-Failure "Could not parse the visual-register priority summary."
            }
            else {
                foreach ($priority in @('P0', 'P1', 'P2', 'done')) {
                    $groupName = $priority.ToLowerInvariant()
                    $summaryCount = [int]$priorityMatch.Groups[$groupName].Value
                    $actualCount = 0
                    if ($priorityCounts.ContainsKey($priority)) { $actualCount = $priorityCounts[$priority] }
                    if ($summaryCount -ne $actualCount) {
                        Add-Failure "Visual-register priority '$priority' is $summaryCount; table contains $actualCount."
                    }
                }
            }
        }

        $beforeTable = $visualRegisterText.Substring(0, $visualRegisterText.IndexOf('<!-- LOCAL_ASSET_TABLE_START -->'))
        $strayRows = @([regex]::Matches($beforeTable, '(?m)^\|\s*`[^`]+`\s*\|\s*\d+\s*\|'))
        if ($strayRows.Count -gt 0) {
            Add-Failure "Visual register contains $($strayRows.Count) asset-table row(s) outside the authoritative marked table."
        }
        else {
            Add-Pass "No visual asset rows exist outside the authoritative table."
        }
    }

    $obsoleteVisualStatements = @(
        @{ Pattern = '(?i)The Prim''ta larva and free Goa''uld symbiote still use the same image'; Label = "obsolete shared Prim'ta/free-Goa'uld image finding" }
    )
    foreach ($entry in $obsoleteVisualStatements) {
        if ($visualRegisterText -match $entry.Pattern) {
            Add-Failure "Visual register contains an obsolete finding: $($entry.Label)."
        }
    }
}

if ($RequirePublicationReady) {
    $forbiddenPatterns = @(
        @{ Pattern = '(?i)\bNot published\b'; Label = 'Not published' },
        @{ Pattern = '(?i)\bBefore publication\b'; Label = 'Before publication' },
        @{ Pattern = '(?i)\bprepares publication\b'; Label = 'prepares publication' },
        @{ Pattern = '(?i)\bprépare(?:nt)? la publication\b'; Label = 'prépare la publication' },
        @{ Pattern = '(?i)\bRévision à tester\b'; Label = 'Révision à tester' },
        @{ Pattern = '(?i)\bVersion de DLL attendue\b'; Label = 'Version de DLL attendue' },
        @{ Pattern = '(?i)\bfocused validation is in progress\b'; Label = 'focused validation is in progress' },
        @{ Pattern = '(?i)\bvalidation en cours\b'; Label = 'validation en cours' },
        @{ Pattern = '(?i)\bremain required\b'; Label = 'remain required' },
        @{ Pattern = '(?i)\breste requis\b'; Label = 'reste requis' }
    )

    Assert-NoForbiddenWording "docs/PROJECT_STATE.md" $projectStateText $forbiddenPatterns
    Assert-NoForbiddenWording "docs/TESTING_CURRENT.md" $testingCurrentText $forbiddenPatterns
    Assert-NoForbiddenWording "docs/VISUAL_ASSET_REGISTER.md" $visualRegisterText $forbiddenPatterns

    if ($null -ne $testingCurrentText -and $testingCurrentText -notmatch '(?m)^Version de DLL validée\s*:') {
        Add-Failure "docs/TESTING_CURRENT.md must use 'Version de DLL validée' in publication-ready mode."
    }

    if ($null -ne $projectStateText -and $null -ne $modVersion) {
        $tagPattern = '(?m)^- Final annotated tag:\s*`v' + [regex]::Escape($modVersion) + '`\.\s*$'
        if ($projectStateText -notmatch $tagPattern) {
            Add-Failure "docs/PROJECT_STATE.md does not record final annotated tag v$modVersion."
        }
        if ($projectStateText -notmatch '(?i)\b(validated|validé|validée|published|publié|publiée)\b') {
            Add-Failure "docs/PROJECT_STATE.md does not clearly state a validated or published final state."
        }
    }

    if ($failures.Count -eq 0) {
        Add-Pass "Publication-ready wording and final identifiers are coherent."
    }
}

Write-Host ""
if ($failures.Count -gt 0) {
    Write-Host "Documentation consistency check failed with $($failures.Count) issue(s)." -ForegroundColor Red
    exit 1
}

Write-Host "Documentation consistency check passed." -ForegroundColor Green
Write-Host "Version: $modVersion"
Write-Host "Assembly: $assemblyVersion"
exit 0
