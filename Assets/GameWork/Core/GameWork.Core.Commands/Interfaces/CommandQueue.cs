using System.Collections.Generic;
using System.Linq;
using GameWork.Core.Commands.Interfaces;

namespace GameWork.Core.Commands
{
    public class CommandQueue : ICommandInterface
    {
        private readonly List<ICommand> _commands = new List<ICommand>();

        public bool HasCommands
        {
            get { return _commands.Any(); }
        }

        public void AddCommand(ICommand command)
        {
            _commands.Add(command);
        }

        public void AddCommands(IEnumerable<ICommand> commands)
        {
            _commands.AddRange(commands);
        }

        public ICommand TakeFirstCommand()
        {
            var command = _commands[0];
            _commands.RemoveAt(0);
            return command;
        }

        public ICommand[] TakeAllCommands()
        {
            var commands = _commands.ToArray();
            _commands.Clear();
            return commands;
        }
    }
}