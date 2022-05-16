using Microsoft.AspNetCore.Mvc;
using Todo.Api.Requests;

namespace Todo.Api.Controllers;

[ApiController]
public class TodoController : ControllerBase
{
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("todo/list")]
    public async Task<IActionResult> Get([FromQuery] ListTodoItemsRequest request) =>
        Ok(await _mediator.Send(request));
    
    [HttpPost("todo/create")]
    public async Task<IActionResult> Get([FromBody] CreateTodoItemRequest request) =>
        Ok(await _mediator.Send(request));

    [HttpPost("todo/complete")]
    public async Task<IActionResult> Get([FromBody] CompleteTodoItemRequest request) =>
        Ok(await _mediator.Send(request));
}