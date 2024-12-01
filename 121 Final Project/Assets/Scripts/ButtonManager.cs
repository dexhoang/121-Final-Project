using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;  // Import LINQ for the Count method

public class ButtonManager : MonoBehaviour
{
    public static ButtonManager Instance;

    [SerializeField] private GameObject _buttonPanel;
    [SerializeField] private TextMeshProUGUI waterText;
    [SerializeField] private TextMeshProUGUI sunText;
    [SerializeField] private Button increaseLevelsButton;
    [SerializeField] private TextMeshProUGUI counterText;

    [SerializeField] private TextMeshProUGUI stage3CounterText; // Text for stage 3 plant counter
    [SerializeField] private GameObject stage3ReachedTextbox;  // Textbox to unhide when condition is met

    private int dayCounter = 0;
    private int stage3Counter = 0; // Counter for plants at growth stage 3

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (increaseLevelsButton != null)
        {
            increaseLevelsButton.onClick.AddListener(OnIncreaseLevelsClicked);
        }

        // Ensure the textbox is hidden at the start
        if (stage3ReachedTextbox != null)
        {
            stage3ReachedTextbox.SetActive(false);
        }
    }

    public void ShowButtonsAtTile(Vector3 tilePosition, float water, float sun)
    {
        _buttonPanel.SetActive(true);
        waterText.text = "Water Level: " + water.ToString();
        sunText.text = "Sun Level: " + sun.ToString();
    }

    public void HideButtons()
    {
        _buttonPanel.SetActive(false);
    }

    public bool PanelActive()
    {
        return _buttonPanel.activeSelf;
    }

    private void OnIncreaseLevelsClicked()
    {
        dayCounter++;
        UpdateDayCounterText();
    }

    private void UpdateDayCounterText()
    {
        if (counterText != null)
        {
            counterText.text = "Day: " + dayCounter.ToString();
        }
    }

    // Method to increment stage 3 counter
    public void IncrementStage3Counter()
    {
        stage3Counter++;
        UpdateStage3CounterText();

        // Unhide the textbox if the counter reaches 10
        if (stage3Counter == 10 && stage3ReachedTextbox != null)
        {
            stage3ReachedTextbox.SetActive(true);
        }
        Debug.Log("Increment plants: " + stage3Counter);
    }

    // Method to decrement stage 3 counter
    public void DecrementStage3Counter()
    {
        stage3Counter = Mathf.Max(0, stage3Counter - 1); // Ensure counter doesn't go below 0
        UpdateStage3CounterText();
        Debug.Log("Decrement plants: " + stage3Counter);
    }

    // Update the UI for stage 3 counter
    private void UpdateStage3CounterText()
    {
        //Debug.Log("Update Plants: " + stage3Counter);

        if (stage3CounterText != null)
        {
            stage3CounterText.text = "Stage 3 Plants: " + stage3Counter;
        }
    }

    // Sync ButtonManager with SaveLoadManager data
    public void SyncWithSaveData()
    {
        //Debug.Log("Plant count: " + stage3Counter);
        SaveLoadManager saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null || saveLoadManager.LoadedGameState == null)
        {
            Debug.LogWarning("SaveLoadManager or loaded game state not found.");
            return;
        }

        // Sync the day counter
        dayCounter = saveLoadManager.LoadedGameState.dayCount;
        UpdateDayCounterText();

        // Debug log the total number of plant states loaded
        Debug.Log("Total plants in loaded game state: " + saveLoadManager.LoadedGameState.plantStates.Count);

        /*// Sync the stage 3 plant counter using LINQ to filter for growthStage == 3
        stage3Counter = saveLoadManager.LoadedGameState.plantStates.Count(plantState => plantState.growthStage == 3);
        Debug.Log("Sync Plants: " + stage3Counter);*/

        // Ensure the game state is loaded
        if (saveLoadManager.LoadedGameState == null) {
            Debug.LogError("No game state loaded!");
        } else {
            // Log the count of plants with growthStage == 3
            int count = saveLoadManager.LoadedGameState.plantStates.Count(plantState => plantState.growthStage == 3);
            Debug.Log($"Sync Plants: {count}");
            stage3Counter = count;  // Sync the counter
        }
        
        // Debug log the count of stage 3 plants
        Debug.Log("Stage 3 plants: " + stage3Counter);

        UpdateStage3CounterText();

        // If stage 3 counter reaches 10, unhide the textbox
        if (stage3Counter >= 10 && stage3ReachedTextbox != null)
        {
            stage3ReachedTextbox.SetActive(true);
        }
    }
}


