using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;

public class GridSystem : MonoBehaviour
{
    private static GridSystem _instance;
    public static GridSystem instance {get => _instance;}
    private List<NodeData> _nodes = new List<NodeData>();
    [SerializeField] private ConstructionData _constructionData; // Reference to ConstructionData script

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }

        JsonReader jsonReader = new JsonReader("GridData.json");
        // jsonReader.GenerateMapData();
        _nodes = jsonReader.ReadNodesFromJson(); // Path to your JSON file
        InitializeGrid();
    }

     /// <summary>
    /// Get all cell a building mark
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns> List pos of cell <summary>
    public static List<Vector2> AreaByPosition(Vector3 pos, int width, int height)
    {
        float x = pos.x;
        float y = pos.z;
        float minX;
        float minY;

        minX = x - width/2f + 0.5f;
        minY = y - height/2f + 0.5f;
        // Debug.Log($"Here {pos} {minX} {minY} {width/2} {height/2}");
        List<Vector2> result = new List<Vector2>();
        for(float i = minY; i<  (minY + height); i++)
        {
            for (float j = minX; j< (minX + width); j++)
            {
                Vector2 node = new Vector2(j,i);
                result.Add(node);
            }
        }
        return result;
    }

    /// <summary>
    /// Spawn building in to scene from data of json file
    /// </summary> <summary>
    /// 
    /// </summary>
    public void InitializeGrid()
    {
        foreach (NodeData node in _nodes)
        {
            int id = node.id;
            if (id != 0 && id != 1000)
            {
                int level = node.level;
                float x = node.x;
                float y = node.y;
                float direction = node.direction;

                ObjectData objectNode = _constructionData.GetObjectDataById(id);
                // Debug.Log(id);
                GameObject prefabToInstantiate = objectNode.prefab;
                
                x = (objectNode.width & 1) == 1 ? x : x - 0.5f;
                y = (objectNode.height & 1) == 1 ? y : y - 0.5f;

                if (prefabToInstantiate != null)
                {
                    Vector3 spawnPosition = new Vector3(x, 0, y);
                    // Debug.Log(spawnPosition);
                    GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                    BuildingController scriptController = instantiatedObject.GetComponent<BuildingController>();
                    scriptController.SpawnFromJson(level, objectNode);
                }
                else
                {
                    Debug.LogWarning("Prefab for id " + id + " and level " + level + " not assigned or missing.");
                }
            }
            
        }
    }

    /// <summary>
    /// Check a cell was placed
    /// </summary>
    /// <param name="position"></param>
    /// <returns> true or false</returns>
    public bool IsAllowedPlace(Vector2 position)
    {
        // Debug.Log(position);
        float x = position.x;
        float y = position.y;
        
        NodeData dataCell = _nodes.Find(e => e.x == x && e.y == y);
            Debug.Log(" x y"+x+"  "+y);
        if (dataCell == null)
        {
            Debug.Log("Not fine x y"+x+"  "+y);
            return false;
        } 
        // if (dataCell.id != 0) Debug.Log(dataCell.id);
        return  dataCell.id == 0 ? true : false;
    }

    /// <summary>
    /// Place a building on ground
    /// </summary>
    /// <param name="area"></param>
    /// <param name="id"></param>
    public void PlaceBuilding(List<Vector2> area, int id, Vector2 pos)
    {
        foreach(Vector2 cell in area)
        {
            float x = cell.x;
            float y = cell.y;
            NodeData node = _nodes.Find(e => e.x == x && e.y == y);
            node.id = 1000;
            // Debug.Log($"{node.x}  {node.y}  {node.id}");
        }
        // Debug.Log("Cell main confirm: "+ pos);
        _nodes.Find(e => e.x == pos.x && e.y == pos.y).id = id;

        JsonReader jsonReader = new JsonReader("GridData.json");
        jsonReader.WriteNewData(_nodes);
    }

    public void UnPlaceBuilding(List<Vector2> area)
    {
        foreach(Vector2 cell in area)
        {
            float x = cell.x;
            float y = cell.y;
            NodeData node = _nodes.Find(e => e.x == x && e.y == y);
            node.id = 0;
            // Debug.Log($"Node where reset when click: {node.id} {node.x} {node.y}");
            // Debug.Log($"{node.x}  {node.y}  {node.id}");
        }
        // foreach(NodeData e in _nodes)
        // {
        //     if (e.id != 0)
        //     {
        //         Debug.Log($"Node id != 0 afer reset: {e.id} {e.x};{e.y}");
        //     }
        // }
        JsonReader jsonReader = new JsonReader("GridData.json");
        jsonReader.WriteNewData(_nodes);
    }

}
