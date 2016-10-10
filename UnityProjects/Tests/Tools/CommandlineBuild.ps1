$unityPath = "C:\Program Files\Unity\Editor\Unity.exe"

$gameWorkRoot = "GameWork.Unity"
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$parentDir = Split-Path -Parent $scriptDir

$logFile = "$parentDir\Tools\CommandlineBuild.log"
$projectDir = "$parentDir"

Write-Output "Logging to: $logFile"
Write-Output "Opening project: $projectDir"

& $unityPath -quit -batchMode -logFile $logFile -projectPath $projectDir -executeMethod GameWork.Unity.Editor.Build.CommandlineBuilder.Build Android iOS WebGL StandaloneWindows

Pause