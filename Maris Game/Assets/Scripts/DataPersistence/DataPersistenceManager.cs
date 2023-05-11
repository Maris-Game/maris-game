using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandeler dataHandler;

    public static DataPersistenceManager instance {get; private set;} 

    private void Start() {
        this.dataHandler = new FileDataHandeler(Application.persistentDataPath, fileName, useEncryption);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame() {
        this.gameData = new GameData();

        for(int i = 0; i < GameManager.instance.inputManager.keys.Count; i++) {
            gameData.keys.Add(KeyCode.None);
        }
        gameData.keys = GameManager.instance.inputManager.keys;
        Debug.Log(gameData.keys);
    }

    public void LoadGame() {
        this.gameData = dataHandler.Load();

        if(this.gameData == null) {
            Debug.Log("No data found");
            NewGame();
        }

        //push data to scripts
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame() {
        foreach(IDataPersistence dataPersistenceObj in dataPersistenceObjects) {
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
