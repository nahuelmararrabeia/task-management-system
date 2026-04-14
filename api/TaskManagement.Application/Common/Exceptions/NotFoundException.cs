namespace TaskManagement.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, string key, string value)
        : base($"{name} with {key} '{value}' was not found.")
    {
    }
}