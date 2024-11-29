namespace TaskManagement.Communication.Responses;
public class ResponseShortCategoryJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<ResponseShortTaskJson> Tasks { get; set; } = new List<ResponseShortTaskJson>();
}
