namespace CashFlow.Domain.Security.Crypyography;
public interface IPasswordEncrypter
{
    string Encrypt(string password);
}
