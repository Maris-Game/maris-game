using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    public GameData gameData;
    private List<IDataPersistence> DataPersistenceObjects;
    private FileDataHandeler dataHandler;

    public static DataPersistenceManager instance {get; private set;} 

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
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("Scene Loaded");
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }


    public void OnSceneUnloaded(Scene scene) {
        Debug.Log("Scene Unloaded");
        SaveGame();
    }


    public void NewGame() {
        this.gameData = new GameData();

        gameData.keys = GameManager.instance.inputManager.keys;  
    }

    public void LoadGame() {
        this.gameData = dataHandler.Load();
        if(this.gameData == null) {
            Debug.Log("No data found");
            NewGame();
        }

        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        

        //push data to scripts
        foreach(IDataPersistence dataPersistenceObj in DataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame() {
        foreach(IDataPersistence dataPersistenceObj in DataPersistenceObjects) {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit() {
        SaveGame();
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects() {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
