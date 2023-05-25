
using EticaretApi.Application.Repositories;
using EticaretApi.Application.RequestParameters;
using EticaretApi.Application.Services;
using EticaretApi.Application.ViewModels.Products;
using EticaretApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EticaretApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly private IFileService fileService;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
        }

        #region Örnek
        /*
         [HttpGet] //bunu drekt bıldırmek gerek degılse swıger patlar
        public async Task Get()
        {
            //await _productWriteRepository.AddRangeAsync(new (){
            // new (){Id=Guid.NewGuid(), Name="Product 1",Price=100 ,CreateDate=DateTime.Now ,Stock=5},
            // new (){Id=Guid.NewGuid(), Name="Product 2",Price=200 ,CreateDate=DateTime.Now ,Stock=15},
            // new (){Id=Guid.NewGuid(), Name="Product 3",Price=300 ,CreateDate=DateTime.Now ,Stock=25}
            //});
            //await _productWriteRepository.SaveAsync();

            Product p = await _productReadRepository.GetByIdAsync("",false);//2.paramete true deault olarak ıstersen false de yapabılırsın falsede takıp brakılır
            p.Name = "Taha";
            await _productWriteRepository.SaveAsync(); //Burada ıkısı farklı methotlardan gelıyor ama scop olarak dbcontrxt ı IoC kontaınera ekledıgımız ıcın bıze 1 kere olusturcaktır yanı aynı dbcontext te oluyor gıbı bır bırının verılerını kontrol edebılırler 

        }
        [HttpGet("{id}")] //linkdekı ıd yı yakalar
        public async Task<IActionResult> Get(string id)
        {
            Product product = await _productReadRepository.GetByIdAsync(id);
            _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10 });/*,CreateDate=DateTime.Now burayı herzaman yazamayız bunu merkezılestırmek lazım *//*
            return Ok(product);
    }

        //[HttpGet]
        //public async Task Test()
        //{
        //    _productWriteRepository.AddAsync(new() { Name = "C Product", Price = 1.500F, Stock = 10 });/*,CreateDate=DateTime.Now burayı herzaman yazamayız bunu merkezılestırmek lazım */
        //}

        #endregion
        #region Servise Api sağlama

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination) //[FromQuery] queryden gonderılen datayı yakaladık 
        {
            var totalcount = _productReadRepository.GetAll(false).Count();
            var data = _productReadRepository.GetAll(false)
            .Select(p => new
            {
                p.Id,//ıd yı sılme ıslemı yparsa dıye gonderıyorum 
                p.Name,
                p.Stock,
                p.Price,
                p.CreateDate,
                p.UpdateDate
            })
            .Skip(pagination.Page * pagination.Size)
            .Take(pagination.Size)
            .ToList(); //Skip dedıgımız atla  Take=5 her sayfada 5 urun gosterılcek sayfa 0 ıken 5 ı sayfa 1 ıken ılk 5 ı atlar sonrakı 5 den 10 a kadar olanı geıtırı  
            return Ok(new
            {
                totalcount,
                data
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var data = _productReadRepository.GetByIdAsync(id, false);
            data.Wait();
            var data1 = data.Result;
            return Ok(data1);
        }

        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model) //normalde boyle entıtıy ıle karsılanmaz veri
        {
            await _productWriteRepository.AddAsync(model); //ekledik 
            await _productWriteRepository.SaveAsync(); //Kaydettik
            return StatusCode((int)HttpStatusCode.Created);//ekleme yapıldı kodu doncek 201 doner
        }

        [HttpPut]
        public async Task<ActionResult> Put(VM_Update_Product model) //guncelleme ıslemı yapılcak 
        {
            //if (ModelState.IsValid)
            //{
            //buradakı  calısmaz cunku kendısının yapmıs oldugu default bır dogrulayıcı kontrol sıstemı var orda ılk kontrolu yapar gecebılrıse burayı mesgul eder etmezse drekt hata fırlatır o yapılanamda soyle eklenir
            //builder.Services.AddControllers().AddFluentValidation(configration=>configration.RegisterValidatorsFromAssemblyContaining<CreateProductValidater>())
            //.ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);  bu yapılanma yazılırise ılk oto kontrol yapılmaz burası calısır burada manuel bı kotorl ıslemı yapar
            //}
            Product product = await _productReadRepository.GetByIdAsync(model.Id); // Id verıp tum nesneyı elde ettık 
            product.Stock = model.Stock;
            product.Price = model.Price;
            product.Name = model.Name;

            await _productWriteRepository.SaveAsync();

            return Ok();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await _productWriteRepository.RemoveAsync(id);
            if (data)
            {
                await _productWriteRepository.SaveAsync();
                return Ok(true);
            }

            return Ok(false);
        }

        #region Dosya ıslemını katmanlara ayırdık eskı ıslem acıklamalar ıcın kalıyor burada 
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


        #endregion

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(IFormFile files)
        {
            var data =await  fileService.UploadAsync("resource/product-images",files);

            return  Ok(data);
        }


        #endregion
    }
}
