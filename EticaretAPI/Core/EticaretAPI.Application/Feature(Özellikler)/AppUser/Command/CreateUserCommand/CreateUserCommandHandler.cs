
using EticaretAPI.Application.Abstraction.Hubs;
using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.DTOs.User;
using EticaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;


namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;
      
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
         CreateUserResponse response=  await _userService.CreateAsync(new() {
                Email = request.Email,
                NameSurname=request.NameSurname,
                Password=request.Password,
                PasswordConfirm=request.PasswordConfirm,
                Username=request.Username,
            });


            return new()
            {
                Message=response.Message,
                Succeeded=response.Succeeded,   
            };
            //throw new UserCreateFailedException();
        }
    }
}
