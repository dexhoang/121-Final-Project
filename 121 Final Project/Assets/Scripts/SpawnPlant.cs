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

            if (selectedTile.currentPlant != null)
            {
                float waterThreshold = selectedTile.growthStage == 0 ? 20f : 40f;
                float sunThreshold = selectedTile.growthStage == 0 ? 20f : 40f;

                // Check for similar plants nearby
                int similarPlantCount = selectedTile.CountSimilarPlants(selectedTile.currentPlant.tag);

                // Apply growth boost based on nearby similar plants
                float growthBoost = similarPlantCount * 2f;
                waterThreshold -= growthBoost;
                sunThreshold -= growthBoost;

                // Growth logic
                if (selectedTile.waterLevel >= waterThreshold && selectedTile.sunLevel >= sunThreshold)
                {
                    if (selectedTile.growthStage == 0)
                    {
                        Debug.Log("The plant has grown to stage 1!");
                        ReplacePlantWithGrown(selectedTile, 1);
                    }
                    else if (selectedTile.growthStage == 1)
                    {
                        Debug.Log("The plant has grown to the final stage!");
                        ReplacePlantWithGrown(selectedTile, 2);
                    }
                }
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

            if (selectedTile.currentPlant != null)
            {
                // Decrement counter
                if (selectedTile.growthStage == 2)
                {
                    ButtonManager.Instance.DecrementStage3Counter();
                }

                Destroy(selectedTile.currentPlant);
            }

            selectedTile.currentPlant = Instantiate(plantPrefab, selectedTile.transform.position, Quaternion.identity);
            selectedTile.growthStage = 0;
        }
    }

    private void ReplacePlantWithGrown(Tile tile, int newGrowthStage)
    {
        if (tile.currentPlant != null)
        {
            GameObject newPlantPrefab = null;

            if (tile.currentPlant.CompareTag("Sunflower"))
            {
                newPlantPrefab = newGrowthStage == 1 ? grownSunflower : finalSunflower;
            }
            else if (tile.currentPlant.CompareTag("Mushroom"))
            {
                newPlantPrefab = newGrowthStage == 1 ? grownMushroom : finalMushroom;
            }
            else if (tile.currentPlant.CompareTag("Herb"))
            {
                newPlantPrefab = newGrowthStage == 1 ? grownHerb : finalHerb;
            }

            if (newPlantPrefab != null)
            {
                Destroy(tile.currentPlant);
                tile.currentPlant = Instantiate(newPlantPrefab, tile.transform.position, Quaternion.identity);

                if (tile.growthStage == 2)
                {
                    ButtonManager.Instance.DecrementStage3Counter();
                }

                tile.growthStage = newGrowthStage;
            }
        }
    }

    public void DestroyPlant()
    {
        Tile selectedTile = Tile.SelectedTile;
        if (selectedTile != null && selectedTile.currentPlant != null)
        {
            if (selectedTile.growthStage == 2) // If destroying a stage 3 plant, decrement counter
            {
                ButtonManager.Instance.DecrementStage3Counter();
            }

            Destroy(selectedTile.currentPlant);
            selectedTile.growthStage = 0;
        }
    }
}
