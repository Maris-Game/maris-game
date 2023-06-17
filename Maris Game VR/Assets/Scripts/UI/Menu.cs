using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject mainSettings;
    public GameObject inputMenu;

    public void Awake() {
        if(mainMenu != false) {
            mainMenu.SetActive(true);
            settingsMenu.SetActive(false);
            mainSettings.SetActive(true);
            inputMenu.SetActive(false);
        }    
    }

    public void LoadNextScene() {
        GameManager.instance.LoadNextScene();
    }
}
