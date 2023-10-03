using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence
{
    static class Configuration
    {
      static public string ConnectionString//ServiceRegistrationda çağırılır
        {
            get
            {
                //Bu configurationlar yüklediğimiz configuration extension,configuration extension json eklentileri sayesinde ulaşılır. Configuration manager
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/EticaretAPI.Presentation"));//Bu yol, yapılandırma dosyasının nerede bulunduğunu belirtir.
                configurationManager.AddJsonFile("appsettings.json");
                return configurationManager.GetConnectionString("PostgreSQL");//GetConnectionString yöntemi kullanılarak appsettings.json dosyasından PostgreSQL veritabanı bağlantı dizesi alınır ve döndürülür. 
            }
        }
    }
}
