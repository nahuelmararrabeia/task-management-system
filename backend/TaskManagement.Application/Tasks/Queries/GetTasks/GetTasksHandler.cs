using MediatR;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.Queries.GetTasks
{
    public class GetTasksHandler : IRequestHandler<GetTasksQuery, GetTasksResponse>
    {
        private readonly ITaskRepository _repository;

        public GetTasksHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetTasksResponse> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            var (tasks, totalCount) = await _repository.GetAllAsync(request.Page, request.PageSize);

            var items = tasks
                .Select(t => new TaskSummaryDTO(
                    t.Id,
                    t.Title,
                    t.Description,
                    t.CreatedAt
                ));

            return new GetTasksResponse(items, totalCount);
        }
    }
}
