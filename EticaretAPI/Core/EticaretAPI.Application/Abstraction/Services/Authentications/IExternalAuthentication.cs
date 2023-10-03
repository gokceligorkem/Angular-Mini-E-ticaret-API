

namespace EticaretAPI.Application.Abstraction.Services.Authentications
{
    public interface IExternalAuthentication
    {
        Task<DTOs.Token> FacebookLoginAsync(string authToken,int tokenLife);
        Task<DTOs.Token> GoogleLoginAsync(string idToken, int tokenLife);
     
    }
}
