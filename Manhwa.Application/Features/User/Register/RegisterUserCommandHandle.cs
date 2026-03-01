using Manhwa.Domain.Repositories;
using MediatR;
using Manhwa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manhwa.Application.Common.Interfaces;

namespace Manhwa.Application.Features.User.Register
{
    public class RegisterUserCommandHandler
        : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterUserResponse> Handle(
            RegisterUserCommand request,
            CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
                throw new Exception("Email already exists");

            if (await _userRepository.ExistsByUsernameAsync(request.Username, cancellationToken))
                throw new Exception("Username already exists");

            var user = new Domain.Entities.User
            {
                Username = request.Username,
                Email = request.Email,
                Password = _passwordHasher.Hash(request.Password)
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new RegisterUserResponse
            {
                Id = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
