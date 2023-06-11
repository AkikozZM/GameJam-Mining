using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    public int attack;
    public int durability;
    private string targetTag = "Blocks";

    private void Update()
    {
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block block = collision.gameObject.GetComponent<Block>();
        if (collision.gameObject.CompareTag(targetTag))
        {
            block.TakeHit(attack);
            this.durability--;
        }
    }
}
