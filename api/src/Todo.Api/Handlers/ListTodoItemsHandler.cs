using Todo.Api.Requests;

namespace Todo.Api.Handlers;

public class ListTodoItemsHandler : IRequestHandler<ListTodoItemsRequest, IEnumerable<TodoItem>>
{
    private readonly ITodoRepository _todoRepository;

    public ListTodoItemsHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoItem>> Handle(ListTodoItemsRequest request, CancellationToken cancellationToken)
    {
        var list = await _todoRepository.List();
        return list.Where(item => PassesFilter(request.IncludeCompleted, item));
    }

    private static bool PassesFilter(bool includeCompleted, TodoItem item)
    {
        return includeCompleted || (!includeCompleted && item.Completed == null);
    }
}