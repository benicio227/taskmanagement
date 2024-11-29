using TaskManagement.Communication.Enums;

namespace TaskManagement.Communication.Responses;
public class ResponseShortTaskJson
{
    public string Title { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Type { get; set; } = string.Empty;
}
