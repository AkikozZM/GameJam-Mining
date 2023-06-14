using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyChunk : MonoBehaviour
{
    public GameObject spawn;
    public float deadZone = 100;
    public float chunkiter = 1;
    // Start is called before the first frame update
    void Start()
    {
        spawn = GameObject.Find("Spawn");

    }

    // Update is called once per frame
    void Update()
    {
        if (spawn.transform.position.y > (100.5*chunkiter)-5 && spawn.transform.position.y < (110.5*chunkiter)-5)
        {
            Destroy(gameObject);
        }
    }
}
