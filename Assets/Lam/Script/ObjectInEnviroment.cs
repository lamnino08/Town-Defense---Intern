using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInEnviroment : MonoBehaviour
{
    private LayerMask _wallLayerMask;
    /*
        0 => north
        1 => East
        2 => South
        3 => West
    */
    private Transform[] Wall = new Transform[4];
}
