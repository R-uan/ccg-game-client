namespace GameClient.Models
{
    public record MatchConnectionInfo(string PlayerId, string CurrentDeckId,string PlayerAuthToken) {}
    public record MatchReconnectionInfo(string PlayerId, string PlayerAuthToken) {}
}