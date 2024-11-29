using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Domain.Repositories;
using TaskManagement.Domain.Repositories.Tasks;
using TaskManagement.Domain.Repositories.TasksCategory;
using TaskManagement.Domain.Repositories.Users;
using TaskManagement.Domain.Security.Cryptography;
using TaskManagement.Domain.Security.Tokens;
using TaskManagement.Domain.Services.LoggedUser;
using TaskManagement.Infrastructure.DataAccess;
using TaskManagement.Infrastructure.DataAccess.Repositories;
using TaskManagement.Infrastructure.Services.LoggedUser;
using TaskManagement.Infrastructure.Tokens;

namespace TaskManagement.Infrastructure;
public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddToken(services, configuration);

        services.AddScoped<IPasswordEncripter, Security.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpiresMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<ITasksReadOnlyRepository, TaskRepository>();
        services.AddScoped<ITasksWriteOnlyRepository, TaskRepository>();
        services.AddScoped<ITasksUpdateOnlyRepository, TaskRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITasksCategoryWriteOnlyRepository, TaskCategoryRepository>();
        services.AddScoped<ITasksCategoryReadOnlyRepository, TaskCategoryRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();

    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        var version = new Version("8.0.39");
        var serverVersion = new MySqlServerVersion(version);


        services.AddDbContext<TaskManagementDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
