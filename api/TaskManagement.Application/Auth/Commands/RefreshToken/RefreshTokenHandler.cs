using MediatR;
using System.Security.Cryptography;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Common.Interfaces;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Auth.Commands.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _jwt;

        public RefreshTokenHandler(IUserRepository userRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IJwtTokenGenerator jwt)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwt = jwt;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand command, CancellationToken ct)
        {
            var token = command?.RefreshToken;

            if (token == null)
                throw new UnauthorizedException(string.Empty);

            var storedToken = await _refreshTokenRepository.GetByTokenAsync(token);

            if (storedToken == null || !storedToken.IsActive)
                throw new UnauthorizedException(string.Empty);

            var user = await _userRepository.GetByIdAsync(storedToken.UserId);

            if (user == null)
                throw new UnauthorizedException(string.Empty);

            storedToken.Revoke();
            await _refreshTokenRepository.UpdateAsync(storedToken);

            var newAccessToken = _jwt.Generate(user);
            
            var newRefreshToken = new Domain.Entities.RefreshToken
            (
                user.Id,
                Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                DateTime.UtcNow.AddDays(30)
            );

            await _refreshTokenRepository.AddAsync(newRefreshToken);

            return new RefreshTokenResponse(newAccessToken, newRefreshToken.Token);
        }
    }
}
