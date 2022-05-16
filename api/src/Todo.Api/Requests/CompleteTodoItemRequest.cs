namespace Todo.Api.Requests;

public class CompleteTodoItemRequest : IRequest
{
    public string Id { get; set; }
}