// Player/SyncedData/LocalPlayerDataManager.cs

using UnityEngine;
using UnityEngine.Networking;

namespace Player.SyncedData {
    public class LocalPlayerDataManager : NetworkBehaviour {
        public PlayerDataForClients localData;

        public override void OnStartLocalPlayer()
        {
            base.OnStartLocalPlayer();

            LocalPlayerDataStore store = LocalPlayerDataStore.GetInstance();
            if (store.playerColour == new Color(0, 0, 0, 0)) {
                store.playerColour = Random.ColorHSV();
            }

            localData.SetPlayerColour(store.playerColour);
            localData.OnPlayerColourUpdated += OnPlayerColourUpdated;
        }

        public void OnPlayerColourUpdated(Color newColour)
        {
            LocalPlayerDataStore.GetInstance().playerColour = newColour;
        }
    }
}