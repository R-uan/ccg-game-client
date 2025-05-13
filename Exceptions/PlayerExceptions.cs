namespace GameClient.Exceptions {
    public class UnauthenticatedPlayerException(string? message) : Exception(message) {}
    public class PlayerProfileNotFoundException(string? message) : Exception(message) {}
    
}
