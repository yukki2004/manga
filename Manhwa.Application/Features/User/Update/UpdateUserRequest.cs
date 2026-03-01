using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.Update
{
    public class UpdateUserRequest
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}
