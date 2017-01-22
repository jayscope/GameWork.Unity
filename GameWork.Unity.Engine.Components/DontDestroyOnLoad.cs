using UnityEngine;

namespace GameWork.Unity.Engine.Components
{
    public class DontDestroyOnLoad : MonoBehaviour
	{
		private void OnEnable()
		{
			DontDestroyOnLoad(this);
		}
	}
}
