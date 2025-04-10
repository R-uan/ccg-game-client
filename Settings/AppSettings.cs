namespace GameClient.Settings
{
    public class AppSettings
    {
        public required string AuthServerAddr { get; set; }
        public required string GameServerAddr { get; set; }
        public required string DeckServerAddr { get; set; }
        public required string CardServerAddr { get; set; }
        public required string MatchmakerServerAddr { get; set; }
    }
}