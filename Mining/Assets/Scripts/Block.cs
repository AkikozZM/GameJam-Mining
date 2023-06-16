using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int durability;
    public string type;
    public ScoreManager scoreManager;
    public Sprite broken25;
    public Sprite broken50;
    public Sprite broken75;
    public int currentDura;
    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("Canvas");
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
        currentDura = durability;
    }
    private void Update()
    {
        // 75% broken
        if (currentDura < durability * 0.25 / 1)
        {
            transform.GetComponent<SpriteRenderer>().sprite = broken75;
        }
        //50% broken
        else if (currentDura < durability * 0.50 / 1)
        {
            transform.GetComponent<SpriteRenderer>().sprite = broken50;
        }
        //25% broken
        else if (currentDura < durability * 0.75 / 1) { 
            transform.GetComponent<SpriteRenderer>().sprite = broken25;
        }
    }
    public void TakeHit(int hit)
    {
        currentDura -= hit;
        if (currentDura <= 0)
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
