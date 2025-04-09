namespace GameClient.Requests
{
    public record RegisterRespose(string Token);
    public record RegisterRequest(string Email, string Username, string Password);
}