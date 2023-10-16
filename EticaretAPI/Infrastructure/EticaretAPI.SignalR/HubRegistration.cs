using EticaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.SignalR
{
    public static class HubRegistration
    {
        public static void MapHubRegistration(this WebApplication application)
        {
            application.MapHub<ProductHub>("/products-hub");
            application.MapHub<OrderHub>("/order-hub");
        }
    }
}
