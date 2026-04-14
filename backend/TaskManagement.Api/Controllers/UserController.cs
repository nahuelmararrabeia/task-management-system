using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Users.Commands.CreateUser;
using TaskManagement.Application.Users.Commands.LoginUser;

namespace TaskManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create(CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginUserResponse>> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
