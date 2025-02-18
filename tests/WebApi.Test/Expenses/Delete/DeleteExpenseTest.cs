using CashFlow.Exception;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.Delete
{
    public class DeleteExpenseTest : CashFlowClassFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

        public DeleteExpenseTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            _token = customWebApplicationFactory.User_Team_Member.GetToken();
            _expenseId = customWebApplicationFactory.Expense_TeamMember.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoDelete($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.NoContent);

            result = await DoGet($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var result = await DoDelete($"{METHOD}/{100}", _token, culture);

            result.StatusCode.Should().Be(HttpStatusCode.NotFound);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            var errors = response.RootElement.GetProperty("errorMessages").EnumerateArray();

            var expectedMessage = ResourceErrorMessages.ResourceManager.GetString("EXPENSE_NOT_FOUND", new CultureInfo(culture));

            errors.Should().ContainSingle().Which.GetString().Should().Be(expectedMessage);
        }
    }
}
