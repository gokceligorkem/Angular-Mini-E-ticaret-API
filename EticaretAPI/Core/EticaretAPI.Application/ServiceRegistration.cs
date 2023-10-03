using EticaretAPI.Application.RequestParameters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection collection)
        {
            collection.AddScoped<Pagination>(); 

            collection.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(ServiceRegistration).Assembly); });
            collection.AddHttpClient();
        }
    }
}
