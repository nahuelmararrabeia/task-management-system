using MediatR;

namespace TaskManagement.Application.Auth.Commands.LoginUser
{
    public record LoginUserCommand(
        string Email,
        string Password
    ) : IRequest<LoginUserResponse>;
}
