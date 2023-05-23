using Microsoft.EntityFrameworkCore;
using EticaretApi.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EticaretApiDbContext>(options => options.UseSqlServer(Configration.ConnectionString)); //buradakı bı connectıon strıng ıfadesı hatalı aslında daha sora burayı degıstırcez
        }
    }
}
