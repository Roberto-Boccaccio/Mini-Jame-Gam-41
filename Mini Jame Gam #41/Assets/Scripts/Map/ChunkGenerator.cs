using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkGenerator : MonoBehaviour
{
    //Variables
    public Tilemap tileMap;
    public BoxCollider2D bc;
    public List<Tile> terrainTiles;


    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {

        // Get bounds of the BoxCollider2D
        Bounds bounds = bc.bounds;

        // Convert world bounds to tilemap grid (cell) positions
        Vector3Int min = tileMap.WorldToCell(bounds.min);
        Vector3Int max = tileMap.WorldToCell(bounds.max);

        // Loop within the bounds
        for (int x = min.x; x <= max.x; x++)
        {
            for (int y = min.y; y <= max.y; y++)
            {
                Vector3 worldPos = tileMap.CellToWorld(new Vector3Int(x, y, 0)) + tileMap.cellSize / 2f;

                // Check if this cell is really within the collider (in case of rounding issues)
                if (bc.OverlapPoint(worldPos))
                {
                    Vector3Int tilePosition = new Vector3Int(x, y, 0);
                    int rand = Random.Range(0, terrainTiles.Count);
                    tileMap.SetTile(tilePosition, terrainTiles[rand]);
                }
            }
        }
        /*
        for (int x = 0; x < ChunkWidth; x++)
        {
            for (int y = 0; y < ChunkHeight; y++)
            {
                Vector3Int TileCenter = new Vector3Int(x, y, 0);
                int rand = Random.Range(0, terrainTiles.Count);
                tileMap.SetTile(TileCenter, terrainTiles[rand]);
            }
        }
        */
    }
}
