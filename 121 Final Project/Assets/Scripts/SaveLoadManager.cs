using UnityEngine;
using TMPro;  // Make sure to include TextMeshPro namespace
using System.Collections.Generic;
using System.IO;


public class SaveLoadManager : MonoBehaviour
{
   public Transform player;  // Reference to the player object
   public TMP_Text dayCountText;  // TextMeshPro component for day count
   public TMP_Text plantCountText;  // TextMeshPro component for plant count
   public TMP_Text sunLevelText;  // TextMeshPro component for sun level
   public TMP_Text waterLevelText;  // TextMeshPro component for water level
   public Plant[] plants;  // Array of all plant objects in the game


   private string filePath;


   // Expose the loaded game state
   public GameState LoadedGameState { get; private set; }


   // Save data class to hold all game state information
   [System.Serializable]
   public class GameState
   {
       public Vector3 playerPosition;
       public int dayCount;
       public float waterLevel;
       public float sunLevel;
       public List<PlantState> plantStates = new List<PlantState>();
   }


   [System.Serializable]
   public class PlantState
   {
       public Vector3 position;
       public int growthStage;  // Track the growth stage of the plant
   }


   private void Start()
   {
       filePath = Application.persistentDataPath + "/savegame.json";  // Save file path


       // Ensure that LoadedGameState is initialized
       if (LoadedGameState == null)
       {
           LoadedGameState = new GameState();
       }


       // Check for null references
       if (player == null) Debug.LogError("Player reference is missing!");
       if (dayCountText == null) Debug.LogError("DayCountText reference is missing!");
       if (plantCountText == null) Debug.LogError("PlantCountText reference is missing!");
       if (sunLevelText == null) Debug.LogError("SunLevelText reference is missing!");
       if (waterLevelText == null) Debug.LogError("WaterLevelText reference is missing!");
       if (plants == null || plants.Length == 0) Debug.LogError("Plants array is missing or empty!");
   }


   // Save the game data
   public void SaveGame()
   {
       if (player == null || dayCountText == null || waterLevelText == null || sunLevelText == null || plants == null)
       {
           Debug.LogError("Critical references are missing. Cannot save game.");
           return;
       }


       GameState gameState = new GameState
       {
           playerPosition = player.position,
           dayCount = int.TryParse(dayCountText.text.Split(':')[1].Trim(), out int dayCount) ? dayCount : 0,  // Safe parsing
           waterLevel = float.TryParse(waterLevelText.text.Split(':')[1].Trim(), out float waterLevel) ? waterLevel : 0f,  // Safe parsing
           sunLevel = float.TryParse(sunLevelText.text.Split(':')[1].Trim(), out float sunLevel) ? sunLevel : 0f  // Safe parsing
       };


       // Save the state of each plant (position and growth stage)
       foreach (Plant plant in plants)
       {
           if (plant == null) continue;


           gameState.plantStates.Add(new PlantState
           {
               position = plant.transform.position,
               growthStage = plant.growthStage  // Assuming growthStage is an integer representing the stage
           });
       }


       // Convert the game state to JSON and save it to a file
       string json = JsonUtility.ToJson(gameState, true);
       File.WriteAllText(filePath, json);
       Debug.Log("Game saved to " + filePath);
       // Debug.Log("Day before: " + LoadedGameState.dayCount);
       Debug.Log("After: " + LoadedGameState.plantStates.Count);
   }


   // Load the game data
   public void LoadGame()
   {
       if (!File.Exists(filePath))
       {
           Debug.LogWarning("Save file not found!");
           return;
       }


       string json = File.ReadAllText(filePath);
       LoadedGameState = JsonUtility.FromJson<GameState>(json);


       // Ensure LoadedGameState is valid
       if (LoadedGameState == null)
       {
           Debug.LogError("Loaded game state is null. Cannot load game.");
           return;
       }


       // Restore the player position
       if (player != null)
       {
           player.position = LoadedGameState.playerPosition;
       }


       // Restore the day count, water level, and sun level
       if (dayCountText != null)
           dayCountText.text = "Day: " + LoadedGameState.dayCount;


       if (waterLevelText != null)
           waterLevelText.text = "Water Level: " + LoadedGameState.waterLevel.ToString("F1");


       if (sunLevelText != null)
           sunLevelText.text = "Sun Level: " + LoadedGameState.sunLevel.ToString("F1");


       // Restore each plant's state
       foreach (PlantState plantState in LoadedGameState.plantStates)
       {
           foreach (Plant plant in plants)
           {
               if (plant == null) continue;


               if (plant.transform.position == plantState.position)
               {
                   plant.growthStage = plantState.growthStage;
                   Debug.Log("Growth Stage: " + plant.growthStage);
                   break;
               }
           }
       }


       // Update the plant count text
       UpdatePlantCountText();


       // Sync the ButtonManager with the loaded data
       if (ButtonManager.Instance != null)
       {
           ButtonManager.Instance.SyncWithSaveData();
       }


       Debug.Log("Game loaded from " + filePath);
       //Debug.Log("Day before: " + LoadedGameState.dayCount);
       Debug.Log("Before: " + LoadedGameState.plantStates.Count);
   }


   // Update the plant count text after loading
   private void UpdatePlantCountText()
   {
       int stage3PlantCount = 0;


       foreach (Plant plant in plants)
       {
           if (plant == null) continue;


           if (plant.growthStage == 3)  // Assuming stage 3 is the fully grown stage
           {
               stage3PlantCount++;
           }
       }


       if (plantCountText != null)
           plantCountText.text = "Stage 3 Plants: " + stage3PlantCount;
   }
}




   // Debug.Log("Day before: " + LoadedGameState.dayCount);
      // Debug.Log("Plant count before: " + LoadedGameState.plantStates.Count);


