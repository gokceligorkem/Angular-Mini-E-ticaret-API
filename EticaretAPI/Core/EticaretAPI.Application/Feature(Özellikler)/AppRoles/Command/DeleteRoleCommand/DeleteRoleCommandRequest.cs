using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.DeleteRoleCommand
{
    public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
    {
        public string Id { get; set; }
    }
}