namespace GameWork.Unity.Components.Utilities
{
	public static class ComponentFinder
	{
		public static TComponent Find<TComponent>(string path, UnityEngine.Transform root = null)
		{
			return Transform.Utilities.TransformFinder.Find(path, root).GetComponent<TComponent>();
		}
	}
}
