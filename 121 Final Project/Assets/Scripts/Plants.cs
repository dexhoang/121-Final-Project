using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : SaveableObject
{
    public int growthStage; // Current growth stage of the plant

    public override ObjectState GetState()
    {
        ObjectState state = new ObjectState
        {
            id = id,
            position = transform.position,
            rotation = transform.rotation,
            growthStage = growthStage // Save growth stage
        };
        return state;
    }

    public override void SetState(ObjectState state)
    {
        transform.position = state.position;
        transform.rotation = state.rotation;
        growthStage = state.growthStage; // Restore growth stage
        UpdatePlantVisuals();
    }

    private void UpdatePlantVisuals()
    {
        // Logic to update the appearance of the plant based on growthStage
        Debug.Log($"Plant {id} updated to growth stage {growthStage}");
    }
}