using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IDataPersistence
{
    public string[] interactSort = new string[] {"Collectible", "Coin"};
    public int arrayIndex = 0;
    public string collectibleName;
    public bool collected = false;
    
    public string collectibleID;
    [ContextMenu("Generate ID for collectible")]
    private void GenerateGuid() {
        collectibleID = System.Guid.NewGuid().ToString();
    }

    public void LoadData(GameData data) {
        data.collectiblesCollected.TryGetValue(collectibleID, out collected);
        if(collected) {
            this.gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) {
        if(data.collectiblesCollected.ContainsKey(collectibleID)) {
            data.collectiblesCollected.Remove(collectibleID);
        }
        data.collectiblesCollected.Add(collectibleID, this.collected);
    }

    public void Interacted() {
        if(this.interactSort[arrayIndex] == "Collectible") {
            this.collected = true;
            this.gameObject.SetActive(false);
        }
    }

}
