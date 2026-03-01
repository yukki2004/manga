using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.ChangePassword
{
    public class ChangePasswordCommand : IRequest<Unit>
    {
        public int UserId { get; init; }
        public string CurrentPassword { get; init; } = default!;
        public string NewPassword { get; init; } = default!;
    }
}
