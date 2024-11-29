using System.Text.Json.Serialization;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities;
public class Task
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;

    public DateTime Date { get; set; }
    public TaskType Type { get; set; }
    
    public long UserId {  get; set; }
    public User User { get; set; } = null!;

   [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    //A chave estrangeira CategoryId está na tabela de Task porque o relacionamento entre tarefas e categorias é do tipo "muitos-para-um"
    //relacionamento "muitos-para-um": Uma categoria pode ter muitas tarefas | Uma tarefa só pode pertencer a uma única categoria
    //O lado "muitos" do relacionamento (tarefas) precisa guardar a referência para identificar a que categoria pertence
    public TaskCategory Category { get; set; } = null!; // Relacionamento com a tabela TaskCategory
    //A propriedade Category é usada para representar o objeto completo da categoria a que a tarefa pertence

}
