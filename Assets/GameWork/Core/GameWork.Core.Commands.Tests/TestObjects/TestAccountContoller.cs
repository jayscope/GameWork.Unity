using GameWork.Core.Commands.Accounts.Interfaces;

namespace GameWork.Core.Commands.Tests.TestObjects
{
	public class TestAccountContoller : ILoginAction, ILogoutAction, IRegisterAction
	{
		private readonly string _username;
		private readonly string _password;

		public bool IsLoggedIn { get; private set; }
		public bool IsRegistered { get; private set; }
		public bool IsLoggedOut { get; private set; }

		public TestAccountContoller(string username, string password)
		{
			_username = username;
			_password = password;
		}

		public void Login(string username, string password)
		{
			IsLoggedIn = username == _username && password == _password;
		}

		public void Logout()
		{
			IsLoggedOut = true;
		}

		public void Register(string username, string password)
		{
			IsRegistered = username == _username && password == _password;
		}
	}
}
