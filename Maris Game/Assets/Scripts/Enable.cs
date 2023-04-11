using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Enable : MonoBehaviour
{
    private InputManager inputManager;
    public int keyNumberInManager;
    public string settingSort;
    public string preText;

    public TextMeshProUGUI text;
    public Slider slider;
    

    private void Awake() {
        inputManager = GameManager.instance.inputManager;
    }

    public void OnEnable() {
        if(settingSort == "key") {
            text.text = preText + ": " + inputManager.keys[keyNumberInManager].ToString();
        } else if(settingSort == "slider") {
            if(slider.gameObject.name.Contains("SensX")) {
                slider.value = inputManager.sensX;
            } else if (slider.gameObject.name.Contains("SensY")) {
                slider.value = inputManager.sensY;
            }
        }
        
    }  
}
