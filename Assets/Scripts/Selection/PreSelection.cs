using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreSelection : MonoBehaviour
{
    public GameObject preSelectionIndicator;

    private void OnMouseOver()
    {
        if (preSelectionIndicator != null)
        {
           preSelectionIndicator.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if(preSelectionIndicator != null)
        {
          preSelectionIndicator.SetActive(false);
        }
    }
}
