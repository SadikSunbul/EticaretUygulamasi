using EticaretApi.Application.Abstractions.Storage;
using EticaretApi.Application.Abstractions.Storage.Azure;
using EticaretApi.Application.Services;
using EticaretApi.Infrastructure.Enums;
using EticaretApi.Infrastructure.Services;
using EticaretApi.Infrastructure.Services.Stogare;
using EticaretApi.Infrastructure.Services.Stogare.Azure;
using EticaretApi.Infrastructure.Services.Stogare.Local;
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
            serviceCollection.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection serviceCollection)where T:Storage,IStorage
            //hem bunu IStorage ıplament edıcek hemde storage den turıycek 

        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection serviceCollection, StorageType storageType) 

        {
            switch (storageType)
            {
                case StorageType.Local:
                    serviceCollection.AddScoped<IStorage,LocalStorage>();
                    break;
                case StorageType.Azure:
                    serviceCollection.AddScoped<IAzureStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    serviceCollection.AddScoped<IStorage, LocalStorage>();
                    break;
            }
            
        }
    }
}
