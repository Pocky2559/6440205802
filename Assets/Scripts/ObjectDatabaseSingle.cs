using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[Serializable]
public class ObjectDatabaseSingle :ScriptableObject
{
    public GameObject Prefab;
    public int ID;
    public string Name;
    public string Info;
    
}
