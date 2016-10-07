using GameWork.Core.States;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class TestStateOne : SequenceState
	{
		public const string StateName = "One"; 

		public override string Name
		{
			get { return StateName; }
		}

		public override void Enter()
		{
		}

		public override void Exit()
		{
		}

		public override void NextState()
		{
			ChangeState(TestStateTwo.StateName);
		}

		public override void PreviousState()
		{
		}
	}
}
