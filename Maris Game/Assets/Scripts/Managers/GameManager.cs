using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool paused;
    public LocalManager localManager;
    public InputManager inputManager;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance);
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
