using System.Diagnostics;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class SaveCurrentCommitInfo
{
	private const int TimeOutMilliseconds = 1000;

	private static string WriteFileCommands
	{
		get
		{
			return "git show --oneline -s > " +
					CurrentCommitInfoPath;
		}
	}
	
	private static string GitProjectFolder
	{
		get
		{
			return Directory.GetParent(Application.dataPath).Parent.FullName;
		}
	}

	private static string CurrentCommitInfoPath
	{
		get
		{
			return Application.dataPath + "/GameWork/GitCommit.txt";
		}
	}

	public static void GetAndSave()
	{
		if (File.Exists(CurrentCommitInfoPath))
		{
			File.Delete(CurrentCommitInfoPath);
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
		while (!File.Exists(CurrentCommitInfoPath) && timer.ElapsedMilliseconds < TimeOutMilliseconds)
		{
			Thread.Sleep(10);	
		}

		AssetDatabase.ImportAsset(CurrentCommitInfoPath);

		if (!File.Exists(CurrentCommitInfoPath))
		{
			Debug.LogError("Failed: " + WriteFileCommands);
		}
		else
		{
			Debug.Log("Created: " + CurrentCommitInfoPath);	
		}
	}
}
