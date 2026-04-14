using MediatR;
using TaskManagement.Domain.Interfaces.Repositories;
using TaskManagement.Application.Common.Exceptions;

namespace TaskManagement.Application.Tasks.Commands.UpdateTask;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _repository;

    public UpdateTaskHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _repository.GetByIdAsync(request.Id);

        if (task is null)
            throw new NotFoundException("Task", "Id", request.Id.ToString());

        task.Update(request.Title, request.Description);

        await _repository.UpdateAsync(task);
    }
}