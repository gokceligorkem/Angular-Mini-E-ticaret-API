using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.Feature_Özellikler_.AppUser.Command.RefreshTokenCommand
{
    public class RefreshTokenCommanRequest:IRequest<RefreshTokenCommanResponse>
    {
        public string RefreshToken { get; set; }
    }
}
