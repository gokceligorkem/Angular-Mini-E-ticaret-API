using MediatR;

namespace EticaretAPI.Application.Feature_Özellikler_.AppRoles.Command.AuthorizationEndPointsCommand
{
    public class AssignRoleEndPointCommandRequest:IRequest<AssignRoleEndPointCommandResponse>
    {
        public string[] Roles { get; set; }
        public string Code { get; set; }
        public string Menu { get; set; }
        public Type? Type { get; set; }
    }
}