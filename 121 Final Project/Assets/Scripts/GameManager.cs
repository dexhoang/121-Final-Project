using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SaveManager saveManager;
    public int currentSaveSlot = 1; // Default slot
    public SaveData currentGameData;

    private void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        CheckAutoSave();
    }

    private void CheckAutoSave()
    {
        if (saveManager.SaveExists(0)) // Auto-save is slot 0
        {
            Debug.Log("Auto-save available. Prompting player...");
            // Display UI to ask if they want to load auto-save
        }
    }

    public void SaveCurrentGame()
    {
        SaveData data = CollectGameData();
        saveManager.SaveGame(data, currentSaveSlot);
        Debug.Log("Gameis saving");
    }

    public void LoadGameFromSlot(int slot)
    {
        SaveData loadedData = saveManager.LoadGame(slot);
        if (loadedData != null)
        {
            ApplyGameData(loadedData);
        }
        Debug.Log("Game is Loading");
    }

    public void AutoSaveGame()
    {
        SaveData data = CollectGameData();
        saveManager.SaveGame(data, 0); // Auto-save to slot 0
        Debug.Log("Game is auto-saving...");
    }

    private SaveData CollectGameData()
    {
        // Collect data from game state
        return new SaveData
        {
            playerLevel = 5, // Example data
            plantCollected = 100f, // Example data
            playerPosition = new Vector3(10, 0, 5) // Example data
        };
    }

    private void ApplyGameData(SaveData data)
    {
        // Apply loaded data to game state
        Debug.Log($"Player Level: {data.playerLevel}");
        Debug.Log($"Player Health: {data.plantCollected}");
        Debug.Log($"Player Position: {data.playerPosition}");
    }

    private float autoSaveInterval = 60f; // 5 minutes
    private float autoSaveTimer;

    private void Update()
    {
        autoSaveTimer += Time.deltaTime;
        if (autoSaveTimer >= autoSaveInterval)
        {
            AutoSaveGame();
            autoSaveTimer = 0f;
            Debug.Log("Auto-Saving...");
        }
    }
}
