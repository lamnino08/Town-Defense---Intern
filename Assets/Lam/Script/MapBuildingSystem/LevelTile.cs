using UnityEngine.Tilemaps;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/UnitTile")]
public class UnitTile : Tile
{
    public TileType type;
}

public enum TileType
{
    grass = 0,
    tree = 1,
}