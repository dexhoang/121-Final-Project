using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _clickBorder;
    [SerializeField] private Text waterText;
    [SerializeField] private Text sunText;

    private Transform _player;
    private static Tile _selectedTile;
    private GridManager _gridManager;  // Reference to GridManager
    private int _tileIndex;           // This tile's index in the grid array

    public float _playerRange = 2f;
    public static Tile SelectedTile => _selectedTile;
    public GameObject currentPlant;

    private bool _hasIncrementedStage3Counter = false;
    public bool HasIncrementedStage3Counter
    {
        get => _hasIncrementedStage3Counter;
        set => _hasIncrementedStage3Counter = value;
    }

    public void Init(bool isOffset, GridManager gridManager, int tileIndex)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        _gridManager = gridManager;
        _tileIndex = tileIndex;

        UpdateUI(); // Use grid data to update UI
    }

    private void OnMouseEnter()
    {
        if (IsWithinRange())
        {
            _highlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        _highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (IsWithinRange())
        {
            SelectTile();
            var waterLevel = GetWaterLevel();
            var sunLevel = GetSunLevel();
            ButtonManager.Instance.ShowButtonsAtTile(transform.position, waterLevel, sunLevel);
        }
        else
        {
            if (_selectedTile != null)
            {
                _selectedTile.DeselectTile();
            }
            ButtonManager.Instance.HideButtons();
        }
    }

    private void SelectTile()
    {
        if (_selectedTile != null && _selectedTile != this)
        {
            _selectedTile.DeselectTile();
        }

        _selectedTile = this;
        _clickBorder.SetActive(true);
    }

    public void DeselectTile()
    {
        _clickBorder.SetActive(false);
    }

    private bool IsWithinRange()
    {
        float distance = Vector3.Distance(_player.position, transform.position);
        return distance < _playerRange;
    }
    public byte GetWaterLevel()
    {
        return _gridManager.GetTileData(_tileIndex, 0);
    }

    public byte GetSunLevel()
    {
        return _gridManager.GetTileData(_tileIndex, 1);
    }

    public byte GetGrowthStage()
    {
        return _gridManager.GetTileData(_tileIndex, 2);
    }

    public void SetGrowthStage(byte newStage)
    {
        _gridManager.SetTileData(_tileIndex, GetWaterLevel(), GetSunLevel(), newStage);
    }

    public void UpdateUI()
    {
        if (waterText != null)
        {
            waterText.text = "Water: " + GetWaterLevel();
        }
        if (sunText != null)
        {
            sunText.text = "Sun: " + GetSunLevel();
        }
    }

    public int CountSimilarPlants(string plantTag)
    {
        int count = 0;

        // Offsets for neighboring tiles
        Vector2Int[] directions = {
            new Vector2Int(1, 0),  // Right
            new Vector2Int(-1, 0), // Left
            new Vector2Int(0, 1),  // Up
            new Vector2Int(0, -1), // Down
            new Vector2Int(1, 1),  // Top-right
            new Vector2Int(-1, 1), // Top-left
            new Vector2Int(1, -1), // Bottom-right
            new Vector2Int(-1, -1) // Bottom-left
        };

        foreach (var dir in directions)
        {
            // Find the neighboring tile
            Vector2 neighborPosition = new Vector2(transform.position.x + dir.x, transform.position.y + dir.y);
            Collider2D hit = Physics2D.OverlapPoint(neighborPosition);

            if (hit != null)
            {
                Tile neighborTile = hit.GetComponent<Tile>();
                if (neighborTile != null && neighborTile.currentPlant != null)
                {
                    // Check if the neighboring plant has the same tag
                    if (neighborTile.currentPlant.CompareTag(plantTag))
                    {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    //// Save the plant's state (use SaveLoadManager's PlantState class)
    //public SaveLoadManager.PlantState SavePlantState()
    //{
    //    return new SaveLoadManager.PlantState
    //    {
    //        position = transform.position,
    //        growthStage = growthStage
    //    };
    //}

    //// Restore the plant's state (use SaveLoadManager's PlantState class)
    //public void RestorePlantState(SaveLoadManager.PlantState plantState)
    //{
    //    growthStage = plantState.growthStage;
    //}
}