namespace TaskManagement.Domain.Security.Tokens;
public interface ITokenProvider
{
    string TokenOnRequest();
}
