using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkTriggerScript : MonoBehaviour
{
    public ChunkGeneration generation;
    // Start is called before the first frame update
    void Start()
    {
        generation = GameObject.FindGameObjectWithTag("generation").GetComponent<ChunkGeneration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        generation.spawnChunk();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
