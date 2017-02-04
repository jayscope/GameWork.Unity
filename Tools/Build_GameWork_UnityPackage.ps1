# Must be run from tools folde

$unityPath = "C:\Program Files\Unity\Editor\Unity.exe"

$logFile = ".\BuildPackage.log"
$projectDir = resolve-path "..\UnityProject\"

Write-Output "Logging to: $logFile"
Write-Output "Opening project: $projectDir"

& $unityPath -quit -batchMode -logFile $logFile -projectPath $projectDir -executeMethod GameWork.Unity.Assets.Editor.BuildGameWorkPackage.Build

Pause