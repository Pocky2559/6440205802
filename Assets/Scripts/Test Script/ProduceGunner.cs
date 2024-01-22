using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceGunner : MonoBehaviour
{
    public GameObject gunner;
  public void TrainGunner()
  {
        Debug.Log("Train Gunner");
      Instantiate(gunner);
  }
}
