using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    [SerializeField] private int _width, _height;
    [SerializeField] private Tile _tilePrefab;

    private byte[] gridArray;
    private List<Tile> tileList = new List<Tile>();

    public Vector3 gridStartPosition = Vector3.zero;
    public int tilesInGrid = 0;

    public Transform gridParent;

    private void Start()
    {
        GenerateGrid();
    }
    void GenerateGrid()
    {
        int totalTiles = _width * _height;
        tilesInGrid = totalTiles;
        gridArray = new byte[totalTiles * 3];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 position = new Vector3(x, -y, 0) + gridStartPosition;
                var spawnedTile = Instantiate(_tilePrefab, position, Quaternion.identity);
                spawnedTile.name = $"Tile {x}, {y}";

                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                int tileIndex = x + y * _width;

                if (gridParent != null)
                {
                    spawnedTile.transform.SetParent(gridParent);
                }

                spawnedTile.Init(isOffset, this, tileIndex);
                tileList.Add(spawnedTile);
            }
        }
    }

    public byte GetTileData(int tileIndex, int dataIndex)
    {
        return gridArray[tileIndex * 3 + dataIndex];
    }

    public void SetTileData(int tileIndex, byte waterLevel, byte sunLevel, byte plantLevel)
    {
        gridArray[tileIndex * 3 + 0] = waterLevel;
        gridArray[tileIndex * 3 + 1] = sunLevel;
        gridArray[tileIndex *3 + 2] = plantLevel;
    }

    private void PrintTileData(int tileIndex)
    {
        byte waterLevel = gridArray[tileIndex * 3 + 0];
        byte sunLevel = gridArray[tileIndex * 3 + 1];
        byte state = gridArray[tileIndex * 3 + 2];

        Debug.Log($"Tile {tileIndex} Water: {waterLevel} Sun: {sunLevel} Plant State: {state}");
    }
}