using FluentValidation;

namespace TaskManagement.Application.Tasks.Commands.AssignUser
{
    public class AssignUserToTaskValidator : AbstractValidator<AssignUserToTaskCommand>
    {
        public AssignUserToTaskValidator()
        {
            RuleFor(x => x.TaskId)
                .NotEmpty().WithMessage("TaskId is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");
        }
    }
}
