using CashFlow.Domain.Enums;
using FluentAssertions;
using System.Net;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.Expenses.GetById
{
    public class GetExpenseByIdTest : CashFlowClassFixture
    {
        private const string METHOD = "api/Expenses";

        private readonly string _token;
        private readonly long _expenseId;

        public GetExpenseByIdTest(CustomWebApplicationFactory customWebApplicationFactory) : base(customWebApplicationFactory)
        {
            _token = customWebApplicationFactory.User_Team_Member.GetToken();
            _expenseId = customWebApplicationFactory.Expense_TeamMember.GetId();
        }

        [Fact]
        public async Task Success()
        {
            var result = await DoGet($"{METHOD}/{_expenseId}", _token);

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = await result.Content.ReadAsStreamAsync();

            var response = await JsonDocument.ParseAsync(body);

            response.RootElement.GetProperty("id").GetInt64().Should().Be(_expenseId);
            response.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("description").GetString().Should().NotBeNullOrWhiteSpace();
            response.RootElement.GetProperty("date").GetDateTime().Should().NotBeAfter(DateTime.Today);
            response.RootElement.GetProperty("amount").GetDecimal().Should().BeGreaterThan(0);

            var paymentType = response.RootElement.GetProperty("paymentType").GetInt32();
            Enum.IsDefined(typeof(PaymentType), paymentType).Should().BeTrue();
        }

        [Theory]
        [ClassData(typeof(CultureInlineDataTest))]
        public async Task Error_Expense_Not_Found(string culture)
        {
            var result = await DoGet($"{METHOD}/{123}", _token, culture);
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
