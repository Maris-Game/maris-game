using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandeler
{
    private string dataDirPath = ""; 
    private string dataFileName = "";
    private bool useEncryption = false;
    private readonly string encryptionCodeWord = "word";
    

    public FileDataHandeler(string dataDirPath, string dataFileName, bool useEncryption) {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
        this.useEncryption = useEncryption;
    }

    public GameData Load() {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath)) {
            try {
                // load data from the file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open)) {
                    using (StreamReader reader  = new StreamReader(stream) ) {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                
                //optionally decrypt the data
                if(useEncryption) {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            catch (Exception e) {
               Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e); 
            }
        }
        return loadedData;
    }

    public void Save(GameData data) {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try {
            //create the directory where the file will be written
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
            string dataToStore = JsonUtility.ToJson(data, true);

            //optionally encrypt the data
            if(useEncryption) {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            //write the data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                   writer.Write(dataToStore); 
                }
            }
        }
        catch (Exception e) {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt(string data) {
        string modifiedData = "";
        for(int i = 0; i < data.Length; i++) {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
