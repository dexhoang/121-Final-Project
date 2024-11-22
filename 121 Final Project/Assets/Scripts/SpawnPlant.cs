using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPlant : MonoBehaviour
{
    [SerializeField] private GameObject sunflower;
    [SerializeField] private GameObject mushroom;
    [SerializeField] private GameObject herb;

    [SerializeField] private GameObject grownSunflower;
    [SerializeField] private GameObject grownMushroom;
    [SerializeField] private GameObject grownHerb;

    [SerializeField] private GameObject finalSunflower;
    [SerializeField] private GameObject finalMushroom;
    [SerializeField] private GameObject finalHerb;

    [SerializeField] private float firstGrowthThreshold = 20f;
    [SerializeField] private float finalGrowthThreshold = 40f;

    private void Update()
    {
        if (Tile.SelectedTile != null)
        {
            Tile selectedTile = Tile.SelectedTile;

            // Check if water and sun levels meet the thresholds
            if (selectedTile.growthStage == 0 && selectedTile.waterLevel >= firstGrowthThreshold && selectedTile.sunLevel >= firstGrowthThreshold)
            {
                Debug.Log("The plant has grown to stage 1!");
                ReplacePlantWithGrown(selectedTile, 1);
            }
            else if (selectedTile.growthStage == 1 && selectedTile.waterLevel >= finalGrowthThreshold && selectedTile.sunLevel >= finalGrowthThreshold)
            {
                Debug.Log("The plant has grown to the final stage!");
                ReplacePlantWithGrown(selectedTile, 2);
            }
        }
    }

    public void SpawnSunflower()
    {
        SpawnPlantOnTile(sunflower);
    }

    public void SpawnMushroom()
    {
        SpawnPlantOnTile(mushroom);
    }

    public void SpawnHerb()
    {
        SpawnPlantOnTile(herb);
    }

    private void SpawnPlantOnTile(GameObject plantPrefab)
    {
        if (Tile.SelectedTile != null)
        {
            Tile selectedTile = Tile.SelectedTile;

            // Destroy the current plant if it exists
            if (selectedTile.currentPlant != null)
            {
                Destroy(selectedTile.currentPlant);
            }

            // Instantiate the new plant and track it
            selectedTile.currentPlant = Instantiate(plantPrefab, selectedTile.transform.position, Quaternion.identity);
            selectedTile.growthStage = 0; // Reset growth stage
            Debug.Log("Plant has been planted on the tile!");
        }
        else
        {
            Debug.Log("No tile is selected.");
        }
    }

    private void ReplacePlantWithGrown(Tile tile, int newGrowthStage)
    {
        if (tile.currentPlant != null)
        {
            GameObject newPlantPrefab = null;

            // Determine the next growth stage prefab
            if (tile.currentPlant.CompareTag("Sunflower"))
            {
                if (newGrowthStage == 1)
                {
                    newPlantPrefab = grownSunflower;
                }
                else if (newGrowthStage == 2)
                {
                    newPlantPrefab = finalSunflower;
                }
            }
            else if (tile.currentPlant.CompareTag("Mushroom"))
            {
                if (newGrowthStage == 1)
                {
                    newPlantPrefab = grownMushroom;
                }
                else if (newGrowthStage == 2)
                {
                    newPlantPrefab = finalMushroom;
                }
            }
            else if (tile.currentPlant.CompareTag("Herb"))
            {
                if (newGrowthStage == 1)
                {
                    newPlantPrefab = grownHerb;
                }
                else if (newGrowthStage == 2)
                {
                    newPlantPrefab = finalHerb;
                }
            }

            // Replace the plant if a prefab was found
            if (newPlantPrefab != null)
            {
                Destroy(tile.currentPlant);
                tile.currentPlant = Instantiate(newPlantPrefab, tile.transform.position, Quaternion.identity);
                tile.growthStage = newGrowthStage;
            }
        }
    }

    public void DestroyPlant()
    {
        Tile selectedTile = Tile.SelectedTile;
        if (selectedTile != null)
        {
            Destroy(selectedTile.currentPlant);
        }
    }
}
