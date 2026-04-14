using FluentAssertions;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.Domain.Tasks
{
    public class UpdateTaskTests
    {
        [Fact]
        public void Should_Update_Task_Properly()
        {
            var task = new TaskItem("Old", "Desc");

            task.Update("New", "New Desc");

            task.Title.Should().Be("New");
        }

        [Fact]
        public void Should_Throw_When_Title_Is_Empty()
        {
            var task = new TaskItem("Old", "Desc");

            Assert.Throws<ArgumentNullException>(() =>
                task.Update("", "Desc"));
        }
    }
}
