using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcesStatus : MonoBehaviour // this script is contain about all resources in the game
{
    public int food_Amount;
    public int wood_Amount;
    public int gold_Amount;
    public int stone_Amount;
    public LevelResourcesDatabase levelResourcesDatabase;
    public int levelNumber;

    public TMP_Text food_Text;
    public TMP_Text wood_Text;
    public TMP_Text gold_Text;
    public TMP_Text stone_Text;

    public void Awake()
    {
        food_Amount = levelResourcesDatabase.levelData[levelNumber].foodAmount;
        wood_Amount = levelResourcesDatabase.levelData[levelNumber].woodAmount;
        gold_Amount = levelResourcesDatabase.levelData[levelNumber].goldAmount;
        stone_Amount = levelResourcesDatabase.levelData[levelNumber].stoneAmount;

        food_Text.text = food_Amount.ToString();
        wood_Text.text = wood_Amount.ToString();
        gold_Text.text = gold_Amount.ToString();
        stone_Text.text = stone_Amount.ToString();
    }

    public void ResourcesChange(string resourcesName, int resourcesAmount) // when the resources is change this method will be active to calculate new value
    {
        if(resourcesName == "Food")
        {
            food_Amount = food_Amount + resourcesAmount;
            food_Text.text = food_Amount.ToString();
        }

        if (resourcesName == "Wood")
        {
            wood_Amount = wood_Amount + resourcesAmount;
            wood_Text.text = wood_Amount.ToString();
        }

        if (resourcesName == "Gold")
        {
            gold_Amount = gold_Amount + resourcesAmount;
            gold_Text.text = gold_Amount.ToString();
        }

        if (resourcesName == "Stone")
        {
            stone_Amount = stone_Amount + resourcesAmount;
            stone_Text.text = stone_Amount.ToString();
        }
    }
}
