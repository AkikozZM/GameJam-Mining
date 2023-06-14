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
       // Debug.Log("111");
        controller.BlocksMoveUp();
        controller.transform.Translate(0f, 1f, 0);

    }
}
