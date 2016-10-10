$gameWorkRoot = "GameWork.Unity"
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$splitIndex = $scriptDir.IndexOf($gameWorkRoot)
$parentDir = $scriptDir.Substring(0, $splitIndex + $gameWorkRoot.Length)

function DeleteDir([String] $dir)
{
    If(Test-Path $dir)
    {
        Write-Output "Removing: $dir"
        Remove-Item $dir -Recurse
    }
}

function CopyGameWorkDlls([String] $sourceDir, [String] $destDir)
{
    If(!(Test-Path $destDir))
    {
        Write-Output "Creating: $destDir"
        New-Item -ItemType directory $destDir | Out-Null
    }

    Get-ChildItem $sourceDir *.dll -Recurse | Where-Object {$_.BaseName.Contains("GameWork.") -and !$_.BaseName.Contains(".Tests")} | Copy-Item -Destination $destDir -Verbose
}

function CopyGameWork([String] $projectDir)
{
    # GameWork.Core
    $coreSourceDir = Join-Path $parentDir "GameWork.Core"
    $coreDestDir = "$projectDir\Assets\GameWork\Core"

    DeleteDir $coreDestDir
    CopyGameWorkDlls $coreSourceDir $coreDestDir

    # GameWork.Unity
    $unitySourceDir = $parentDir
    $unityDestDir = "$projectDir\Assets\GameWork\Unity"

    DeleteDir $unityDestDir

    Get-ChildItem -Directory $unitySourceDir | Where-Object {$_.BaseName.Contains("GameWork.Unity") -and !$_.BaseName.Contains(".Tests")} |
    Foreach-Object {
        Write-Output $_.FullName

        CopyGameWorkDlls $_.FullName $unityDestDir    
    }
}

$projectsDir = Join-Path $parentDir "UnityProjects"

 Get-ChildItem -Directory $projectsDir |
    Foreach-Object {

        $projectDir = $_.FullName

        Write-Output "Starting copy for: $projectDir" 
        
        CopyGameWork $projectDir
    }

#Pause