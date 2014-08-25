using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace NotLimited.Framework.Identity.MongoDb
{
	public static class AuthManagerExtensions
	{
		public static async Task<ExternalLoginInfoEx> GetExternalLoginInfoAsyncEx(this IAuthenticationManager manager, string xsrfKey, string expectedValue)
		{
			var info = await manager.GetExternalLoginInfoAsync(xsrfKey, expectedValue);

			return GetExternalLoginInfo(info);
		}

		public static async Task<ExternalLoginInfoEx> GetExternalLoginInfoAsyncEx(this IAuthenticationManager manager)
		{
			var info = await manager.GetExternalLoginInfoAsync();
			
			return GetExternalLoginInfo(info);
		}

		private static ExternalLoginInfoEx GetExternalLoginInfo(ExternalLoginInfo info)
		{
			var result = new ExternalLoginInfoEx
			{
				DefaultUserName = info.DefaultUserName,
				Email = info.Email,
				ExternalIdentity = info.ExternalIdentity,
				Login = new MongoLoginInfo(info.Login.LoginProvider, info.Login.ProviderKey)
			};

			result.Login.AccessToken = info.ExternalIdentity.GetClaimValue("accesstoken");
			result.Login.UserId = info.ExternalIdentity.GetClaimValue("userid");

			return result;
		}

		private static string GetClaimValue(this ClaimsIdentity identity, string claim)
		{
			var item = identity.FindFirst(claim);
			return item != null ? item.Value : null;
		}
	}
}