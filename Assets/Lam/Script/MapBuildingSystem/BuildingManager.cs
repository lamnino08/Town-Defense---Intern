using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;



// using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class BuildingManager : MonoBehaviour
{
    public int level;
    [SerializeField] private Renderer _renderer;
    public ObjectData dataObject; 
    private PlaneCheckGrid _plane;
    private void Start()
    {
        _plane = GetComponentInChildren<PlaneCheckGrid>();
    }

    public void StartPlace(ObjectData data)
    {
        dataObject = data;

        BuildingController controller = this.AddComponent<BuildingController>();
        controller.SetData(dataObject.width, dataObject.height, this);
    }

    public bool CheckData()
    {
        return !_plane.isCollisonWithOtherBuilding();
    }

    public void PlaceSuccess()
    {
        Vector3 newPosition = transform.position;
        newPosition.y = 0;
        transform.position = newPosition;
        _plane.NormalColor();
        NodeData node = new NodeData(dataObject.id, 1, transform.position.x, transform.position.z);
        ConstructionSave.instance.AddBuilding(node);

        Destroy(GetComponent<BuildingController>());
    }
}
