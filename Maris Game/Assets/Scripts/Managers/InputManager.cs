using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour, IDataPersistence
{
    public GameObject settingsMenu;
    public bool askingInput;
    public float sensValueUIDelay = 2f;
    private string curName;
    private TextMeshProUGUI curText;

    
    [Header("Controls")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode pauseKey = KeyCode.Escape;
    public KeyCode switchClothKey = KeyCode.Tab; 
    public KeyCode clothesOnKey = KeyCode.E;
    public KeyCode interactKey = KeyCode.Q;
    public List<KeyCode> keys;
    public string[] keyNames;

    [Header("Other Settings")]
    public float sensX;
    public float sensY;

    private void Start() {
        UpdateKeys();
    }

    private void Update() {
    }

    public void UpdateKeys() {
        forwardKey = keys[0];
        backwardKey = keys[1];
        leftKey = keys[2];
        rightKey = keys[3];
        sprintKey = keys[4];
        pauseKey = keys[5];
        switchClothKey = keys[6]; 
        clothesOnKey = keys[7];
        interactKey = keys[8];
    } 

    IEnumerator dissapearText(TextMeshProUGUI curDissapearText, float delay) {
        yield return new WaitForSeconds(delay);
        curDissapearText.enabled = false;
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }

    public void LoadData(GameData data) {
        this.sensX = data.sensX;
        this.sensY = data.sensY; 
        this.keys = data.keys;
    }

    public void SaveData(ref GameData data) {
        data.sensX = this.sensX;
        data.sensY = this.sensY;
        data.keys = this.keys;
    }
}
