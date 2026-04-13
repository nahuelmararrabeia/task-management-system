using FluentValidation;

namespace TaskManagement.Application.Users.Commands
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() {
            RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Password)
                .MinimumLength(6);
        }
    }
}
