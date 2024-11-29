namespace TaskManagement.Exception.ExceptionsBase;
public class NotFoundException : TaskManagementException
{
    public string Error { get; set; }
    public NotFoundException(string errorMessage)
    {
        Error = errorMessage;
    }
}
