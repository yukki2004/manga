using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<GetCurrentUserResponse>
    {
        public int UserId { get; init; }
    }
}
