using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_ecommerce.Service.Interfaces
{
	public interface IPhotoService
	{
		Task<string> AddPhoto(IFormFile file, string FolderName);
		Task<bool> DeletePhoto(string fileName, string FolderName);
	}
}
