namespace GameClient.Requests
{
    public record CreateDeckRequest(string Name, List<string> Cards);
}
