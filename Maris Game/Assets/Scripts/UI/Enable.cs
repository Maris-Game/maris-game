using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Enable : MonoBehaviour
{
    public InputManager inputManager;
    public int keyNumberInManager;
    public string[] settingSort = new string[] { "Key", "Slider"};
    public int arrayIndex = 0;
    public string preText;
    

    public TextMeshProUGUI text;
    public Slider slider;

    private void Start() {
        inputManager = GameManager.instance.inputManager;
    }

    public void OnEnable() {
        if(inputManager.sensX == null || inputManager.sensY == null) { 
            Debug.LogError("Data is null");
            return;
        }


        if(settingSort[arrayIndex] == "Key") {   
            text.text = preText + ": " + inputManager.keys[keyNumberInManager].ToString();
        } else if(settingSort[arrayIndex] == "Slider") { 

            if(slider.gameObject.name.Contains("SensX")) {
                slider.value = inputManager.sensX;
            } else if (slider.gameObject.name.Contains("SensY")) {
                slider.value = inputManager.sensY;
            }
        }
        
    }  
}
