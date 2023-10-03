using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.GoogLoginUserCommand
{
    public class GoogleLoginCommanRequest:IRequest<GoogleLoginCommanResponse>
    {
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string FirstName { get; set; }
        public string Name { get; set; }
        public string? Lastname { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public string Provider { get; set; }
    }
}
