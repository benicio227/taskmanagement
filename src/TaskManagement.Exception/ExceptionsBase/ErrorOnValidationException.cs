namespace TaskManagement.Exception.ExceptionsBase;
public class ErrorOnValidationException : TaskManagementException
{
    public List<string> Errors { get; set; }
    public ErrorOnValidationException(List<string> errorMessages)
    {
        Errors = errorMessages;
    }
}
