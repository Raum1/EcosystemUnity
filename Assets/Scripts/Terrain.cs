using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] Transform grass, water, food;
    [SerializeField] int size;
    [Range(0f, 1f)]
    [SerializeField] float height, scaler;
    public List<Transform> terrainBlocks = new List<Transform>();

    [ContextMenu("CreateMap")]
    void CreateMap()
    {
        DestroyTerrain();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float perlinNoise = Mathf.PerlinNoise((float)i * scaler, (float)j * scaler);
                if (perlinNoise > height)
                {
                    var terrain = Instantiate(water, new Vector2(i * water.localScale.x, j * water.localScale.y), Quaternion.identity).transform;
                    terrain.parent = transform;
                    terrainBlocks.Add(terrain);
                }
                else
                {
                    var terrain = Instantiate(grass, new Vector2(i * grass.localScale.x, j * water.localScale.y), Quaternion.identity).transform;
                    terrain.parent = transform;
                    terrainBlocks.Add(terrain);
                    float perlinNoiseFood = Mathf.PerlinNoise((float)i * scaler * 5f + 7000, (float)j * scaler * 5f + 7000);
                    if (perlinNoiseFood > 0.7f)
                    {
                        var foodObj = Instantiate(food, new Vector2(i * grass.localScale.x, j * water.localScale.y), Quaternion.identity).transform;
                        foodObj.parent = transform;

                    }
                }
            }
        }
    }
    [ContextMenu("DestroyTerrain")]
    private void DestroyTerrain()
    {
        terrainBlocks = new List<Transform>();
        var oldObjects = FindObjectsOfType<TerrainBlock>();
        foreach (var oldObject in oldObjects)
        {
            DestroyImmediate(oldObject.gameObject);
        }
        var oldFood = FindObjectsOfType<Food>();
        foreach (var food in oldFood)
        {
            DestroyImmediate(food.gameObject);
        }
    }

    private void OnValidate()
    {
        if (size <= 20)
            CreateMap();
    }

}
