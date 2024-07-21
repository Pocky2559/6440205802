using System.Collections;
using System.Collections.Generic;
//using UnityEditor.MPE;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public int ID;
    public bool isSelectBuilding;
    private bool isResourcesEnough;
    [SerializeField] PreviewSystem previewSystem;
    [SerializeField] private BuildingCost buildingCost;
    [SerializeField] private ResourcesStatus resourcesStatus;
    [SerializeField] private PlacementNotification placementNotification;
   public void GetIndexFromButton(int index)
   {
        ID = index;
        isSelectBuilding = true;
        previewSystem.CreatObjectPreview();
   }

   private void Update() // if not enough resources to build. it will notify player that it is need more resources
   {
      if ((resourcesStatus.wood_Amount < buildingCost.buildingCostsData[ID].woodRequire 
           || resourcesStatus.stone_Amount < buildingCost.buildingCostsData[ID].stoneRequire)
           && isSelectBuilding == true)
      {
          //Notify Need More Resources
          placementNotification.ShowNeedMoreResources();
          isResourcesEnough = false;
      }
      else if((resourcesStatus.wood_Amount >= buildingCost.buildingCostsData[ID].woodRequire
           || resourcesStatus.stone_Amount >= buildingCost.buildingCostsData[ID].stoneRequire)
           && isSelectBuilding == true)
        {
          placementNotification.HideNeedMoreResources();
          if(isResourcesEnough == false) 
          {
                isResourcesEnough = true;
                previewSystem.CanPlaceObjectStatus();
          }
      }
   }
}
