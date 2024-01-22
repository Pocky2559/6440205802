using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewStatus : MonoBehaviour
{
    public Material validMaterial;
    public Material invalidMaterial;

    //these method use for get the material from the reference Game Object and assign new material for them
    //PlacementValid() use for assigning the new material of reference Game Object into green color to tell that you can place this Game Object
    public void PlacementValid()
    {
        MeshRenderer meshRenderer= GetComponent<MeshRenderer>();
        Material oldMaterial = meshRenderer.material;
        meshRenderer.material = validMaterial;
    }
    //PlacementInValid() use for assigning the new material of reference Game Object into red color to tell that you cannot place this Game Object
    public void PlacementInValid()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material oldMaterial = meshRenderer.material;
        meshRenderer.material = invalidMaterial;
    }

}
