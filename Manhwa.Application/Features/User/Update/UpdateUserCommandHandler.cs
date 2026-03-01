using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.Update
{
    public class UpdateUserCommandHandler
        : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserCommandHandler(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateUserResponse> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
                throw new Exception("User not found");

            if (user.Username != request.Username)
            {
                var exists = await _userRepository
                    .ExistsByUsernameAsync(request.Username, cancellationToken);

                if (exists)
                    throw new Exception("Username already exists");

                user.Username = request.Username;
            }

            if (user.Email != request.Email)
            {
                var exists = await _userRepository
                    .ExistsByEmailAsync(request.Email, cancellationToken);

                if (exists)
                    throw new Exception("Email already exists");

                user.Email = request.Email;
            }


            _userRepository.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            return new UpdateUserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
