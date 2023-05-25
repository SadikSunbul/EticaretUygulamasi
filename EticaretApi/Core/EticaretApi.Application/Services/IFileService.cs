using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Application.Services
{
    public interface IFileService
    {
        Task<List<(string filenme,string path)>> UploadAsync(string path, List<IFormFile> files);
        Task<string> FileRenameAsync(string fileName);
        Task<bool> CopyFileAsync(string path, IFormFile file); 
    }
}
