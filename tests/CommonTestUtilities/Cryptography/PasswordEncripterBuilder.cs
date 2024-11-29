using Moq;
using TaskManagement.Domain.Security.Cryptography;

namespace CommonTestUtilities.Cryptography;
public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(passwordEncrypter => passwordEncrypter.Encrypt(It.IsAny<string>())).Returns("!@#123dgrv");
    }

    public PasswordEncripterBuilder Verify(string password)
    {
        _mock.Setup(passwordEncrypter => passwordEncrypter.Verify(password, It.IsAny<string>())).Returns(true);

        return this;
    }
    public IPasswordEncripter Build()
    {
        return _mock.Object;
    }
}
