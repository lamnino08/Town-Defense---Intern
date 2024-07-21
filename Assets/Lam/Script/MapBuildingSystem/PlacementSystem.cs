using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    private static PlacementSystem _instance;
    public static PlacementSystem instance { get => _instance; }
    [SerializeField] private Grid _grid;
    [SerializeField] private ConstructionData _data;
    [SerializeField] private Collider _planeCollider;
    private Camera _camera;
    public GameObject _currentBuil;

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

    /// <summary>
    /// Spawn a building when click build a building a building on UI
    /// </summary>
    /// <param name="id"></param>
    public void StartPlaceBuilding(int id)
    {
        ObjectData data = _data.GetObjectDataById(id);
        if (_currentBuil != null)
        {
            Destroy(_currentBuil);
        }
        
        if (data.costToBuild.Check())
        {
            _currentBuil = Instantiate(data.prefab, Vector3.zero, Quaternion.identity);
            BuildingManager script = _currentBuil.GetComponent<BuildingManager>();
            script.StartPlace(data);
        } else
        {
            Debug.Log("Ngheo qua xay khong duoc nha");
        }
    }

    private Vector3 GetMouseOnWorld()
    {

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (_planeCollider.Raycast(ray, out hit, Mathf.Infinity))
        {
             return hit.point;
        }

        return Vector3.zero;
    }

    public Vector3 GetPositionGrid()
    {
        Vector3 mousePosition = GetMouseOnWorld();
        Vector3Int cellPosition = _grid.WorldToCell(mousePosition);
        Vector3 cellCenterPosition = _grid.GetCellCenterWorld(cellPosition); 
        cellCenterPosition.y = 0;
        return cellCenterPosition;
    }
}
