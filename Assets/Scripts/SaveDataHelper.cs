using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveDataHelper {

    /**
     * Saves the save data to the disk
     */
    public static void saveDataToDisk(SaveData saveData, string saveName)
    {
        string savePath = Application.persistentDataPath + "/"+ saveName + ".gd";
        Debug.Log("Saving file to disk " + savePath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, saveData);
        file.Close();
    }

    /**
     * Loads the save data from the disk
     */
    public static SaveData loadSaveDataFromDisk(string saveName)
    {

        SaveData saveData = null;
        string savePath = Application.persistentDataPath + "/"+ saveName + ".gd";
        Debug.Log("Loading file from disk " + savePath);
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            saveData = (SaveData)bf.Deserialize(file);
            file.Close();
        }
        return saveData;
    }

    public static void updateSaveRecord(string update)
    {
        string savePath = Application.persistentDataPath + "/saveRecord.gd";
        BinaryFormatter bf = new BinaryFormatter();
        if (File.Exists(savePath))
        {
            FileStream file = File.Open(savePath, FileMode.Open);
            
            file.Close();
        }
        else
        {
            FileStream file = File.Create(savePath);
            bf.Serialize(file, update);
            file.Close();
        }
    }

    /**
     * Loads the save data from the disk
     */
    public static PlayerData loadGameDataFromDisk(string saveName)
    {

        PlayerData gameData = null;
        string savePath = Application.persistentDataPath + "/" + saveName + ".gd";
        Debug.Log("Loading file from disk " + savePath);
        if (File.Exists(savePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(savePath, FileMode.Open);
            string jsonString = (string)bf.Deserialize(file);
            Debug.Log(jsonString);
            gameData = JsonUtility.FromJson<PlayerData>(jsonString);
            file.Close();
        }
        return gameData;
    }

    /**
     * Saves the save data to the disk
     */
    public static void saveDataToDisk(PlayerData gameData, string saveName)
    {
        string jsonString = JsonUtility.ToJson(gameData);
        string savePath = Application.persistentDataPath + "/" + saveName + ".gd";
        Debug.Log("Saving file to disk " + savePath);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        bf.Serialize(file, jsonString);
        file.Close();
    }
}
