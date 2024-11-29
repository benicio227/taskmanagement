namespace TaskManagement.Domain.Entities;
public class TaskCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    public long UserId {  get; set; }
    public User User { get; set; } = null!;

    public ICollection<Task> Tasks { get; set; } = new List<Task>(); // Relacionamento com a tabela Task
    //A coleção Tasks existe porque representa a relação entre uma categoria e as tarefas associadas a ela (relação um para muitos)
    //Exemplo: Uma categoria chamada "Trabalho" pode ter várias tarefas, como "Enviar relatório", "Participar de reunião"
    //Essa coleção serve para armazenar todas as tarefas que pertencem a uma categoria específica
    //Isso é útil quando você precisa, por exemplo, listar todas as tarefas de uma determinada categoria.

}
