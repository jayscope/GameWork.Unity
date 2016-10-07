using System;
using GameWork.Core.Commands.Interfaces;
using GameWork.Core.Commands.States;
using GameWork.Core.States.Controllers;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class TestStateCommandResolver : CommandResolver
	{
		private readonly SequenceStateController _stateController;

		public TestStateCommandResolver(SequenceStateController stateController)
		{
			_stateController = stateController;
		}

		public override void ProcessCommand(ICommand command)
		{
			var changeStateCommand = command as ChangeStateCommand;
			if (changeStateCommand != null)
			{
				changeStateCommand.Execute(_stateController);
				return;
			}

			var nextStateCommand = command as NextStateCommand;
			if (nextStateCommand != null)
			{
				nextStateCommand.Execute(_stateController);
				return;
			}

			var previousStateCommand = command as PreviousStateCommand;
			if (previousStateCommand != null)
			{
				previousStateCommand.Execute(_stateController);
				return;
			}
		}
	}
}
