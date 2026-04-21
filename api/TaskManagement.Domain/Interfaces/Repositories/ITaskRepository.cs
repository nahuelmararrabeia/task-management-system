using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task AddAsync(TaskItem task);
        Task UpdateAsync(TaskItem task);
        Task<TaskItem?> GetByIdAsync(Guid id);
        Task<TaskItem?> GetByIdWithUserAsync(Guid id);
        Task<(List<TaskItem>, int)> GetPagedAsync(int page, int pageSize);

        Task<(List<TaskItem>, int)> GetPagedWithUserAsync(int page, int pageSize);
    }
}
