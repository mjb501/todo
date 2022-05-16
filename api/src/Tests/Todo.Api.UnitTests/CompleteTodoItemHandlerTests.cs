using Moq;
using Todo.Api.Handlers;
using Todo.Api.Requests;
using Todo.Data;

namespace Todo.Api.UnitTests
{
    [TestClass]
    public class CompleteTodoItemHandlerTests
    {
        [DataTestMethod]
        [DataRow("tttt", false)]
        [DataRow("", false)]
        [DataRow("F127A464-AA83-4B8E-B194-10ECA2A8F009", true)]
        [DataRow(null, false)]
        public async Task Should_CallComplete_When_GuidIsValid(string id, bool isValid)
        {
            var expectedResult = isValid ? Times.Once() : Times.Never();

            var mockRepo = new Mock<ITodoRepository>();

            var sut = new CompleteTodoItemsHandler(mockRepo.Object);

            var result = await sut.Handle(new CompleteTodoItemRequest { Id = id }, new CancellationToken());

            mockRepo.Verify(repo => repo.Complete(It.IsAny<Guid>()), expectedResult);
        }
    }
}