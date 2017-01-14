using UnityEngine;

namespace GameWork.Unity.Engine.GameObject.Components
{
    public class DontDestroyOnLoad : MonoBehaviour
	{
		private void OnEnable()
		{
			DontDestroyOnLoad(this);
		}
	}
}
