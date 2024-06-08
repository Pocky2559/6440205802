using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingFunctionNotify : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;

    [Header("Notify Not Enough Resources")]
    [SerializeField] private TMP_Text notEnoughResourcesTMPText;
    [SerializeField] private Color notEnoughResourcesFade;

    [Header("Notify Need more house")]
    [SerializeField] private TMP_Text needMoreHousesTMPText;
    [SerializeField] private Color needMoreHousesFade;

    private void Start()
    {
        // Notify Not EnoughResources
        notEnoughResourcesFade = notEnoughResourcesTMPText.color;
        notEnoughResourcesFade.a = 0;
        notEnoughResourcesTMPText.color = notEnoughResourcesFade;

        // Notify Need More Houses
        needMoreHousesFade = needMoreHousesTMPText.color;
        needMoreHousesFade.a = 0;
        needMoreHousesTMPText.color = needMoreHousesFade;
    }

    //Notify "Not Enough Resources"
    public void NotifyNotEnoughResources()
    {
        notEnoughResourcesFade.a = 1;
        notEnoughResourcesTMPText.color = notEnoughResourcesFade;
        StartCoroutine(CloseNotifyNotEnoughResources());
    }
    IEnumerator CloseNotifyNotEnoughResources()
    {
        yield return new WaitForSeconds(1); // wait for 1.5 seconds

        while(notEnoughResourcesTMPText.color.a > 0)
        {
            notEnoughResourcesFade.a -= fadeSpeed * Time.deltaTime; //start fading
            notEnoughResourcesTMPText.color = notEnoughResourcesFade;
            yield return null;
        }
    }

    //Notify Need more Houses
    public void NotifyNeedMoreHouses()
    {
        needMoreHousesFade.a = 1;
        needMoreHousesTMPText.color = needMoreHousesFade;
        StartCoroutine(CloseNotifyNeedMoreHouses());
    }
    IEnumerator CloseNotifyNeedMoreHouses()
    {
        yield return new WaitForSeconds(1);
        while(needMoreHousesTMPText.color.a > 0)
        {
            needMoreHousesFade.a -= fadeSpeed * Time.deltaTime; //start fading
            needMoreHousesTMPText.color = needMoreHousesFade;
            yield return null;
        }
    }
}
