# Must be run from ./Tools directory

$sourcePath = "..\GameWork.Core"
$destPath = "..\UnityProject\Assets\GameWork\Core"

If(Test-Path $destPath)
{
    Write-Output "Removing: $destPath"
    Remove-Item $destPath -Recurse
}

New-Item -ItemType directory $destPath | Out-Null

$sourcePath = Resolve-Path $sourcePath
$destPath = Resolve-Path $destPath

Get-ChildItem $sourcePath -Recurse -Include '*.cs' -Exclude 'AssemblyInfo.cs' | Where-Object {!$_.FullName.Contains(".Tests")} | Foreach-Object `
{
    $destDir = Split-Path ($_.FullName -Replace [regex]::Escape($sourcePath), $destPath)
    $destDir = $destDir.Replace("GameWork.Core.", "")
    
    if (!(Test-Path $destDir))
    {
        Write-Output "Creating: $destDir"
        New-Item -ItemType directory $destDir | Out-Null
    }

    Write-Output "Copying: $_"
    Copy-Item $_ -Destination $destDir
}

Pause