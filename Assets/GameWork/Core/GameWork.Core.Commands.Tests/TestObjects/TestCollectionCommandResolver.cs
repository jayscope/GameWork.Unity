using GameWork.Core.Commands.Interfaces;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class TestCollectionCommandResolver<T> : CommandResolver
	{
		private readonly TestCollection<T> _testCollection;

		public TestCollectionCommandResolver(TestCollection<T> testCollection)
		{
			_testCollection = testCollection;
		}

		public override void ProcessCommand(ICommand command)
		{
			var addToTestCollectionCommand = (AddToTestCollectionCommand<T>) command;
			if (addToTestCollectionCommand != null)
			{
				addToTestCollectionCommand.Execute(_testCollection);
			}
		}
	}
}
