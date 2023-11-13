using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.DTOs.Configuration;
using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;


namespace EticaretAPI.Persistence.Services
{
    public class AuthorizationEndPointService : IAuthorizationEndPointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndPointReadRepository _endPointReadRepository;
        readonly IEndPointWriteRepository _endPointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;
        public AuthorizationEndPointService(IApplicationService applicationService, IEndPointReadRepository endPointReadRepository, IEndPointWriteRepository endPointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endPointReadRepository = endPointReadRepository;
            _endPointWriteRepository = endPointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndPointAsync(string[] roles, string code, string menu, Type type)
        {

            Domain.Entities.Menu _menu = await  _menuReadRepository.GetSingleAsync(m => m.Name == menu);
            if (_menu == null)
            {
                _menu = new()
                {
                    ID = Guid.NewGuid(),
                    Name = menu,
                };
                await _menuWriteRepository.AddAsync(_menu);

                await _menuWriteRepository.SaveAsync();
            }

            EndPoint? endpoint = await _endPointReadRepository.Table.Include(e => e.Menu).Include(i=>i.Roles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if(endpoint== null)
            {
               var action= _applicationService.GetAuthorizeDefinitionEndpoints(type)
                .FirstOrDefault(m => m.Name == menu)?
                .Actions.FirstOrDefault(m=>m.Code==code);
                endpoint = new()
                {
                    ID = Guid.NewGuid(),
                    Code = action.Code,
                    Definition = action.Definition,
                    HttpType = action.HttpType,
                    ActionType = action.ActionType,
                    Menu = _menu
                    

                };
                await _endPointWriteRepository.AddAsync(endpoint);
                await _endPointWriteRepository.SaveAsync();
            }
            foreach (var role in endpoint.Roles)
                endpoint.Roles.Remove(role);

            var appRoles=await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
                endpoint.Roles.Add(role);

            await _endPointWriteRepository.SaveAsync();
        }

        public async Task<List<string>> GetRolesEndPointAsync(string code, string menu)
        {
          EndPoint? endPoint=await _endPointReadRepository.Table.Include(e => e.Roles).Include(m=>m.Menu).FirstOrDefaultAsync(e => e.Code==code && e.Menu.Name==menu);
            if(endPoint!=null)
           return endPoint.Roles.Select(e => e.Name).ToList();
            return null;
        }
    }
}
