using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.VerifyPassword
{
    public class VerifyPasswordRequest
    {
        public string Password { get; init; } = default!;
    }

}
