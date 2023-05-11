using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IDataPersistence
{
    public static GameManager instance;
    public bool paused;
    public LocalManager localManager;
    public InputManager inputManager;

    [Header("Settings")]
    public bool fullScreen;

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance.gameObject);
        inputManager = GetComponent<InputManager>();
        OnSceneLoaded();
        
    }

    public void OnSceneLoaded() {
        this.transform.FindChild("DataManager").GetComponent<DataPersistenceManager>().LoadGame();
        localManager = null;
        localManager = FindObjectOfType<LocalManager>();
    }

    private void Update() {
        
    }
    public void StartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    public void LoadData(GameData data) {
        this.fullScreen = data.fullScreen;
    }

    public void SaveData(ref GameData data) {

    }
}
