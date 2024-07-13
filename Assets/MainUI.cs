using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    private static MainUI _instance;
    public static MainUI instance {get => _instance; }
    [SerializeField] GameObject _buttonBuildingUI;

    private void Start() 
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetActiveButtonBuilding(BuildingController active)
    {
        if (active)
        {
            _buttonBuildingUI.SetActive(true);
        } else
        {
            _buttonBuildingUI.SetActive(false);
        }
    }
}
