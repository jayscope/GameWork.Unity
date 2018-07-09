using UnityEngine;

namespace GameWork.Unity.Components
{
    public class DontDestroyOnLoad : MonoBehaviour
	{
		private void OnEnable()
		{
			DontDestroyOnLoad(this);
		}
	}
}
