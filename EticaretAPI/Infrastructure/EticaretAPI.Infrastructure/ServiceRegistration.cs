using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Storage;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Infrastructure.Enums;
using EticaretAPI.Infrastructure.Services.MailService;
using EticaretAPI.Infrastructure.Services.StorageConcrete;
using EticaretAPI.Infrastructure.Services.StorageConcrete.Azure;
using EticaretAPI.Infrastructure.Services.StorageConcrete.Local;
using EticaretAPI.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;


namespace EticaretAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IFileService, FileService>();
            serviceCollection.AddScoped<IStorageService, StogareService>();
            serviceCollection.AddScoped<ITokenHandler,TokenHandler>();
            serviceCollection.AddScoped<IMailService, MailService>();


        }
        public static void AddStogare<T>(this IServiceCollection serviceCollection) where T: class, IStorage
        {
            serviceCollection.AddScoped<IStorage, T>();
        }
        public static void AddStogare(this IServiceCollection serviceCollection,StogareEnum stogareEnum) 
        {
            switch (stogareEnum)
            {
                case StogareEnum.Azure:
                    serviceCollection.AddScoped<IStorage, AzureStogare>();
                    break;
                case StogareEnum.AWS:

                    break;
                case StogareEnum.LocalStogare:
                    serviceCollection.AddScoped<IStorage, LocalStogare>();
                    break;
                   
                default:
                    serviceCollection.AddScoped<IStorage, LocalStogare>();
                    break;
            }
        }
    }
}
