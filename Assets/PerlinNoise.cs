using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    public Color[] pix;
    Texture2D CreatePerlinTexture(int size, float scaler)
    {
        var texture = new Texture2D(size, size);
        //var Color[] colors;
        pix = new Color[size * size];
        float gray = 0;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                gray = Mathf.PerlinNoise(i * scaler, j * scaler);
                pix[(int)j * size + (int)i] = new Color(gray, gray, gray);

            }
        }
        texture.SetPixels(pix);
        texture.Apply();
        return texture;
    }
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = CreatePerlinTexture(500, 0.025f);
    }
}
