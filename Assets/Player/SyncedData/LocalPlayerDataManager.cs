// Player/SyncedData/LocalPlayerDataManager.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class LocalPlayerDataManager : NetworkBehaviour {

        public PlayerDataForClients clientData;

        public override void OnStartLocalPlayer ()
        {
            LocalPlayerDataStore store = LocalPlayerDataStore.GetInstance();
            if (store.playerColour == new Color(0, 0, 0, 0)) {
                store.playerColour = Random.ColorHSV();
            }

            clientData.SetColour(store.playerColour);
            clientData.OnColourUpdated += OnPlayerColourUpdated;
        }

        public void OnPlayerColourUpdated (GameObject player, Color newColour)
        {
            LocalPlayerDataStore.GetInstance().playerColour = newColour;
        }
    }
}