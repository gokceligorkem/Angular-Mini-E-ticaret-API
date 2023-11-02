using EticaretAPI.Application.Abstraction.Services.Authentications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Abstraction.Services
{
    public interface IAuthService:IExternalAuthentication,IInternalAuthentication
    {
        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);


    }
}
