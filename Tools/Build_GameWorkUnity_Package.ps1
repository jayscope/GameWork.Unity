$unityPath = "C:\Program Files\Unity\Editor\Unity.exe"

$gameWorkRoot = "GameWork.Unity"
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$splitIndex = $scriptDir.IndexOf($gameWorkRoot)
$parentDir = $scriptDir.Substring(0, $splitIndex + $gameWorkRoot.Length)

$logFile = "$parentDir\Tools\BuildPackage.log"
$projectDir = "$parentDir\UnityProjects\PackageBuilder"

Write-Output "Logging to: $logFile"
Write-Output "Opening project: $projectDir"

& $unityPath -quit -batchMode -logFile $logFile -projectPath $projectDir -executeMethod BuildGameWorkPackage.Build

Pause