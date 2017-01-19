using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameWork.Unity.Engine.GameObject
{
	public static class FindUtil
	{
		/// <summary>
		/// Breadth first search for game objects.
		/// 
		/// Root cannot be inactive.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="root"></param>
		/// <returns></returns>
		public static Transform[] FindAll(string path, Transform root = null)
		{
			var segments = path.Split('/');
			var level = 0;

			if (root == null)
			{
				var rootGmeObject = UnityEngine.GameObject.Find(segments[level]);
				level++;

				if (rootGmeObject == null)
				{
					Debug.LogWarning("Couldn't find any object at path: " + path);
					return null;
				}

				root = rootGmeObject.transform;
			}

			var currentLevel = new List<Transform> { root };

			var matches = FindMatches(level, segments, currentLevel);

			return matches.ToArray();
		}

		public static UnityEngine.GameObject[] FindAllGameObjects(string path)
		{
			var results = FindAll(path);
			return results.Select(t => t.gameObject).ToArray();
		}

		/// <summary>
		/// Breadth first search for game objects.
		/// 
		/// Root cannot be inactive.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static Transform Find(string path, Transform root = null)
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

		public static UnityEngine.GameObject FindGameObject(string path, UnityEngine.GameObject root = null)
		{
			var result = Find(path, root.transform);
			return result.gameObject;
		}
		
		public static UnityEngine.GameObject[] FindAllChildren(string path, UnityEngine.GameObject root = null)
		{
			var result = Find(path, root?.transform);

			var childCount = result.childCount;

			if (childCount < 1)
			{
				Debug.LogWarning($"Couldn't find any children of the object matching the path: \"{path}\"");
				return null;
			}

			var children = new List<Transform>();

			for (var i = 0; i < childCount; i++)
			{
				children.Add(result.GetChild(i));
			}

			return children.Select(t => t.gameObject).ToArray();
		}

		private static List<Transform> FindMatches(int level, IList<string> pathSegments, List<Transform> currentLevel)
		{
			while (pathSegments.Count > level && currentLevel.Count > 0)
			{
				var nextLevel = new List<Transform>();

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