using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Moq;

namespace CommonTestUtilities.Repositories
{
    public class ExpenseReadOnlyRepositoryBuilder
    {
        private readonly Mock<IExpensesReadOnlyRepository> _repository;

        public ExpenseReadOnlyRepositoryBuilder()
        {
            _repository = new Mock<IExpensesReadOnlyRepository>();
        }

        public ExpenseReadOnlyRepositoryBuilder GetAll(User user, List<Expense> expenses)
        {
            _repository.Setup(x => x.GetAll(user)).ReturnsAsync(expenses);
            return this;
        }

        public ExpenseReadOnlyRepositoryBuilder GetById(User user, Expense? expense)
        {
            if (expense != null)
                _repository.Setup(x => x.GetById(user, expense.Id)).ReturnsAsync(expense);

            return this;
        }

        public ExpenseReadOnlyRepositoryBuilder FilterByMonth(User user, List<Expense> expenses)
        {
            _repository.Setup(x => x.FilterByMonth(user, It.IsAny<DateOnly>())).ReturnsAsync(expenses);
            return this;
        }

        public IExpensesReadOnlyRepository Build()
        {
            return _repository.Object;
        }
    }
}
