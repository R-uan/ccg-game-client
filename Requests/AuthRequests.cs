namespace GameClient.Requests
{
    public record LoginRespose(string Token);
    public record LoginRequest(string Email, string Password);

    public record RegisterRespose(string Token);
    public record RegisterRequest(string Email, string Username, string Password);

}