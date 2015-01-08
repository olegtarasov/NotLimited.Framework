using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NotLimited.Framework.Server.Services;

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
		private readonly IStorageService _storageService;

	    public ImageHelper(IStorageService storageService)
	    {
	        _storageService = storageService;
	    }

	    public List<string> StoreImage(byte[] bytes, string directory, params ImageSize[] sizes)
	    {
	        using (var ms = new MemoryStream(bytes))
	        {
	            return StoreImage(ms, directory, sizes);
	        }
	    }

	    public List<string> StoreImage(Stream stream, string directory, params ImageSize[] sizes)
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
			    string fileName;
                string filePath = _storageService.GetRandomFileNameWithPath(directory, ".jpg", out fileName);
			    using (var memoryStream = new MemoryStream())
			    {
                    try
                    {
                        saveImg.SaveJpeg(memoryStream, 80);
                    }
                    catch (Exception e)
                    {
                        throw new ImageProcessingException("Error saving image to server!", e);
                    }

			        memoryStream.Seek(0, SeekOrigin.Begin);
                    _storageService.StoreFile(memoryStream, filePath);
			    }
                
				result.Add(fileName);
				saveImg.Dispose();
			}

			img.Dispose();

			return result;
		}
	}
}