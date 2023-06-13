using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    private ScoreManager scoreManager;
    private bool isActionInProgress = false;
    private GameObject[] bagArray = new GameObject[4];

    public class axeObj
    {
        public GameObject axe;
        public int weight;
    }

    private void Start()
    {
        createStarter();
        GameObject scoreManagerObj = GameObject.Find("Canvas");
        scoreManager = scoreManagerObj.GetComponent<ScoreManager>();
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
        if (!isActionInProgress)
        {
            StartCoroutine(PlayerAction());
        }
        //create diggers
        if (Input.GetKeyDown(KeyCode.C))
        {
            CreateDigger();
        }
        
    }
    private GameObject getCurrentAxe()
    {
        Transform[] curr = dig.GetComponentsInChildren<Transform>();
        GameObject ret = curr[1].gameObject;
        return ret;
    }
    private GameObject[] getCurrentAxeInBag()
    {
        Transform[] curr = bag.GetComponentsInChildren<Transform>();
        GameObject[] ret = new GameObject[curr.Length];
        int i = 0;
        foreach(Transform obj in curr)
        {
            ret[i] = obj.gameObject;
            i++;
        }
        return ret;
    }
    private void moveToBag(GameObject axe)
    {
        axe.transform.SetParent(bag.transform);
        axe.SetActive(false);
    }
    private void pickOneAxeToHand()
    {

    }
    private void CreateDigger()
    {
        if (scoreManager.getDiamondPoints() >= collectionPoints)
        {
            scoreManager.subtractDiamondPoints(collectionPoints);
            //get current axe
            GameObject currAxe = getCurrentAxe();
            moveToBag(currAxe);
            //create iron axe
            createDiamond();
        }        
        else if (scoreManager.getGoldPoints() >= collectionPoints)
        {
            scoreManager.subtractGoldPoints(collectionPoints);
            //get current axe
            GameObject currAxe = getCurrentAxe();
            moveToBag(currAxe);
            //create iron axe
            createGold();
        }
        else if (scoreManager.getIronPoints() >= collectionPoints)
        {
            scoreManager.subtractIronPoints(collectionPoints);
            //get current axe
            GameObject currAxe = getCurrentAxe();
            moveToBag(currAxe);
            //create iron axe
            createIron();
        }
        else if (scoreManager.getCopperPoints() >= collectionPoints)
        {
            //need to check if my current bag already has copper axe first


            //if not, create a new copper axe
            scoreManager.subtractCopperPoints(collectionPoints);
            //get current axe
            GameObject currAxe = getCurrentAxe();
            moveToBag(currAxe);
            //create copper axe
            createCopper();
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
    private void BlocksMoveUp()
    {
        spwan.transform.position += new Vector3(0f, 1f, 0f);
    }
    private IEnumerator PlayerAction()
    {
        StartAction();
        bool isBlock = false;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
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
            dig.transform.Translate(0, -0.6f, 0);
        } 
        else if (direction == Vector2.left)
        {
            dig.SetActive(true);
            dig.transform.Translate(-0.6f, 0, 0);
        } 
        else if (direction == Vector2.right)
        {
            dig.SetActive(true);
            dig.transform.Translate(0.6f, 0, 0);
        }
    }
    private void resetDigger()
    {
        dig.transform.localPosition = Vector3.zero;
    }
}
