using UnityEngine;

public class WorldGenerator : MonoBehaviour
{

    public GameObject DirtPrefab;
    public GameObject GrassPrefab;

    private int minX = -16;
    private int maxX = 16;

    private int minY = -10;
    private int maxY = 10;

    PerlinNoise noise;

	void Start ()
    {
        long seed = 9483217L;
        noise = new PerlinNoise(seed);
        this.Regenerate();
	}

    private void Regenerate()
    {
        float width = DirtPrefab.transform.lossyScale.x;
        float height = DirtPrefab.transform.lossyScale.y;

        for (int x = minX; x < maxX; x++) 
        {
            int columnHeight = minY + noise.GetNoise(x - minX, maxY - minY);
            for (int y = minY; y < columnHeight; y++)
            {
                GameObject.Instantiate(DirtPrefab, new Vector2(x * width, y * height), Quaternion.identity);
            }
        }
    }
	
}
