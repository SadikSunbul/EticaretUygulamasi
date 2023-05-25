using EticaretApi.Application.Services;
using EticaretApi.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretApi.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureService(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFileService, FileService>(); //scop ekledık cunku bız tek bı request ıcın 1 tane new yeter
        }
    }
}
