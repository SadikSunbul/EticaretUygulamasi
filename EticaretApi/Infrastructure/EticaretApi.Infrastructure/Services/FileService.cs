using EticaretApi.Application.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Infrastructure.Services
{
    public class FileService : IFileService
    {
        public IWebHostEnvironment _webHostEnvironment { get; }
        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //todo denemek yapmaklazn 

        public async Task<List<(string filenme, string path)>> UploadAsync(string path, List<IFormFile> files)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, path);
            //_webHostEnvironment.WebRootPath wwwroot konumunu veırı sonra ıcerısınde resource/product-images adresını alır 
            //Path.Combine() yöntemi, belirtilen dizin yollarını birleştirerek tek bir dize oluşturur.
            if (!Directory.Exists(Path.GetDirectoryName(uploadPath))) //ıcındekı adreste bır dosya varmı dıye bakar yoksa olusturu ıcerıde
            {
                Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)); //bu dizini olustur dedik
            }

            List<(string filenme, string path)> datas = new();

            List<bool> results = new();
            foreach (var file in files)
            {
                string fileNewName=await FileRenameAsync(file.FileName);

                bool result=await CopyFileAsync($"{uploadPath}\\{fileNewName}",file);
                datas.Add((fileNewName, $"{uploadPath}\\{fileNewName}"));
                results.Add(result);
            }

            if (results.TrueForAll(r=>r.Equals(true))) //ıcerıdekı hepsı dogru ıse 
            {
                return datas;
            }
            return null;
            //todo Eğerki yukarıdaki if gecerlı degılse burda dosyaların sunucuda yuklenırken hata aldıgına daır uyarıcı bır exceptıon fırlatılması gerekıyor
            
        }

        public Task<string> FileRenameAsync(string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync(); //filetrımı bosaltmak lazım degılse verıelr karısır onu burada bosalttık
                return true;
            }
            catch (Exception)
            {
                //todo log!
                //hatırlatıcı deneme 
                throw;
            }
            
        }
    }
}
