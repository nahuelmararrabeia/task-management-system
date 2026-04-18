using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands.AssignUser;
using TaskManagement.Application.Tasks.Commands.ChangeStatus;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Commands.DeleteTask;
using TaskManagement.Application.Tasks.Commands.UpdateTask;
using TaskManagement.Application.Tasks.Queries.GetTaskById;
using TaskManagement.Application.Tasks.Queries.GetTasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;

    public TasksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<CreateTaskResponse>> Create(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Update(Guid id, UpdateTaskCommand command)
    {
        if (id != command.Id)
            return BadRequest();

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteTaskCommand(id));
        return NoContent();
    }

    [HttpPatch("{taskId:guid}/assign/{userId:guid}")]
    [Authorize]
    public async Task<IActionResult> AssignUser(Guid taskId, Guid userId, CancellationToken cancellationToken)
    {
        var command = new AssignUserToTaskCommand(taskId, userId);
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpPatch("{taskId:guid}/status")]
    [Authorize]
    public async Task<IActionResult> ChangeStatus(Guid taskId, ChangeTaskStatusCommand command)
    {
        var cmd = command with { TaskId = taskId };
        await _mediator.Send(cmd);
        return NoContent();
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<GetTaskByIdResponse>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id));

        return Ok(result);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetTasksQuery(page, pageSize);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}