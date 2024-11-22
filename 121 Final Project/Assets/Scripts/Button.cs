//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonManager : MonoBehaviour
//{
//    [SerializeField] private Button increaseWaterSunButton; // The button to increase water/sun levels
//    private Tile _currentSelectedTile;

//    // Singleton instance for easy access
//    public static ButtonManager Instance;

//    private void Awake()
//    {
//        // Ensure there is only one instance of ButtonManager
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void Start()
//    {
//        // Add listener to the button to call OnIncreaseWaterSunClicked when clicked
//        increaseWaterSunButton.onClick.AddListener(OnIncreaseWaterSunClicked);

//        // Initially hide the button
//        increaseWaterSunButton.gameObject.SetActive(false);
//    }

//    // This method will be called when a tile is clicked
//    public void SetSelectedTile(Tile tile)
//    {
//        _currentSelectedTile = tile;
//    }

//    public void ShowButtonsAtTile(Vector3 tilePosition, float waterLevel, float sunLevel)
//    {
//        // Position the button near the selected tile
//        Vector3 buttonPosition = tilePosition + new Vector3(0, 1, 0); // Adjust the button's position
//        increaseWaterSunButton.transform.position = Camera.main.WorldToScreenPoint(buttonPosition);

//        // Show the button
//        increaseWaterSunButton.gameObject.SetActive(true);
//    }

//    public void HideButtons()
//    {
//        // Hide the button
//        increaseWaterSunButton.gameObject.SetActive(false);
//    }

//    // Called when the button is clicked
//    private void OnIncreaseWaterSunClicked()
//    {
//        // Make sure a tile is selected
//        if (_currentSelectedTile != null)
//        {
//            // Increase levels of the selected tile
//            _currentSelectedTile.IncreaseLevels();
//        }
//    }
//}
