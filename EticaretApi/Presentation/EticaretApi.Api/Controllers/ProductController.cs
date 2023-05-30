
using Azure.Storage.Sas;
using EticaretApi.Application.Abstractions.Storage;
using EticaretApi.Application.Features.Commends.CreateProduct;
using EticaretApi.Application.Features.Queries.GetAllProduct;
using EticaretApi.Application.Repositories;
using EticaretApi.Application.RequestParameters;
using EticaretApi.Application.Services;
using EticaretApi.Application.ViewModels.Products;
using EticaretApi.Domain.Entities;
using EticaretApi.Domain.Entities._File;
using EticaretApi.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EticaretApi.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService fileService;
        private readonly IFileWriteRepository fileWriteRepository;
        private readonly IFileReadRepository fileReadRepository;
        private readonly IInvoiceFileReadRepository ınvoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository ınvoiceFileWriteRepository;
        private readonly IProductImageFileReadRepository productImageFileReadRepository;
        private readonly IProductImageWriteRepository productImageWriteRepository;

        private readonly IConfiguration configuration;

        private readonly IMediator mediator;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IInvoiceFileReadRepository ınvoiceFileReadRepository, IInvoiceFileWriteRepository ınvoiceFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageWriteRepository productImageWriteRepository, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            this.fileService = fileService;
            this.fileWriteRepository = fileWriteRepository;
            this.fileReadRepository = fileReadRepository;
            this.ınvoiceFileReadRepository = ınvoiceFileReadRepository;
            this.ınvoiceFileWriteRepository = ınvoiceFileWriteRepository;
            this.productImageFileReadRepository = productImageFileReadRepository;
            this.productImageWriteRepository = productImageWriteRepository;

            this.configuration = configuration;
            this.mediator = mediator;
        }


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




        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest) //[FromQuery] queryden gonderılen datayı yakaladık 
        {
            GetAllProducQueryResponse response = await mediator.Send(getAllProductQueryRequest); //kendısı algılar handlerda ona gore sonucu olusturup degerı gonderırır 
            return Ok(response);
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
        public async Task<IActionResult> Post( CreateProductCommendRequest createProductCommendRequest) //normalde boyle entıtıy ıle karsılanmaz veri
        {
            var response=await mediator.Send(createProductCommendRequest);

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





        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload(string id)
        //{

            #region tEST
            //var data=await storageService.UploadAsync("resource/files",Request.Form.Files);
            // //  var data =fileService.UploadAsync("resource/product-images", file);
            // //  data.Wait();
            // //  var data1 = data.Result;
            // await productImageWriteRepository.AddRangeAsync(data.Select(d => new ProductImageFile()
            // {
            //     FileName = d.fileName,
            //     Path = d.pathOrContainerName,
            //     Storage = storageService.StorageName
            // }).ToList());
            // await _productWriteRepository.SaveAsync();
            #endregion
        //    List<(string filename, string pathcontaınernaem)> result = await storageService.UploadAsync("photo-images", Request.Form.Files);
        //    Product product = await _productReadRepository.GetByIdAsync(id);
        //    //foreach (var r in result)
        //    //{
        //    //    product.ProductImageFiles.Add(new (){
        //    //        FileName = r.filename,
        //    //        Path = r.pathcontaınernaem,
        //    //        Storage = storageService.StorageName,
        //    //        Products = new List<Product>() { product }
        //    //    });
        //    //}
        //    await productImageWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
        //    {
        //        FileName = r.filename,
        //        Path = r.pathcontaınernaem,
        //        Storage = storageService.StorageName,
        //        Products = new List<Product>() { product }
        //    }).ToList());

        //    await productImageWriteRepository.SaveAsync();

        //    return Ok();
        //}

        //[HttpGet("[action]/{id}")] //ıd yı rout dan alıcaz
        //public async Task<IActionResult> GetProductImages(string id)
        //{
        //    Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

        //    return Ok(product.ProductImageFiles.Select(p => new
        //    {
        //        Path = $"{configuration["BaseStorageUrl"]}/{p.Path}", //verıyı alıcagı yerı ayarladık azure yerınden 
        //        p.FileName
        //    }));
        //}
        //[HttpDelete("[action]/{id}/{imageId}")]
        //public async Task<IActionResult> DeleteproductImage(string id, string imageId)
        //{
        //    Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        //    ProductImageFile productImage = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
        //    product.ProductImageFiles.Remove(productImage);
        //    await _productWriteRepository.SaveAsync();
        //    return Ok();
        //}

    }
}
