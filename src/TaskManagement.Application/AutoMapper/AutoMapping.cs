using AutoMapper;
using TaskManagement.Communication.Requests;
using TaskManagement.Communication.Responses;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        // Mapeando TaskCategory para ResponseTaskCategoryJson
        CreateMap<TaskCategory, ResponseTaskCategoryJson>();

        // Mapeamento das Tasks em uma categoria
        CreateMap<TaskCategory, ResponseCategorysJson>()
            .ForMember(dest => dest.Categorys, opt => opt.MapFrom(src => src.Tasks));

        //        Origem dos dados                Destino dos dados
        CreateMap<RequestTaskJson, Domain.Entities.Task>();

        CreateMap<RequestCategoryJson, TaskCategory>();

        CreateMap<RequestUserJson, User>()
            .ForMember(destino => destino.Password, configuracao => configuracao.Ignore());
            //Aqui o automapper vai mapear todas as propriedades, mas para a propriedade password no destino(User)
            //Eu quero que ele ignore essa propriedade Password.
    }

    private void EntityToResponse()
    {
        //                   (Task) Origem dos dados (ResponseTaskJson) Destino dos dados
        CreateMap<Domain.Entities.Task, ResponseTaskJson>();

        CreateMap<Domain.Entities.Task, ResponseShortTaskJson>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()));

        // Corrigido: Mapeamento correto para a lista de Tasks em TaskCategory
        CreateMap<TaskCategory, ResponseCategorysJson>()
            .ForMember(dest => dest.Categorys, opt => opt.MapFrom(src => src.Tasks)); // Mapear Tasks para Categorys, se necessário
        
        CreateMap<TaskCategory, ResponseShortCategoryJson>()
           .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));


        //TaskCategory (a entidade, geralmente vinda do banco de dados)
        //ResponseShortCategoryJson (o objeto de transferência de dados, ou DTO, usado para responder na API)
        //Quando as propriedades entre objetos tem tipos diferentes, temos que mapear manualmente:
        //.ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks))
        //dest.Tasks -> Destino: A propriedade Tasks no DTO ResponseShortCategoryJson
        //Exemplo: public List<ResponseShortTaskJson> Tasks { get; set; }
        //src.Tasks → Fonte: A propriedade Tasks na entidade TaskCategory.
        //Exemplo: public ICollection<Task> Tasks { get; set; }
        CreateMap<TaskCategory, ResponseShortCategoryJson>()
            .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));

        // Certifique-se de mapear TaskCategory para ResponseTaskCategoryJson também
        CreateMap<TaskCategory, ResponseTaskCategoryJson>();

        CreateMap<TaskCategory, ResponseShortCategoryJson>();

        CreateMap<User, ResponseUserJson>();

    }
}
