using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject spwan;
    public GameObject dig;
    private bool isActionInProgress = false;
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
