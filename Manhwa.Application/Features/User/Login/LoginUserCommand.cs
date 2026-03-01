using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.Login
{
    public class LoginUserCommand : IRequest<LoginUserResponse>
    {
        public string Identifier { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
