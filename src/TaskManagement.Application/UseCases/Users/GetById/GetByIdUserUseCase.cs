using AutoMapper;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Repositories.Users;

namespace TaskManagement.Application.UseCases.Users.GetById;
public class GetByIdUserUseCase : IGetByIdUserUseCase
{
    private readonly IUserReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    public GetByIdUserUseCase(
        IUserReadOnlyRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseUserJson> Execute(long id)
    {
        var user = await _repository.GetById(id);

        return _mapper.Map<ResponseUserJson>(user);
    }
}
