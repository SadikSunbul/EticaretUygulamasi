using Microsoft.EntityFrameworkCore;
using EticaretApi.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EticaretApi.Persistence.Repositories;
using EticaretApi.Application.Repositories;

namespace EticaretApi.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EticaretApiDbContext>(options => options.UseSqlServer(Configration.ConnectionString),ServiceLifetime.Singleton); //buradakı bı connectıon strıng ıfadesı hatalı aslında daha sora burayı degıstırcez
            services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>(); //ICustomerReadRepository ıstenınce CustomerReadRepository döner
            services.AddSingleton<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddSingleton<IProductReadRepository,ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository,ProductWriteRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();
            services.AddSingleton<IOrderReadRepository,OrderReadRepository>();
        }
    }
}
