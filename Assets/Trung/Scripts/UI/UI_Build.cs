namespace Trung.Scene
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    public class UI_Build : MonoBehaviour
    {
        [SerializeField] public GameObject _elements = null;

        public RectTransform buttonConfirm = null;
        public RectTransform buttonCancel = null;
        private static UI_Build _instance = null; public static UI_Build instance { get { return _instance; } }

        private bool _active = true; public bool isActive { get { return _active; } }

        private void Awake()
        {
            _instance = this;
            _elements.SetActive(false);
        }

        private void Start()
        {
            buttonConfirm.gameObject.GetComponent<Button>().onClick.AddListener(Confirm);
            buttonCancel.gameObject.GetComponent<Button>().onClick.AddListener(Cancel);
            buttonConfirm.anchorMin = Vector3.zero;
            buttonConfirm.anchorMax = Vector3.zero;
            buttonCancel.anchorMin = Vector3.zero;
            buttonCancel.anchorMax = Vector3.zero;
        }

        private void Update()
        {
            if (Building.instance != null && CameraController.instance.isPlacingBuilding)
            {
                Vector3 end = UI_Main.instance._grid.GetEndPosition(Building.instance);

                Vector3 planeDownLeft = CameraController.instance.CameraScreenPositionToPlanePosition(Vector2.zero);
                Vector3 planeTopRight = CameraController.instance.CameraScreenPositionToPlanePosition(new Vector2(Screen.width, Screen.height));

                float w = planeTopRight.x - planeDownLeft.x;
                float h = planeTopRight.z - planeDownLeft.z;

                float endW = end.x - planeDownLeft.x;
                float endH = end.z - planeDownLeft.z;

                Vector2 screenPoint = new Vector2(endW / w * Screen.width, endH / h * Screen.height);
                Vector2 confirmPoint = screenPoint;
                confirmPoint.x += (buttonConfirm.rect.width + 10f);
                buttonConfirm.anchoredPosition = confirmPoint;

                Vector2 cancelPoint = screenPoint;
                cancelPoint.x -= (buttonCancel.rect.width + 10f);
                buttonCancel.anchoredPosition = cancelPoint;
            }
        }

        public void SetStatus(bool status)
        {
            _active = status;
            _elements.SetActive(status);
        }

        public void Confirm()
        {
            if (Building.instance != null)
            {
                if (UI_Main.instance._grid.CanPlaceBuilding(Building.instance, Building.instance.currentX, Building.instance.currentY))
                {
                    Instantiate(Building.instance);
                    UI_Main.instance._grid.buildings.Add(Building.instance);
                }
            }
        }

        public void Cancel()
        {
            if (Building.instance != null)
            {
                CameraController.instance.isPlacingBuilding = false;
                Building.instance.RemovedFromGrid();
                Debug.Log("UI_Build: Cancel");
            }
        }
    }
}
