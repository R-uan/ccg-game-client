using System;

namespace GameClient;

public class AppSettings
{
    public required string AuthServerAddr { get; set; }
    public required string GameServerAddr { get; set; }
    public required string DeckServerAddr { get; set; }
    public required string MatchmakerServerAddr { get; set; }
}
