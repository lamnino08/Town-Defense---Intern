using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableTile : ScriptableObject
{
    public int levelIndex;
    public List<SaveTile> groundTiles;
    public List<SaveTile> unitTiles;
}
[Serializable]
public class SaveTile
{
    public Vector3Int pos;
    public UnitTile tile;
}
