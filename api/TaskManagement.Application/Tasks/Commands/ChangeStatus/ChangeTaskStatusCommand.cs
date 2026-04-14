using MediatR;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Tasks.Commands.ChangeStatus
{
    public record ChangeTaskStatusCommand(
        Guid TaskId,
        TaskItemStatus Status
    ) : IRequest;
}
