using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command;
using EticaretAPI.Application.Helpers;
using EticaretAPI.Application.Repository;
using EticaretAPI.Domain.Entities;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly IEndPointReadRepository _endpointRead;

        public UserService(UserManager<AppUser> userManager, IEndPointReadRepository endpointRead)
        {
            _userManager = userManager;
            _endpointRead = endpointRead;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUser.Username,
                Email = createUser.Email,
                NameSurname = createUser.NameSurname,
            }, createUser.Password); ;
            CreateUserResponse response = new() { Succeeded = result.Succeeded };
            if (result.Succeeded)
            {
                response.Message = "Kullanıcı başarıyla oluşturuldu";
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code}-{error.Description}<br>";
                }
            }
            return response;
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user,DateTime accessTokenDate,int addOnTokenDate)
        {
           
            if (user != null)
            {
                user.RefreshToken= refreshToken;
                user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else { throw new NotFoundUserException(); }
            
        }
        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
          AppUser user=await  _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
              IdentityResult result= await _userManager.ResetPasswordAsync(user, resetToken,newPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                }
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task<List<GetListUserDTO>> GetAllUserAsync(int page, int size)
        {
           var users=await _userManager.Users
                .Skip(page*size)
                .Take(size).ToListAsync();

            return users.Select(user => new GetListUserDTO
            {
                Id=user.Id,
                Email=user.Email,
                NameSurname=user.NameSurname,
                UserName = user.UserName
            }).ToList();
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);

                await _userManager.AddToRolesAsync(user, roles);
            }
        }

        public async Task<string[]> GetRolesToUser(string userIdOrName)
        {
            AppUser user =await _userManager.FindByIdAsync(userIdOrName);
            if(user ==null)
               user = await _userManager.FindByNameAsync(userIdOrName);
            if (user!=null)
            {
                var userRoles= await _userManager.GetRolesAsync(user);
                return userRoles.ToArray();
            }
            return new string[] {};
        }

        public async Task<bool> HasRolePermissionToEndPointAsync(string name, string code)
        {
            var userRoles =await GetRolesToUser(name);
            if (!userRoles.Any()) return false;

          EndPoint? endpoint= await _endpointRead.Table.
                Include(e => e.Roles)
                .FirstOrDefaultAsync(e=>e.Code==code);
            if (endpoint == null)
                return false;

            var hasRole = false;

            var endPointRoles = endpoint.Roles.Select(e => e.Name);
            foreach(var userRole in userRoles)
            {
                foreach (var endPointRole in endPointRoles)
                    if (userRole == endPointRole)
                        return true;
            }
            return false;

        }

        public int TotalUsersCount => _userManager.Users.Count();

    }
}
