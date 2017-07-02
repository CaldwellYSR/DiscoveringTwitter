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
        long seed = Random.Range(1000000, 9999999);
        noise = new PerlinNoise(seed);
        this.Regenerate();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            long seed = Random.Range(1000000, 9999999);
            noise = new PerlinNoise(seed);
            this.Regenerate();
        }
    }

    private void Regenerate()
    {

        GameObject[] terrain = GameObject.FindGameObjectsWithTag("Terrain");
        foreach (GameObject tmp in terrain)
        {
            Destroy(tmp);
        }

        float width = DirtPrefab.transform.lossyScale.x;
        float height = DirtPrefab.transform.lossyScale.y;

        for (int x = minX; x < maxX; x++) 
        {
            int columnHeight = minY + noise.GetNoise(x - minX, maxY - minY);
            for (int y = minY; y < columnHeight; y++)
            {
                GameObject prefab = (y == columnHeight - 1) ? GrassPrefab : DirtPrefab;
                GameObject instance = GameObject.Instantiate(prefab, new Vector2(x * width, y * height), Quaternion.identity);
                instance.tag = "Terrain";
                instance.transform.parent = this.transform;
            }
        }
    }
	
}
