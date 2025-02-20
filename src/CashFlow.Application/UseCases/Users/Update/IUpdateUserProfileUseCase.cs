using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Users.Update;

public interface IUpdateUserProfileUseCase
{
    Task Execute(RequestUpdateUserJson request);
}
