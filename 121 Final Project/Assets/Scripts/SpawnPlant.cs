using System;
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
                float waterThreshold = selectedTile.GetGrowthStage() == 0 ? firstGrowthThreshold : finalGrowthThreshold;
                float sunThreshold = 50;

                //check for similar plants nearby
                int similarPlantCount = selectedTile.CountSimilarPlants(selectedTile.currentPlant.tag);

                //apply growth boost
                int growthBoost = similarPlantCount * 2;
                waterThreshold -= growthBoost;
                sunThreshold -= growthBoost * 4;

                //checks and updates plant stage
                if (selectedTile.GetWaterLevel() >= waterThreshold && selectedTile.GetSunLevel() >= sunThreshold)
                {
                    if (selectedTile.GetGrowthStage() == 0)
                    {
                        Debug.Log("The plant has grown to stage 1!");
                        ReplacePlantWithGrown(selectedTile, 1);
                    }
                    else if (selectedTile.GetGrowthStage() == 1)
                    {
                        Debug.Log("The plant has grown to the final stage!");
                        ReplacePlantWithGrown(selectedTile, 2);

                        if (!selectedTile.HasIncrementedStage3Counter)
                        {
                            ButtonManager.Instance.IncrementStage3Counter();
                            selectedTile.HasIncrementedStage3Counter = true;
                        }
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
                if (selectedTile.GetGrowthStage() == 2)
                {
                    ButtonManager.Instance.DecrementStage3Counter();
                }

                Destroy(selectedTile.currentPlant);
            }

            selectedTile.currentPlant = Instantiate(plantPrefab, selectedTile.transform.position, Quaternion.identity);
            selectedTile.SetGrowthStage(0);
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

                if (tile.GetGrowthStage() == 2)
                {
                    ButtonManager.Instance.DecrementStage3Counter();
                }

                tile.SetGrowthStage((byte)newGrowthStage);
            }
        }
    }

    public void DestroyPlant()
    {
        Tile selectedTile = Tile.SelectedTile;
        if (selectedTile != null && selectedTile.currentPlant != null)
        {
            if (selectedTile.GetGrowthStage() == 2 && selectedTile.HasIncrementedStage3Counter)
            {
                ButtonManager.Instance.DecrementStage3Counter();
                selectedTile.HasIncrementedStage3Counter = false;
            }

            Destroy(selectedTile.currentPlant);
            selectedTile.currentPlant = null;
            selectedTile.SetGrowthStage(0);
            Debug.Log("Plant destroyed successfully.");
        }
        else
        {
            Debug.Log("No plant to destroy on this tile.");
        }
    }
}
