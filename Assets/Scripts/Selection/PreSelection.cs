using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSelection : MonoBehaviour
{
    public GameObject preSelectionIndicator;

    private void OnMouseOver()
    {
        preSelectionIndicator.SetActive(true);
    }

    private void OnMouseExit()
    {
        preSelectionIndicator.SetActive(false);
    }
}
