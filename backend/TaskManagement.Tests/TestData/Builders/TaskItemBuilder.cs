using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.TestData.Builders
{
    public class TaskItemBuilder
    {
        private string _title = "Default Task";
        private string _description = "Default Description";
        private User? _assignedUser;

        public TaskItemBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public TaskItemBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public TaskItemBuilder WithAssignedUser(User user)
        {
            _assignedUser = user;
            return this;
        }

        public TaskItem Build()
        {
            var task = new TaskItem(_title, _description);

            if(_assignedUser is not null)
            {
                task.AssignUser(_assignedUser.Id);
                typeof(TaskItem)
                    .GetProperty(nameof(TaskItem.AssignedUser))!
                    .SetValue(task, _assignedUser);
            }

            return task;
        }
    }
}
