using Todo.Api.Requests;

namespace Todo.Api.Handlers;

public class CompleteTodoItemsHandler : IRequestHandler<CompleteTodoItemRequest>
{
    private readonly ITodoRepository _todoRepository;

    public CompleteTodoItemsHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<Unit> Handle(CompleteTodoItemRequest request, CancellationToken cancellationToken)
    {
        var valid = Guid.TryParse(request.Id, out var itemId);
        if (valid)
        {
            await _todoRepository.Complete(itemId);
        }
        return Unit.Value;
    }
}