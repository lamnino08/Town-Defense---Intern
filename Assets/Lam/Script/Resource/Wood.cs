using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wood Resource", menuName = "Resources/Wood")]
public class Wood : Resource
{
    public override void Manufacture()
    {
        Penhouse.instance.AddWood(amount);
    }
}
