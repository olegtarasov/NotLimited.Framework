using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace NotLimited.Framework.Server.Helpers
{
	public static class ImageExtensions
	{
		/// <summary>
		/// Resizes an image
		/// </summary>
		/// <param name="image">Image to resize</param>
		/// <param name="width">Desired width</param>
		/// <param name="height">Desired height</param>
		/// <returns></returns>
		public static Image Resize(this Image image, int width, int height)
		{
			float scaleWidth = ((float)width / (float)image.Width);
			float scaleHeight = ((float)height / (float)image.Height);
			float scale = scaleHeight < scaleWidth ? scaleHeight : scaleWidth;

			int destWidth = (int)((image.Width * scale) + 0.5);
			int destHeight = (int)((image.Height * scale) + 0.5);

			Bitmap bitmap = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
			bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.Clear(Color.White);
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

				graphics.DrawImage(image,
					new Rectangle(0, 0, destWidth, destHeight),
					new Rectangle(0, 0, image.Width, image.Height),
					GraphicsUnit.Pixel);
			}
			return bitmap;
		}

		/// <summary>
		/// Saves in image to JPEG file with specified quality
		/// </summary>
		/// <param name="image">Image to save</param>
		/// <param name="path">Image path</param>
		/// <param name="level">JPEG quality</param>
		public static void SaveJpeg(this Image image, string path, int level)
		{
			ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);

			// Create an Encoder object based on the GUID
			// for the Quality parameter category.
			Encoder enc = Encoder.Quality;

			// Create an EncoderParameters object.
			// An EncoderParameters object has an array of EncoderParameter
			// objects. In this case, there is only one
			// EncoderParameter object in the array.
			EncoderParameters encParams = new EncoderParameters(1);
			EncoderParameter encParam = new EncoderParameter(enc, level);

			encParams.Param[0] = encParam;
			image.Save(path, jgpEncoder, encParams);
		}

		private static ImageCodecInfo GetEncoder(ImageFormat format)
		{
			return ImageCodecInfo.GetImageDecoders().FirstOrDefault(codec => codec.FormatID == format.Guid);
		}
	}  
}