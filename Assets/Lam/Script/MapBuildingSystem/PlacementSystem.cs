using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    private void Update() 
    {
        Vector3 mousePostion = GetMousOnWorld();
        Vector3Int gridPos = _grid.WorldToCell(mousePostion);
        Debug.Log(gridPos);
    }

    private Vector3 GetMousOnWorld()
    {
         Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         return new Vector3(mouseWorldPos.x, mouseWorldPos.y, 0);
    }
}
