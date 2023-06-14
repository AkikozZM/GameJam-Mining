using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digger : MonoBehaviour
{
    public int attack;
    public int durability;
    public int quality;
    private string targetTag = "Blocks";
    private void Update()
    {
        if (durability <= 0)
        {
            //go pick the best choice axe for player
            PlayerController.needToSwitch = true;

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
