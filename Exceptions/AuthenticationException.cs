namespace GameClient.Exceptions  {
    public class FailedLoginException(string? message) : LoggingException(message) {}
    public class FailedRegistrationException(string? message) : LoggingException(message) {}
    public class UnauthorizedTokenException(string? message) : LoggingException(message) {}
    public class PlayerNotAuthenticatedException(string? message) : LoggingException(message) {}
}
