using System.Collections;
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
    public GameObject value;
    private AudioSource audioSource;
    private AudioSource audioSource2;
    private AudioSource audioSource3;
    private void displayValue()
    {
        Instantiate(value, transform.position, Quaternion.identity); 
    }

    private void Start()
    {
        GameObject scoreManagerObj = GameObject.Find("Canvas");
        audioSource = GameObject.Find("collect_sound").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("collect_hit").GetComponent<AudioSource>();
        audioSource3 = GameObject.Find("collect_error").GetComponent<AudioSource>();
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
        if (type != "bad")
        {
            currentDura -= hit;
            if (currentDura <= 0)
            {
                audioSource.Play();
                displayValue();
                Destroy(gameObject);
            }
            else
            {
                audioSource2.Play();
            }
        } else if (type == "bad")
        {
            audioSource3.Play();
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
