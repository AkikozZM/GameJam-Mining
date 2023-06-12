using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability;
    public string type;
    public ScoreManager scoreManager;
    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("Canvas");
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
    }
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
            case "copper":
                scoreManager.addCopperPoints(1);
                break;
            case "iron":
                scoreManager.addIronPoints(1);
                break;
            case "gold":
                scoreManager.addGoldPoints(1);
                break;
            case "diamond":
                scoreManager.addDiamondPoints(1);
                break;
        }
    }
}
