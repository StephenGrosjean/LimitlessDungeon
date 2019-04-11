using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise : MonoBehaviour
{
    [SerializeField] public Texture2D texture;

    [SerializeField] private int width = 256;
    [SerializeField] private int height = 256;
    [SerializeField] private float scale = 20f;
    [SerializeField] private float offsetX = 100;
    [SerializeField] private float offsetY = 100;

    private void Start() {
        offsetX = Random.Range(0, 99999f);
        Texture2D texture2D = GenerateTexture();
        texture = texture2D;
    }

   public Texture2D GenerateTexture() {
        Texture2D texture = new Texture2D(width, height);

        for(int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {

                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    Color CalculateColor(int x, int y) {

        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        return new Color(sample, sample, sample);
        
    }
}
