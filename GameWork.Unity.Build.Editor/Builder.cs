using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameWork.Unity.Build.Editor
{
    /// <summary>
    /// Contains the build logic.
    /// </summary>
	public static class Builder
	{
		public static string BuildPath { private get; set; }

		public static string TargetBuildExtension
		{
			get
			{
				switch (EditorUserBuildSettings.activeBuildTarget)
				{
					case BuildTarget.StandaloneWindows64:
					case BuildTarget.StandaloneWindows:
						return ".exe";

					case BuildTarget.WebGL:
					case BuildTarget.iOS:
						return string.Empty;

					case BuildTarget.Android:
						return ".apk";

				    default:
				        throw new NotImplementedException();
                }
			}
		}

		public static string[] Build(BuildTarget[] buildTargets)
		{
			var buildPaths = new string[buildTargets.Length];

			for (var i = 0; i < buildTargets.Length; i++)
			{
				buildPaths[i] = Build(buildTargets[i]);
			}

			return buildPaths;
		}

		public static string Build()
		{
			return Build(EditorUserBuildSettings.activeBuildTarget);
		}

		public static string Build(BuildTarget buildTarget)
		{
			SetPlatform(buildTarget);
			SetDefaults();

			var buildEventCache = new BuildEventCache();
			buildEventCache.Execute(EventType.Pre, buildTarget);

			var buildPath = BuildPath;
			CheckBuildPath(buildPath);
			BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, buildPath, buildTarget, BuildOptions.None);

			buildEventCache.Execute(EventType.Post, buildTarget);

			Debug.Log($"Build to: {buildPath}");

			return buildPath;
		}

		private static void SetPlatform(BuildTarget buildTarget)
		{
			if (EditorUserBuildSettings.activeBuildTarget != buildTarget)
			{
				EditorUserBuildSettings.SwitchActiveBuildTarget(BuildPipeline.GetBuildTargetGroup(buildTarget), buildTarget);
			}
		}

		private static void SetDefaults()
		{
			BuildPath = $"{Directory.GetParent(Application.dataPath).FullName}/Build/" +
						$"{PlayerSettings.productName}_{EditorUserBuildSettings.activeBuildTarget}{TargetBuildExtension}";
		}

		private static void CheckBuildPath(string buildPath)
		{
			if (Path.HasExtension(buildPath))
			{
				if (File.Exists(buildPath))
				{
					File.Delete(buildPath);
				}
				else
				{
					var buildDir = Path.GetDirectoryName(buildPath);
					CheckBuildDir(buildDir);
				}
			}
			else
			{
				if (Directory.Exists(buildPath))
				{
					Directory.Delete(buildPath, true);
				}
				else
				{
					var buildDir = Directory.GetParent(buildPath).FullName;
					CheckBuildDir(buildDir);
				}
			}
		}

		private static void CheckBuildDir(string buildDir)
		{
			if (!Directory.Exists(buildDir))
			{
				Directory.CreateDirectory(buildDir);
			}
		}
	}
}