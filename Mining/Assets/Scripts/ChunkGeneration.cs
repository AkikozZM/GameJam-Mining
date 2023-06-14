using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public GameObject chunk;
    public GameObject spawn;
    public float spawnRate = 10;
    public float timer = 0;
    public float depth = 0;
    public float count = 0;

    // Start is called before the first frame update
    void Start()
    {
        chunk = Instantiate(chunk, transform.position, transform.rotation);
        chunk.GetComponent<PerlinNoiseMap>().map_height = 100;
        setParent(chunk);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnChunk()
    {
        chunk = Instantiate(chunk, transform.position, transform.rotation);
        chunk.GetComponent<PerlinNoiseMap>().map_height = 93;
        chunk.GetComponent<DestroyChunk>().chunkiter++;
        setParent(chunk);
    }

    void setParent (GameObject chunk)
    {
        chunk.transform.SetParent(spawn.transform);
    }
}
