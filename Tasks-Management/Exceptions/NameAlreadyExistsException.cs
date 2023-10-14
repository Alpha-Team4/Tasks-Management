namespace TasksManagement.Exceptions;
public class NameAlreadyExistsException : ApplicationException
{
    public NameAlreadyExistsException(string message)
        : base(message)
    {
    }
}
