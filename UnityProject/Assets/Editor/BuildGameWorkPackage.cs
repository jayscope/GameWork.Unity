using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GameWork.Unity.Assets.Editor
{
	public static class BuildGameWorkPackage
	{
		private static string PackageFile
		{
			get
			{
				var rootDir = Directory.GetParent(Application.dataPath).Parent.FullName;
				var packageFile = rootDir + "/Build/GameWork.unitypackage";
				return packageFile;
			}
		}

		private static readonly string[] FileNameBlacklist = new string[]
		{
			"$RANDOM_SEED$"
		};

		[MenuItem("Tools/Build GameWork Package")]
		public static void Build()
		{
			EditorUtility.DisplayProgressBar("Building GameWork Package", "Pre-processing...", 0);

			GenerateLinkXml.Generate();

			try
			{
				SaveCurrentCommitInfo.GetAndSave();
			}
			catch (Exception exception)
			{
				Debug.LogError(exception.Message);
			}

			var packageAssetPaths = new List<string>();
			var assetPaths = AssetDatabase.GetAllAssetPaths();

			// GameWork
			var progress = 0f;
			foreach (var assetPath in assetPaths)
			{
				EditorUtility.DisplayProgressBar("Building GameWork Package", assetPath, progress / assetPaths.Length);

				if (assetPath.StartsWith(Paths.RelativeGameWorkFolder)
					&& IsNotBlacklisted(assetPath))
				{
					packageAssetPaths.Add(assetPath);
					Debug.Log("Adding: " + assetPath);
				}

				progress++;
			}

			EditorUtility.DisplayProgressBar("Building GameWork Package", "Exporting...", 1);

			var packageDir = Path.GetDirectoryName(PackageFile);
			if (!Directory.Exists(packageDir))
			{
				Directory.CreateDirectory(packageDir);
			}

			AssetDatabase.ExportPackage(packageAssetPaths.ToArray(), PackageFile);

			Debug.Log("Exported package to: \"" + PackageFile + "\"");

			EditorUtility.ClearProgressBar();
		}

		private static bool IsNotBlacklisted(string assetPath)
		{
			var fileName = Path.GetFileName(assetPath);
			return !FileNameBlacklist.Contains(fileName);
		}
	}
}