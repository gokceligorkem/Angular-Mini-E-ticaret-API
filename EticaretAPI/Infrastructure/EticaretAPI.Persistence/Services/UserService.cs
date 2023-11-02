using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command;
using EticaretAPI.Application.Helpers;
using EticaretAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
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

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
