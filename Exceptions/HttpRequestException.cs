namespace GameClient.Exceptions
{
    public class CardRequestException(string? message) : LoggingException(message) {}
    public class DeckRequestException(string? message) : LoggingException(message) {}
}