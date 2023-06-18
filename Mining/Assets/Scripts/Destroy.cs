using UnityEngine;

public class Destroy : MonoBehaviour
{
    private string targetTag = "Blocks";
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }
}
