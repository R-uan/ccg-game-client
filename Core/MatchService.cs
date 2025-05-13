using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using GameClient.Models;

namespace GameClient.Core
{
    public class MatchService
    {
        private AuthManager AuthManager { get; }
        private ClientState ClientState { get; }
        
        public MatchService(AuthManager authManager, ClientState clientState)
        {
            this.AuthManager = authManager;
            this.ClientState = clientState;
        }

        public async Task ConnectToMatch()
        {
        }
    }
}