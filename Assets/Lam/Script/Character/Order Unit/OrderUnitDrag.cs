using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class OrderUnitDrag : MonoBehaviour
{
    [SerializeField] public RectTransform selectionAreaTransform;
    [SerializeField] private LayerMask leagueLayerMask;
    private OrderUnitSelection _unitSlection;
    Camera mainCam;
    Rect selectionBox;

    Vector2 _startPos;
    Vector2 _endPos;

    private void Start() 
    {
        mainCam = Camera.main;
        _startPos = Vector2.zero;
        _endPos = Vector2.zero;
        _unitSlection = OrderUnitSelection.Instance;
    }

    private void Update() 
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift))
        {
            // Debug.Log("here");
            _startPos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            _endPos = Input.mousePosition;
            DrawVisual();
            // DrawVSeletion();
        }

        if (Input.GetMouseButtonUp(0) && Input.GetKey(KeyCode.LeftShift))
        {
            SelectUnitInBox();
            _startPos = Vector2.zero;
            _endPos = Vector2.zero;
            DrawVisual();
        }

        
    }

    private void DrawVisual()
    {
        Vector2 boxStart = _startPos;
        Vector2 boxEnd = _endPos;

        Vector2 boxCenter = (_startPos + _endPos) / 2;
        selectionAreaTransform.position = boxCenter;

        Vector2 boxSize = new Vector2(Mathf.Abs(boxStart.x - boxEnd.x), Mathf.Abs(boxStart.y - boxEnd.y));

        selectionAreaTransform.sizeDelta = boxSize;
    }

    private void SelectUnitInBox()
    {
        Vector3 startWorldPos = GetMouseWorldPosition(_startPos);
        Vector3 endWorldPos = GetMouseWorldPosition(_endPos);

        Vector3 center = (startWorldPos + endWorldPos) / 2;
        Vector3 size = new Vector3(
            Mathf.Abs(startWorldPos.x - endWorldPos.x),
            Mathf.Abs(0.3f),
            Mathf.Abs(startWorldPos.z - endWorldPos.z)
        );
        // Debug.Log(size);

        Collider[] hitColliders = Physics.OverlapBox(center, size / 2, Quaternion.identity, leagueLayerMask);

        foreach (var hitCollider in hitColliders)
        {
            _unitSlection.ClickSelection(hitCollider.gameObject);
        }
    }

   

     private Vector3 GetMouseWorldPosition(Vector2 screenPosition)
    {
        Ray ray = mainCam.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            return hitInfo.point + new Vector3(0,0.5f,0);
        }
        return Vector3.zero;
    }

   
}
