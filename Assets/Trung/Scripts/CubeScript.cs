using System.Collections;
using System.Collections.Generic;
using Trung.Scene;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public Renderer plane;
    public List<Renderer> planes = new List<Renderer>();
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckOverlap())
        {
            plane.material.color = Color.red;
        }
        else
        {
            plane.material.color = Color.green;
        }
    }

    private void OnMouseDown()
    {

    }

    private void OnMouseUp()
    {
        CameraShop.instance._movingBuilding = false;
    }

    private void OnMouseDrag()
    {
        Vector3 pos = PlaceSystem.instance.GetPositionGrid();
        CameraShop.instance._movingBuilding = true;
       
        if (pos != transform.position)
        {
            transform.position = new Vector3(pos.x + 0.5f, 0, pos.z + 0.5f);
        }
    }

    public bool CheckOverlap()
    {
        Bounds planeBounds = plane.bounds;
        foreach (var plane in planes)
        {
            Bounds existingPlane = plane.bounds;
            if (existingPlane.Intersects(planeBounds))
            {
                return true;
            }
        }
        return false; 
    }
}
