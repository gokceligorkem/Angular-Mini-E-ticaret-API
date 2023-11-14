using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.AssignRoleToUserCommand
{
    public class AssignRoleToUserCommandRequest:IRequest<AssignRoleToUserCommandResponse>
    {
        public string userId { get; set; }
        public string[] Roles { get; set; }
    }
}