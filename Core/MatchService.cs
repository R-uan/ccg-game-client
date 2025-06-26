using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using GameClient.Models;

namespace GameClient.Core
{
    public class MatchService(AuthManager authManager, ClientState clientState)
    {
        private Thread? GameStateThread { get;  set; }
        private bool Connected { get; set; } = false;
        private AuthManager AuthManager { get; } = authManager;
        private ClientState ClientState { get; } = clientState;

        public Task ConnectToMatch()
        {
            Logger.Info("Attempting to connect to match...");
            int connected = SynapseNet.ConnectToServer();
            if (connected == 1)
            {
                Logger.Info("Successfully connected to match");
                this.Connected = true;
            }
            else
            {
                Logger.Error("Failed to connect to match");
            }
            
            this.GameStateThread =  new Thread(this.CycleGameStateRetrival);
            this.GameStateThread.Start();
            
            return Task.CompletedTask;
        }

        public Task ConnectPlayer()
        {
            if (this.Connected)
            {
                var authToken = AuthManager.Token;
                var currentDeckId = this.ClientState.PlayerDecks?[0].Id.ToString();
                var playerId = this.ClientState.PlayerAccount?.Id.ToString();
                Logger.Debug($"{authToken} - {currentDeckId} - {playerId}");
                if (authToken != null && currentDeckId != null && playerId != null)
                {
                    Logger.Info("Attempting to connect player to match...");
                    SynapseNet.ConnectPlayerToMatchServer(new MatchConnectionInfo(playerId, currentDeckId, authToken));
                }
            }
            
            return Task.CompletedTask;
        }
        
        public Task ReconnectPlayer()
        {
            if (this.Connected)
            {
                var authToken = AuthManager.Token;
                var currentDeckId = this.ClientState.PlayerDecks?[0].Id.ToString();
                var playerId = this.ClientState.PlayerAccount?.Id.ToString();
                if (authToken != null && currentDeckId != null && playerId != null)
                {
                    Logger.Info("Attempting to connect player to match...");
                    SynapseNet.ReconnectPlayerToMatchServer(new MatchReconnectionInfo(playerId, authToken));
                }
            }
            
            return Task.CompletedTask;
        }

        public Task PlayCard()
        {
            var deck = ClientState.PlayerDecks?[0].Cards;
            if (deck != null)
            {
                var card = deck[0].Id;
                var actor = ClientState.PlayerAccount!.Id;
                SynapseNet.PlayCard(card, actor, null, null);
            }
            
            return Task.CompletedTask;
        }
        
        private void CycleGameStateRetrival()
        {
            Logger.Info("Listening to game state");
            while (this.Connected == true)
            {
                Thread.Sleep(1000);   
                var gameState = SynapseNet.RetrieveGameState();
                if (gameState.Error != null)
                {
                    Logger.Error(gameState.Error);
                }
                else
                {
                    Logger.Info($"Retrieved GameState of {gameState.Value!.Length} bytes.");
                }
            }
        }
    }
}