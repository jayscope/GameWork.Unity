$unityPath = "C:\Program Files\Unity\Editor\Unity.exe"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$parentDir = Split-Path -Parent $scriptDir
$logFile = Join-Path $scriptDir "BuildPackage.log"

Write-Output "Logging to: $logFile"
Write-Output "Opening project: $parentDir"

& $unityPath -quit -batchMode -logFile $logFile -projectPath $parentDir -executeMethod BuildGameWorkPackage.Build

Pause