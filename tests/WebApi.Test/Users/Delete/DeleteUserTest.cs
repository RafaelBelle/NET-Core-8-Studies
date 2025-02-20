using FluentAssertions;
using System.Net;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : CashFlowClassFixture
{
    private const string METHOD = "api/User";

    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
    {
        _token = customWebApplicationFactory.User_Team_Member.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(METHOD, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
