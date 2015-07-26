using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NotLimited.Framework.Server.Helpers
{
    public class HashedPassword
	{
		public HashedPassword()
		{
		}

		public HashedPassword(byte[] salt, byte[] password)
		{
			Salt = salt;
			Password = password;
		}

		public byte[] Salt;
		public byte[] Password;
	}

    public static class Hashing
	{
		private static readonly Encoding _enc = new UTF8Encoding();

        public static HashedPassword HashPassword(string password)
		{
			var result = new HashedPassword {Salt = new byte[64]};
			
			using (var rng = new RNGCryptoServiceProvider())
			{
				rng.GetNonZeroBytes(result.Salt);
			}

			using (var hmac = new HMACSHA512(result.Salt))
			{
				result.Password = hmac.ComputeHash(_enc.GetBytes(password));
			}

			return result;
		}

        public static bool ValidatePassword(HashedPassword password, string request)
		{
			using (var hmac = new HMACSHA512(password.Salt))
			{
				var hash = hmac.ComputeHash(_enc.GetBytes(request));

				return hash.SequenceEqual(password.Password);
			}
		}
	}
}