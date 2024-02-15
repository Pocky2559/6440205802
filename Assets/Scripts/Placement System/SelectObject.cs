using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    public int ID;
    [SerializeField] PreviewSystem previewSystem;
   public void GetIndexFromButton(int index)
    {
        ID = index;
        previewSystem.CreatObjectPreview();
    }
}
