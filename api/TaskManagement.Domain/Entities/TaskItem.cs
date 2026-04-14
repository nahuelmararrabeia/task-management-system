using TaskManagement.Domain.Common;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem : AuditableEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public Guid? AssignedUserId { get; private set; }
        public User? AssignedUser { get; private set; }
        public TaskItemStatus Status { get; private set; }

        public TaskItem(string title, string description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;

            Status = TaskItemStatus.Pending;
        }

        public void Update(string title, string description)
        {
            if(string.IsNullOrEmpty(title))
                throw new ArgumentNullException("Title cannot be empty");

            Title = title;
            Description = description; 
        }

        public void AssignUser(Guid userId)
        {
            AssignedUserId = userId;
        }

        public void UnassignUser()
        {
            AssignedUserId = null;
        }

        public void ChangeStatus(TaskItemStatus status)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot modify deleted task");

            Status = status;
        }

        public void Delete()
        {
            if (IsDeleted)
                return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
