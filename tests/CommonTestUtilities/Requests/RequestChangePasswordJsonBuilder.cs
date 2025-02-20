using Bogus;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestChangePasswordJsonBuilder
{
    public static RequestChangePasswordJson Build()
    {
        return new Faker<RequestChangePasswordJson>()
            .RuleFor(x => x.Password, f => f.Internet.Password())
            .RuleFor(x => x.NewPassword, f => f.Internet.Password(prefix: "!Aa1"));
    }
}
