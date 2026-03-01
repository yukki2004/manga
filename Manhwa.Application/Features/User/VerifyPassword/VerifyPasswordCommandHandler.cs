using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.VerifyPassword
{
    public class VerifyPasswordCommandHandler
        : IRequestHandler<VerifyPasswordCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public VerifyPasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> Handle(
            VerifyPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
                return false;

            return _passwordHasher.Verify(
                request.Password,
                user.Password);
        }
    }
}
