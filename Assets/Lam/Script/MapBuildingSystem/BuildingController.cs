using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingController : MonoBehaviour
{
    public Vector3 _offset = Vector3.zero;
    
    [SerializeField] private Renderer _renderer;
    private int width, height;
    private BuildingManager _buildingManager;

    private Vector3 _previous;
    private bool isCheck = false;

    private void Start()
    {
        _previous = transform.position;
    }

    private void Update() 
    {
        FollowMousePosition();
    }

    public void SetData(int width, int height,BuildingManager buildingManager)
    {
        this.width = width;
        this.height = height;
        _buildingManager = buildingManager;
        if ((width & 1) == 0) _offset.x += 0.5f;
        if ((height & 1) == 0) _offset.z += 0.5f;

        transform.position += _offset;
    }

    private void FollowMousePosition()
    {
        Vector3 mousePosInGrid = PlacementSystem.instance.GetPositionGrid();
        if (isCheck)
        {
            CheckData();
            isCheck = false;
        }
        if (_previous != mousePosInGrid)
        {
            isCheck = true;
            Movement(mousePosInGrid);
            _previous = mousePosInGrid;
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return; // Exit if the click is on UI
            }
            if (CheckData())
            {
                _buildingManager.PlaceSuccess();
            } else
            {
                Destroy(gameObject);
            }
        }
        
        if (Input.GetMouseButtonDown(1)) 
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown() 
    {
        
    }

    public bool CheckData()
    {
        return _buildingManager.CheckData();
    }

    private void Movement(Vector3 mousePosInGrid)
    {
        // {
            Vector3 nepose = new Vector3(mousePosInGrid.x, 0.01f, mousePosInGrid.z);
            transform.position = nepose + _offset;
            _previous = transform.position;
        // }
    }
}
