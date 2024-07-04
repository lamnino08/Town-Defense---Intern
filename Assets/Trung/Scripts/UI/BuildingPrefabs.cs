namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using Trung.Scene.XTrung.Scene;
    using UnityEngine;
    using UnityEngine.UI;

    public class BuildingPrefabs : MonoBehaviour
    {
        [SerializeField] private string buildingName = "";
        [SerializeField] private TextMeshProUGUI nameBuilding = null;
        [SerializeField] private Image image = null;
        [SerializeField] private Button _button = null;
        [SerializeField] private int _prefabIndex = 0;

        void Start()
        {
            nameBuilding.text = buildingName;
            _button.onClick.AddListener(Clicked);
        }

        private void Clicked()
        {
            UI_Shop.instance.SetStatus(false);
            UI_Main.instance.SetStatus(true);
            UI_Build.instance.SetStatus(true);
            Vector3 position = Vector3.zero;

            Building building = Instantiate(UI_Main.instance._buildingPrefabs[_prefabIndex], position, Quaternion.identity);

            building.PlacedOnGrid(9, 23);

            Building.instance = building;
            CameraController.instance.isPlacingBuilding = true;
        }
    }
}
