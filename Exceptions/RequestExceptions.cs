namespace GameClient.Exceptions
{
    public class ParsingResponseException(string? message) : LoggingException(message) {}
}