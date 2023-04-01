using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject pauseMenuUI;
    public static bool paused = false;

    private void Awake() {
        pauseMenuUI = GameObject.Find("Pause Menu");
        pauseMenuUI.SetActive(false);
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if(Input.GetKeyDown(gameManager.pauseKey)) {
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
        gameManager.ChangeSens(slider);
    }
}
