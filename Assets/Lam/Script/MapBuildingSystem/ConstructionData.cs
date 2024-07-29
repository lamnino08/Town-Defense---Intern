using System;
using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


[Serializable]
public class Cost
{
    public int rock;
    public int wooden;
    public int gold;

    public bool Check()
    {
        if (!Penhouse.instance.CheckUseReSource(rock, gold, wooden)) return false;

        return true;
    }

    public void Use()
    {
        Penhouse ph = Penhouse.instance;
        if (!ph.UserResource(rock, gold, wooden))
        {
            Debug.Log("Khong du tai nguyen nhung da xay cong trinh");
        }
    }
    
}

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
    public GameObject prefab {get; private set;}
    [field: SerializeField]
    public Cost costToBuild {get; private set;}
}

public class ConstructionData : MonoBehaviour
{
    private static ConstructionData _instance;
    public static ConstructionData instance;
    [SerializeField] private List<ObjectData> _listConstruction = new List<ObjectData>();
    [SerializeField] private GameObject _buildingDestroyEffect; public GameObject buildingDestroyEffect => _buildingDestroyEffect;
    [SerializeField] private GameObject _charcaterDestroy; public GameObject charcaterDestroy => _charcaterDestroy;
    [SerializeField] private GameObject _goldObject; public GameObject gold => _goldObject;

    private void Awake() 
    {
        if (_instance ==  null)
        {
            instance = this;
        }     else
        {
            Destroy(gameObject);
        }
    }

    public ObjectData GetObjectDataById(int id)
    {
        return _listConstruction.Find(d => d.id == id);
    }

    public ObjectData GetObjectDataByIndex(int index)
    {
        return _listConstruction[index];
    }
}
