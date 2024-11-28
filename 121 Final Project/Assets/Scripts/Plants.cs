using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Plant : SaveableObject
{
   public int growthStage; // Current growth stage of the plant


   public override ObjectState GetState()
   {
       Debug.Log("In get function");
       ObjectState state = new ObjectState
       {
           id = id,
           position = transform.position,
           rotation = transform.rotation,
           growthStage = growthStage // Save growth stage
       };
       Debug.Log("Get growth: " + growthStage);


       return state;
   }


   public override void SetState(ObjectState state)
   {
       Debug.Log("In set function");
       transform.position = state.position;
       transform.rotation = state.rotation;
       growthStage = state.growthStage; // Restore growth stage
       Debug.Log("Set growth: " + growthStage);
       UpdatePlantVisuals();
   }


   private void UpdatePlantVisuals()
   {
       Debug.Log("In update function");
       // Logic to update the appearance of the plant based on growthStage
       Debug.Log("Plant growth: " + growthStage);
       Debug.Log($"Plant {id} updated to growth stage {growthStage}");
   }
}
