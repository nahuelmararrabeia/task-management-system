using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Auth.Commands.LoginUser
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly IUserRepository _repository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwt;

        public LoginUserHandler(IUserRepository repository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenGenerator jwt)
        {
            _repository = repository;
            _refreshTokenRepository = refreshTokenRepository;
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

            var accessToken = _jwt.Generate(user);
            var refreshToken = new Domain.Entities.RefreshToken
            (
                user.Id,
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateTime.UtcNow.AddDays(30)
            );

            await _refreshTokenRepository.AddAsync(refreshToken);

            return new LoginUserResponse(accessToken, refreshToken.Token);
        }
    }
}
