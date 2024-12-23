using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height;
    [SerializeField] private int _startX, _startY;
    [SerializeField] private Tile _tilePrefab;
    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        for (int x = _startX; x < _startX + _width; x++)
        {
            for (int y = _startY; y < _startY + _height; y++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
            }
        }
    }
}
