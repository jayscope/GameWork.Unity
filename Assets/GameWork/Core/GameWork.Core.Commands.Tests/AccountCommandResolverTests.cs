using GameWork.Core.Commands.Accounts;
using GameWork.Core.Commands.Tests.TestObjects;
using NUnit.Framework;

namespace GameWork.Core.Commands.Tests
{
	[TestFixture]
    public class AccountCommandResolverTests
    {
		private const string Username = "testUser";
		private const string Password = "testPassword";

		private readonly TestAccountContoller _accountContoller = new TestAccountContoller(Username, Password);
	    private readonly TestAccountCommandResolver _commandResolver;

	    public AccountCommandResolverTests()
	    {
		    _commandResolver = new TestAccountCommandResolver(_accountContoller);
	    }

		[Test]
		public void Register()
		{
			Assert.False(_accountContoller.IsRegistered);

			var command = new RegisterCommand(Username, Password);
			_commandResolver.ProcessCommand(command);

			Assert.True(_accountContoller.IsRegistered);
		}

		[Test]
		public void Login()
		{
			Assert.False(_accountContoller.IsLoggedIn);

			var command = new LoginCommand(Username, Password);
			_commandResolver.ProcessCommand(command);

			Assert.True(_accountContoller.IsLoggedIn);
		}

		[Test]
		public void Logout()
		{
			Assert.False(_accountContoller.IsLoggedOut);

			var command = new LogoutCommand();
			_commandResolver.ProcessCommand(command);

			Assert.True(_accountContoller.IsLoggedOut);
		}
	}
}