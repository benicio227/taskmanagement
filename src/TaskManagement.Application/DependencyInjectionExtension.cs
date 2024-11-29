using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.AutoMapper;
using TaskManagement.Application.UseCases.Login;
using TaskManagement.Application.UseCases.Tasks.Delete;
using TaskManagement.Application.UseCases.Tasks.GetAll;
using TaskManagement.Application.UseCases.Tasks.GetById;
using TaskManagement.Application.UseCases.Tasks.Register;
using TaskManagement.Application.UseCases.Tasks.Update;
using TaskManagement.Application.UseCases.TasksCategory.Delete;
using TaskManagement.Application.UseCases.TasksCategory.GetAll;
using TaskManagement.Application.UseCases.TasksCategory.GetById;
using TaskManagement.Application.UseCases.TasksCategory.Register;
using TaskManagement.Application.UseCases.TasksManagement.Register;
using TaskManagement.Application.UseCases.Users.Delete;
using TaskManagement.Application.UseCases.Users.GetById;
using TaskManagement.Application.UseCases.Users.Register;

namespace TaskManagement.Application;
public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapping(services);
        AddUseCase(services);
    }

    private static void AddAutoMapping(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCase(IServiceCollection services)
    {
        services.AddScoped<IRegisterTaskUseCase, RegisterTaskUseCase>();
        services.AddScoped<IGetAllTaskUseCase, GetAllTaskUseCase>();
        services.AddScoped<IGetByIdTaskUseCase, GetByIdTaskUseCase>();
        services.AddScoped<IDeleteTaskUseCase, DeleteTaskUseCase>();
        services.AddScoped<IUpdateTaskUseCase, UpdateTaskUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();

        services.AddScoped<IRegisterTaskCategoryUseCase, RegisterTaskCategoryUseCase>();
        services.AddScoped<IGetAllCategoryUseCase, GetAllCategoryUseCase>();

        services.AddScoped<IGetByIdUserUseCase, GetByIdUserUseCase>();
        services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddScoped<IDoLoginUserUseCase, DoLoginUserUseCase>();
        services.AddScoped<IGetByIdCategoryUseCase, GetByIdCategoryUseCase>();
        services.AddScoped<IDeleteByIdTaskCategoryUseCase, DeleteByIdTaskCategoryUseCase>();


    }
}
