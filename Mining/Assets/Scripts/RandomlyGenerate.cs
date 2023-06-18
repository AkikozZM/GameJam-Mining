using UnityEngine;

public class RandomlyGenerate : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject spawn;
    public int rows;
    public int cols;
    private float spawnPosition;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = spawn.transform.position.y;
        this.GenerateSprites();
    }
    void GenerateSprites()
    {
        float left = -3.5f;
        //populate per row
        for (int row = 0; row < rows; row++)
        {
            //populate per col
            for (int col = 0; col < cols; col++)
            {
                int possible = Random.Range(0, 10);
                if (possible > 4)
                {
                    Vector2 pos = new Vector2(left + col, spawnPosition - row);
                    int pick = RandomlyPickNum(prefabs.Length);
                    GameObject newSprite = Instantiate(prefabs[pick], pos, Quaternion.identity);
                    newSprite.transform.SetParent(transform);
                }
            }
        }
    }
    private int RandomlyPickNum(int range)
    {
        int ret = Random.Range(0, range);
        return ret;
    }
}
