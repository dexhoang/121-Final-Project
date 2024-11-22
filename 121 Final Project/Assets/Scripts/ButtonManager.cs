using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        waterText.text = "Water Level: " + water.ToString("F1");
        sunText.text = "Sun Level: " + sun.ToString("F1");
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

        Tile[] allTiles = GameObject.FindObjectsOfType<Tile>();
        foreach (Tile tile in allTiles)
        {
            tile.IncreaseLevelsRandomly();
        }

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
    }

    // Method to decrement stage 3 counter
    public void DecrementStage3Counter()
    {
        stage3Counter = Mathf.Max(0, stage3Counter - 1); // Ensure counter doesn't go below 0
        UpdateStage3CounterText();
    }

    // Update the UI for stage 3 counter
    private void UpdateStage3CounterText()
    {
        if (stage3CounterText != null)
        {
            stage3CounterText.text = "Stage 3 Plants: " + stage3Counter;
        }
    }
}
