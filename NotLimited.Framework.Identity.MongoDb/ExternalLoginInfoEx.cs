using Microsoft.AspNet.Identity.Owin;

namespace NotLimited.Framework.Identity.MongoDb
{
	public class ExternalLoginInfoEx : ExternalLoginInfo
	{
		public new MongoLoginInfo Login { get; set; }
	}
}