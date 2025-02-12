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

    public int resolutionWidth;
    public int resolutionHeight;
    public bool fullScreen;
    public bool vSync;
    public bool subtitles;
    public int subtitleSize;
    public string language;

    public GameData() {
        this.sensX = 100f;
        this.sensY = 100f;
        collectiblesCollected = new SerializableDictionary<string, bool>();
        resolutionWidth = 0;
        resolutionHeight = 0;

        this.fullScreen = true;
        this.vSync = true;
        this.subtitles = true;
        this.subtitleSize = 60;
        this.language = "English";
    }
}
