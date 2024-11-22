using E_ecommerce.Service.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Repo
{
	public class PhotoService:IPhotoService
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public PhotoService(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}
		public async Task<string> AddPhoto(IFormFile file, string FolderName)
		{
			string photopath = Path.Combine(_webHostEnvironment.WebRootPath, FolderName);
			if (!Directory.Exists(photopath))
			{
				Directory.CreateDirectory(photopath);
			}
			var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
			var filepath = Path.Combine(photopath, filename);
			using (var stream = new FileStream(filepath, FileMode.Create))
			{
				await file.CopyToAsync(stream);
			}
			return filename;
		}

		public async Task<bool> DeletePhoto(string fileName, string folderName)
		{
			// Construct the full path to the file
			string photoPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName, fileName);

			// Check if the file exists
			if (File.Exists(photoPath))
			{
				// Delete the file
				File.Delete(photoPath);
				return true;
			}

			// File not found
			return false;
		}
	}
}
