using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using GameClient.Models;

namespace GameClient.Core
{
    public class MatchService
    {
        private Thread GameStateThread { get;  set; }
        public bool Connected { get; private set; }
        private AuthManager AuthManager { get; }
        private ClientState ClientState { get; }

        public MatchService(AuthManager authManager, ClientState clientState)
        {
            this.Connected = false;
            this.AuthManager = authManager;
            this.ClientState = clientState;
        }

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