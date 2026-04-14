using FluentAssertions;
using TaskManagement.Domain.Entities;
using TaskManagement.Tests.Unit.TestData.Builders;

namespace TaskManagement.Tests.Unit.Domain.Users
{
    public class UpdateUserTest
    {
        [Fact]
        public void Should_Update_User_Properly()
        {
            var newName = "newName";
            var newEmail = "newemail@new.com";
            var user = new UserBuilder().Build();

            user.Update("newName", "newemail@new.com");

            user.Name.Should().Be(newName);
            user.Email.Should().Be(newEmail);
        }

        [Fact]
        public void Should_Throw_When_Name_Is_Empty()
        {
            var user = new UserBuilder().Build();

            Assert.Throws<ArgumentNullException>(() =>
                user.Update("", "email@email.com"));
        }

        [Fact]
        public void Should_Throw_When_Email_Is_Empty()
        {
            var user = new UserBuilder().Build();

            Assert.Throws<ArgumentNullException>(() =>
                user.Update("name", ""));
        }
    }
}
