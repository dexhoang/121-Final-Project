using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int playerLevel;
    public float plantCollected;
    public Vector3 playerPosition;
    public string timestamp; // Useful for displaying save metadata
}

public class SaveManager : MonoBehaviour
{
    private string savePath => Application.persistentDataPath + "/SaveSlot_";

    public void SaveGame(SaveData data, int slot)
    {
        data.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Add timestamp
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath + slot + ".json", json);
        Debug.Log($"Game saved to slot {slot}");
    }

    public SaveData LoadGame(int slot)
    {
        string filePath = savePath + slot + ".json";
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log($"Game loaded from slot {slot}");
            return data;
        }
        else
        {
            Debug.LogError($"No save file found in slot {slot}");
            return null;
        }
    }

    public void DeleteSave(int slot)
    {
        string filePath = savePath + slot + ".json";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log($"Deleted save slot {slot}");
        }
        else
        {
            Debug.LogError($"Save file in slot {slot} does not exist.");
        }
    }

    public bool SaveExists(int slot)
    {
        return File.Exists(savePath + slot + ".json");
    }
}