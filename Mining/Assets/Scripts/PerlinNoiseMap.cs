using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;

    public GameObject prefab_empty;
    public GameObject prefab_dirt;
    public GameObject prefab_stone;
    public GameObject prefab_copper;
    public GameObject prefab_iron;
    public GameObject prefab_gold;
    public GameObject prefab_diamond;
    public GameObject prefab_bedrock;
    public GameObject charactor;
    public int depth = 0;
    bool changed = false;


    int map_width = 7;
    public int map_height = 50;


    //Recommend 4 to 20
    float magnification = 7.0f;

    
    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

       // Start is called before the first frame update
    public void Start()
    {
        charactor = GameObject.Find("Charactor");
        this.depth = charactor.GetComponent<PlayerController>().depth;
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
        GenerateCopper();
        GenerateIron();
        GenerateGold();
        GenerateDiamond();
        int x = UnityEngine.Random.Range(0, 3);
        if (x == 0)
        {
            GenerateStructure1(UnityEngine.Random.Range(10, 35));
        }
        else if (x == 1)
        {
            GenerateStructure2(UnityEngine.Random.Range(10, 35));
        } else if (x == 2)
        {
            int gen1_depth = UnityEngine.Random.Range(15, 30);
            GenerateStructure2(gen1_depth);
            GenerateStructure1(gen1_depth + 10);
        }
    }

    public void CreateTileset() {
        tileset = new Dictionary<int, GameObject>();
        tileset.Add(0, prefab_empty);
        tileset.Add(1, prefab_dirt);
        tileset.Add(2, prefab_stone);
        tileset.Add(3, prefab_copper);
        tileset.Add(4, prefab_iron);
        tileset.Add(5, prefab_gold);
        tileset.Add(6, prefab_diamond);
        tileset.Add(7, prefab_bedrock);
    }

    public void CreateTileGroups() {
        tile_groups = new Dictionary<int, GameObject>();
        foreach (KeyValuePair<int, GameObject> prefab_pair in tileset) {
            GameObject tile_group = new GameObject(prefab_pair.Value.name);
            tile_group.transform.parent = gameObject.transform;
            tile_group.transform.localPosition = new Vector3(0,0,0);
            tile_groups.Add(prefab_pair.Key, tile_group);
        }
    }

    public void GenerateMap() {
        int seed = UnityEngine.Random.Range(0, 9999);
        for (int x = 0; x < map_width; x++)
        {
            noise_grid.Add(new List<int>());
            tile_grid.Add(new List<GameObject>());
            for (int y = 0; y < map_height; y++)
            {
                int tile_id = GetIdUsingPerlinCaves(x, y, seed);
                noise_grid[x].Add(tile_id);
                CreateTile(tile_id, x, y, this.changed);
                this.changed = true;
            }
        }


    }

    public void GenerateCopper()
    {
        int seed = UnityEngine.Random.Range(0, 9999);
        for (int x = 0; x < map_width; x++)
        {
            for (int y = 0; y < map_height; y++)
            {
                if (noise_grid[x][y] != 0)
                {
                    int tile_id = GetIdUsingPerlinCopper(x, y, seed);
                    if (tile_id != -1)
                    {
                        noise_grid[x][y] = tile_id;
                        CreateTile(tile_id, x, y, this.changed);
                    }
                }
    
            }
        }

    }

    public void GenerateIron()
    {
        int seed = UnityEngine.Random.Range(0, 9999);
        for (int x = 0; x < map_width; x++)
        {
            for (int y = 0; y < map_height; y++)
            {
                if (noise_grid[x][y] != 0)
                {
                    int tile_id = GetIdUsingPerlinIron(x, y, seed);
                    if (tile_id != -1)
                    {
                        noise_grid[x][y] = tile_id;
                        CreateTile(tile_id, x, y, this.changed);
                    }
                }

            }
        }

    }

    public void GenerateGold()
    {
        int seed = UnityEngine.Random.Range(0, 9999);
        for (int x = 0; x < map_width; x++)
        {
            for (int y = 0; y < map_height; y++)
            {
                if (noise_grid[x][y] != 0)
                {
                    int tile_id = GetIdUsingPerlinGold(x, y, seed);
                    if (tile_id != -1)
                    {
                        noise_grid[x][y] = tile_id;
                        CreateTile(tile_id, x, y, this.changed);
                    }
                }

            }
        }

    }

    public void GenerateDiamond()
    {
        int seed = UnityEngine.Random.Range(0, 9999);
        for (int x = 0; x < map_width; x++)
        {
            for (int y = 0; y < map_height; y++)
            {
                if (noise_grid[x][y] != 0)
                {
                    int tile_id = GetIdUsingPerlinDiamond(x, y, seed);
                    if (tile_id != -1)
                    {
                        noise_grid[x][y] = tile_id;
                        CreateTile(tile_id, x, y, this.changed);
                    }
                }

            }
        }

    }

    public void GenerateStructure1(int depth_start)
    {
        for (int x = 0; x < map_width; x++)
        {
            //noise_grid.Add(new List<int>());
            //tile_grid.Add(new List<GameObject>());
            for (int y = depth_start; y < (depth_start + 8); y++)
            {
                int tile_id = GetIdUsingStructure1Generation(x, y, depth_start);
                noise_grid[x][y] = tile_id;
                CreateTile(tile_id, x, y, this.changed);

            }
        }
    }

    public void GenerateStructure2(int depth_start)
    {
        for (int x = 0; x < map_width; x++)
        {

            for (int y = depth_start; y < (depth_start + 6); y++)
            {
                int tile_id = GetIdUsingStructure2Generation(x, y, depth_start);
                noise_grid[x][y] = tile_id;
                CreateTile(tile_id, x, y, this.changed);

            }
        }
    }

    int GetIdUsingPerlinCaves(int x, int y, int seed)
    {
     
        float raw_perlin = Mathf.PerlinNoise(
            (x - seed) / magnification,
            (y - seed) / magnification
        );
        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);
        if ((y> 0 && y < 5) || y > (map_height-2))
        {
            return 1;
        }
        else if( y== 0)
        {
            return 0;
        }
  

        
       
        if (this.depth < 30)
        {
            if (clamp_perlin < 0.45)
            {
                return 0;
            }
            else if (clamp_perlin >= 0.45 && clamp_perlin < 0.9)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        else if(this.depth < 70)
        {
            if (clamp_perlin < 0.4)
            {
                return 0;
            }
            else if (clamp_perlin >= 0.4 && clamp_perlin < 0.85)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }


        //All lower depths
        else
        {
            if (clamp_perlin < 0.35)
            {
                return 0;
            }
            else if (clamp_perlin >= 0.35 && clamp_perlin < 0.7)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }

    int GetIdUsingPerlinCopper(int x, int y, int seed)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x + seed) / magnification,
            (y + seed) / magnification
        );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);

        if (this.depth < 30)
        {
            return -1;
        }
        else if (this.depth< 70)
        {
            if (clamp_perlin > .85)
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }

        else if (this.depth < 150)
        {
            if (clamp_perlin > .8)
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (clamp_perlin > .7)
            {
                return 3;
            }
            else
            {
                return -1;
            }
        }


    }

    int GetIdUsingPerlinIron(int x, int y, int seed)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x + seed) / magnification,
            (y + seed) / magnification
        );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);

        if (this.depth < 50)
        {
            return -1;
        }
        else if (this.depth < 100)
        {
            if (clamp_perlin > .85)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }

        else if (this.depth < 150)
        {
            if (clamp_perlin > .8)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (clamp_perlin > .75)
            {
                return 4;
            }
            else
            {
                return -1;
            }
        }

    }

    int GetIdUsingPerlinGold(int x, int y, int seed)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x + seed) / magnification,
            (y + seed) / magnification
        );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);

        if (this.depth < 100)
        {
            return -1;
        }
        else if (this.depth < 150)
        {
            if (clamp_perlin > .9)
            {
                return 5;
            }
            else
            {
                return -1;
            }
        }

        else if (this.depth < 200)
        {
            if (clamp_perlin > .8)
            {
                return 5;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (clamp_perlin > .7)
            {
                return 5;
            }
            else
            {
                return -1;
            }
        }

    }


    int GetIdUsingPerlinDiamond(int x, int y, int seed)
    {
        float raw_perlin = Mathf.PerlinNoise(
            (x + seed) / magnification,
            (y + seed) / magnification
        );

        float clamp_perlin = Mathf.Clamp(raw_perlin, 0.0f, 1.0f);


        if (this.depth < 150)
        {
            return -1;
        }
        else if (this.depth < 200)
        {
            if (clamp_perlin > .85)
            {
                return 6;
            }
            else
            {
                return -1;
            }
        }

        else if (this.depth < 250)
        {
            if (clamp_perlin > .8)
            {
                return 6;
            }
            else
            {
                return -1;
            }
        }
        else
        {
            if (clamp_perlin > .7)
            {
                return 6;
            }
            else
            {
                return -1;
            }
        }

    }

    int GetIdUsingStructure1Generation(int x, int y, int depth_start)
    {
        if (y > (depth_start + 2))
        {
            if (x % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 7;
            }
        }
        else
        {
            if (x % 4 == 0)
            {
                return 6;
            }
            else if(x%2 == 0)
            {
                return 5;
            }
            else
            {
                return 7;
            }
        }
    }

    int GetIdUsingStructure2Generation(int x, int y, int depth_start)
    {
        if (x == 0 || x == map_width-1)
        {
            return 0;
        }
        else if (x==1 || x == map_width - 2)
        {
            return 7;
        }
        else
        {
            if (y == depth_start)
            {
                return 5;
            }
            else if (y % 2 == 0)
            {
                return 0;
            }
            else
            { 
                if ((y-1) % 4 == 0)
                {
                    if (x == 2)
                    {
                        return 0;
                    } else
                    {
                        return 7;
                    }
                } else
                {
                    if (x == 4)
                    {
                        return 0;
                    } else
                    {
                        return 7;
                    }
                }
            }
        }
    }

    void CreateTile(int tile_id, int x, int y, bool changed)
    {
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);
        if (!changed)
        {
            tile_grid[x].Add(tile);
        }
        else
        {
            tile_grid[x].Add(tile);
        }
        

    }

}
