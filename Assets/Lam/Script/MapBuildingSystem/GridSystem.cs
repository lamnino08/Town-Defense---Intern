using UnityEngine;
using System.Collections.Generic;

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

    public void InitializeGrid()
    {
        foreach (NodeData node in _nodes)
        {
            int id = node.id;
            if (id != 0)
            {
                int level = node.level;
                float x = node.x;
                float y = node.y;
                int direction = node.direction;

                ObjectData objectNode = _constructionData.GetObjectDataById(id);
                GameObject prefabToInstantiate = objectNode.prefab[level-1];
                
                x = (objectNode.width & 1) == 1 ? x : x - 0.5f;
                y = (objectNode.height & 1) == 1 ? y : y - 0.5f;

                if (prefabToInstantiate != null)
                {
                    Vector3 spawnPosition = new Vector3(x, y, 0f);

                    GameObject instantiatedObject = Instantiate(prefabToInstantiate, spawnPosition, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Prefab for id " + id + " and level " + level + " not assigned or missing.");
                }
            }
            
        }
    }

    public bool IsPositionIsPlace(Vector3 position)
    {
        // Debug.Log(position);
        float x = position.x;
        float y = position.z;
        
        NodeData dataCell = _nodes.Find(e => e.x == x && e.y == y);
        if (dataCell == null) return false;
        return  dataCell.id == 0 ? true : false;
    }
}
