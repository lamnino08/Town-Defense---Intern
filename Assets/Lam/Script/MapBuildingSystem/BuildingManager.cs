using Unity.VisualScripting;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private int _level;
    private Renderer _renderer;
    public ObjectData dataObject; 
    private PlaneCheckGrid _plane;
    SpawnSoldierInBuilding _spawnSoldier;
    private void Start()
    {
        _plane = GetComponentInChildren<PlaneCheckGrid>();
    }

    public void StartPlace(ObjectData data)
    {
        dataObject = data;

        BuildingController controller = this.gameObject.AddComponent<BuildingController>();
        controller.SetData(dataObject.width, dataObject.height, this);
    }

    public bool CheckData()
    {
        return !_plane.isCollisonWithOtherBuilding();
    }

    public void PlaceSuccess()
    {
        //Place 
        Vector3 newPosition = transform.position;
        newPosition.y = 0;
        transform.position = newPosition;
        _plane.NormalColor();

        // Update data
        NodeData node = new NodeData(dataObject.id, 1, transform.position.x, transform.position.z);
        ConstructionSave.instance.AddBuilding(node);

        //Spawn soldier
        Spawn();

        Destroy(GetComponent<BuildingController>());
    }

    public void Spawn()
    {
        _spawnSoldier = GetComponent<SpawnSoldierInBuilding>();
        if (_spawnSoldier)
        {
            _spawnSoldier.Spawn();
        } 
    }
}
