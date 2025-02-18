using CashFlow.Exception;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace CashFlow.Application.UseCases.Users.Register;
public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ERROR_MESSAGE_KEY = "ErrorMessage";
    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{ERROR_MESSAGE_KEY}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        bool passwordIsNull = string.IsNullOrWhiteSpace(password);

        if (passwordIsNull)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        bool passwordIsLesserThanMinimumCharacters = password.Length < 8;
        bool passwordContainCapitalLetter = Regex.IsMatch(password, @"[A-Z]+");
        bool passwordContainLowercaseLetter = Regex.IsMatch(password, @"[a-z]+");
        bool passwordContainDigit = Regex.IsMatch(password, @"[0-9]+");
        bool passwordContainSpecialSymbol = Regex.IsMatch(password, @"[\!\@\#\$\%\.\*]+");

        if (passwordIsLesserThanMinimumCharacters
            || passwordContainCapitalLetter == false
            || passwordContainLowercaseLetter == false
            || passwordContainDigit == false
            || passwordContainSpecialSymbol == false)
        {
            context.MessageFormatter.AppendArgument(ERROR_MESSAGE_KEY, ResourceErrorMessages.INVALID_PASSWORD);
            return false;
        }

        return true;
    }
}
