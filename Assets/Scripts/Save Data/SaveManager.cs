using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static string directory = "/SaveData/";
    public static string fileName = "MySaveData.txt";
    public static void Save(GameData gameData)
    {
        string dir = Application.persistentDataPath + directory;
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(dir + fileName, json);
    }
    public static GameData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;
        GameData gameData = new GameData();
        if (File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            gameData = JsonUtility.FromJson<GameData>(json);
        }
        else
            Debug.Log("File doesn't exist");
        return gameData;
    }
}
