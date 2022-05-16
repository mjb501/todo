using Todo.Api.Requests;

namespace Todo.Data;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> List();
    
    Task<Guid> Create(TodoItem newItem);

    Task Complete(Guid id);
}