using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createUser);
        Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnTokenDate);
        Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
        Task<List<GetListUserDTO>> GetAllUserAsync(int page,int size);
        int TotalUsersCount { get; }
        Task AssignRoleToUserAsync(string userId, string[] Roles);
        Task<string[]> GetRolesToUser(string userIdOrName);
        Task<bool> HasRolePermissionToEndPointAsync(string name,string code);
    }
}
