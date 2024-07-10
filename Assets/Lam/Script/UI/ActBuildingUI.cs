using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBuildingUI : MonoBehaviour
{
    private static ActBuildingUI _instance;
    public static ActBuildingUI instance {get => _instance; }

    public BuildingController _currentBuildingAction;

    [SerializeField] private GameObject _buildingActionUI;
    [SerializeField] private GameObject _upgradeBtn;
    [SerializeField] private GameObject _confirmBtn;
    [SerializeField] private GameObject _cancelBtn;

    private void Start() 
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        } 
        Destroy(this.gameObject);
    }

    public void StartPlaceBuilding(BuildingController buildingScript)
    {
        _currentBuildingAction = buildingScript;

        //UI
        _buildingActionUI.SetActive(_buildingActionUI);
        _upgradeBtn.SetActive(false);
        _confirmBtn.SetActive(true);
        _cancelBtn.SetActive(false);
    }

    public void CoinfirmPlaceBuidling()
    {
        _currentBuildingAction.Place();
        //UI
        // _buildingActionUI.SetActive(_buildingActionUI);
        // _upgradeBtn.SetActive(false);
        // _confirmBtn.SetActive(true);
        // _cancelBtn.SetActive(false);
    }

}
