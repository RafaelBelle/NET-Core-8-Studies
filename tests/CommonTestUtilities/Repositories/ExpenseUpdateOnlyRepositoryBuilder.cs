using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpenseUpdateOnlyRepositoryBuilder
    {
        private readonly Mock<IExpensesUpdateOnlyRepository> _repository;

        public ExpenseUpdateOnlyRepositoryBuilder()
        {
            _repository = new Mock<IExpensesUpdateOnlyRepository>();
        }

        public ExpenseUpdateOnlyRepositoryBuilder GetById(User user, Expense? expense)
        {
            if (expense != null)
                _repository.Setup(x => x.GetById(user, expense.Id)).ReturnsAsync(expense);

            return this;
        }

        public IExpensesUpdateOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
