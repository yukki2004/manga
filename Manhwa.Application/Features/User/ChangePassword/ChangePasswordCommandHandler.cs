using Manhwa.Application.Common.Interfaces;
using Manhwa.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Application.Features.User.ChangePassword
{
    public class ChangePasswordCommandHandler
        : IRequestHandler<ChangePasswordCommand, Unit>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public ChangePasswordCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(
            ChangePasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(
                request.UserId,
                cancellationToken);

            if (user is null)
                throw new Exception("User not found");

            var valid = _passwordHasher.Verify(
                request.CurrentPassword,
                user.Password);

            if (!valid)
                throw new Exception("Current password is incorrect");

            user.Password = _passwordHasher.Hash(request.NewPassword);

            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
