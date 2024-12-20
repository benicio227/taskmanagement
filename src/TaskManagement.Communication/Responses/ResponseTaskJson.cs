﻿namespace TaskManagement.Communication.Responses;
public class ResponseTaskJson
{
    public int Id {  get; set; }
    public string Title {  get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Description {  get; set; } = string.Empty;
}
