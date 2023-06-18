using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public GameObject chunk;
    public GameObject spawn;
    //public GameObject prefab;
    public float depth = 0;
    int chunkiter = 0;
    bool changed = false;
    

    // Start is called before the first frame update
    void Start()
    {
        chunk = Instantiate(chunk, transform.position, transform.rotation);
        chunk.name = "chunk" + chunkiter;
        chunkiter++;

        setParent(chunk);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnChunk()
    {
        if (!changed)
        {
            transform.Translate(0, -8f, 0);
            changed = true;
        }
        PerlinNoiseMap script = chunk.GetComponent<PerlinNoiseMap>();
        //script.map_height = 90;
        chunk = Instantiate(chunk, transform.position, transform.rotation);
        chunk.name = "chunk" + chunkiter;
        //chunk.GetComponent<PerlinNoiseMap>().map_height = 90;
        chunkiter++;

        setParent(chunk);

    }

    void setParent (GameObject chunk)
    {
        chunk.transform.SetParent(spawn.transform);
    }
}
