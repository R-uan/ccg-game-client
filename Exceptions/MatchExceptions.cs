namespace GameClient.Exceptions
{
    public class PlayerConnectionException(string? message) : LoggingException(message) {}
    public class ServerConnectionException(string? message) : LoggingException(message) {}
}