using System;
using UnityEditor;

namespace GameWork.Unity.Editor.Build
{
    public static class Builder
    {
        public static string BuildPath { private get; set; }
        
        public static void Build(string[] buildTargets)
        {
            foreach (var buildTargetString in buildTargets)
            {
                var buildTarget = (BuildTarget) Enum.Parse(typeof(BuildTarget), buildTargetString);
                Build(buildTarget);
            }
        }

        public static void Build()
        {
            Build(EditorUserBuildSettings.activeBuildTarget);
        }

        public static void Build(BuildTarget buildTarget)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTarget);

            SetDefaults();
            
            var buildEventCache = new BuildEventCache();
            buildEventCache.Execute(EventType.Pre, buildTarget);

            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, BuildPath, buildTarget, BuildOptions.None);

            buildEventCache.Execute(EventType.Post, buildTarget);
        }

        private static void SetDefaults()
        {
            BuildPath = "Builds/" + EditorUserBuildSettings.activeBuildTarget;
        }
    }
}