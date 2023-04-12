using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;
    private GameObject pauseMenuUI;
    private TextMeshProUGUI curText;
    public static bool paused = false;

    private void Awake() {
        pauseMenuUI = GameObject.Find("Pause Menu");
        pauseMenuUI.SetActive(false);
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void Update() {
        if(Input.GetKeyDown(inputManager.pauseKey)) {
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

    public void ChangeSens(Slider slider) {
        GameManager.instance.inputManager.ChangeSens(slider);
        inputManager.GetText(curText);
    }

    public void GetText(TextMeshProUGUI text) {
        curText = text;
    }
}
