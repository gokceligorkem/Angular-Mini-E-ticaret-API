using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IAuthorizationEndPointService
    {
        public Task AssignRoleEndPointAsync(string[] roles, string code, string menu, Type type);
        public Task<List<string>> GetRolesEndPointAsync(string code,string menu);
    }
}
