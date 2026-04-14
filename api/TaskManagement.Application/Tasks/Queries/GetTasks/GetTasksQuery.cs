using MediatR;

namespace TaskManagement.Application.Tasks.Queries.GetTasks
{
    public record GetTasksQuery(int Page = 1, int PageSize = 10)
    : IRequest<GetTasksResponse>;
}
