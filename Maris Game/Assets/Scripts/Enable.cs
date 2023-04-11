using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Enable : MonoBehaviour
{
    private InputManager inputManager;
    public string stringName;
    public string settingSort;
    public string previewText;

    public TextMeshProUGUI text;
    public Slider slider;
    

    private void Awake() {
        inputManager = GameManager.instance.inputManager;
    }

    public void OnEnable() {
        if(settingSort == "text") {
            text.text = inputManager.nameOf(stringName).ToString();
        } else if(settingSort == "slider") {
            
        }
        
    }  
}
