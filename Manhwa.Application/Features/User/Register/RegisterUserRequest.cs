using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.Register
{
    public class RegisterUserRequest
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Password { get; init; } = default!;
    }
}
