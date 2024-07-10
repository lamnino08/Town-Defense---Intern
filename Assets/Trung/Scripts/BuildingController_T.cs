namespace Trung.Scene
{
    using UnityEngine;

    public class BuildingController_T : MonoBehaviour
    {
        public Vector3 _offset = Vector3.zero;
        [SerializeField] private bool _isDragging;
        [SerializeField] private Renderer _renderer;
        private ConstructData _data;
        public int width_x, height_z;
        public int idBuilding;

        private Vector3 _previous;
        public bool canMoving = true;
        public bool canEdit = false;

        private void Start()
        {
            _data = PlaceSystem.instance._data;
            _previous = transform.position;
            SetPlaneColor();
        }

        void Update()
        {
        }

        private void OnMouseDown()
        {
            if (!canMoving && canEdit)
            {
                if (PlaceSystem.instance.CurrentBuildExist())
                {
                    return;
                }
                else
                {
                    MainUI_T.instance._edit_BTN.SetActive(true);
                }
            }
            else
            {
                MainUI_T.instance._edit_BTN.SetActive(false);
            }
        }

        private void OnMouseDrag()
        {
            if (canMoving)
            {
                CameraShop.instance._movingBuilding = true;
                Vector3 mousePosInGrid = PlaceSystem.instance.GetPositionGrid();
                if (_previous != mousePosInGrid)
                {
                    Movement(mousePosInGrid);
                }
                SetPlaneColor();
            }
        }

        private void CheckData(Vector3 mousePosInGrid)
        {
            // if (GridSystem.instance.IsPositionIsPlace(mousePosInGrid))
            // {
            //     _renderer.material.color = Color.green;
            // }
            // else
            // {
            //     _renderer.material.color = Color.red;
            // }
        }

        private void SetPlaneColor()
        {
            if (CheckPos())
            {
                _renderer.material.color = Color.green;
            }
            else
            {
                LogicScript.instance._currentBuild = this;
                _renderer.material.color = Color.red;
            }
        }

        public bool CheckPos()
        {
            float posX = transform.position.x;
            float posZ = transform.position.z;
            if (posX - width_x < -11 || posX + width_x > 28 || posZ - height_z < -14 || posZ + height_z > 35)
            {
                return false;
            }
            if (CheckRectOverLap()) return false;
            return true;
        }

        public bool CheckOverlap()
        {
            Bounds planeBounds = _renderer.bounds;
            foreach (var building in GridMap.instance.Buildings)
            {
                if (building != this)
                {
                    if (building._renderer.bounds.Intersects(planeBounds))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public bool CheckRectOverLap()
        {
            float x = transform.position.x;
            float z = transform.position.z;
            float w = _renderer.bounds.size.x;
            float h = _renderer.bounds.size.z;

            Rect rect2 = new Rect(x, z, w, h);
                foreach (var building in GridMap.instance.Buildings)
                {
                    if (building != this)
                    {
                        float xx = building.transform.position.x;
                        float zz = building.transform.position.z;
                        float ww = building._renderer.bounds.size.x;
                        float hh = building._renderer.bounds.size.z;
                        Rect rect = new Rect(xx, zz, ww, hh);
                        if (rect2.Overlaps(rect))
                        {
                            return true;
                        }
                    }
            }
            return false;
        }


        private void Movement(Vector3 mousePosInGrid)
        {
            Vector3 nepose = new Vector3(mousePosInGrid.x, 0.022f, mousePosInGrid.z);
            transform.position = nepose + _offset;
            _previous = transform.position;
        }

        private void OnMouseUp()
        {
            /*_renderer.material.color = Color.green;
            Vector3 mousePosInGrid = PlaceSystem.instance.GetPositionGrid();
            Vector3 nepose = new Vector3(mousePosInGrid.x, 0.02f, mousePosInGrid.z);
            MainUI_T.instance.SetActiveButtonBuilding(this);*/
            CameraShop.instance._movingBuilding = false;
        }

    }

}