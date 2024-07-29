using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penhouse : MonoBehaviour
{
    private static Penhouse _instance;
    public static Penhouse instance {get => _instance;}

    [SerializeField] private int _level = 0; public int level {get => _level;}
    [SerializeField] private int _wooden = 0; public int wooden {get => _wooden; }
    [SerializeField] private int _gold = 0; public int gold {get => _gold; }
    [SerializeField]  private int _rock = 0; public int rock {get => _rock; }


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

    private void Start()
    {
        GameManager.instance.LoadData(gameObject);
    }
    public void SetData(int rock, int gold, int wood)
    {
        _rock = rock;
        _gold = gold;
        _wooden = wood;
        
        UpdateUI();
    }

    public void ResetHealth()
    {
        GetComponent<MainHouseHealth>().Reset();
    }

    public void AddWood(int amount)
    {
        _wooden += amount;
        UpdateUI();
        Debug.Log($"Add Wood: {amount} => total: {_wooden}");
    }

    public void AddGold(int amount)
    {
        _gold += amount;
        UpdateUI();
        Debug.Log($"Add Gold: {amount} => total: {_gold}");
    }

    public void AddRock(int amount)
    {
        _rock += amount;
        UpdateUI();
        Debug.Log($"Add Rock: {amount} => total: {_rock}");
    }

    public void SubWood(int amount)
    {
        _wooden -= amount;
        Debug.Log($"Add Wood: {amount} => total: {_wooden}");
    }

    public void SubGold(int amount)
    {
        _gold += amount;
        Debug.Log($"Add Gold: {amount} => total: {_gold}");
    }

    public void SubRock(int amount)
    {
        _rock += amount;
        Debug.Log($"Add Rock: {amount} => total: {_rock}");
    }

    public bool UserResource(int rock, int gold, int wood)
    {
        // Debug.Log($"user resouce:{rock}   {gold}   {wood}");
        if (rock > _rock || gold > _gold || wood > _wooden) return false;
        Debug.Log($"{_gold}  {_rock}  {_wooden}");
        _rock -= rock;
        _gold -= gold;
        _wooden -= wood;
        Debug.Log($"{_gold}  {_rock}  {_wooden}");
        UpdateUI();
        return true;
    }
    

    public bool CheckUseReSource(int rock, int gold, int wood)
    {
        Debug.Log($"{_gold}  {_rock}  {_wooden}");
        if (rock > _rock || gold > _gold || wood > _wooden) return false;
        return true;
    }

    private void UpdateUI()
    {
        ResourceUI.instance.UpdateResource(_wooden, _rock, _gold);
    }

}
