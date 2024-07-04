using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    private Camera _camera;
    public Vector3 _offset = Vector3.zero;
    [SerializeField] private bool _isDragging;
    [SerializeField] private Renderer _renderer;
    public int idBuilding;

    private Vector3 _firstPostion;
    private Vector3 _previous;

    private void Start()
    {
        _camera = Camera.main;
        _previous = transform.position;
    }

    void Update()
    {
    }

    private void OnMouseDown()
    {
        _isDragging = true;
        _renderer.material.color = Color.green;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosInGrid = PlacementSystem.instance.GetPositionGrid();
        if (_previous != mousePosInGrid)
        {
            Movement(mousePosInGrid);
            CheckData(mousePosInGrid);
        }
        
    }

    private void CheckData(Vector3 mousePosInGrid)
    {
        if (GridSystem.instance.IsPositionIsPlace(mousePosInGrid))
        {
            _renderer.material.color = Color.green;
        } else 
        {
            _renderer.material.color = Color.red;
        }
    }

    private void Movement(Vector3 mousePosInGrid)
    {
        {
            Vector3 nepose = new Vector3(mousePosInGrid.x, 0.022f, mousePosInGrid.z);
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
        // _isDragging = false;
        _renderer.material.color = Color.green;
        Vector3 mousePosInGrid = PlacementSystem.instance.GetPositionGrid();
        Vector3 nepose = new Vector3(mousePosInGrid.x, 0.02f, mousePosInGrid.z);

        MainUI.instance.SetActiveButtonBuilding(this);
    }
}
