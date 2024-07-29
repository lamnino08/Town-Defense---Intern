using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerData", menuName = "ScriptableManager")]
public class ScriptableManager : ScriptableObject 
{
    public int _rock; public int rock => _rock;
    public int _gold; public int gold => _gold;
    public int _wooden; public int wooden => _wooden;

    public void Save(int rock, int gold, int wood)
    {
        _wooden = wood;
        _rock = rock;
        _gold = gold;
    }
}
