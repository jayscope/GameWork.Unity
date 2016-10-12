using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class BuildGameWorkPackage
{
    [MenuItem("Tools/Build GameWork Package")]
    public static void Build()
    {
        var rootDir = Directory.GetParent(Application.dataPath).Parent.FullName;
        var packageFile = rootDir + "/Build/GameWork.Unity.unitypackage";
        var gameWorkDir = "Assets/GameWork";

        var packageAssetPaths = new List<string>();

        foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
        {
            if (assetPath.StartsWith(gameWorkDir) 
                && File.Exists(assetPath)   // is file?
                && !Path.GetDirectoryName(assetPath).Contains(".Tests"))
            {
                
                packageAssetPaths.Add(assetPath);
            }
        }

        var packageDir = Path.GetDirectoryName(packageFile);
        if (!Directory.Exists(packageDir))
        {
            Directory.CreateDirectory(packageDir);
        }

        AssetDatabase.ExportPackage(packageAssetPaths.ToArray(), packageFile);

        Debug.Log("Exported package to: \"" + packageFile  + "\"");
    }
}