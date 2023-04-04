using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool paused;
    public LocalManager localManager;

    [Header("Settings")]
    public float sensX;
    public float sensY;

    [Header("Controls")]
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode pauseKey = KeyCode.Escape;

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        localManager = FindObjectOfType<LocalManager>();
    }

    public void OnSceneLoaded() {
        localManager = null;
        localManager = FindObjectOfType<LocalManager>();
    }

    private void Update() {
        
    }

    

    public void ChangeSens(Slider slider) {
        if(slider.name =="SensX Slider") {
            sensX = slider.value;
        } else if(slider.name == "SensY Slider") {
            sensY = slider.value;
        }
    }

    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
}
