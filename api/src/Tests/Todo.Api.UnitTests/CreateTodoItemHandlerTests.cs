using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Todo.Api.Handlers;
using Todo.Api.Requests;
using Todo.Data;

namespace Todo.Api.UnitTests
{
    [TestClass]
    public class CreateTodoItemHandlerTests
    {
        [DataTestMethod]
        [DataRow("tttt")]
        [DataRow("")]
        [DataRow("t3a4b5")]
        [DataRow("3&amp;amp;5*")]
        [DataRow("123")]
        [DataRow(null)]
        public async Task Should_EnforceUppercaseOnText_When_CreatingTodoItem(string text)
        {
            var expected = text?.ToUpper();
            var mockRepo = new Mock<ITodoRepository>();

            var sut = new CreateTodoItemHandler(mockRepo.Object);
            var result = await sut.Handle(new CreateTodoItemRequest { Text = text }, new CancellationToken());

            mockRepo.Verify(repo => repo.Create(It.Is<TodoItem>(item => item.Text == expected)));
        }
    }
}