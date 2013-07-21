using System;
using System.Drawing;
using System.IO;

namespace NotLimited.Framework.Server.Helpers
{
	public class ImageHelper
	{
		private readonly FileSystemHelper _fsHelper;

		public ImageHelper(FileSystemHelper fsHelper)
		{
			this._fsHelper = fsHelper;
		}

		public string StoreImage(Stream stream, string path, int maxWidth, int maxHeight)
		{
			Image img;
			Image saveImg;

			// Get the image
			try
			{
				img = Image.FromStream(stream);
			}
			catch (Exception e)
			{
				throw new ImageProcessingException("Error processing the image!", e);
			}

			// Resize an image if needed
			if (img.Height > maxHeight || img.Width > maxWidth)
			{
				saveImg = ImageExtensions.Resize(img, maxWidth, maxHeight);
				img.Dispose();
			}
			else
				saveImg = img;

			// And save it to disk
			string fileName = _fsHelper.GetRandomFileName(path, ".jpg");
			try
			{
				ImageExtensions.SaveJpeg(saveImg, _fsHelper.CombineServerPath(path, fileName), 80);
			}
			catch (Exception e)
			{
				throw new ImageProcessingException("Error saving image to server!", e);
			}

			saveImg.Dispose();

			return fileName;
		}
	}
}