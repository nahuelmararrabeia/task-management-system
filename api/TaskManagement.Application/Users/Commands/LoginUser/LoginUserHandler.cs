namespace TaskManagement.Application.Users.Commands.LoginUser
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using TaskManagement.Application.Common.Exceptions;
    using TaskManagement.Application.Common.Interfaces;
    using TaskManagement.Domain.Interfaces.Repositories;

    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IJwtTokenGenerator _jwt;

        public LoginUserHandler(IUserRepository repository, IJwtTokenGenerator jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.GetByEmailAsync(request.Email);

            if (user is null)
                throw new UnauthorizedException("Invalid credentials");

            var hasher = new PasswordHasher<User>();

            var result = hasher.VerifyHashedPassword(
                null!,
                user.PasswordHash,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedException("Invalid credentials");

            var token = _jwt.Generate(user);

            return new LoginUserResponse(token);
        }
    }
}
