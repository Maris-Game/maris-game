using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    private InputManager inputManager;
    public Resolution[] resolutions;

    

    [Header("Other Settings")]
    public bool isFullScreen;
    public bool pauseMenu = false;


    [Header("UI objects")]
    public TMP_Dropdown resolutionDropdown;
    public GameObject inputMenu;
    public TextMeshProUGUI curText;

    private void Start() {
        inputManager = GameManager.instance.inputManager;
        inputMenu = FindGameObject<inputMenu>();l
        
        //resolutions stuff
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> resOptions = new List<string>();
        int currentResIndex = 0;

        for(int i = 0; i < resolutions.Length; i++) {
            string option = resolutions[i].width + " x " + resolutions[i].height; 
            resOptions.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height) {
                currentResIndex = i;
            }
        }
        resolutionDropdown.AddOptions(resOptions);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();

        //checks if the scene is Main Menu, if not. The settings menu will be a pause menu
        if(SceneManager.GetActiveScene().name != "MainMenu") {
            pauseMenu = true;
        }
    }

    private void Update() {
        if(inputManager.askingInput && inputMenu != null) {
            if(Input.anyKeyDown) {
                KeyCode curKey = checkKey();
                int? curNum = isNumber(curKey);
                
                for(int i = 0; i < inputManager.keyNames.Length; i++) {
                    if(inputManager.keyNames[i] == curName) {
                        inputManager.keys[i] = curKey;
                    }
                }
                inputManager.UpdateKeys();

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
                Debug.Log("Key clicked: " + kcode);
                return kcode;    
            } 
        }   
        return KeyCode.None; 
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
        } 
        return null;
    }

    public void SetFullScreen(bool fullScreen) {
        isFullScreen = fullScreen;
        Screen.fullScreen = isFullScreen;
        GameManager.instance.fullScreen = isFullScreen;
    }

    public void ChangeSens(Slider slider) {
        if(slider.name =="SensX Slider") {
            sensX = inputManager.slider.value;
        } else if(slider.name == "SensY Slider") {
            sensY = inputManagerslider.value;
        }
        curText = slider.gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
        curText.enabled = true;
        curText.text = Mathf.Round(slider.value).ToString();
        StartCoroutine(dissapearText(curText, sensValueUIDelay));
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }
}
