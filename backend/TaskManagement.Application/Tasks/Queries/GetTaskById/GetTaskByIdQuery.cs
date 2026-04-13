using MediatR;

namespace TaskManagement.Application.Tasks.Queries.GetTaskById
{
    public record GetTaskByIdQuery(Guid Id) : IRequest<GetTaskByIdResponse>;
}
