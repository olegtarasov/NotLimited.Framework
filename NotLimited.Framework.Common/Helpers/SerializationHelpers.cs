using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace NotLimited.Framework.Common.Helpers
{
	public static class SerializationHelpers
	{
		public static void Serialize<T>(this T obj, Stream stream)
		{
			var serializer = new BinaryFormatter();
			serializer.Serialize(stream, obj);
		}

		public static T Deserialize<T>(this Stream stream)
		{
			var serializer = new BinaryFormatter();
			return (T)serializer.Deserialize(stream);
		}
	}
}