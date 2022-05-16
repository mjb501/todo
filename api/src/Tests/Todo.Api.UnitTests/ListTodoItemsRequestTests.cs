using Moq;
using Todo.Api.Handlers;
using Todo.Api.Requests;
using Todo.Data;

namespace Todo.Api.UnitTests
{
    [TestClass]
    public class ListTodoItemsRequestTests
    {
        [TestMethod]
        public async Task Should_ReturnAllItems_When_IncludingCompleted()
        {
            await ListReturnItems(new ListTodoItemsRequest { IncludeCompleted = true }, i => true);
        }

        [TestMethod]
        public async Task Should_ReturnOnlyUncompletedItems_When_NotIncludingCompleted()
        {
            await ListReturnItems(new ListTodoItemsRequest { IncludeCompleted = false }, i => i.Completed == null);
        }

        [TestMethod]
        public async Task Should_ReturnOnlyUncompletedItems_When_Default()
        {
            await ListReturnItems(new ListTodoItemsRequest(), i => i.Completed == null);
        }

        [TestMethod]
        public async Task Should_ReturnItemsInDescendingOrderOfCreation()
        {
            var todoItems = CreateTestItems();
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.List()).Returns(Task.FromResult(todoItems));

            var sut = new ListTodoItemsHandler(mockRepo.Object);
            var request = new ListTodoItemsRequest { IncludeCompleted = true };
            var result = await sut.Handle(request, new CancellationToken());

            var expected = todoItems.OrderByDescending(i => i.Created).ToList();

            CollectionAssert.AreEqual(result.ToList(), expected);
        }

        private async Task ListReturnItems(ListTodoItemsRequest request, Func<TodoItem, bool> filter)
        {
            var todoItems = CreateTestItems();
            var mockRepo = new Mock<ITodoRepository>();
            mockRepo.Setup(repo => repo.List()).Returns(Task.FromResult(todoItems));

            var sut = new ListTodoItemsHandler(mockRepo.Object);
            var result = await sut.Handle(request, new CancellationToken());

            var expected = todoItems.OrderByDescending(i => i.Created).
                Where(filter).ToList();

            CollectionAssert.AreEqual(result.ToList(), expected);
        }

        private IEnumerable<TodoItem> CreateTestItems()
        {
            return new List<TodoItem>
            {
                new TodoItem {
                    Completed = DateTime.Today.AddDays(-5),
                    Created = DateTime.Today.AddDays(-5)
                },
                new TodoItem {
                    Completed = null,
                    Created = DateTime.Today.AddDays(-3)
                },
                new TodoItem {
                    Completed = DateTime.Today.AddDays(-1),
                    Created = DateTime.Today.AddDays(-1)
                }
            };
        }
    }
}