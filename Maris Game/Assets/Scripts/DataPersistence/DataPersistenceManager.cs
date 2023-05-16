using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    public GameData gameData;
    private List<IDataPersistence> StandardDataPersistenceObjects;
    private List<IDataPersistence> OtherDataPersistenceObjects;
    private FileDataHandeler dataHandler;

    public static DataPersistenceManager instance {get; private set;} 

    private void Start() {
        this.dataHandler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
        this.StandardDataPersistenceObjects = FindAllDataPersistenceObjects();
        FirstLoad();
    }

    public void NewGame() {
        this.gameData = new GameData();

        for(int i = 0; i < GameManager.instance.inputManager.keys.Count; i++) {
            gameData.keys.Add(KeyCode.None);
        }
        gameData.keys = GameManager.instance.inputManager.keys;
        Debug.Log(gameData.keys);
    }

    public void FirstLoad() {
        this.gameData = dataHandler.Load();

        LoadGame();
    }

    public void LoadGame() {
        if(this.gameData == null) {
            Debug.Log("No data found");
            NewGame();
        }

        this.OtherDataPersistenceObjects = FindAllDataPersistenceObjects();

        //push data to scripts
        foreach(IDataPersistence dataPersistenceObj in StandardDataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
        foreach(IDataPersistence dataPersistenceObj in OtherDataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);        
        }
    }

    public void SaveGame() {
        foreach(IDataPersistence dataPersistenceObj in StandardDataPersistenceObjects) {
            dataPersistenceObj.SaveData(ref gameData);
        }
        foreach(IDataPersistence dataPersistenceObj in OtherDataPersistenceObjects) {
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
