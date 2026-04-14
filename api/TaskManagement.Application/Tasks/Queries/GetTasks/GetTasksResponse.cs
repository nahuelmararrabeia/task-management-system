namespace TaskManagement.Application.Tasks.Queries.GetTasks
{
    public record GetTasksResponse(
        IEnumerable<TaskSummaryDTO> Items,
        int TotalCount);
}
