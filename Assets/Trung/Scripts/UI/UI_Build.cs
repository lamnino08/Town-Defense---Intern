namespace Trung.Scene
{
    namespace XTrung.Scene
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
                
            }

            private void Start()
            {
                _elements.SetActive(false);
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
                    screenPoint.y += 30;
                    Vector2 confirmPoint = screenPoint;
                    confirmPoint.x += (buttonConfirm.rect.width - 20f);
                    buttonConfirm.anchoredPosition = confirmPoint;

                    Vector2 cancelPoint = screenPoint;
                    float des = Building.instance.rows == 1 ? 20f : GetMappedValue(CameraController.instance._zoom);
                    cancelPoint.x -= (buttonCancel.rect.width + des);
                    buttonCancel.anchoredPosition = Vector2.Lerp(buttonCancel.position, cancelPoint, 100 * Time.deltaTime);
                }
            }

            public float GetMappedValue(float value)
            {
                switch (value)
                {
                    case < 1: return 500; 
                    case < 1.5f: return 450; 
                    case <= 2: return 260;
                    case <= 2.5f: return 200;
                    case <= 3: return 165;
                    case <= 3.5f: return 120;
                    case <= 4: return 100;
                    case <= 4.5f: return 90;
                    case <= 5: return 85;
                    case <= 5.5f: return 80;
                    case <= 6: return 65;
                    case <= 6.5f: return 60;
                    case <= 7: return 60;
                    case <= 7.5f: return 50;
                    case <= 8: return 50;
                    case <= 8.5f: return 35;
                    default: return 35;
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
                    if (Building.instance != null)
                    {
                        Building temp = Building.instance;

                        if (UI_Main.instance._grid.CanPlaceBuilding(temp, temp.currentX, temp.currentY))
                        {
                            Building newBuilding = Instantiate(temp);

                            newBuilding.PlacedOnGrid(temp.currentX, temp.currentY);

                            UI_Main.instance._grid.buildings.Add(newBuilding);

                            Cancel();
                        }
                        else
                        {
                            Debug.Log("Cannot place the building at the specified position.");
                        }
                    }
                }
            }

            public void Cancel()
            {
                if (Building.instance != null)
                {
                    CameraController.instance.isPlacingBuilding = false;
                    Building.instance.RemovedFromGrid();
                }
            }
        }

    }

}
