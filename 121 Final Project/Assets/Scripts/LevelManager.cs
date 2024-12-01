using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GridManager _gridManager; // Reference to the GridManager
    [SerializeField] private int minWaterLevel = 0;
    [SerializeField] private int maxWaterLevel = 255;
    [SerializeField] private int minSunLevel = 0;
    [SerializeField] private int maxSunLevel = 255;

    private void Start()
    {
        if (_gridManager == null)
        {
            Debug.LogError("GridManager reference is not set!");
            return;
        }

        RandomizeTileData();
    }

    public void RandomizeTileData()
    {
        for (int i = 0; i < _gridManager.tilesInGrid; i++)
        {
            //get current tile data
            byte currentWater = _gridManager.GetTileData(i, 0);
            byte currentSun = _gridManager.GetTileData(i, 1);

            //generate random values
            byte randomWater = (byte)Random.Range(minWaterLevel, maxWaterLevel + 1);
            byte randomSun = (byte)Random.Range(minSunLevel, maxSunLevel + 1);

            //add to current levels and clamp to byte range (0-255)
            byte newWater = (byte)Mathf.Clamp(currentWater + randomWater, 0, 255);

            //update the tile data
            _gridManager.SetTileData(i, newWater, randomSun, 0);
        }

        Debug.Log("Accumulated random water and sun levels for all tiles.");
    }
}
