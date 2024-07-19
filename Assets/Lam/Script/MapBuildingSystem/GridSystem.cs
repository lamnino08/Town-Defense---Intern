using UnityEngine;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine.Tilemaps;

public class GridSystem : MonoBehaviour
{
    private static GridSystem _instance;
    public static GridSystem instance {get => _instance;}
    private List<NodeData> _nodes = new List<NodeData>();
    [SerializeField] Tilemap _natureMap;
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
        jsonReader.GenerateMapData();
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
                GameObject prefabToInstantiate = objectNode.prefab;
                
                x = (objectNode.width & 1) == 1 ? x : x - 0.5f;
                y = (objectNode.height & 1) == 1 ? y : y - 0.5f;

                if (prefabToInstantiate != null)
                {
                    Vector3 spawnPosition = new Vector3(x, 0, y);
                    GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                    BuildingController scriptController = instantiatedObject.GetComponent<BuildingController>();
                    // scriptController.SpawnFromJson(level, objectNode);
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
        float x = position.x;
        float y = position.y;
        
        NodeData dataCell = _nodes.Find(e => e.x == x && e.y == y);
        if (dataCell == null)
        {
            return false;
        } 
        return  dataCell.id == 0 ? true : false;
    }

    /// <summary>
    /// Place a building on ground
    /// </summary>
    /// <param name="area"></param>
    /// <param name="id"></param>
    public void PlaceBuilding(List<Vector2> area, int id, Vector2 pos)
    {
        Debug.Log("place");
        foreach(Vector2 cell in area)
        {
            float x = cell.x;
            float y = cell.y;
            NodeData nodeAround = NodeByCell(x,y);
            nodeAround.id = 1000;
        }
            NodeData node = NodeByCell(pos.x,pos.y);
            // if (node == null)
            // {
            //     Debug.Log($"not found {pos.x} {pos.y}");
            // }
            Debug.Log($"{node.x} {node.y}");
            node.id = id;

        JsonReader jsonReader = new JsonReader("GridData.json");
        jsonReader.WriteNewData(_nodes);
    }

    /// <summary>
    /// Delete data a building from data 
    /// </summary>
    /// <param name="area"></param>
    public void UnPlaceBuilding(List<Vector2> area)
    {
        Debug.Log("unplace");
        foreach(Vector2 cell in area)
        {
            float x = cell.x;
            float y = cell.y;
            NodeData node = NodeByCell(x,y);
            // if (node == null)
            // {
            //     Debug.Log($"not found {x} {y}");
            // }
            node.id = 0;
        }
    
        JsonReader jsonReader = new JsonReader("GridData.json");
        jsonReader.WriteNewData(_nodes);
    }

    /// <summary>
    /// coordinate to index of list node
    /// </summary>
    /// <param name="x">coordinate x</param>
    /// <param name="y">coordinate y</param>
    /// <returns></returns>
    private NodeData NodeByCell(float x, float y)
    {
        float roundedX = RoundToNearestHalf(x);
        float roundedY = RoundToNearestHalf(y);

        int index = Mathf.FloorToInt(roundedY + 3.5f)*49 + Mathf.FloorToInt(roundedX + 3.5f);
        // Debug.Log($"{roundedX} {roundedY} {index} {_nodes[index].x} {_nodes[index].y}");
        return _nodes[index];
    }

    /// <summary>
    /// Round a number like 4.99999 to 4.5
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public float RoundToNearestHalf(float number)
    {
        return Mathf.Round(number * 2f) / 2f;
    }

}
