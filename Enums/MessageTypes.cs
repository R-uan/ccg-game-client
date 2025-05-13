namespace GameClient.Enums
{
    public enum MessageTypes
    {
        Disconnect = 0x00,
        Connect = 0x01,
        Ping = 0x02,

        GameState = 0x10,

        PlayCard = 0x11,
        AttackPlayer = 0x12,

        InvalidHeader = 0xFA,
        AlreadyConnected = 0xFB,
        InvalidPlayerData = 0xFC,
        InvalidChecksum = 0xFD,
        Error = 0xFE,
    }
}
