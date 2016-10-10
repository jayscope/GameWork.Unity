using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace GameWork.Unity.Editor.Build.Tests
{
    [TestFixture]
    public static class BuildTests
    {
        [TestCase(BuildTarget.Android)]
        [TestCase(BuildTarget.iOS)]
        [TestCase(BuildTarget.WebGL)]
#if UNITY_EDITOR_WIN
        [TestCase(BuildTarget.StandaloneWindows)]
#else
    [TestCase(BuildTarget.StandaloneOSXIntel)]
#endif
        public static void Build(BuildTarget buildTarget)
        {
            var buildPath = Builder.Build(buildTarget);

            var buildExists = File.Exists(buildPath) || Directory.Exists(buildPath);

            Assert.True(buildExists, "The build does not exist: " + buildPath);
        }
    }
}