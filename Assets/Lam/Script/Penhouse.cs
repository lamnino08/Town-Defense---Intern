using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penhouse : MonoBehaviour
{
    private static Penhouse _instance;
    public static Penhouse instance {get => _instance;}

    private int _level; public int level {get => _level;}
    private int _wooden; public int wooden {get => _wooden; }
    private int _gold; public int gold {get => _gold; }
    private int _rock; public int roc {get => _rock; }


    private void Awake() 
    {
        if (_instance && _instance != this)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
        }
    }    

    public void AddWood(int amount)
    {
        _wooden += amount;
        Debug.Log($"Add Wood: {amount} => total: {_wooden}");
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        Debug.Log($"Add Gold: {amount} => total: {_gold}");
    }

    public void AddRock(int amount)
    {
        _rock += amount;
        Debug.Log($"Add Rock: {amount} => total: {_rock}");
    }
}
