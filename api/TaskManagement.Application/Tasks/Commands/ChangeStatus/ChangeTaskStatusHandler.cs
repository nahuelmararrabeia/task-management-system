using MediatR;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.Commands.ChangeStatus
{
    public class ChangeTaskStatusHandler : IRequestHandler<ChangeTaskStatusCommand>
    {
        private readonly ITaskRepository _repository;

        public ChangeTaskStatusHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.TaskId);

            if (task is null)
                throw new NotFoundException("Task", "Id", request.TaskId.ToString());

            task.ChangeStatus(request.Status);

            await _repository.UpdateAsync(task);
        }
    }
}
