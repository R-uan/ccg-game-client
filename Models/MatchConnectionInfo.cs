namespace GameClient.Models;

public class MatchConnectionInfo
{
    public required string PlayerId { get; set; }
    public required string CurrentDeckId { get; set; }
    public required string PlayerAuthToken { get; set; }
}