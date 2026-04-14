using MediatR;

namespace TaskManagement.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(
    string Email,
    string Name,
    string Password
) : IRequest<CreateUserResponse>;
}
