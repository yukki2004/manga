using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.GetCurrentUser
{
    public class GetCurrentUserQueryHandler
        : IRequestHandler<GetCurrentUserQuery, GetCurrentUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public GetCurrentUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetCurrentUserResponse> Handle(
            GetCurrentUserQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
                throw new Exception("User not found");

            return new GetCurrentUserResponse
            {
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
