using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    Dictionary<int, GameObject> tileset;
    Dictionary<int, GameObject> tile_groups;

    public GameObject prefab_empty;
    public GameObject prefab_stone;
    public GameObject prefab_dirt;
    public GameObject prefab_copper;
    public GameObject prefab_iron;
    public GameObject prefab_gold;
    public GameObject prefab_diamond;

    public int depth = 0;

    int map_width =7;
    int map_height = 100;

    int x_offset = 0;
    int y_offset = 0;


    //Recommend 4 to 20
    float magnification = 7.0f;

    
    List<List<int>> noise_grid = new List<List<int>>();
    List<List<GameObject>> tile_grid = new List<List<GameObject>>();

       // Start is called before the first frame update
    public void Start()
    {
        CreateTileset();
        CreateTileGroups();
        GenerateMap();
        GenerateCopper();
        GenerateIron();
        GenerateGold();
        GenerateDiamond();
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
                CreateTile(tile_id, x, y);
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
                        noise_grid[x].Add(tile_id);
                        CreateTile(tile_id, x, y);
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
                        noise_grid[x].Add(tile_id);
                        CreateTile(tile_id, x, y);
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
                        noise_grid[x].Add(tile_id);
                        CreateTile(tile_id, x, y);
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
                        noise_grid[x].Add(tile_id);
                        CreateTile(tile_id, x, y);
                    }
                }

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
        if (this.depth < 30)
        {
            if (clamp_perlin < 0.5)
            {
                return 0;
            }
            else if (clamp_perlin >= 0.5 && clamp_perlin < 0.9)
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
            if (clamp_perlin < 0.3)
            {
                return 0;
            }
            else if (clamp_perlin >= 0.3 && clamp_perlin < 0.7)
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
        //print(clamp_perlin);
        //float scale_perlin = clamp_perlin * tileset.Count;


        //Min depth of being able to spawn
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
        //print(clamp_perlin);
        //float scale_perlin = clamp_perlin * tileset.Count;

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
        //print(clamp_perlin);
        //float scale_perlin = clamp_perlin * tileset.Count;

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

    void CreateTile(int tile_id, int x, int y)
    {
        GameObject tile_prefab = tileset[tile_id];
        GameObject tile_group = tile_groups[tile_id];
        GameObject tile = Instantiate(tile_prefab, tile_group.transform);

        tile.name = string.Format("tile_x{0}_y{1}", x, y);
        tile.transform.localPosition = new Vector3(x, y, 0);
        tile_grid[x].Add(tile);

    }

}
