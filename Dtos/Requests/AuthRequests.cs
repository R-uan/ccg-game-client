namespace GameClient.Requests
{
    public record LoginResponse(string Token);
    public record LoginRequest(string Email, string Password);

    public record RegisterResponse(string Token);
    public record RegisterRequest(string Email, string Username, string Password);
}