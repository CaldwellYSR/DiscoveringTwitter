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

    // Each seed is a 7 digit random integer
    // If the same seed is given the same random 
    // noise will be returned
    public PerlinNoise(long seed)
    {
        this.seed = seed;
    }

    // Get random number given int x and a range
    // range will determine the upper limit for the 
    // random number (ie: get random number between 1 and range)
    public int GetNoise(int x, int range)
    {
        // Frequency is the size of the "chunks" we are looking at.
        // In our current world generator our world map goes from -16 to 16
        // So it is 32 integers long. With frequency of 16 we have 2 chunks
        // (-16 - 0) (0 - 16)
        int frequency = 16;

        float noise = 0;

        // Calculus convergence trick... range / 2 + range / 4 + range / 8 ...
        // converges down range at infinity.
        range /= 2;

        // Whie we still have frequency to look at
        while (frequency > 0)
        {

            // Which "chunk" we are looking at will be the given
            // integer divided by the frequency
            int chunkIndex = x / frequency;

            // find our "progress" along the current chunk. 
            // multiplying frequency by 1f to avoid integer math
            float progress = (x % frequency) / (frequency * 1f);

            // random number for the current chunk
            float leftRandom = this.random(chunkIndex, range);

            // random number for the next chunk
            float rightRandom = this.random(chunkIndex + 1, range);

            // Since we will round this noise to an integer 
            // we multiply our progress by the two randoms to be our noise
            noise += (1 - progress) * leftRandom + progress * rightRandom;

            // Dividing the frequency and range helps with the convergence 
            // and makes the randomness look more natural
            frequency /= 2;
            range /= 2;

            // Making sure range doesn't converge to 0
            range = Mathf.Max(1, range);
        }

        // Since our sprites are 1 unity unit in size we round this to an int
        return Mathf.RoundToInt(noise);

    }

    private float random(int index, int range)
    {
        // Take our seed to the power of 5 to get a really large number
        // Then we modulo it with the range to get the remainder in that range.
        // This is a way to get a "random" number that will be consistent per seed
        return (Mathf.Pow(index + this.seed, 5f) % range);
    }
}