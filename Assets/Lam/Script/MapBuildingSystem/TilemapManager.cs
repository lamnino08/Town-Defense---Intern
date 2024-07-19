using UnityEngine.Tilemaps;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using Unity.VisualScripting;

public class TilemapManager : MonoBehaviour 
{
    [SerializeField] private Tilemap _natureMap, _constructionMap;
    [SerializeField] private int _levelIndex;

    public void SaveMap()
    {
        var newLevel = ScriptableObject.CreateInstance<ScriptableTile>();

        newLevel.levelIndex = _levelIndex;
        newLevel.name = $"Level {_levelIndex}";

        newLevel.groundTiles = GetTileFromMap(_natureMap).ToList();
        newLevel.unitTiles = GetTileFromMap(_constructionMap).ToList();

        ScriptableObjectUtility.SaveLevelFile(newLevel);

        IEnumerable<SaveTile> GetTileFromMap(Tilemap map)
        {
            foreach(var pos in map.cellBounds.allPositionsWithin)
            {
                if (map.HasTile(pos))
                {
                    UnitTile levelTile = map.GetTile<UnitTile>(pos);

                    yield return new SaveTile()
                    {
                        pos = pos,
                        tile = levelTile,
                    };
                }
            }
        }
    }

    public void ClearMap()
    {
        Tilemap[] maps = FindObjectsOfType<Tilemap>();

        foreach(Tilemap e in maps)
        {
            e.ClearAllTiles();
        }
    }

    public void LoadMap()
    {
        var map = Resources.Load<ScriptableTile>($"Map/mapData");
        if (map == null)
        {
            Debug.LogError("Map data is not exist");
            return;
        }

        ClearMap();

        foreach(SaveTile savetiled in map.groundTiles)
        {
            _natureMap.SetTile(savetiled.pos, savetiled.tile);
            // Debug.Log(savetiled.tile.gameObject.name);
        }

        foreach(var savetiled in map.unitTiles)
        {
            _constructionMap.SetTile(savetiled.pos, savetiled.tile);
        }
    }

    public bool CheckPosHasTile(Vector3Int pos)
    {
        if (_natureMap.HasTile(pos) || _constructionMap.HasTile(pos))
        {
            return false;
        }

        return false;
    }
}

#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
    public static void SaveLevelFile(ScriptableTile level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Map/mapData.asset");

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}    

#endif
