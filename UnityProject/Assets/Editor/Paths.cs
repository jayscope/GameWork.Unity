using UnityEngine;

namespace GameWork.Unity.Assets.Editor
{
	public static class Paths
	{
		public const string GameWorkFolderName = "GameWork";

		public static string RelativeGameWorkFolder
		{
			get { return "Assets/" + GameWorkFolderName; }
		}

		public static string AbsoluteGameWorkFolder
		{
			get { return Application.dataPath + "/" + GameWorkFolderName; }
		}
	}
}
