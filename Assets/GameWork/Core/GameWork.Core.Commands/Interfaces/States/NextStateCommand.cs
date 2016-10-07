using GameWork.Core.Commands.Interfaces;
using GameWork.Core.States;

namespace GameWork.Core.Commands.States
{
    public struct NextStateCommand : ICommand
    {
        public void Execute(object parameter)
        {
            var castParameter = (SequenceState)parameter;
            castParameter.NextState();
        }
    }
}
