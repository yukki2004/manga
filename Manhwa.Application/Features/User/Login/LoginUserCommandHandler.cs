using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.Login
{
    public class LoginUserCommandHandler
        : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IIdentityService _identityService;

        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IIdentityService identityService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _identityService = identityService;
        }

        public async Task<LoginUserResponse> Handle(
            LoginUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdentifierAsync(
                request.Identifier,
                cancellationToken);

            if (user is null)
                throw new Exception("Invalid credentials");

            var valid = _passwordHasher.Verify(
                request.Password,
                user.Password);

            if (!valid)
                throw new Exception("Invalid credentials");

            var token = _identityService.GenerateAccessToken(user);

            return new LoginUserResponse
            {
                Id = user.UserId,
                Username = user.Username,
                Email = user.Email,
                AccessToken = token
            };
        }
    }
}
