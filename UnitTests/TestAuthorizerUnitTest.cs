using Common.Authorization;
using Common.Messaging;
using HandlerTemplate.Commands;
using HandlerTemplate.Events;
using Moq;

namespace UnitTests;

public class TestAuthorizerUnitTest
{
    private readonly TestAuthorizer _testTestAuthorizer = new TestAuthorizer();

    [Fact]
    public async Task Test1()
    {
        var testData = new TestData(null);

        var result = await _testTestAuthorizer.AuthorizeAsync(testData);

        Assert.False(result);
    }


    [Fact]
    public async Task Test2()
    {
        var testData = new TestData(null);

        var mock = Mock.Of<IEventPublisher>();

        var commandContainer = new MessageContainer<AddCommand, CommandMetadata>(new AddCommand(null),
            new CommandMetadata([], String.Empty, Guid.NewGuid()), new MessageSource(Guid.NewGuid()));

        var result = await _testTestAuthorizer.AuthorizeAsync(testData, async result =>
        {
            await mock.PublishAsync(commandContainer, new FailedEvent(string.Join(", ",
                result.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"))));
        });

        Assert.False(result);

        Mock.Get(mock).Verify(x => x.PublishAsync(commandContainer, It.IsAny<FailedEvent>()), Times.Once);
    }
}

file record FailedEvent(string Reason) : Message;