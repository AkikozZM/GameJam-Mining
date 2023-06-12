using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneration : MonoBehaviour
{
    public GameObject chunk;
    public GameObject spawn;
    public float spawnRate = 10;
    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnChunk();
            timer = 0;
        }
        
    }

    void spawnChunk()
    {
        GameObject prefab = Instantiate(chunk, transform.position, transform.rotation);
        setParent(prefab);
    }

    void setParent (GameObject chunk)
    {
        chunk.transform.SetParent(spawn.transform);
    }
}
