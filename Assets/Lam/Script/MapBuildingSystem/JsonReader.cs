using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NodeData
{
    public int id;
    public int level;
    public float x;
    public float y;
    public float direction;
}

[System.Serializable]
public class NodeDataWrapper
{
    public List<NodeData> nodes;
}

public class JsonReader
{
    private string _filePath;

    public JsonReader(string filePath)
    {
        _filePath = Path.Combine(Application.streamingAssetsPath, filePath);
    }

    public List<NodeData> ReadNodesFromJson()
    {
        List<NodeData> nodeDataList = new List<NodeData>();

        if (File.Exists(_filePath))
        {
            string jsonContent = File.ReadAllText(_filePath);
            NodeDataWrapper wrapper = JsonUtility.FromJson<NodeDataWrapper>(jsonContent);
            if (wrapper != null && wrapper.nodes != null)
            {
                nodeDataList = wrapper.nodes;
            }
            else
            {
                Debug.LogError("Failed to parse JSON content from file: " + _filePath);
            }
        }
        else
        {
            Debug.LogError("File not found: " + _filePath);
        }

        return nodeDataList;
    }

    public void GenerateMapData()
    {
        NodeDataWrapper nodeDataWrapper = new NodeDataWrapper();
        nodeDataWrapper.nodes = new List<NodeData>();

        for (int y = -4; y < 45; y++)
        {
            for (int x = -4; x < 45; x++)
            {
                NodeData node = new NodeData
                {
                    id = 0,
                    level = 0,
                    x = x + 0.5f,
                    y = y + 0.5f,
                    direction = 0
                };
                nodeDataWrapper.nodes.Add(node);
            }
        }

        string json = JsonUtility.ToJson(nodeDataWrapper, true);
        string filePath = Path.Combine(Application.streamingAssetsPath, _filePath);
        File.WriteAllText(filePath, json);
    }

    public void WriteNewData(List<NodeData> nodes)
    {
        NodeDataWrapper nodeDataWrapper = new NodeDataWrapper();
        nodeDataWrapper.nodes = nodes;

        string json = JsonUtility.ToJson(nodeDataWrapper, true);
        File.WriteAllText(_filePath, json);
    }
}
