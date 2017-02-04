# Must be executed from the Tools folder

$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\MSBuild.exe"

$nuget = ".\nuget.exe"

# 1. Build .\GameWork.Core\GameWork.Core.sln (GameWork.Unity is dependant on this project)

Push-Location "..\GameWork.Core\Tools"

& ".\BuildAll.ps1"

Pop-Location

# 2. Run .\Tools\CreateCoreReferrenceJunctions.bat (this will create the symlinks required for GameWork.Unity.sln to resolve any dependencies on GameWork.Core)

& ".\CreateCoreReferenceJunctions.bat"

# 3. Build .\GameWork.Unity.sln.

& $nuget restore "..\GameWork.Unity.sln"
& $msbuild "..\GameWork.Unity.sln"

# 4. Run .\Tools\CreateUnityReferenceJunctions.bat (This will create the symlinks necessary for the UnityProject to build the GameWork.unitypackage)

& ".\CreateUnityReferenceJunctions.bat"

# 5. Open the UnityProject in the Unity Editor and use the menu Tools/Build GameWork Package to build the GameWork.unitypackage.
# Check BuildPackage.log int .\Tools for any errors

& ".\Build_GameWork_UnityPackage.ps1"

# 6. Find the GameWork.unitypackage in .\Build
# This one is for you to do ;)