using TaskManagement.Domain.Entities;

namespace TaskManagement.Domain.Security.Tokens;
public interface IAccessTokenGenerator
{
    string Generate(User user);
}
