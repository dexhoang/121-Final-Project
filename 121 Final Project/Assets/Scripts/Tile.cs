using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _clickBorder;

    private Transform _player;
    public float _playerRange = 2f;
    private static Tile _selectedTile;

    public float waterLevel = 50f;
    public float sunLevel = 50f;

    public void Init(bool isOffset)
    {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
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
        } else
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
}

