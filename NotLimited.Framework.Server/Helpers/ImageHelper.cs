using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace NotLimited.Framework.Server.Helpers
{
	public class ImageSize
	{
		public ImageSize(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public ImageSize(int size) : this(size, size)
		{
		}

		public int Width;
		public int Height;
	}

	public class ImageHelper
	{
		private readonly FileSystemHelper _fsHelper;

		public ImageHelper(FileSystemHelper fsHelper)
		{
			_fsHelper = fsHelper;
		}

		public List<string> StoreImage(Stream stream, string path, params ImageSize[] sizes)
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

			var result = new List<string>(sizes.Length);

			for (int i = 0; i < sizes.Length; i++)
			{
				// Resize an image if needed
				if (img.Height > sizes[i].Height || img.Width > sizes[i].Width)
					saveImg = img.Resize(sizes[i].Width, sizes[i].Height);
				else
					saveImg = new Bitmap(img);

				// And save it to disk
				string fileName = _fsHelper.GetRandomFileName(path, ".jpg");
				try
				{
					saveImg.SaveJpeg(_fsHelper.CombineServerPath(path, fileName), 80);
				}
				catch (Exception e)
				{
					throw new ImageProcessingException("Error saving image to server!", e);
				}

				result.Add(fileName);
				saveImg.Dispose();
			}

			img.Dispose();

			return result;
		}
	}
}