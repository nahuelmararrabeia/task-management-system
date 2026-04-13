namespace TaskManagement.Application.Tasks.Queries.GetTasks
{
    public record TaskSummaryDTO(
        Guid Id,
        string Title,
        string Description,
        DateTimeOffset CreatedAt
    );
}
