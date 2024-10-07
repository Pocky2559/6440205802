using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummariseGatheredResources : MonoBehaviour
{
    [SerializeField] private ResourcesStatus resourcesStatus;

    [Header("Summarise Resources After Lose")]
    [SerializeField] private TMP_Text food_Amount_L;
    [SerializeField] private TMP_Text wood_Amount_L;
    [SerializeField] private TMP_Text gold_Amount_L;
    [SerializeField] private TMP_Text stone_Amount_L;

    [Header("Summarise Resources After Win")]
    [SerializeField] private TMP_Text food_Amount_W;
    [SerializeField] private TMP_Text wood_Amount_W;
    [SerializeField] private TMP_Text gold_Amount_W;
    [SerializeField] private TMP_Text stone_Amount_W;

    private void Awake()
    {
        resourcesStatus = GameObject.FindGameObjectWithTag("ResourcesStatus").GetComponent<ResourcesStatus>();
    }

    //Call this method when the game is finished
    public void SummariseResourcesAfterLose()
    {
        food_Amount_L.text = resourcesStatus.food_Amount.ToString();
        wood_Amount_L.text = resourcesStatus.wood_Amount.ToString();
        gold_Amount_L.text = resourcesStatus.gold_Amount.ToString();
        stone_Amount_L.text = resourcesStatus.stone_Amount.ToString();
    }

    public void SummariseResourcesAfterWin()
    {
        food_Amount_W.text = resourcesStatus.food_Amount.ToString();
        wood_Amount_W.text = resourcesStatus.wood_Amount.ToString();
        gold_Amount_W.text = resourcesStatus.gold_Amount.ToString();
        stone_Amount_W.text = resourcesStatus.stone_Amount.ToString();
    }
}
