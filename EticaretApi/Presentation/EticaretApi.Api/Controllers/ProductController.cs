
using EticaretApi.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet] //bunu drekt bıldırmek gerek degılse swıger patlar
        public async void Get()
        {
            await _productWriteRepository.AddRangeAsync(new (){
             new (){Id=Guid.NewGuid(), Name="Product 1",Price=100 ,CreateDate=DateTime.Now ,Stock=5},
             new (){Id=Guid.NewGuid(), Name="Product 2",Price=200 ,CreateDate=DateTime.Now ,Stock=15},
             new (){Id=Guid.NewGuid(), Name="Product 3",Price=300 ,CreateDate=DateTime.Now ,Stock=25}
            });
            await _productWriteRepository.SaveAsync();
            
        }
    }
}
