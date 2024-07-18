using System.Collections;
using System.Collections.Generic;
using System.Data;

// using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class BuildingController : MonoBehaviour
{
    private Camera _camera;
    public Vector3 _offset = Vector3.zero;
    public  int level;
    static Color _normalColor = new Color(50f/255f, 65f/255f, 49f/255f);
    static Color _validColor = new Color(60f/255f, 70f/255f, 49f/255f);
    // static Color _validColor = new Color(50f/255f, 70f/255f, 49f/255f);
    static Color _inValidColor = new Color(104f/255f, 43f/255f, 17f/255f);
    [SerializeField] private bool _isDragging;
    [SerializeField] private Renderer _renderer;
    public ObjectData dataObject; 
    private bool _isPlaceed = false;
    public bool isPlaced {get => _isPlaceed;}

    private Vector3 _firstPostion;
    private Vector3 _previous;

    private void Start()
    {
        _camera = Camera.main;
        _previous = transform.position;
    }

    public void SetData(ObjectData dataa)
    {
        this.dataObject = dataa;
        if ((dataObject.width & 1) == 0) _offset.x += 0.5f;
        if ((dataObject.height & 1) == 0) _offset.z += 0.5f;
        transform.position += _offset;

        if (CheckData())
        {
            Place();
            ActBuildingUI.instance.AllowRemove(true);
            _renderer.material.color = _validColor;
        } else 
        {
            ActBuildingUI.instance.AllowRemove(false);
            _renderer.material.color = _inValidColor;
        }
    }

    public void SpawnFromJson(int level, ObjectData data)
    {
        _isPlaceed = true;
        this.level = level;
        dataObject = data;
        if ((dataObject.width & 1) == 0) _offset.x += 0.5f;
        if ((dataObject.height & 1) == 0) _offset.z += 0.5f;

        _renderer.material.color = _validColor;

    }

    private void OnMouseDown()
    {
        if (!_isPlaceed)
        {
            _isDragging = true;
            _renderer.material.color = _validColor;
            List<Vector2> rs = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
            
            if (_isPlaceed)
            {
                // Debug.Log("here");
                _firstPostion = transform.position;   
                ActBuildingUI.instance.ClickOnBuilding(this);
                GridSystem.instance.UnPlaceBuilding(rs);
            }
        }
    }

    private void OnMouseDrag()
    {
        if (!_isPlaceed)
        {
            Vector3 mousePosInGrid = PlacementSystem.instance.GetPositionGrid();
            if (_previous != mousePosInGrid)
            {
                // Debug.Log(_previous+"    "+mousePosInGrid); 
                Movement(mousePosInGrid);
                if (CheckData())
                {
                    _renderer.material.color = _validColor;
                } else 
                {
                    _renderer.material.color = _inValidColor;
                }
                _previous = mousePosInGrid;
                
            }
            if (Input.GetMouseButtonDown(1) && dataObject.id == 7)
            {
                Rotate();
            }
        }
    }

    public bool CheckData()
    {
        List<Vector2> area = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
        foreach(Vector2 cell in area)
        {
            // Debug.Log(cell);
            if (!GridSystem.instance.IsAllowedPlace(cell))
            {
                // Debug.Log("Herer"+cell);
                return false;
            }
        }

        return true;
       
    }

    private void Movement(Vector3 mousePosInGrid)
    {
        {
            Vector3 nepose = new Vector3(mousePosInGrid.x, 0.03f, mousePosInGrid.z);
            // Debug.Log(_offset+"    "+nepose);
            transform.position = nepose + _offset;
            _previous = transform.position;
        }
    }

    public void Rotate()
    {
        transform.Rotate(0,90,0);
    }

    private void OnMouseUp()
    {
        if (!_isPlaceed)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            Place();
        }
        // ActBuildingUI.instance.StartPlaceBuilding(this);
    }

    public bool Place()
    {
        if (CheckData())
        {
            float x = (dataObject.width & 1) == 1 ? transform.position.x : transform.position.x + 0.5f;
            float y = (dataObject.height & 1) == 1 ? transform.position.z : transform.position.z + 0.5f;
            Vector2 cellMain = new Vector2(x,y);
            List<Vector2> area = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
            GridSystem.instance.PlaceBuilding(area, dataObject.id, cellMain);

            // ActBuildingUI.instance._currentBuildingAction = null;
            PlacementSystem.instance._currentBuil = null;
            _isPlaceed = true;
            ActBuildingUI.instance.AllowRemove(true);
            return true;
        } else
        {
            if (_firstPostion == Vector3.zero)
            {
                // Destroy(gameObject);
                return false;
            }
            transform.position = _firstPostion;
            _renderer.material.color = _validColor;
            return false;
        }
    }

    public void Remove()
    {
        if (_isPlaceed)
        {
            List<Vector2> rs = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
            GridSystem.instance.UnPlaceBuilding(rs);
        } 
        Destroy(gameObject);
    }
}
