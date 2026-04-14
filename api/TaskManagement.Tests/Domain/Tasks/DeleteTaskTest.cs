using FluentAssertions;
using TaskManagement.Tests.Unit.TestData.Builders;

namespace TaskManagement.Tests.Unit.Domain.Tasks
{
    public class DeleteTaskTest
    {
        [Fact]
        public void Should_Delete_Task_Properly()
        {
            var task = new TaskItemBuilder().Build();

            task.Delete();

            task.IsDeleted.Should().BeTrue();
            task.DeletedAt.Should().NotBeNull();
        }

        [Fact]
        public void Should_Not_Modify_DeletedAt_When_Task_AlreadyDeleted()
        {
            var task = new TaskItemBuilder().Build();

            task.Delete();
            var firstDeletedAt = task.DeletedAt;

            task.DeletedAt.Should().NotBeNull();

            task.Delete();

            task.DeletedAt.Should().Be(firstDeletedAt);
            task.IsDeleted.Should().BeTrue();
        }
    }
}
