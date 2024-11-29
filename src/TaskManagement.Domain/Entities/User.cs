namespace TaskManagement.Domain.Entities;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<Task> Tasks { get; set; } = new List<Task>();

    public ICollection<TaskCategory> Categories { get; set; } = new List<TaskCategory>();


}
