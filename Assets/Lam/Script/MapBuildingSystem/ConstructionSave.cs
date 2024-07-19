using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;

public class ConstructionSave : MonoBehaviour 
{
    private static ConstructionSave _instance;
    public static ConstructionSave instance => _instance;

    [SerializeField] private ConstructionData _constructionData;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddBuilding(NodeData node)
    {
        ScriptableConstruction constructionMapData = Resources.Load<ScriptableConstruction>($"Map/ConstructionData");
        constructionMapData.constructions.Add(node);
        ScriptableObjectConstructionUtility.SaveLFile(constructionMapData);
    }

    public void RemoveBuidling(NodeData node)
    {
        ScriptableConstruction constructionMapData = Resources.Load<ScriptableConstruction>($"Map/ConstructionData");
        constructionMapData.constructions.Remove(node);
        ScriptableObjectConstructionUtility.SaveLFile(constructionMapData);
    }

    public void ClearMap()
    {
        
    }

    public void LoadMap()
    {
        ScriptableConstruction map = Resources.Load<ScriptableConstruction>($"Map/ConstructionData");
        foreach(NodeData e in map.constructions)
        {
            Vector3 pos = new Vector3(e.x, 0, e.y);
            Instantiate(_constructionData.GetObjectDataById(e.id).prefab);
        }
    }
}

#if UNITY_EDITOR
public static class ScriptableObjectConstructionUtility
{
    public static void SaveLFile(ScriptableConstruction level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Map/ConstructionData.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}    

#endif
