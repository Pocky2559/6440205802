using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class BuildCannonNotification : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;

    [Header("Required Components")]
    [SerializeField] private UnitDatabaseSO unitDatabase;
    [SerializeField] private ResourcesStatus resourcesStatus;
    [SerializeField] private HouseList population;

    [Header("Notify Require Artillery Shop")]
    [SerializeField] private TMP_Text notifyBuildArtilleryShop;
    [SerializeField] private Color notifyBuildArtilleryShopFade;

    [Header("Notify Need More Resources")]
    [SerializeField] private TMP_Text notifyNeedMoreResources;
    [SerializeField] private Color notifyNeedMoreResourcesFade;

    [Header("Notify Need More Houses")]
    [SerializeField] private TMP_Text notifyNeedMoreHouses;
    [SerializeField] private Color notifyNeedMoreHousesFade;
 
    private void Start() //make Text invisible
    {

        //Notify Require Artillery Shop
        notifyBuildArtilleryShopFade = notifyBuildArtilleryShop.color;
        notifyBuildArtilleryShopFade.a = 0;
        notifyBuildArtilleryShop.color = notifyBuildArtilleryShopFade;

        //Notify Need More Resources
        notifyNeedMoreResourcesFade = notifyNeedMoreResources.color;
        notifyNeedMoreResourcesFade.a = 0;
        notifyNeedMoreResources.color = notifyNeedMoreResourcesFade;

        //Notify Need More Houses
        notifyNeedMoreHousesFade = notifyNeedMoreHouses.color;
        notifyNeedMoreHousesFade.a = 0;
        notifyNeedMoreHouses.color = notifyNeedMoreHousesFade;
    }

    public void NotificationBuildCannon()
    {
        if (GameObject.FindGameObjectWithTag("Artillary") == null) //if don't have artillery shop
        {
            notifyBuildArtilleryShopFade.a = 1;
            notifyBuildArtilleryShop.color = notifyBuildArtilleryShopFade;
            StartCoroutine(CloseNotifyRequireArtilleryShop());
        }
        else if(GameObject.FindGameObjectWithTag("Artillary") != null
                && (resourcesStatus.food_Amount < unitDatabase.unitDetails[4].foodCost
                || resourcesStatus.gold_Amount < unitDatabase.unitDetails[4].goldCost))
        {
            notifyNeedMoreResourcesFade.a = 1;
            notifyNeedMoreResources.color = notifyNeedMoreResourcesFade;
            StartCoroutine(CloseNotifyNeedMoreResources());
        }
        else if (population.currentPopulation + unitDatabase.unitDetails[4].population > population.currentPopulationCapacity)
        {
            notifyNeedMoreHousesFade.a = 1;
            notifyNeedMoreHouses.color = notifyNeedMoreHousesFade;
            StartCoroutine(CloseNotifyNeedMoreHouses());
        }
    }
    IEnumerator CloseNotifyRequireArtilleryShop()
    {
        yield return new WaitForSeconds(2);
        while(notifyBuildArtilleryShop.color.a > 0)
        {
            notifyBuildArtilleryShopFade.a -= fadeSpeed * Time.deltaTime; //start fading
            notifyBuildArtilleryShop.color = notifyBuildArtilleryShopFade;
            yield return null;
        }
    }
    IEnumerator CloseNotifyNeedMoreResources()
    {
        yield return new WaitForSeconds(2);
        while (notifyNeedMoreResources.color.a > 0)
        {
            notifyNeedMoreResourcesFade.a -= fadeSpeed * Time.deltaTime; //start fading
            notifyNeedMoreResources.color = notifyNeedMoreResourcesFade;
            yield return null;
        }
    }
    IEnumerator CloseNotifyNeedMoreHouses()
    {
        yield return new WaitForSeconds(2);
        while (notifyNeedMoreHouses.color.a > 0)
        {
            notifyNeedMoreHousesFade.a -= fadeSpeed * Time.deltaTime; //start fading
            notifyNeedMoreHouses.color = notifyNeedMoreHousesFade;
            yield return null;
        }
    }
}
