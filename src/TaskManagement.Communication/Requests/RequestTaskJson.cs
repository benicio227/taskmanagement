using TaskManagement.Communication.Enums;

namespace TaskManagement.Communication.Requests;
public class RequestTaskJson
{
    public string Title {  get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date {  get; set; }
    public TaskType Type { get; set; }

    public int CategoryId { get; set; }
    //public long UserId { get; set; }
}
