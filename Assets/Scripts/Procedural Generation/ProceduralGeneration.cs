using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int width;
    [SerializeField] int height;
    [SerializeField] float smoothness;
    float generator = 1;
    [SerializeField] TileBase groundTile;
    [SerializeField] Tilemap groundTilemap;
    int[,] map;
    void Start()
    {
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().PlaySound("Arcade Music");
        Generation();
    }

    void Generation()
    {
        generator = Random.Range(1, 10000);
        groundTilemap.ClearAllTiles();
        map = GenerateArray(width, height, true);
        map = TerrainGeneration(map);
        RenderMap(map, groundTilemap, groundTile);
    }
    public int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = (empty) ? 0 : 1;
            }
        }
        return map;
    }
    public int[,] TerrainGeneration(int[,] map)
    {
        int perlinHeight;
        for (int x = 0; x < width; x++)
        {
            perlinHeight = Mathf.RoundToInt(Mathf.PerlinNoise(x / smoothness, generator) * height / 2);
            perlinHeight += height / 2;
            for (int y = 0; y < perlinHeight; y++)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }
    public void RenderMap(int[,] map, Tilemap groundTileMap, TileBase groundTilebase)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (map[x, y] == 1)
                {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTilebase);
                }
            }
        }
    }
    public void StopMusic()
    {
        FindObjectOfType<AudioManager>().GetComponent<AudioManager>().StopSound("Arcade Music");
    }
}
