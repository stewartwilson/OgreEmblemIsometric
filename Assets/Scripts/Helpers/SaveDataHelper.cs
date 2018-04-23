using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveDataHelper {

    /**
     * Loads the save data from the disk
     */
    public static List<PlayerData> getListOfSaves()
    {

        List<PlayerData> saveList = new List<PlayerData>();
        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] fileInfo = info.GetFiles();
        foreach (FileInfo file in fileInfo)
        {
            saveList.Add(loadPlayerData(file.Name));
            Debug.Log("Loading file from disk " + file.Name);
        }
        
        return saveList;
    }

    /**
     * Loads the save data from the disk
     */
    public static PlayerData loadPlayerData(string saveName)
    {

        PlayerData gameData = null;
        string savePath = Application.persistentDataPath + "/" + saveName;
        Debug.Log("Loading file from disk " + savePath);
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            //string jsonString = (string)bf.Deserialize(file);
            //Debug.Log(jsonString);
            //gameData = JsonUtility.FromJson<PlayerData>(jsonString);
            gameData = (PlayerData)bf.Deserialize(file);
            file.Close();
        }
        return gameData;
    }


    /**
     * Saves the save data to the disk
     */
    public static string updateSaveFile(PlayerData data, string saveName)
    {
        //TODO safely make backup of save file before overwriting, restore on faile write
        string savePath = Application.persistentDataPath + "/" + saveName;
        string jsonString = JsonUtility.ToJson(data);
        Debug.Log("Saving file to disk " + savePath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
        file.SetLength(0);
        bf.Serialize(file, jsonString);
        file.Close();
        return saveName;
    }

    /**
     * Saves the save data to the disk
     */
    public static string createSaveFile(PlayerData data)
    {
        string saveName = createPlayerSaveName(data);
        string savePath = Application.persistentDataPath + "/" + saveName;
        string jsonString = JsonUtility.ToJson(data);
        Debug.Log("Saving file to disk " + savePath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, data);
        file.Close();
        return saveName;
    }

    public static string createPlayerSaveName(PlayerData data)
    {
        return data.playerName;
    }
}
