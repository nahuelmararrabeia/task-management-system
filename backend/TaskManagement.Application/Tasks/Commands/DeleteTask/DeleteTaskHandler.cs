using MediatR;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.Commands.DeleteTask
{
    public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
    {
        private readonly ITaskRepository _repository;

        public DeleteTaskHandler(ITaskRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _repository.GetByIdAsync(request.Id);

            if (task is null)
                throw new NotFoundException("Task", "Id", request.Id.ToString());

            task.Delete();

            await _repository.UpdateAsync(task);
        }
    }
}
