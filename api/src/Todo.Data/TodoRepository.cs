using Microsoft.EntityFrameworkCore;
using Todo.Api.Requests;

namespace Todo.Data;

public class TodoRepository: ITodoRepository
{
    private readonly TodoContext _context;

    public TodoRepository(TodoContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> List()
    {
        return await _context
            .TodoItems
            .ToArrayAsync();
    }

    public async Task<Guid> Create(TodoItem newItem)
    {
        await _context.TodoItems.AddAsync(newItem);
        await _context.SaveChangesAsync();
        return newItem.Id;
    }

    public async Task Complete(Guid itemId)
    {
        var todoItem = await _context.TodoItems.FirstOrDefaultAsync(item => item.Id == itemId);
        if (todoItem == null)
            return;
        todoItem.Completed = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}