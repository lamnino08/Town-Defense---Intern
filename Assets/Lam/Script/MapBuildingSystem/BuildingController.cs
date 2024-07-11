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
    [SerializeField] private bool _isDragging;
    [SerializeField] private Renderer _renderer;
    public ObjectData dataObject; 
    private bool _isPlaceed = false;

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
        Debug.Log(_offset);
        // Debug.Log(transform.position);

        if (CheckData())
        {
            _renderer.material.color = Color.green;
        } else 
        {
            _renderer.material.color = Color.red;
        }
    }

    public void SpawnFromJson(int level, ObjectData data)
    {
        _isPlaceed = true;
        this.level = level;
        dataObject = data;
        if ((dataObject.width & 1) == 0) _offset.x += 0.5f;
        if ((dataObject.height & 1) == 0) _offset.z += 0.5f;
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _renderer.material.color = Color.green;
        List<Vector2> rs = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
        _firstPostion = transform.position;   

        // List<Vector2> areaold = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
        // Debug.Log(areaold.Count);
        
        if (_isPlaceed)
        {
            ActBuildingUI.instance.ClickOnBuilding(this);
            GridSystem.instance.UnPlaceBuilding(rs);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosInGrid = PlacementSystem.instance.GetPositionGrid();
        if (_previous != mousePosInGrid)
        {
            // Debug.Log(_previous+"    "+mousePosInGrid); 
            Movement(mousePosInGrid);
            if (CheckData())
            {
                _renderer.material.color = Color.green;
            } else 
            {
                _renderer.material.color = Color.red;
            }
            _previous = mousePosInGrid;
            
        }
        if (Input.GetMouseButtonDown(1) && dataObject.id == 7)
        {
            Rotate();
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
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        
        // ActBuildingUI.instance.StartPlaceBuilding(this);
    }

    public bool Place()
    {
        if (CheckData())
        {
            

            float x = (dataObject.width & 1) == 1 ? transform.position.x : transform.position.x + 0.5f;
            float y = (dataObject.height & 1) == 1 ? transform.position.z : transform.position.z + 0.5f;
            Vector2 cellMain = new Vector2(x,y);
            Debug.Log(transform.position);
            List<Vector2> area = GridSystem.AreaByPosition(transform.position, dataObject.width, dataObject.height);
            GridSystem.instance.PlaceBuilding(area, dataObject.id, cellMain);

            ActBuildingUI.instance._currentBuildingAction = null;
            PlacementSystem.instance._currentBuil = null;
            _isPlaceed = true;

            // _renderer.material.color = new Color(0.2f, 0.7f, 0.2f);
            return true;
        } else
        {
            Debug.Log("here");
            transform.position = _firstPostion;
            return false;
        }
    }
}
