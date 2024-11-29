
namespace TaskManagement.Exception.ExceptionsBase
{
    public class InvalidLoginException : TaskManagementException
    {
        public string Error { get; set; }
        public InvalidLoginException(string errorMessage)
        {
            Error = errorMessage;
        }   
    }
}
