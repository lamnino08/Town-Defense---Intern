using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    private static PlacementSystem _instance;
    public static PlacementSystem instance { get => _instance; }
    [SerializeField] private Grid _grid;
    [SerializeField] private ConstructionData _data;
    [SerializeField] private GameObject mousePrefab;
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

    private void Update()
    {
        // Update logic if necessary
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
        _currentBuil = Instantiate(data.prefab, Vector3.zero, Quaternion.identity);
        BuildingController script = _currentBuil.GetComponent<BuildingController>();
        script.SetData(data);

        ActBuildingUI.instance.StartPlaceBuilding(script);
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
