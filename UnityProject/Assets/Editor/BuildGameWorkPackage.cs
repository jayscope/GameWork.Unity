using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class BuildGameWorkPackage
{
    private const string GameWorkDir = "Assets/GameWork";

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

            if (assetPath.StartsWith(GameWorkDir))
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
}