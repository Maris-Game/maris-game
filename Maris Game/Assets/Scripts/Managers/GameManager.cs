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
    public InputManager inputManager { get; private set; }

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
        inputManager = GetComponent<InputManager>();
        OnSceneLoaded();
        
    }

    public void OnSceneLoaded() {
        localManager = null;
        localManager = FindObjectOfType<LocalManager>();
    }

    private void Update() {
        
    }
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
}
