using MediatR;

namespace TaskManagement.Application.Tasks.GetTaskById
{
    public record GetTaskByIdCommand(Guid Id) : IRequest<GetTaskByIdResponse>;
}
