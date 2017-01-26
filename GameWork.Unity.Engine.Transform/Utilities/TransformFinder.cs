using System.Collections.Generic;
using UnityEngine;

namespace GameWork.Unity.Engine.Transform.Utilities
{
	public static class TransformFinder
	{
		public static UnityEngine.Transform[] FindAll(string path, UnityEngine.Transform root = null)
		{
			var segments = path.Split('/');
			var level = 0;

			if (root == null)
			{
				var rootGmeObject = GameObject.Find(segments[level]);
				level++;

				if (rootGmeObject == null)
				{
					Debug.LogWarning("Couldn't find any object at path: " + path);
					return null;
				}

				root = rootGmeObject.transform;
			}

			var currentLevel = new List<UnityEngine.Transform> { root };

			var matches = FindMatches(level, segments, currentLevel);
			return matches.ToArray();
		}

		public static UnityEngine.Transform Find(string path, UnityEngine.Transform root = null)
		{
			var results = FindAll(path, root);

			if (results.Length != 1)
			{
				if (results.Length == 0)
				{
					Debug.LogWarning($"Couldn't find any objects matching the path: \"{path}\"");
				}
				else
				{
					Debug.LogWarning($"Found {results.Length} objects matching the path: \"{path}\"");
				}

				return null;
			}

			return results[0];
		}
		
		
		public static UnityEngine.Transform[] FindAllChildren(string path, UnityEngine.Transform root = null)
		{
			var result = Find(path, root);

			var childCount = result.childCount;

			if (childCount < 1)
			{
				Debug.LogWarning($"Couldn't find any children of the object matching the path: \"{path}\"");
				return null;
			}

			var children = new List<UnityEngine.Transform>();

			for (var i = 0; i < childCount; i++)
			{
				children.Add(result.GetChild(i));
			}

			return children.ToArray();
		}

		private static List<UnityEngine.Transform> FindMatches(int level, IList<string> pathSegments, List<UnityEngine.Transform> currentLevel)
		{
			while (pathSegments.Count > level && currentLevel.Count > 0)
			{
				var nextLevel = new List<UnityEngine.Transform>();

				foreach (var transform in currentLevel)
				{
					for (var i = 0; i < transform.childCount; i++)
					{
						if (transform.GetChild(i).name == pathSegments[level])
						{
							nextLevel.Add(transform.GetChild(i));
						}
					}
				}
				
				level++;
				currentLevel.Clear();
				currentLevel = nextLevel;
			}

			return currentLevel;
		}
	}
}