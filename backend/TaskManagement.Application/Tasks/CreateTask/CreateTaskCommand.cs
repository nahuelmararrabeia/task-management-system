using MediatR;

namespace TaskManagement.Application.Tasks.CreateTask
{
    public record CreateTaskCommand(string Title, string Description) : IRequest<CreateTaskResponse>;
}
