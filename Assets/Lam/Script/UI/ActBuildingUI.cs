using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBuildingUI : MonoBehaviour
{
    private static ActBuildingUI _instance;
    public static ActBuildingUI instance {get => _instance; }

    [SerializeField] private GameObject _buildingActionUI;
    [SerializeField] private GameObject _upgradeBtn;
    // [SerializeField] private GameObject _confirmBtn;
    [SerializeField] private GameObject _removeBtn;
    [SerializeField] private PlacementSystem _placemenSystem;

    private void Start() 
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        } 
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Set UI for like confirm, cancel button on UI when Place or click a building
    /// </summary>
    public void StartPlaceBuilding(int id)
    {
        _placemenSystem.StartPlaceBuilding(id);
    }

    public void AllowRemove(bool active)
    {
        _removeBtn.SetActive(active);
    }

    public void ExitUI()
    {
       
    }

}
