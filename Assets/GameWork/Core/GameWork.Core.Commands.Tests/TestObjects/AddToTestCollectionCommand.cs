using GameWork.Core.Commands.Interfaces;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class AddToTestCollectionCommand<T> : ICommand<TestCollection<T>>
	{
		private readonly T _value;

		public AddToTestCollectionCommand(T value)
		{
			_value = value;
		}

		public void Execute(TestCollection<T> implementor)
		{
			implementor.Add(_value);
		}
	}
}
