namespace GameClient.Exceptions;

public class LoggingException : Exception
{
    protected LoggingException(string? message) : base(message)
    {
        if (message != null) Logger.Error(message);
    }
}