// Player/SyncedData/PlayerDataForClients.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class PlayerDataForClients : NetworkBehaviour {

        public delegate void ColourUpdated (Color newColour);
        public event ColourUpdated OnColourUpdated;

        [SyncVar(hook = "UpdateColour")]
        private Color colour;

        // use this for re-triggering the hooks on scene load
        public override void OnStartClient ()
        {
            // don't update for local player as handled by LocalPlayerOptionsManager
            // don't update for server as only the clients need this
            if (!isLocalPlayer && !isServer) {
                UpdateColour(colour);
            }
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
                this.OnColourUpdated(newColour);
            }
        }
    }
}