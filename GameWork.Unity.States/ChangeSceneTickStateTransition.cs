namespace GameWork.Unity.States
{
	public abstract class ChangeSceneTickStateTransition : ChangeSceneEventStateTransition
	{
		protected ChangeSceneTickStateTransition(string toStateName, string toSceneName) : base(toStateName, toSceneName)
		{
		}
	}
}
