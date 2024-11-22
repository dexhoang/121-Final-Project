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
    public float _playerRange = 2f;
    private static Tile _selectedTile;

    public GameObject currentPlant;
    public int growthStage = 0;

    public float waterLevel = 0f;
    public float sunLevel = 0f;
    public static Tile SelectedTile => _selectedTile;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        RandomizeLevels();
        UpdateUI();
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
    private void RandomizeLevels()
    {
        waterLevel = 0f;
        sunLevel = 0f;
    }

    public void UpdateUI()
    {
        if (waterText != null)
        {
            waterText.text = "Water: " + waterLevel.ToString("F1");
        }
        if (sunText != null)
        {
            sunText.text = "Sun: " + sunLevel.ToString("F1");
        }
    }

    public void IncreaseLevelsRandomly()
    {
        float additionalWater = Random.Range(0f, 3f);
        float additionalSun = Random.Range(0f, 3f);
        waterLevel += additionalWater;
        sunLevel += additionalSun;
        UpdateUI();
    }
}