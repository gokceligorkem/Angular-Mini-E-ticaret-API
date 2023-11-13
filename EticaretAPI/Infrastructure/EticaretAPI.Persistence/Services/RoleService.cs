using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Feature_Özellikler_.AppRoles.Query.GetRoles;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> CreateRole(string Name)
        {
         IdentityResult result= await  _roleManager.CreateAsync(new() {Id=Guid.NewGuid().ToString(), Name = Name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(string id)
        {
            AppRole role =await _roleManager.FindByIdAsync(id);
            IdentityResult result= await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public (object, int) GetAllRoles(int page,int size)
        {
            var data = _roleManager.Roles;
            IQueryable<AppRole> getRoles = null;
            if (page != -1 && size != -1)
                getRoles = data.Skip(page * size).Take(size);
            else
                getRoles = data;
        
            return (getRoles.Select(r => new { r.Id, r.Name }),data.Count());  
        }

        public async Task<(string id, string name)> GetRoleById(string id)
        {
          string role= await _roleManager.GetRoleIdAsync(new() { Id=id});
            return (id, role);
        }

        public async Task<bool> UpdateRole(string id, string Name)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            role.Name = Name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
    }
}
