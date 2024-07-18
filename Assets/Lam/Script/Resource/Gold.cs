using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Resource", menuName = "Resources/Gold")]
public class Gold : Resource
{
    public override void Manufacture()
    {
        Penhouse.instance.AddGold(amount);
    }
}
