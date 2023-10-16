
using EticaretAPI.Persistence.Contexts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using EticaretAPI.Application.Repository;
using EticaretAPI.Persistence.Repository;
using EticaretAPI.Application.Repository.File;
using EticaretAPI.Persistence.Repository.File;
using EticaretAPI.Persistence.Repository.Invoice;
using EticaretAPI.Application.Repository.Invoice;
using EticaretAPI.Domain.Entities.Identity;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Persistence.Services;
using EticaretAPI.Application.Abstraction.Services.Authentications;
using EticaretAPI.Application.Repository.Basket;
using EticaretAPI.Application.Repository.BasketItem;

namespace EticaretAPI.Persistence
{
    public static class ServiceRegistration
    {
       
       
        public static void AddPersistenceServices(this IServiceCollection services)
        {
         
            services.AddDbContext<EticaretDbContext>(options=>options.UseNpgsql(Configuration.ConnectionString));
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                
            }).AddEntityFrameworkStores<EticaretDbContext>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>(); 
            
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWritRepository, OrderWriterRepository>();

            services.AddScoped<IFileBaseReadRepository, FileReadRepository>();
            services.AddScoped<IFileBaseWriteRepository, FileWriteRepository>();
            services.AddScoped<IFileImageReadRepository, FileImageReadRepository>();
            services.AddScoped<IFileImageWriteRepository, FileImageWriteRepository>();

            services.AddScoped<IInvoiceReadRepository, InvoiceReadRepository>();
            services.AddScoped<IInvoiceWriteRepository, InvoiceWriteRepository>();

            services.AddScoped<IBasketReadRepository, BasketReadRepository>();
            services.AddScoped<IBasketWriteRepository, BasketWriteRepository>();

            services.AddScoped<IBasketItemReadRepository, BasketItemReadRepository>();
            services.AddScoped<IBasketItemWriteRepository, BasketItemWriteRepository>();



            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IBasketService, BasketService>();

        }
    }
}
