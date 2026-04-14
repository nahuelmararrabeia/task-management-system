using MediatR;
using TaskManagement.Application.Common.Exceptions;
using TaskManagement.Domain.Interfaces.Repositories;

namespace TaskManagement.Application.Tasks.Commands.AssignUser
{
    public class AssignUserToTaskHandler : IRequestHandler<AssignUserToTaskCommand>
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IUserRepository _userRepository;

        public AssignUserToTaskHandler(
            ITaskRepository taskRepository,
            IUserRepository userRepository)
        {
            _taskRepository = taskRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(AssignUserToTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskRepository.GetByIdAsync(request.TaskId)
                ?? throw new NotFoundException("Task", "Id", request.TaskId.ToString());

            if (task.IsDeleted)
                throw new InvalidOperationException("Cannot assign a user to a deleted task.");

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                throw new NotFoundException("User", "Id", request.UserId.ToString());

            task.AssignUser(request.UserId);

            await _taskRepository.UpdateAsync(task);
        }
    }
}
