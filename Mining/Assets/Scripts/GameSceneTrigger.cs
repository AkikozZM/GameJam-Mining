using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneTrigger : MonoBehaviour
{
    public PlayerController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        controller.BlocksMoveUp();
        dig.transform.localPosition = new Vector3(0, 1, 0);

    }
}
