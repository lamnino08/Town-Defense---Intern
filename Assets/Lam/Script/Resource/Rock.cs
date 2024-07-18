using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Rock Resource", menuName = "Resources/Rock")]
public class Rock : Resource
{
    public override void Manufacture()
    {
        Penhouse.instance.AddRock(amount);
    }
}