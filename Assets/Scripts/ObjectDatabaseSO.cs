using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ObjectDatabaseSO : ScriptableObject
{
    public List<ObjectInfo> objects;
}

[Serializable]
public class ObjectInfo
{
    public GameObject Prefab;
    public int ID;
    public string Name;
}



