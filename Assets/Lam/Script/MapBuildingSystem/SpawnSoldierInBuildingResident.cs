using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSoldierInBuildingResident : SpawnSoldierInBuilding
{
    public override void Spawn()
    {
        foreach(Transform e in marks)
        {
            Instantiate(soldierPrefab, e.position, Quaternion.identity).GetComponent<ResidentMovement>().SetOwnHome(transform);
        }
    }

    
}
