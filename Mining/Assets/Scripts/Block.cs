using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability;
    public string type;
    public ScoreManager scoreManager;
    public void TakeHit(int hit)
    {
        durability -= hit;
        if (durability <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        switch(type)
        {
            case "dirt":
                scoreManager.addDirtPoints(1);
                break;
            case "green":
                scoreManager.addGreenPoints(1);
                break;
            case "blue":
                scoreManager.addBluePoints(1);
                break;
        }
    }
}
