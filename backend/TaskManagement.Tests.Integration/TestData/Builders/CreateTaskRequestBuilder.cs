using TaskManagement.Application.Tasks.Commands.CreateTask;

namespace TaskManagement.Tests.Integration.TestData.Builders;

public class CreateTaskRequestBuilder
{
    private string _title = "Default Task";
    private string _description = "Default Description";

    public CreateTaskRequestBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }

    public CreateTaskRequestBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public CreateTaskCommand Build()
    {
        return new CreateTaskCommand(_title, _description);
    }
}