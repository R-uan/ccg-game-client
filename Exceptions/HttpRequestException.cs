namespace GameClient.Exceptions
{
    public class CardRequestException(string? message) : LoggingException(message) {}
    public class DeckRequestException(string? message) : LoggingException(message) {}
    
    public class ParsingResponseException(string? message) : LoggingException(message) {}

    public class FailedLoginRequestException(string? message) : LoggingException(message) {}
    public class FailedRegistrationRequestException(string? message) : LoggingException(message) {}
    
    public class UnauthenticatedPlayerException(string? message) : Exception(message) {}
    public class PlayerProfileNotFoundException(string? message) : Exception(message) {}
}