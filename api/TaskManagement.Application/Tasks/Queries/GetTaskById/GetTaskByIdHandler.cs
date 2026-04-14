using MediatR;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.Queries.GetTaskById;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, GetTaskByIdResponse>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetTaskByIdResponse> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdWithUserAsync(request.Id);

        if (task is null)
            throw new NotFoundException("Task", "Id", request.Id.ToString());

        var assignedUser = task.AssignedUser is null ? null
            : new AssignedUserDTO(task.AssignedUser.Id, task.AssignedUser.Name, task.AssignedUser.Email);

        return new GetTaskByIdResponse(task.Id, task.Title, task.Description, task.CreatedAt, assignedUser);
    }
}