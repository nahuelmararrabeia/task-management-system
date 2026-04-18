using MediatR;

namespace TaskManagement.Application.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string? RefreshToken) 
        : IRequest<RefreshTokenResponse>;
}
