using MediatR;

namespace TaskManagement.Application.Tasks.Commands.AssignUser
{
    public record AssignUserToTaskCommand(Guid TaskId, Guid UserId) : IRequest;
}
