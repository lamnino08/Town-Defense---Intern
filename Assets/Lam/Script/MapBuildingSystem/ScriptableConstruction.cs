using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConstructionData", menuName = "2D/Tiles/ConstructionData")]
public class ScriptableConstruction : ScriptableObject
{
    public List<NodeData> constructions;
}

