using UnityEngine;

namespace Trung.Scene
{
    public class PlaceSystem : MonoBehaviour
    {
        private static PlaceSystem _instance;
        public static PlaceSystem instance { get => _instance; }
        [SerializeField] private Grid _grid;
        public ConstructData _data;

        private GameObject _currentBuild;

        public Camera _camera;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else
            {
                Destroy(this);
            }
            _camera = Camera.main;
        }

        private void Update()
        {
        }

        public void StartPlaceBuilding(int id)
        {
            BuildingData data = _data.GetObjectDataById(id);
            _currentBuild = Instantiate(data.prefab[0], Vector3.zero, Quaternion.identity);
            MainUI_T.instance.build_Status(true);
            MainUI_T.instance.edit_Status(false);
            BuildingController_T script = _currentBuild.GetComponent<BuildingController_T>();
            script.idBuilding = id;
            if ((data.width & 1) == 0)
            {
                script._offset = new Vector3(0.5f, 0, 0.5f);
            }

        }

        private Vector3 GetMouseOnWorld()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, 0);

            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                return hitPoint;
            }

            return Vector3.zero;
        }

        public Vector3 GetPositionGrid()
        {
            Vector3 mousePosition = GetMouseOnWorld();
            Vector3Int cellPosition = _grid.WorldToCell(mousePosition);
            Vector3 cellCenterPosition = _grid.GetCellCenterWorld(cellPosition);
            return cellCenterPosition;
        }



        public void CancelBuild()
        {
            if (_currentBuild != null)
            {
                Destroy(_currentBuild);
                _currentBuild = null;
            }
        }

        public void ConfirmBuild()
        {
            if ( _currentBuild != null)
            {
                Debug.Log(_currentBuild.GetComponent<BuildingController_T>().CheckPos() == true);
                if (_currentBuild.GetComponent<BuildingController_T>().CheckPos())
                {
                    BuildingController_T temp = _currentBuild.GetComponent<BuildingController_T>();
                    GridMap.instance.AddBuilding(temp);
                    Debug.Log(GridMap.instance.Buildings.Count);
                    EditStatus(false);
                    MainUI_T.instance.build_Status(false);
                    _currentBuild = null;
                }
                else return;
            }
        }

        public void EditStatus(bool status)
        {
            _currentBuild.GetComponent<BuildingController_T>().canMoving = status;
        }

        public bool CurrentBuildExist()
        {
            if (_currentBuild != null)
            {
                return true;
            }
            return false;
        }

        

    }
}