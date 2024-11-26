using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using TMPro;  // Importing TextMeshPro namespace

public class SceneSaveManager : MonoBehaviour
{
    [Header("References")]
    public TMP_Text dayText;
    public TMP_Text plantCountText; 
    public Transform playerTransform; 

    private int day; 
    private int plantCount; 
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Application.persistentDataPath + "/sceneState.json";
        UpdatePlantCount(); 
    }

    // Save scene state
    public void SaveScene()
    {
        SceneData data = new SceneData();

        // Save all objects tagged as Saveable
        foreach (var obj in FindObjectsOfType<SaveableObject>().Where(o => o.CompareTag("Saveable")))
        {
            data.objectStates.Add(obj.GetState());
        }

        // Save day, plant count, and player position
        day = int.Parse(dayText.text.Replace("Day: ", ""));
        data.day = day;
        data.plantCount = plantCount; 
        data.playerPosition = playerTransform.position;

        // Save data to file
        File.WriteAllText(saveFilePath, JsonUtility.ToJson(data));
        Debug.Log("Scene saved to: " + saveFilePath);
    }

    // Load scene state
    public void LoadScene()
    {
        if (File.Exists(saveFilePath))
        {
            SceneData data = JsonUtility.FromJson<SceneData>(File.ReadAllText(saveFilePath));

            // Restore objects' states
            foreach (var obj in FindObjectsOfType<SaveableObject>().Where(o => o.CompareTag("Saveable")))
            {
                ObjectState state = data.objectStates.FirstOrDefault(s => s.id == obj.id);
                if (state != null) obj.SetState(state);
            }

            // Restore day, plant count, and player position
            day = data.day;
            plantCount = data.plantCount;
            dayText.text = "Day: " + day;
            plantCountText.text = "Stage 3 Plants: " + plantCount; 

            if (data.playerPosition != null)
                playerTransform.position = data.playerPosition;

            Debug.Log("Scene loaded from: " + saveFilePath);
        }
        else
        {
            Debug.LogError("Save file not found!");
        }
    }

    // Update the plant count and display it
    private void UpdatePlantCount()
    {
        // Reset the plant count
        plantCount = 0;

        // Count all active objects tagged as "Plant"
        foreach (var plant in FindObjectsOfType<Plant>().Where(p => p.gameObject.activeInHierarchy)) 
        {
            plantCount++; 
        }

        // Update the UI text with the plant count
        plantCountText.text = "Stage 3 Plants: " + plantCount;
    }
}

// Serializable class to store the scene's data
[System.Serializable]
public class SceneData
{
    public List<ObjectState> objectStates = new List<ObjectState>();
    public int day;
    public int plantCount;
    public Vector3 playerPosition;
}

// Serializable class for individual object states
[System.Serializable]
public class ObjectState
{
    public string id;
    public Vector3 position;
    public Quaternion rotation;
    public int growthStage; 
}

// Base class for objects that can be saved
public abstract class SaveableObject : MonoBehaviour
{
    public string id; 

    public abstract ObjectState GetState();
    public abstract void SetState(ObjectState state);
}

/*public class Plant : SaveableObject
{
    public int growthStage;

    public override ObjectState GetState()
    {
        return new ObjectState
        {
            id = id,
            position = transform.position,
            rotation = transform.rotation,
            growthStage = growthStage
        };
    }

    public override void SetState(ObjectState state)
    {
        id = state.id;
        transform.position = state.position;
        transform.rotation = state.rotation;
        growthStage = state.growthStage;
    }
}*/

