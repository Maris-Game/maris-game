using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class GameData
{
    public float sensX;
    public float sensY;
    public List<KeyCode> keys = new List<KeyCode>();

    public GameData() {
        this.sensX = 100f;
        this.sensY = 100f;
    }
}
