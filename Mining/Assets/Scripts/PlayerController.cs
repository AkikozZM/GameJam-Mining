using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spwan;
    public GameObject dig;
    public GameObject bag;
    public int collectionPoints;
    public GameObject pickaxe_starter;
    public GameObject pickaxe_copper;
    public GameObject pickaxe_iron;
    public GameObject pickaxe_gold;
    public GameObject pickaxe_diamond;
    public int depth;
    public Sprite idle1;
    public Sprite idle2;
    public Sprite idle3;

    private ScoreManager scoreManager;
    private bool isActionInProgress = false;
    private bool gameOver = false;

    public static bool needToSwitch = false;

    private void Start()
    {
        createStarter();
        GameObject scoreManagerObj = GameObject.Find("Canvas");
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
        StartCoroutine(idle());
        
    }
    private IEnumerator idle()
    {
        while(true)
        {
            transform.GetComponent<SpriteRenderer>().sprite = idle1;
            yield return new WaitForSeconds(0.2f);
            transform.GetComponent<SpriteRenderer>().sprite = idle2;
            yield return new WaitForSeconds(0.2f);
            transform.GetComponent<SpriteRenderer>().sprite = idle3;
            yield return new WaitForSeconds(0.2f);
        }
    }
 
    private void createStarter()
    {
        //instantiate starter axe
        pickaxe_starter = Instantiate(pickaxe_starter);
        pickaxe_starter.transform.SetParent(dig.transform);
        pickaxe_starter.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void createCopper()
    {
        pickaxe_copper = Instantiate(pickaxe_copper);
        pickaxe_copper.transform.SetParent(dig.transform);
        pickaxe_copper.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void createIron()
    {
        pickaxe_iron = Instantiate(pickaxe_iron);
        pickaxe_iron.transform.SetParent(dig.transform);
        pickaxe_iron.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void createGold()
    {
        pickaxe_gold = Instantiate(pickaxe_gold);
        pickaxe_gold.transform.SetParent(dig.transform);
        pickaxe_gold.transform.localPosition = new Vector3(0, 0, 0);
    }
    private void createDiamond()
    {
        pickaxe_diamond = Instantiate(pickaxe_diamond);
        pickaxe_diamond.transform.SetParent(dig.transform);
        pickaxe_diamond.transform.localPosition = new Vector3(0, 0, 0);
    }
    void Update()
    {
        if (!OnGround())
        {
            isActionInProgress = true;
        }
        else
        {
            isActionInProgress = false;
        }
        if (!isActionInProgress && !gameOver)
        {
            StartCoroutine(PlayerAction());
        }
        //create diggers
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateDigger();
        }
        if (needToSwitch)
        {
            needToSwitch = false;
            GameObject curr = getCurrentAxe();
            moveToBag(curr);
            GameObject pick = pickOneAxeToHand();
            moveToHand(pick);
        }
        scoreManager.setCurrentAxeName(getCurrentAxeName(getCurrentAxe()));
    }
    private string getCurrentAxeName(GameObject axe)
    {
        switch(axe.name)
        {
            case "Dig_diamond(Clone)":
                return "DiamondAxe";
            case "Dig_gold(Clone)":
                return "GoldAxe";
            case "Dig_iron(Clone)":
                return "IronAxe";
            case "Dig_copper(Clone)":
                return "CopperAxe";
            case "Dig_starter(Clone)":
                return "Starter";
        }
        return null;
    }
    private GameObject getCurrentAxe()
    {
        Transform[] curr = dig.GetComponentsInChildren<Transform>();
        GameObject ret = curr[1].gameObject;
        return ret;
    }
    private GameObject[] getCurrentAxeInBag()
    {
        int n = bag.transform.childCount;
        GameObject[] ret = new GameObject[n];
        for (int i = 0; i < n; i++)
        {
            Transform t = bag.transform.GetChild(i);
            ret[i] = t.gameObject;
        }
        return ret;
    }
    private bool checkAxeInHand(string axe)
    {
        if (dig.transform.Find(axe) != null)
        {
            return true;
        }
        return false;
    }
    private bool checkAxeInBag(string axe)
    {
        if (bag.transform.Find(axe) != null)
        {
            Debug.Log("true");
            return true;
        } 
        return false;
    }
    private void moveToBag(GameObject axe)
    {
        axe.transform.SetParent(bag.transform);
        axe.SetActive(false);
    }
    private void moveToHand(GameObject axe)
    {
        axe.transform.SetParent(dig.transform);
        axe.SetActive(true);
    }
    public GameObject pickOneAxeToHand()
    {
        
        GameObject[] axes = this.getCurrentAxeInBag();
        Dictionary<int, GameObject> map = new Dictionary<int, GameObject>();
        foreach (GameObject axe in axes)
        {
            if (axe.gameObject.GetComponent<Digger>().quality == 5
                && axe.gameObject.GetComponent<Digger>().durability != 0)
            {
                map[5] = axe;
            } else if (axe.gameObject.GetComponent<Digger>().quality == 4
                && axe.gameObject.GetComponent<Digger>().durability != 0)
            {
                map[4] = axe;
            } else if (axe.gameObject.GetComponent<Digger>().quality == 3
                && axe.gameObject.GetComponent<Digger>().durability != 0)
            {
                map[3] = axe;
            } else if (axe.gameObject.GetComponent<Digger>().quality == 2
                && axe.gameObject.GetComponent<Digger>().durability != 0)
            {
                map[2] = axe;
            } else if (axe.gameObject.GetComponent<Digger>().quality == 1)
            {
                map[1] = axe;
            }
        }
        for (int i = 5; i > 1; i--)
        {
            if (map.ContainsKey(i))
            {
                return map[i];
            } 
        }
        return map[1];
    }
    private void CreateDigger()
    {
        if (scoreManager.getDiamondPoints() >= collectionPoints)
        {
            if (checkAxeInBag("Dig_diamond(Clone)") || checkAxeInHand("Dig_diamond(Clone)"))
            {
                pickaxe_diamond.GetComponent<Digger>().durability += collectionPoints;
                scoreManager.subtractDiamondPoints(collectionPoints);
                //and move a best choice axe to hand
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                GameObject pick = pickOneAxeToHand();
                moveToHand(pick);
            }
            else
            {
                scoreManager.subtractDiamondPoints(collectionPoints);
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                //create diamond axe
                createDiamond();
            }
        }        
        else if (scoreManager.getGoldPoints() >= collectionPoints)
        {
            if (checkAxeInBag("Dig_gold(Clone)") || checkAxeInHand("Dig_gold(Clone)"))
            {
                pickaxe_gold.GetComponent<Digger>().durability += collectionPoints;
                scoreManager.subtractGoldPoints(collectionPoints);
                //and move a best choice axe to hand
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                GameObject pick = pickOneAxeToHand();
                moveToHand(pick);
            } 
            else
            {
                scoreManager.subtractGoldPoints(collectionPoints);
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                //create gold axe
                createGold();
            }

        }
        else if (scoreManager.getIronPoints() >= collectionPoints)
        {
            if (checkAxeInBag("Dig_iron(Clone)") || checkAxeInHand("Dig_iron(Clone)"))
            {
                pickaxe_iron.GetComponent<Digger>().durability += collectionPoints;
                scoreManager.subtractIronPoints(collectionPoints);
                //and move a best choice axe to hand
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                GameObject pick = pickOneAxeToHand();
                moveToHand(pick);
            }
            else
            {
                scoreManager.subtractIronPoints(collectionPoints);
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                //create iron axe
                createIron();
            }
        }
        else if (scoreManager.getCopperPoints() >= collectionPoints)
        {
            //need to check if my current bag already has copper axe first
            //or if my hand has this pickaxe
            if (checkAxeInBag("Dig_copper(Clone)") || checkAxeInHand("Dig_copper(Clone)"))
            {
                //just increase the durability of this axe
                pickaxe_copper.GetComponent<Digger>().durability += collectionPoints;
                //and consume current collection pts
                scoreManager.subtractCopperPoints(collectionPoints);
                //and move a best choice axe to hand
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                GameObject pick = pickOneAxeToHand();
                moveToHand(pick);
            }
            else
            {
                //if not, create a new copper axe
                scoreManager.subtractCopperPoints(collectionPoints);
                //get current axe
                GameObject currAxe = getCurrentAxe();
                moveToBag(currAxe);
                //create copper axe
                createCopper();
            }
        }
        else
        {
            Debug.Log("Don't have enough points.");
        }
    }
    private void StartAction()
    {
        isActionInProgress = true;
    }
    private void FinishAction()
    {
        isActionInProgress= false;
    }
    public void BlocksMoveUp()
    {
        spwan.transform.position += new Vector3(0f, 1f, 0f);
        depth++;
    }
    private IEnumerator PlayerAction()
    {
        StartAction();
        bool isBlock = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.GetComponent<SpriteRenderer>().flipX = true;
            if (DetectBlocks(Vector3.left))
            {
                PlayerDig(Vector2.left);
                yield return new WaitForSeconds(0.4f);
                dig.SetActive(false);
                resetDigger();  
            }
            isBlock = DetectBlocks(Vector3.left);
            if (!isBlock)
            {
                transform.Translate(-1f, 1f, 0);
            } 
            else
            {
                transform.Translate(0f, 1f, 0);
            }

            BlocksMoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.GetComponent<SpriteRenderer>().flipX = false;
            if (DetectBlocks(Vector3.right))
            {
                PlayerDig(Vector2.right);
                yield return new WaitForSeconds(0.4f);
                dig.SetActive(false);
                resetDigger();
            }
            isBlock = DetectBlocks(Vector3.right);
            if (!isBlock)
            {
                transform.Translate(1f, 1f, 0);
            }
            else
            {
                transform.Translate(0f, 1f, 0);
            }
            BlocksMoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (OnGround())
            {
                PlayerDig(Vector2.down);
                yield return new WaitForSeconds(0.4f);
                dig.SetActive(false);
                resetDigger();
            }
            isBlock = OnGround();
            if (isBlock)
            {
                transform.Translate(0f, 1f, 0);
            }
            BlocksMoveUp();
        }
        FinishAction();
    }
    private bool OnGround()
    {
        Vector2 rayCastPos = transform.position + Vector3.down * 0.51f;
        RaycastHit2D hit = Physics2D.Raycast(rayCastPos, Vector3.down, 1f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Blocks"))
        {
            //Debug.Log("There is a block");
            return true;
        }
        return false;
    }
    private bool DetectBlocks(Vector3 direction)
    {
        Vector2 rayCastPos = transform.position + direction * 0.49f;
        RaycastHit2D hit = Physics2D.Raycast(rayCastPos, direction, 0.5f);
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Blocks"))
        {
            //Debug.Log("There is a block");
            return true;
        }
        return false;
    }

    private void PlayerDig(Vector2 direction)
    {
        if (direction == Vector2.down)
        {
            dig.SetActive(true);
            dig.transform.localScale = new Vector3(1, -1, 1);
            dig.transform.Translate(0, -0.4f, 0);
        } 
        else if (direction == Vector2.left)
        {
            dig.SetActive(true);
            dig.transform.localScale = new Vector3(-1, 1, 1);
            dig.transform.Translate(-0.6f, 0, 0);
        } 
        else if (direction == Vector2.right)
        {
            dig.SetActive(true);
            dig.transform.localScale = new Vector3(1, 1, 1);
            dig.transform.Translate(0.6f, 0, 0);
        }
    }
    private void resetDigger()
    {
        dig.transform.localPosition = Vector3.zero;
        pickaxe_starter.transform.localPosition = Vector3.zero;
        pickaxe_starter.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        pickaxe_copper.transform.localPosition = Vector3.zero;
        pickaxe_copper.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        pickaxe_iron.transform.localPosition = Vector3.zero;
        pickaxe_iron.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        pickaxe_gold.transform.localPosition = Vector3.zero;
        pickaxe_gold.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        pickaxe_diamond.transform.localPosition = Vector3.zero;
        pickaxe_diamond.transform.localScale = new Vector3(0.3f, 0.3f, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GameOver"))
        {
            Debug.Log("GameOver");
            gameOver = true;
            scoreManager.displayGameOver();

        }
    }
}
