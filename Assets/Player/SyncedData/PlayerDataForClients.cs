// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {

        public delegate void ColourUpdated (GameObject player, Color newColour);
        public event ColourUpdated OnColourUpdated;

        [SyncVar(hook = "UpdateColour")]
        private Color colour;

        public override void OnStartClient ()
        {
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as the server will know on Command call from local player
            if (!isLocalPlayer && !isServer) {
                UpdateColour(colour);
            }
        }

        public Color GetColour ()
        {
            return colour;
        }

        [Client]
        public void SetColour (Color newColour)
        {
            CmdSetColour(newColour);
        }

        [Command]
        public void CmdSetColour (Color newColour)
        {
            colour = newColour;
        }

        [Client]
        public void UpdateColour (Color newColour)
        {
            colour = newColour;
            GetComponentInChildren<MeshRenderer>().material.color = newColour;
            if (this.OnColourUpdated != null) {
                this.OnColourUpdated(gameObject, newColour);
            }
        }
    }
}