namespace TaskManagement.Application.Tasks.Queries.GetTaskById;

public record GetTaskByIdResponse(
    Guid Id,
    string Title,
    string Description,
    DateTimeOffset CreatedAt
);