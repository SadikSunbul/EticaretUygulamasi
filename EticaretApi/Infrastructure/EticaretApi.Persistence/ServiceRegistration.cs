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
            services.AddDbContext<EticaretApiDbContext>(options => options.UseSqlServer(Configration.ConnectionString)); //buradakı bı connectıon strıng ıfadesı hatalı aslında daha sora burayı degıstırcez
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>(); //ICustomerReadRepository ıstenınce CustomerReadRepository döner
            services.AddScoped<ICustomerWriteRepository,CustomerWriteRepository>();
            services.AddScoped<IProductReadRepository,ProductReadRepository>();
            services.AddScoped<IProductWriteRepository,ProductWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderReadRepository,OrderReadRepository>();

        }
    }
}
