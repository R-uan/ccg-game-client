namespace GameClient.Requests
{
    public record LoginRequest(string Email, string Password);
    public record LoginRespose(string Token);
}