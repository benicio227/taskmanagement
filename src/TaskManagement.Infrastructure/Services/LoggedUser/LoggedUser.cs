using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Security.Tokens;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Infrastructure.DataAccess;

namespace TaskManagement.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
{
    private readonly TaskManagementDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(TaskManagementDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    public async Task<User> Get()
    {

        var token = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(token);

        var identifier = jwtSecurityToken.Claims.First(clain => clain.Type == ClaimTypes.Sid).Value;

        return await _dbContext.Users.AsNoTracking().FirstAsync(user => user.Id == int.Parse(identifier));
    }
}
