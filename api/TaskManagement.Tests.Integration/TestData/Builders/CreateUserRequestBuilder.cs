using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.Users.Commands.CreateUser;

namespace TaskManagement.Tests.Integration.TestData.Builders
{
    public class CreateUserRequestBuilder
    {
        private string _email = $"test{Guid.NewGuid()}@test.com";
        private string _name = "Test User";
        private string _password = "Password123!";

        public CreateUserRequestBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public CreateUserRequestBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CreateUserRequestBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public CreateUserCommand Build()
        {
            return new CreateUserCommand(_email, _name, _password);
        }
    }
}
