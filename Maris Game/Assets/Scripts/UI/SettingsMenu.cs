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

    public bool isFullScreen;
    public bool pauseMenu = false;

    [Header("UI objects")]
    public TMP_Dropdown resolutionDropdown;

    private void Start() {
        inputManager = GameManager.instance.inputManager;
        
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

    public void SetFullScreen(bool fullScreen) {
        isFullScreen = fullScreen;
        Screen.fullScreen = isFullScreen;
        GameManager.instance.fullScreen = isFullScreen;
    }
}
