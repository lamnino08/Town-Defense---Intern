using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class ObjectData
{
    [field: SerializeField]
    public int id {get; private set; }
    // [field: SerializeField]
    // public int level {get; private set; }
    [field: SerializeField]
    public int height {get; private set; }
    [field: SerializeField]
    public int width {get; private set; }
    [field: SerializeField]
    // public int direction {get; private set; }
    // [field: SerializeField]
    public float[] health {get; private set; }
    [field: SerializeField]
    public GameObject[] prefab {get; private set;}

}

public class ConstructionData : MonoBehaviour
{
    [SerializeField] List<ObjectData> _listConstruction = new List<ObjectData>();

    public ObjectData GetObjectDataById(int id)
    {
        Debug.Log("here");
        return _listConstruction.Find(d => d.id == id);
    }
}
