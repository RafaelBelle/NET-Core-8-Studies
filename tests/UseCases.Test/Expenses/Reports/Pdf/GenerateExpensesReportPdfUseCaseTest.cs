﻿using CashFlow.Application.UseCases.Expenses.Reports.Pdf;
using CashFlow.Domain.Entities;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using FluentAssertions;

namespace UseCases.Test.Expenses.Reports.Pdf
{
    public class GenerateExpensesReportPdfUseCaseTest
    {
        [Fact]
        public async Task Success()
        {
            var loggedUser = UserBuilder.Build();
            var expenses = ExpenseBuilder.Collection(loggedUser);

            var useCase = CreateUseCase(loggedUser, expenses);

            var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Now));

            result.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Success_Empty()
        {
            var loggedUser = UserBuilder.Build();

            var useCase = CreateUseCase(loggedUser, new List<Expense>());

            var result = await useCase.Execute(DateOnly.FromDateTime(DateTime.Now));

            result.Should().BeEmpty();
        }

        private GenerateExpensesReportPdfUseCase CreateUseCase(User user, List<Expense> expenses)
        {
            var repository = new ExpenseReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
            var loggedUser = LoggedUserBuilder.Build(user);

            return new GenerateExpensesReportPdfUseCase(repository, loggedUser);
        }
    }
}
