using System;
using UnityEditor;

namespace GameWork.Unity.Editor.Build
{
    /// <summary>
    /// Menu entry for the GameWork Unity Build System.
    /// </summary>
    public static class BuilderMenu
    {
        [MenuItem("Tools/GameWork/Build/Active Target")]
        public static void BuildActiveTarget()
        {
            Builder.Build();
        }
    }
}