using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    [SerializeField] Transform grass, water;
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
                    var terrain = Instantiate(water, new Vector3(i * water.localScale.x, .65f, j * water.localScale.z), Quaternion.identity).transform;
                    terrain.parent = transform;
                    terrainBlocks.Add(terrain);
                }
                else
                {
                    var terrain = Instantiate(grass, new Vector3(i * grass.localScale.x, .75f, j * water.localScale.z), Quaternion.identity).transform;
                    terrain.parent = transform;
                    terrainBlocks.Add(terrain);
                }
            }
        }
    }
    [ContextMenu("DestroyTerrain")]
    private void DestroyTerrain()
    {
        var oldObjects = FindObjectsOfType<TerrainBlock>();
        foreach (var oldObject in oldObjects)
        {
            DestroyImmediate(oldObject.gameObject);
        }
    }

    private void OnValidate() {
        if(size <= 20)
            CreateMap();
    }
    
}
