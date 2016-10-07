using System.Collections.Generic;
using GameWork.Core.Commands.Interfaces;
using GameWork.Core.Commands.Tests.TestObjects;
using NUnit.Framework;

namespace GameWork.Core.Commands.Tests
{
	[TestFixture]
	public class CommandResolverTests
	{
		[Test]
		public void ResolveSingleCommand()
		{
			// Setup
			var values = new List<string>()
			{
				"Hannah",
				"Bob",
				"Frank",
				"Franchesca",
				"Zoltan"
			};

			var testCollection = new TestCollection<string>();
			var testCollectionCommandResolver = new TestCollectionCommandResolver<string>(testCollection);

			Assert.AreEqual(0, testCollection.Count); 
			
			// Process
			foreach (var value in values)
			{
				testCollectionCommandResolver.ProcessCommand(new AddToTestCollectionCommand<string>(value));
			}

			// Assert
			Assert.AreEqual(values.Count, testCollection.Count);

			CollectionAssert.AreEqual(values, testCollection);
		}

		[Test]
		public void ResolveMultipleCommands()
		{
			// Setup
			var values = new List<string>()
			{
				"Hannah",
				"Bob",
				"Frank",
				"Franchesca",
				"Zoltan"
			};

			var testCollection = new TestCollection<string>();
			var testCollectionCommandResolver = new TestCollectionCommandResolver<string>(testCollection);

			Assert.AreEqual(0, testCollection.Count);

			var commands = new List<ICommand>();
			foreach (var value in values)
			{
				commands.Add(new AddToTestCollectionCommand<string>(value));
			}

			// Process
			testCollectionCommandResolver.ProcessCommands(commands);

			// Assert
			Assert.AreEqual(values.Count, testCollection.Count);

			CollectionAssert.AreEqual(values, testCollection);
		}

		[Test]
		public void ResolveCommandQueue()
		{
			// Setup
			var values = new List<string>()
			{
				"Hannah",
				"Bob",
				"Frank",
				"Franchesca",
				"Zoltan"
			};

			var commandQueue = new CommandQueue();

			// Process
			foreach (var value in values)
			{
				commandQueue.AddCommand(new AddToTestCollectionCommand<string>(value));
			}

			var testCollection = new TestCollection<string>();
			var testCollectionCommandResolver = new TestCollectionCommandResolver<string>(testCollection);

			// Assert
			Assert.AreEqual(0, testCollection.Count);

			testCollectionCommandResolver.ProcessCommandQueue(commandQueue);

			Assert.AreEqual(values.Count, testCollection.Count);

			CollectionAssert.AreEqual(values, testCollection);
		}
	}
}
