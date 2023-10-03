using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.Abstraction.Token;
using EticaretAPI.Application.DTOs;
using EticaretAPI.Application.DTOs.Facebook;
using EticaretAPI.Application.Exceptions;
using EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.LoginUserCommand;
using EticaretAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EticaretAPI.Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient;
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;


        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }
        async Task<Token> CreateUserExternalAsync(AppUser user,string email,string name,UserLoginInfo info,int tokenLifeTime)
        {
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = email,
                        UserName = email,
                        NameSurname = name
                    };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(user, info);

                Token token = _tokenHandler.CreateAccessToken(tokenLifeTime,user);
                await _userService.UpdateRefreshToken(token.RefreshToken,user,token.Experition,15);

                return token;
            }
            throw new Exception("Authentication error");
        }
        public async Task<Token> FacebookLoginAsync(string authToken, int tokenLife)
        {
            string accessToken = await _httpClient.GetStringAsync($"https://graph.facebook.com/oauth/access_tokenclient_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}&client_secret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessToken);
            string userAcessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookAccessTokenValidation>(userAcessTokenValidation);
            if (validation?.Data.IsValid != null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");
                FacebookUserInfoResponse? facebookUserInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                return await CreateUserExternalAsync(user, facebookUserInfo?.Email, facebookUserInfo.Name, info, tokenLife);
            }
            throw new Exception("Authentication error");

        }

        public async Task<Token> GoogleLoginAsync(string idToken, int tokenLife)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
            var info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

          return  await  CreateUserExternalAsync(user,payload.Email,payload.Name,info,tokenLife);
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password, int tokenLifeTime)
        {
            Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)

                await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                throw new NotFoundUserException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)//Authentication başarılı.
            {
                Token token = _tokenHandler.CreateAccessToken(tokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Experition, 5);

                return token;
            }
        
            throw new AuthenticationErrorException();
        }

        public async Task<Token> RefreshTokenLogin(string refreshToken)
        {
          AppUser? user= await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Experition, 15);
                return token;
            }
            else
                throw new NotFoundUserException();
        }
    }
}
