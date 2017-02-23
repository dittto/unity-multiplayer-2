// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {
        public delegate void PlayerColourUpdated (Color newColour);
        public event PlayerColourUpdated OnPlayerColourUpdated;

        [SyncVar(hook = "UpdatePlayerColour")]
        private Color playerColour;

        // use this for re-triggering the hooks on scene load
        public override void OnStartClient()
        {
            base.OnStartClient();
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as only the clients need this
            if (!isLocalPlayer && !isServer) {
                UpdatePlayerColour(playerColour);
            }
        }

        // use draw.io to create diagram for this pattern

        // also use draw.io to create diagram for showing how all 3 synced options work together when a scene loads

        [Client]
        public void SetPlayerColour(Color newColour)
        {
            CmdSetPlayerColour(newColour);
        }

        [Command]
        public void CmdSetPlayerColour(Color newColour)
        {
            playerColour = newColour;
        }

        [Client]
        public void UpdatePlayerColour(Color newColour)
        {
            playerColour = newColour;
            GetComponentInChildren<MeshRenderer>().material.color = newColour;

            if (this.OnPlayerColourUpdated != null) {
                this.OnPlayerColourUpdated(newColour);
            }
        }
    }
}