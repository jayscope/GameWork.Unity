GameWork.Unity

C# Framework for Unity, extending the .Net C# GameWork.Core framework.

Purpose:

Collection and tools and scripts to aid development in Unity.
Includes GameWork.Core: an engine agnostic .Net C# Framework for game development.


Dependencies:

GameWork.Core which is added as a submodule and has a tool provided to copy files into the GameWork Unity project.


Note:
When building GameWork.Unity.sln, post build scripts copy the .dlls to the projects in UnityProjects/ and then build a unitypackage of the current .dlls to Builds/

These scripts are written in PowerShell which requires permisisons to run post build.

To enable these permissions: 
1. Open PowerShell command prompt as Administrator
2. Run: Set-ExecutionPolicy RemoteSigned -Scope CurrentUser




