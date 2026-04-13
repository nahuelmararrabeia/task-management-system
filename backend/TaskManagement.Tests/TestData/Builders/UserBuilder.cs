namespace TaskManagement.Tests.Unit.TestData.Builders
{
    public class UserBuilder
    {
        private string _name = "username";
        private string _email = "test@test.com";
        private string _passwordHash = "AQAAAAIAAYagAAAAEKf4illNxma4nvTOkXAlVmsHv6tsvAZrJ/D/+Ld4YB2oKSBoRReADcEfh9IVh9WjmA==";

        public UserBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPasswordHash(string passwordHash)
        {
            _passwordHash = passwordHash;
            return this;
        }

        public User Build()
        {
            return new User(_email, _name, _passwordHash);
        }
    }
}
