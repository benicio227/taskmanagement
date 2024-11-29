namespace TaskManagement.Communication.Responses;
public class ResponseTasksJson
{
    public List<ResponseShortTaskJson> Tasks { get; set; } = new List<ResponseShortTaskJson>();
}
