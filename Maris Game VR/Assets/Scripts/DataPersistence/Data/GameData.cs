using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public float sensX;
    public float sensY;

    //collectibles
    public SerializableDictionary<string, bool> collectiblesCollected;
    public List<KeyCode> keys;

    public bool fullScreen;

    public GameData() {
        this.sensX = 100f;
        this.sensY = 100f;
        collectiblesCollected = new SerializableDictionary<string, bool>();

        this.fullScreen = true;
    }
}
