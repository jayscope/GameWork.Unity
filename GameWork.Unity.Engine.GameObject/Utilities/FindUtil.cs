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
		/// <param name="absolutePath"></param>
		/// <returns></returns>
		public static Transform[] FindAll(string absolutePath)
		{
			var segments = absolutePath.Split('/');
			var level = 0;

			var childObject = UnityEngine.GameObject.Find(segments[level]);

			if (childObject == null)
			{
				Debug.LogWarning("Couldn't find any object at path: " + absolutePath);
				return null;
			}

			var rootTransform = childObject.transform;

			var currentLevel = new List<Transform> { rootTransform };

			var matches = FindMatches(++level, segments, currentLevel);

			return matches.ToArray();
		}

		public static UnityEngine.GameObject[] FindAllGameObjects(string absolutePath)
		{
			var results = FindAll(absolutePath);
			return results.Select(t => t.gameObject).ToArray();
		}

		/// <summary>
		/// Breadth first search for game objects.
		/// 
		/// Root cannot be inactive.
		/// </summary>
		/// <param name="absolutePath"></param>
		/// <returns></returns>
		public static Transform Find(string absolutePath)
		{
			var results = FindAll(absolutePath);

			if (results.Length != 1)
			{
				if (results.Length == 0)
				{
					Debug.LogWarning($"Couldn't find any objects matching the path: \"{absolutePath}\"");
				}
				else
				{
					Debug.LogWarning($"Found {results.Length} objects matching the path: \"{absolutePath}\"");
				}

				return null;
			}

			return results[0];
		}

		public static UnityEngine.GameObject FindGameObject(string absolutePath)
		{
			var result = Find(absolutePath);
			return result.gameObject;
		}

		public static UnityEngine.GameObject[] FindAllChildren(string absolutePath)
		{
			var result = Find(absolutePath);

			var childCount = result.childCount;

			if (childCount < 1)
			{
				Debug.LogWarning($"Couldn't find any children of the object matching the path: \"{absolutePath}\"");
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