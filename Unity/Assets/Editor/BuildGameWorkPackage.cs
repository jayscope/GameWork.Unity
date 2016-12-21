using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildGameWorkPackage
{
    private const string ThirdPartyDirName = "ThirdParty";
    private const string GameWorkDir = "Assets/Plugins/GameWork";

    private static string PackagePath
    {
        get
        {
            var rootDir = Directory.GetParent(Application.dataPath).Parent.FullName;
            var packageFile = rootDir + "/Build/GameWork.unitypackage";
            return packageFile;
        }
    }

    [MenuItem("Tools/Build GameWork Package")]
    public static void Build()
    {
        var packageAssetPaths = new List<string>();

        // GameWork
        foreach (var assetPath in AssetDatabase.GetAllAssetPaths())
        {
            if (assetPath.StartsWith(GameWorkDir) 
                && File.Exists(assetPath)   // is file?
                && Path.GetExtension(assetPath) == ".dll"
                && (IsTypeForDirectory(assetPath) || IsThirdParty(assetPath))
                && !Path.GetFileName(assetPath).Contains(".Tests"))
            {
                packageAssetPaths.Add(assetPath);
                Debug.Log("Adding: " + assetPath);
            }
        }

        var packageDir = Path.GetDirectoryName(PackagePath);
        if (!Directory.Exists(packageDir))
        {
            Directory.CreateDirectory(packageDir);
        }

        AssetDatabase.ExportPackage(packageAssetPaths.ToArray(), PackagePath);

        Debug.Log("Exported package to: \"" + PackagePath + "\"");
    }

    private static bool IsTypeForDirectory(string assetPath)
    {
        var dirPath = Path.GetDirectoryName(assetPath);
        var dirName = Path.GetFileName(dirPath);

        var fileName = Path.GetFileName(assetPath);
        var fileNameSegments = fileName.Split('.');

        return fileNameSegments.Contains(dirName);
    }

    private static bool IsThirdParty(string assetPath)
    {
        var dirPath = Path.GetDirectoryName(assetPath);
        var dirName = Path.GetFileName(dirPath);
       
        return dirName == ThirdPartyDirName;
    }
}