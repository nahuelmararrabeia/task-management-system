using MediatR;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Application.Tasks.GetTaskById;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.CreateTask;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdCommand, GetTaskByIdResponse>
{
    private readonly ITaskRepository _repository;

    public GetTaskByIdHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<GetTaskByIdResponse> Handle(GetTaskByIdCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);

        if (task is null)
            throw new NotFoundException("Task", "Id", request.Id.ToString());

        return new GetTaskByIdResponse(task.Id, task.Title, task.Description, task.CreatedAt);
    }
}