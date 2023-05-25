using EticaretApi.Application.Services;
using EticaretApi.Infrastructure;
using EticaretApi.Infrastructure.Operations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Infrastructure.Services
{
    public class FileService /*: IFileService*/
    {
        //[HttpPost("[Action]")] //Üste post var oldugu ıcın artık ısımı ıle cagrılmalı
        //public async Task<IActionResult> Upload(List<IFormFile> files)
        //{
        //    string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, "resource/product-images");
        //    //_webHostEnvironment.WebRootPath wwwroot konumunu veırı sonra ıcerısınde resource/product-images adresını alır 
        //    //Path.Combine() yöntemi, belirtilen dizin yollarını birleştirerek tek bir dize oluşturur.
        //    if (!Directory.Exists(Path.GetDirectoryName(uploadPath))) //ıcındekı adreste bır dosya varmı dıye bakar yoksa olusturu ıcerıde 
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(uploadPath)); //bu dizini olustur dedik

        //    }
        //    foreach (var file in files)
        //    {
        //        Random r = new();
        //        string fileName = Path.GetFileName(file.FileName); //belirtilen dosya yolu dizesinden dosya adını ve uzantısını ayıklar.
        //        string fullPath = Path.Combine(uploadPath, $"{r.Next()}{fileName}"); //bırlestırıyoruz 


        //        using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
        //        await file.CopyToAsync(fileStream);

        //        await fileStream.FlushAsync(); //filetrımı bosaltmak lazım degılse verıelr karısır onu burada bosalttık 


        //    }
        //    return Ok();
        //}
    }
}


    