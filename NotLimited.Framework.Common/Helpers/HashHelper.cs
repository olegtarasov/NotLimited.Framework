using System;
using System.Security.Cryptography;

namespace NotLimited.Framework.Common.Helpers
{
    public static class HashHelper
    {
        public static byte[] ComputeHash(byte[] data)
        {
            using (var sha = new SHA256Managed())
            {
                return sha.ComputeHash(data);
            }
        }
    }
}