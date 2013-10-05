﻿using Microsoft.AspNet.Identity;

namespace NotLimited.Framework.Identity.Raven
{
	public abstract class UserBase : IUser
	{
		public string Id { get; set; }
		public string UserName { get; set; }
	}
}