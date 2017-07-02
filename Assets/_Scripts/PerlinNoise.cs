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

        float noise = 0;
        range /= 2;
        bool hitOne = false;

        while (frequency > 0)
        {
            int chunkIndex = x / frequency;
            float progress = (x % frequency) / (frequency * 1f);
            float leftRandom = this.random(chunkIndex, range);
            float rightRandom = this.random(chunkIndex + 1, range);

            noise += (1 - progress) * leftRandom + progress * rightRandom;

            frequency /= 2;
            range /= 2;

            range = Mathf.Max(1, range);
        }

        return Mathf.RoundToInt(noise);

    }

    private float random(int index, int range)
    {
        return (Mathf.Pow(index + this.seed, 5f) % range);
    }
}