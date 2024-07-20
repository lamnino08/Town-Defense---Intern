using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierInBuilding : MonoBehaviour
{
    [SerializeField] List<Transform> marks = new List<Transform>();
    [SerializeField] GameObject soldierPrefab;

    public void Spawn()
    {
        foreach(Transform e in marks)
        {
            Instantiate(soldierPrefab, e.position, Quaternion.identity);
        }
    }

    
}
