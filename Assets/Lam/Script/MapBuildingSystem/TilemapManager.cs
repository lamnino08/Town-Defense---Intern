using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/LevelTile")]
public class LevelTile : Tile
{
    public TileType type;
}

public enum TileType
{
    grass = 0,
    tree,
}