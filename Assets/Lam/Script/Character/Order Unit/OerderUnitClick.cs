using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OerderUnitClick : MonoBehaviour
{
    private Camera mainCam;
    [SerializeField] private LayerMask LeagueMask;
    [SerializeField] private LayerMask groundMask;
    private OrderUnitSelection unitSlection;

    private void Start() 
    {
        mainCam = Camera.main;
        unitSlection = OrderUnitSelection.Instance;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LeagueMask))
            {
                // Click to chosse
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    unitSlection.ClickSelection(hit.collider.gameObject);
                } else // Normal Click
                {
                    unitSlection.DeSelectAll();
                }

            } else
            {
                // Completely normal click
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    unitSlection.DeSelectAll();
                }
            }
        } 
        
        if (Input.GetMouseButtonUp(1))
        {
            if (unitSlection.listSelection.Count > 0)
            {
                Vector3 TargetPos = GetMouseWorldPosition();
                // Debug.Log(TargetPos);

                List<Vector3> listtarget = GetPosAround(TargetPos, new float[] {2f, 4f, 6f},new int[] {5, 10, 20});
                int targetIndex = 0;
                foreach (GameObject unit in unitSlection.listSelection)
                {
                    // Debug.Log(listtarget[targetIndex]);

                    ArmyLeagueDynamicMovement script = unit.GetComponent<ArmyLeagueDynamicMovement>();
                    script.SetOrderPostion(listtarget[targetIndex]);
                    targetIndex = (targetIndex +1 ) % listtarget.Count;
                }
            }
        }


    }

    private List<Vector3> GetPosAround(Vector3 startPos, float[] distanceArray, int[] positionCount)
{
    List<Vector3> posList = new List<Vector3>();

    posList.Add(startPos); // Thêm vị trí bắt đầu vào danh sách

    for (int i = 0; i < positionCount.Length; i++)
    {
        posList.AddRange(GetPosAroundLayer(startPos, distanceArray[i], positionCount[i]));
    }

    return posList;
}

private List<Vector3> GetPosAroundLayer(Vector3 startPos, float distance, int positionCount)
{
    List<Vector3> posList = new List<Vector3>();

    for (int i = 0; i < positionCount; i++)
    {
        float angle = i * (360.0f / positionCount); 
        Vector3 dir = ApplyRotate(new Vector3(1,0,0), angle); 
        Vector3 position = new Vector3(startPos.x + dir.x * distance, startPos.y, startPos.z + dir.z * distance); 
        posList.Add(position);
    }

    return posList;
}

private Vector3 ApplyRotate(Vector3 vec, float angle)
{
    // Quay vectơ quanh trục y
    Quaternion rotation = Quaternion.Euler(0, angle, 0);
    return rotation * vec;
}

private Vector3 GetMouseWorldPosition()
{
    Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hitInfo))
    {
        return hitInfo.point;
    }
    return Vector3.zero;
}
}
