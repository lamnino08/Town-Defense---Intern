using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    private static PlacementSystem _instance;
    public static PlacementSystem instance { get => _instance; }
    [SerializeField] private Grid _grid;
    [SerializeField] private ConstructionData _data;
    [SerializeField] private GameObject mousePrefab;
    private Camera _camera;
    private GameObject _currentBuil;

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
        if (_camera != null)
        {
            Vector3 mousePositionInWorld = GetPositionGrid();
            Debug.Log("Position On Grid: " + mousePositionInWorld);
        }
    }

    

    // Button build a building by Id 
    public void StartPlaceBuilding(int id)
    {
        ObjectData data = _data.GetObjectDataById(id);
        if (_currentBuil != null)
        {
            Destroy(_currentBuil);
        }
        _currentBuil = Instantiate(data.prefab[0], Vector3.zero, Quaternion.identity);
        BuildingController script = _currentBuil.GetComponent<BuildingController>();

        script.idBuilding = id;
        if ((data.width & 1) == 0)
        {
            script._offset = new Vector3(-0.5f, 0, -0.5f);
        }
    }

    private Vector3 GetMouseOnWorld()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, 0); // Adjusted to Vector3.up

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
}
