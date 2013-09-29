using System;
using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public class UserManagement : IUserManagement
	{
		public string UserId { get; set; }
		public bool DisableSignIn { get; set; }
		public DateTime LastSignInTimeUtc { get; set; }
	}
}