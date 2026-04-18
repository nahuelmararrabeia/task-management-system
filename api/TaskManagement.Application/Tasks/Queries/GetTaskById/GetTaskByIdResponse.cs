using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.Tasks.Queries.GetTaskById;

public record GetTaskByIdResponse(
    Guid Id,
    string Title,
    string Description,
    DateTimeOffset CreatedAt,
    AssignedUserDTO? AssignedUser,
    string Status
);