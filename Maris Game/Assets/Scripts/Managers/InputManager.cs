using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    public GameObject inputMenu;
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
    public KeyCode clothesOn = KeyCode.E;
    public KeyCode[] keys;
    public string[] keyNames;

    [Header("Other Settings")]
    public float sensX;
    public float sensY;

    private void Update() {
        if(askingInput && inputMenu != null) {
            if(Input.anyKeyDown) {
                KeyCode curKey = checkKey();
                int? curNum = isNumber(curKey);
                
                for(int i = 0; i < keyNames.Length; i++) {
                    if(keyNames[i].Contains(curName)) {
                        keys[i] = curKey;
                    }
                }

                UpdateKeys();

                if(curNum == null) {
                    curText.text = curName + ": " + curKey;
                } else { curText.text = curName + ": " + curNum; }
                
            }
        }
    }
    private KeyCode checkKey() {
        //check each key possible to see which key was just pressed
        foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode)) {
                askingInput = false;
                inputMenu.SetActive(false);
                settingsMenu.SetActive(true);
                Debug.Log("KeyCode down: " + kcode);
                return kcode;    
            } 
        }   return KeyCode.None; 
    }
    private int? isNumber(KeyCode key) {
        //check if the key pressed is a number
        for(int i = 48; i < 58; i++) {
            if((KeyCode)i == key) {
                if(i != 58) {
                    return i - 48;
                } else {
                    return 0;
                }
            }
        } return null;
    }

    private void UpdateKeys() {
        forwardKey = keys[0];
        backwardKey = keys[1];
        leftKey = keys[2];
        rightKey = keys[3];
        sprintKey = keys[4];
        pauseKey = keys[5];
        switchClothKey = keys[6]; 
        clothesOn = keys[7];
    } 

    IEnumerator dissapearText(TextMeshProUGUI curDissapearText, float delay) {
        yield return new WaitForSeconds(delay);
        curDissapearText.enabled = false;
    }

    public void ChangeSens(Slider slider) {
        if(slider.name =="SensX Slider") {
            sensX = slider.value;
        } else if(slider.name == "SensY Slider") {
            sensY = slider.value;
        }
        curText = slider.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        curText.enabled = true;
        curText.text = Mathf.Round(slider.value / 10).ToString();
        StartCoroutine(dissapearText(curText, sensValueUIDelay));
    }

    public void askInput(string name) {
        askingInput = true;
        curName = name;
        inputMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void InputGetText(TextMeshProUGUI text) {
        curText = text;
    }
}
