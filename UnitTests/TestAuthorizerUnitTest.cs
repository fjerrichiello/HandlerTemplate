using Common.Authorization;
using Common.Messaging;
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

        var result = await _testTestAuthorizer.AuthorizeAsync(testData, async result =>
        {
            await mock.PublishAsync(new FailedEvent(string.Join(", ",
                result.Errors.Select(x => $"{x.PropertyName}: {x.ErrorMessage}"))));
        });

        Assert.False(result);

        Mock.Get(mock).Verify(x => x.PublishAsync(It.IsAny<FailedEvent>()), Times.Once);
    }
}