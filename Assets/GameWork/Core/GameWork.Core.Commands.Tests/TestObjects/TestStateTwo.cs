using GameWork.Core.States;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class TestStateTwo : SequenceState
	{
		public const string StateName = "Two";

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
			ChangeState(TestStateThree.StateName);
		}

		public override void PreviousState()
		{
			ChangeState(TestStateOne.StateName);
		}
	}
}
