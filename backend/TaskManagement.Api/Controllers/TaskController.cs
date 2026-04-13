using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Tasks.Commands.CreateTask;
using TaskManagement.Application.Tasks.Queries.GetTasks;
using TaskManagement.Application.Tasks.Queries.GetTaskById;

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
    public async Task<ActionResult<CreateTaskResponse>> Create(CreateTaskCommand command)
    {
        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetTaskByIdResponse>> GetById(Guid id)
    {
        var result = await _mediator.Send(new GetTaskByIdQuery(id));

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetTasksQuery(page, pageSize);

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}