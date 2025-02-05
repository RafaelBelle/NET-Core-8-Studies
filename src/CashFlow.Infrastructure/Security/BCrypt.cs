using CashFlow.Domain.Security.Crypyography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security;
internal class BCrypt : IPasswordEncrypter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
