using MediatR;

namespace TaskManagement.Application.Users.Commands.LoginUser
{
    public record LoginUserCommand(
        string Email,
        string Password
    ) : IRequest<LoginUserResponse>;
}
