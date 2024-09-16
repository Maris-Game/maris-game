using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    public SettingsMenu settingsMenu;
    public GameObject pauseMenuUI;
    public TextMeshProUGUI curText;

    private void Awake() {
        //pauseMenuUI = GameObject.Find("Pause Menu");
        pauseMenuUI.SetActive(false);
        gameManager = GameManager.instance;
        settingsMenu = FindObjectOfType<SettingsMenu>();
    }

    private void Update() {
        if(Input.GetKeyDown(GameManager.instance.inputManager.pauseKey)) {
            Pause();
        }
    }

    public void Pause() {
        if(!GameManager.instance.paused) {
            GameManager.instance.paused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pauseMenuUI.SetActive(true);

        } else {
            GameManager.instance.paused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            pauseMenuUI.SetActive(false);
        }
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }
}
