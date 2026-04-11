namespace TaskManagement.Application.Tasks.GetTaskById;

public record GetTaskByIdResponse(
    Guid Id,
    string Title,
    string Description,
    DateTimeOffset CreatedAt
);