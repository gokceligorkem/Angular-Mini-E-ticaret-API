using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.VerifyResetTokenCommand
{
    public class VerifyResetTokenCommandRequest:IRequest<VerifyResetTokenCommandResponse>
    {
        public string resetToken { get; set; }
        public string userId { get; set; }
    }
}
