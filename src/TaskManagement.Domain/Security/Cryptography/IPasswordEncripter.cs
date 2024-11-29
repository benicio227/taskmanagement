namespace TaskManagement.Domain.Security.Cryptography;
public interface IPasswordEncripter
{
    string Encrypt(string Password);

    bool Verify(string password, string passwordHash);
}
