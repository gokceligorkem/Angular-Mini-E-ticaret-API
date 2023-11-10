using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.UpdateRoleCommand
{
    public class UpdateRoleCommandRequest:IRequest<UpdateRoleCommandResponse>
    {
        public string Id { get; set; }
        public string roleName { get; set; }
    }
}