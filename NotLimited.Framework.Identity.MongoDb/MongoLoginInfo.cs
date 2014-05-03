using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class MongoLoginInfo
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public MongoLoginInfo()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:System.Object"/> class.
		/// </summary>
		public MongoLoginInfo(string loginProvider, string providerKey)
		{
			LoginProvider = loginProvider;
			ProviderKey = providerKey;
		}

		public string LoginProvider { get; set; }
		public string ProviderKey { get; set; }
		public string AccessToken { get; set; }
		public string UserId { get; set; }

		public UserLoginInfo ToLoginInfo()
		{
			return new UserLoginInfo(LoginProvider, ProviderKey);
		}
	}
}