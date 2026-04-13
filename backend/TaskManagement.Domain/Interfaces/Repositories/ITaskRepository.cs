using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<(List<TaskItem>, int)> GetAllAsync(int page, int pageSize);
    }
}
