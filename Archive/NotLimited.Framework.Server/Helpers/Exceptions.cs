using System;
using System.Runtime.Serialization;

namespace NotLimited.Framework.Server.Helpers
{
	[Serializable]
	public class ImageProcessingException : Exception
	{
		public ImageProcessingException()
		{
		}

		public ImageProcessingException(string message)
			: base(message)
		{
		}

		public ImageProcessingException(string message, Exception inner)
			: base(message, inner)
		{
		}

		protected ImageProcessingException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}