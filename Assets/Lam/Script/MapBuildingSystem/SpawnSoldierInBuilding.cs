using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierInBuilding : MonoBehaviour
{
    [SerializeField] protected List<Transform> marks = new List<Transform>();
    [SerializeField] protected GameObject soldierPrefab;

    public virtual void Spawn()
    {
        foreach(Transform e in marks)
        {
            GameObject p = Instantiate(soldierPrefab, e.position, Quaternion.identity);
            GameManager.instance.soldier.Add(p);
        }
    }

    
}
