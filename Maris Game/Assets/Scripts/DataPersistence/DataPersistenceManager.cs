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
    private List<IDataPersistence> DataPersistenceObjects;
    private FileDataHandeler dataHandler;

    public static DataPersistenceManager instance {get; private set;} 

    private void Start() {
        this.dataHandler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        FirstLoad();
    }

    public void NewGame() {
        this.gameData = new GameData();

        gameData.keys = GameManager.instance.inputManager.keys;
        //for(int i = 0; i < GameManager.instance.inputManager.keys.Count; i++) {
        //    gameData.keys.Add(KeyCode.None);
        //}
        
        
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

        this.DataPersistenceObjects = FindAllDataPersistenceObjects();
        

        //push data to scripts
        foreach(IDataPersistence dataPersistenceObj in DataPersistenceObjects) {
            Debug.Log(dataPersistenceObj);
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
