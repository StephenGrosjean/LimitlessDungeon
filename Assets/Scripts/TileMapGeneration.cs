using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapGeneration : MonoBehaviour {
    public Tilemap groundTileMap, wallsTileMap;
    public Tile ground, walls;

    public void Generate(CellularAutomata.Cell[,] world, int size) {
        for (int x = 0; x < size; x++) {
            for (int y = 0; y < size; y++) {
                if (world[x, y].isAlive) {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), ground);
                }
                else {
                    wallsTileMap.SetTile(new Vector3Int(x, y, 0), walls);
                }
            }
        }
    }


}
