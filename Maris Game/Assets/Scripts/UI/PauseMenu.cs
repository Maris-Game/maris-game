using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    private SettingsMenu settingsMenu;
    private GameObject pauseMenuUI;
    private TextMeshProUGUI curText;
    public static bool paused = false;

    private void Awake() {
        pauseMenuUI = GameObject.Find("Pause Menu");
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
        if(!paused) {
            paused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pauseMenuUI.SetActive(true);

        } else {
            paused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pauseMenuUI.SetActive(false);
        }
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }
}
