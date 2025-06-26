using System;

namespace GameClient.Models
{
    public class PlayerAccount
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }

        public List<CardCollection>? CardCollection { get; set; }

        public int Level { get; set; }
        public int Experience { get; set; }

        public int Wins { get; set; }
        public int Losses { get; set; }

        public DateTime LastLogin { get; set; }
        public bool IsBanned { get; set; }
    }
}
