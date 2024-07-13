using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActBuildingUI : MonoBehaviour
{
    private static ActBuildingUI _instance;
    public static ActBuildingUI instance {get => _instance; }

    private BuildingController _currentBuildingAction;

    [SerializeField] private GameObject _buildingActionUI;
    [SerializeField] private GameObject _upgradeBtn;
    // [SerializeField] private GameObject _confirmBtn;
    [SerializeField] private GameObject _removeBtn;

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
    public void StartPlaceBuilding(BuildingController buildingScript)
    {
        _currentBuildingAction = buildingScript;

        //UI
        _buildingActionUI.SetActive(_buildingActionUI);
        _upgradeBtn.SetActive(false);
        // _confirmBtn.SetActive(true);
    }

    /// <summary>
    /// Confirm button click on UI
    /// </summary>
    public void CoinfirmPlaceBuidling()
    {
        _currentBuildingAction.Place();

        //UI
        // _buildingActionUI.SetActive(_buildingActionUI);
        // _upgradeBtn.SetActive(false);
        // _confirmBtn.SetActive(true);
        // _removeBtn.SetActive(false);
    }

    /// <summary>
    /// Remove building click on UI
    /// </summary>
    public void RemoveBuildingBtn()
    {
        _currentBuildingAction.Remove();
        _buildingActionUI.SetActive(false);
    }

    /// <summary>
    /// Set UI for like confirm, cancel button on UI when Place or click a building
    /// </summary>
    public void ClickOnBuilding(BuildingController buildingScript)
    {
        if (_currentBuildingAction && !_currentBuildingAction.isPlaced)
        {
            Destroy(_currentBuildingAction.gameObject);
        }
        _currentBuildingAction = buildingScript;

        //UI
        _buildingActionUI.SetActive(true);
        _upgradeBtn.SetActive(true);
        _removeBtn.SetActive(true);
    }

    public void AllowRemove(bool active)
    {
        _removeBtn.SetActive(active);
    }

    public void ExitUI()
    {
        if (!_currentBuildingAction.isPlaced)
        {
            Destroy(_currentBuildingAction.gameObject);
        }
        _buildingActionUI.SetActive(false);
    }

}
