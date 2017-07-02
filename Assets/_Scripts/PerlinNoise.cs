/**
* Perlin Noise Generator: 
*      Seeded random number generator. Takes integer and returns random number based on that integer.
*      Given the same "seed" the noise should always be the same.
* @see http://www.arendpeter.com/Perlin_Noise.html
*/

using System;
using UnityEngine;

public class PerlinNoise
{
    private long seed;

    public PerlinNoise(long seed)
    {
        this.seed = seed;
    }

    public int GetNoise(int x, int range)
    {
        // x == 6

        int frequency = 16;
        int chunkIndex = x / frequency; // Integer math floors this
        Debug.Log("Chunk Index: " + chunkIndex);
        float progress = (x % frequency) / (frequency * 1f);
        Debug.Log("Progress: " + progress);
        float leftRandom = this.random(chunkIndex, range);
        Debug.Log("Left Random: " + leftRandom);
        float rightRandom = this.random(chunkIndex + 1, range);
        Debug.Log("Right Random: " + rightRandom);

        float noise = (1 - progress) * leftRandom + progress * rightRandom;
        Debug.Log("Noise: " + noise);

        return Mathf.RoundToInt(noise);

    }

    private float random(int index, int range)
    {
        Debug.Log("Random: " + Mathf.Pow(index + this.seed, 5f) % range);
        return (Mathf.Pow(index + this.seed, 5f) % range);
    }
}