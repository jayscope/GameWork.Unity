using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameWork.Unity.Assets.Editor
{
	public static class SaveCurrentCommitInfo
	{
		private const int TimeOutMilliseconds = 1000;

		private static string WriteFileCommands
		{
			get
			{
				return "git show --oneline -s > " +
						CurrentCommitInfoFile;
			}
		}
	
		private static string GitProjectFolder
		{
			get
			{
				return Directory.GetParent(Application.dataPath).Parent.FullName;
			}
		}

		private static string CurrentCommitInfoFile
		{
			get
			{
				return Paths.AbsoluteGameWorkFolder + "/GitCommit.txt";
			}
		}

		public static void GetAndSave()
		{
			if (File.Exists(CurrentCommitInfoFile))
			{
				File.Delete(CurrentCommitInfoFile);
			}

			var startInfo = new ProcessStartInfo()
			{
				FileName = "cmd.exe",
				WorkingDirectory = GitProjectFolder,
				UseShellExecute = false,
				RedirectStandardInput = true,
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true
			};
		
			var process = Process.Start(startInfo);
			process.StandardInput.WriteLine();
			process.StandardInput.WriteLine(WriteFileCommands);
			process.StandardInput.WriteLine("exit");
			process.WaitForExit();

			var timer = new Stopwatch();
			timer.Start();
			while (!File.Exists(CurrentCommitInfoFile) && timer.ElapsedMilliseconds < TimeOutMilliseconds)
			{
				Thread.Sleep(10);	
			}

			AssetDatabase.ImportAsset(CurrentCommitInfoFile);

			if (!File.Exists(CurrentCommitInfoFile))
			{
				Debug.LogError("Failed: " + WriteFileCommands);
			}
			else
			{
				Debug.Log("Created: " + CurrentCommitInfoFile);	
			}
		}
	}
}
