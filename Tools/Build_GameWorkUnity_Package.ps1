$unityPath = "C:\Program Files\Unity\Editor\Unity.exe"

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$parentDir = Split-Path -Parent $scriptDir
$logFile = Join-Path $scriptDir "BuildPackage.log"

Write-Output "Logging to: $logFile"
Write-Output "Opening project: $projectDir"

& $unityPath -quit -batchMode -logFile $logFile -projectPath $projectDir -executeMethod BuildGameWorkPackage.Build

Pause