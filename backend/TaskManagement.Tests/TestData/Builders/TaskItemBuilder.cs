using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.TestData.Builders
{
    public class TaskItemBuilder
    {
        private string _title = "Default Task";
        private string _description = "Default Description";

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

        public TaskItem Build()
        {
            return new TaskItem(_title, _description);
        }
    }
}
