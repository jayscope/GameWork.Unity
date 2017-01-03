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
        EditorUtility.DisplayProgressBar("Building GameWork Package", "Pre-processing...", 0);

        var packageAssetPaths = new List<string>();
        var assetPaths = AssetDatabase.GetAllAssetPaths();

        // GameWork
        var progress = 0f;
        foreach (var assetPath in assetPaths)
        {
            EditorUtility.DisplayProgressBar("Building GameWork Package", assetPath, progress / assetPaths.Length);

            if (assetPath.StartsWith(GameWorkDir) 
                && File.Exists(assetPath)   // is file?
                && IsValidFileType(assetPath)
                && (IsTypeForDirectory(assetPath) || IsThirdParty(assetPath) || MustBeKept(assetPath))
                && !Path.GetFileName(assetPath).Contains(".Tests"))
            {
                packageAssetPaths.Add(assetPath);
                Debug.Log("Adding: " + assetPath);
            }

            progress++;
        }

        EditorUtility.DisplayProgressBar("Building GameWork Package", "Exporting...", 1);

        var packageDir = Path.GetDirectoryName(PackagePath);
        if (!Directory.Exists(packageDir))
        {
            Directory.CreateDirectory(packageDir);
        }

        AssetDatabase.ExportPackage(packageAssetPaths.ToArray(), PackagePath);

        Debug.Log("Exported package to: \"" + PackagePath + "\"");

        EditorUtility.ClearProgressBar();
    }

    private static bool MustBeKept(string assetPath)
    {
        var extension = Path.GetExtension(assetPath);
        var validExtensions = new string[] {".md" };
        return validExtensions.Contains(extension);
    }

    private static bool IsValidFileType(string assetPath)
    {
        var extension = Path.GetExtension(assetPath);
        var validExtensions = new string[] {".dll", ".md"};
        return validExtensions.Contains(extension);
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