using MediatR;
using Microsoft.AspNetCore.Identity;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand,  CreateUserResponse>
    {
        private readonly IUserRepository _repository;

        public CreateUserHandler(IUserRepository repository) {
            _repository = repository;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByEmailAsync(request.Email);

            if (existing != null) {
                throw new ConflictException("Email already exists");
            }

            var hasher = new PasswordHasher<User>();

            var passwordHash = hasher.HashPassword(null!, request.Password);

            var user = new User(request.Email, request.Name, passwordHash);

            await _repository.AddAsync(user);

            return new CreateUserResponse(user.Id);
        }
    }
}
