
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EticaretApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        

        [HttpGet] //bunu drekt bıldırmek gerek degılse swıger patlar
        public IActionResult GetProducts()
        {
            
            return Ok();
        }
    }
}
