using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameManager : MonoBehaviour, IDataPersistence
{
    
    public bool paused;

    public VideoPlayer vp;
    public Slider loadBar;

    [Header("Managers")]
    public static GameManager instance;
    public LocalManager localManager;
    public InputManager inputManager;
    public AudioManager audioManager;
    public DataPersistenceManager dataManager;

    [Header("Collectibles/Game booleans")]
    public string[] collectibleIDs;
    public int collectiblesCollected = 0;
    public bool canMakeBomb = false;
    public bool gameOver = false;
    private bool curCollected = false;

    [Header("Settings")]
    public bool fullScreen;

    private void OnEnable () {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void Awake() {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(instance.gameObject);
        print(SceneManager.GetActiveScene().name);

        inputManager = GetComponent<InputManager>();
        audioManager = GetComponentInChildren<AudioManager>();
    }
    
    private void Update() {
        if(collectiblesCollected == collectibleIDs.Length) {
            canMakeBomb = true;
        }
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 

    public void LoadData(GameData data) {
        this.fullScreen = data.fullScreen;

        collectiblesCollected = 0;
        for(int i = 0; i < collectibleIDs.Length; i++) {
            data.collectiblesCollected.TryGetValue(collectibleIDs[i], out curCollected);
            if(curCollected) {
                collectiblesCollected++;  
            } 
        }
    }

    public void SaveData(ref GameData data) {
        data.fullScreen = this.fullScreen;

        if(gameOver) {
            
            for(int i = 0; i < collectibleIDs.Length; i++) {
                if(data.collectiblesCollected.ContainsKey(collectibleIDs[i])) {
                    data.collectiblesCollected.Remove(collectibleIDs[i]);
                }
                    data.collectiblesCollected.Add(collectibleIDs[i], false);
            }
        }
        
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver() {
        if(collectiblesCollected == 0) {
            return;
        }

        gameOver = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("Game Over");
        audioManager.StopAllSounds();
    }

    public void Win() {
        audioManager.StopAllSounds();
        SceneManager.LoadScene("Outro");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        vp = FindObjectOfType<VideoPlayer>();
        if(vp != null) {
            vp.loopPointReached += VideoFinished;
        }

        if(SceneManager.GetActiveScene().name == "Loading Screen") {
            loadBar = FindObjectOfType<Slider>();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if(SceneManager.GetActiveScene().name == "Maris") {
            audioManager.PlaySound("Ambience");
        }
    } 

    private void OnSceneUnloaded(Scene scene) {

    }

    private void VideoFinished(VideoPlayer vp) {
        Debug.Log("Video Ended");
        if(SceneManager.GetActiveScene().name == "Intro") {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);    
        }
    }
}
