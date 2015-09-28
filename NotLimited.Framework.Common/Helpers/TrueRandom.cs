using System;
using System.Security.Cryptography;
using System.Threading;

namespace NotLimited.Framework.Common.Helpers
{
	public static class TrueRandom
	{
		private static readonly RNGCryptoServiceProvider _global = new RNGCryptoServiceProvider();
		private static readonly ThreadLocal<Random> _random = new ThreadLocal<Random>(() =>
		{
			byte[] buffer = new byte[4];
			_global.GetBytes(buffer);
			return new Random(BitConverter.ToInt32(buffer, 0));
		});

		public static Random Generator => _random.Value;
	}
}