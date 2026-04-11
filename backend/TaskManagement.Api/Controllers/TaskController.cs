using Microsoft.AspNetCore.Mvc;
using MediatR;
using TaskManagement.Application.Tasks.CreateTask;
using TaskManagement.Application.Tasks.GetTaskById;

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
        var result = await _mediator.Send(new GetTaskByIdCommand(id));

        return Ok(result);
    }
}