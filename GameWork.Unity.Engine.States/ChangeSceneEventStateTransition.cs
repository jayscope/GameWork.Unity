using GameWork.Core.States.Event;
using UnityEngine.SceneManagement;

namespace GameWork.Unity.Engine.States
{
	public class ChangeSceneEventStateTransition : EventStateTransition
	{
		private readonly string _toSceneName;
		private readonly string _toStateName;

		public ChangeSceneEventStateTransition(string toStateName, string toSceneName)
		{
			_toStateName = toStateName;
			_toSceneName = toSceneName;
		}

		public void ChangeState()
		{
			ExitState(_toStateName);

			SceneManager.sceneLoaded += OnSceneLoaded;
			SceneManager.LoadScene(_toSceneName);
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;

			EnterState(_toStateName);
		}
	}
}
