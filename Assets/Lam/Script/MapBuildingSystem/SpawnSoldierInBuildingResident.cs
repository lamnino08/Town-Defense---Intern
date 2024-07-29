using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierInBuildingResident : SpawnSoldierInBuilding
{
    public override void Spawn()
    {
        foreach(Transform e in marks)
        {
            GameObject p = Instantiate(soldierPrefab, e.position, Quaternion.identity);
            p.GetComponent<ResidentMovement>().SetOwnHome(transform);
            GameManager.instance.Residents.Add(p);
        }
    }

    
}
