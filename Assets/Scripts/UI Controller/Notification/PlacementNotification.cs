using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlacementNotification : MonoBehaviour
{
    [SerializeField] private GameObject canPlaceIcon;
    [SerializeField] private GameObject cannotPlaceIcon;
    [SerializeField] private GameObject notifyNeedMoreResourcesText;
    [SerializeField] private FloatingUI floatingUICanPlace;
    [SerializeField] private FloatingUI floatingUICannotPlace;
    [SerializeField] private FloatingUI floatinfNotifyText;
    [SerializeField] private PreviewSystem previewSystem;

    public void ShowIndicatorCanPlace(GameObject positionOfIcon)
    {
        if (positionOfIcon != null)
        {
            canPlaceIcon.SetActive(true);
            cannotPlaceIcon.SetActive(false);
            floatingUICanPlace.targetFloatingUI = positionOfIcon.transform;
        }
    }

   public void ShowIndicatorCannotPlace(GameObject positionOfIcon)
   {
        if(positionOfIcon != null)
        {
            cannotPlaceIcon.SetActive(true);
            canPlaceIcon.SetActive(false);
            floatingUICannotPlace.targetFloatingUI = positionOfIcon.transform;
        }
    }

   public void ShowNeedMoreResources()
   {
       
        if(previewSystem.previewBuilding != null)
        {  
           notifyNeedMoreResourcesText.SetActive(true);
           floatinfNotifyText.targetFloatingUI = previewSystem.previewBuilding.transform;
           canPlaceIcon.SetActive(false);
           cannotPlaceIcon.SetActive(false);
        }
   }
   public void HideNeedMoreResources()
   {
        notifyNeedMoreResourcesText.SetActive(false);
    }
   public void HideIndicator()
   {
        cannotPlaceIcon.SetActive(false);
        canPlaceIcon.SetActive(false);
   }

   
}
