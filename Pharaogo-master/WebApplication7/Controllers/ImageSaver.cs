using Microsoft.AspNetCore.Hosting;

namespace WebApplication7.Controllers
{
	public static class ImageSaver
	{
		public async static Task<string?> SaveImage(IFormFile? Image, IWebHostEnvironment webHostEnvironment)
		{
			if (Image != null && Image.Length > 0)
			{
				if (IsImageFileValid(Image))
				{
					// Get unique file name
					string uniqueFileName = GetUniqueFileName(Image.FileName);

					// Define image path
					string imagePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", uniqueFileName);

					// Ensure the directory exists
					string directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "Images");
					if (!Directory.Exists(directoryPath))
					{
						Directory.CreateDirectory(directoryPath);
					}

					// Save image to the server
					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						await Image.CopyToAsync(stream);
					}

					// Return relative path for the image
					return Path.Combine("Images", uniqueFileName);
				}
			}

			return null;
		}

		private static string GetUniqueFileName(string fileName)
		{
			fileName = Path.GetFileName(fileName);
			return Path.GetFileNameWithoutExtension(fileName)
				   + "_" + Guid.NewGuid().ToString().Substring(0, 4)
				   + Path.GetExtension(fileName);
		}

		private static bool IsImageFileValid(IFormFile file)
		{
			string[] allowedExtensions = { ".png", ".jpg", ".jpeg" };
			string fileExtension = Path.GetExtension(file.FileName).ToLower();
			return allowedExtensions.Contains(fileExtension);
		}
	}
}
