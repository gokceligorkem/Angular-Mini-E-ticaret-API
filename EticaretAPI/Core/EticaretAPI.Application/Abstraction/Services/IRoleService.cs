using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IRoleService
    {
        Task<bool> CreateRole(string Name);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string id,string Name);
        (object,int) GetAllRoles(int page,int size);
        Task<(string id,string name)> GetRoleById(string id);
    }
}
