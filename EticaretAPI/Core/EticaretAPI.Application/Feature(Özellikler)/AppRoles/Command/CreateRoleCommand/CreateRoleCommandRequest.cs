using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.CreateRoleCommand
{
    public class CreateRoleCommandRequest:IRequest<CreateRoleCommandResponse>
    {
        public string Name { get; set; }
    }
}