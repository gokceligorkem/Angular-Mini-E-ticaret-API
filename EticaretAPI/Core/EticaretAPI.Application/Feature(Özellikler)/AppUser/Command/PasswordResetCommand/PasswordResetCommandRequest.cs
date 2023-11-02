using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.PasswordResetCommand
{
    public class PasswordResetCommandRequest:IRequest<PasswordResetCommandResponse>
    {
        public string Email { get; set; }
    }
}
