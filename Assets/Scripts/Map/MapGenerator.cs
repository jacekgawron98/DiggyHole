using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<Tile> tilesList;
    public GameObject tileTemplate;
    private List<List<Tile>> map;
    private List<List<bool>> route;

    public GameObject mapObject;

    public int xLimit;
    public int yLimit;

    enum Direction
    {
        Down = 0,
        Left = 1,
        Right = 2
    }

    void Start()
    {
        map = new List<List<Tile>>();
        GenerateRoute();
        GenerateMap();
        DrawMap();
    }

    void GenerateRoute()
    {
        int x = 0, y = 0;
        Direction direction = Direction.Down;

        x = Random.Range(1, xLimit - 1);
        direction = (Direction)Random.Range(0, 3);

        SeedRoute();

        route[y][x] = true;

        while (y < yLimit - 1)
        {

            switch (direction)
            {
                case Direction.Down:
                    {
                        y++;
                        break;
                    }
                case Direction.Left:
                    {
                        x--;
                        break;
                    }
                case Direction.Right:
                    {
                        x++;
                        break;
                    }
            }
            route[y][x] = true;
            direction = GetNewDirection(x, y, direction);
        }
    }

    private Direction GetNewDirection(int x, int y, Direction direction)
    {
        bool isCorrect = false;
        while (!isCorrect)
        {
            direction = (Direction)Random.Range(0, 3);
            if (direction == Direction.Left && (x == 0 || route[y][x - 1]))
            {
                isCorrect = false;
            }
            else if (direction == Direction.Right && (x == (xLimit - 1) || route[y][x + 1]))
            {
                isCorrect = false;
            }
            else
            {
                isCorrect = true;
            }
        }

        return direction;
    }

    private void SeedRoute()
    {
        route = new List<List<bool>>();
        for (int i = 0; i < yLimit; i++)
        {
            route.Add(new List<bool>());
            for (int j = 0; j < xLimit; j++)
            {
                route[i].Add(false);
            }
        }
    }

    void DrawMap()
    {
        for (int y = 0; y < map.Count; y++)
        {
            for (int x = 0; x < map[y].Count; x++)
            {
                Vector2 position = new Vector2(tileTemplate.transform.position.x + x, tileTemplate.transform.position.y - y);
                tileTemplate.GetComponent<GeneratedTile>().SetTile(map[y][x]);
                Instantiate(tileTemplate, position, Quaternion.identity, transform);
            }
        }
    }

    void GenerateMap()
    {
        for (int y = 0; y < yLimit; y++)
        {
            map.Add(new List<Tile>());
            for (int x = 0; x < xLimit; x++)
            {
                if (route[y][x])
                {
                    map[y].Add(tilesList[0]);
                }
                else
                {
                    map[y].Add(GetRandomTile());
                }
            }
        }
    }

    Tile GetRandomTile()
    {
        float totalRarity = 0;

        foreach (Tile t in tilesList)
        {
            totalRarity += t.spawnChance;
        }

        float tileRandom = Random.Range(0, totalRarity);
        foreach (Tile t in tilesList)
        {
            if (tileRandom <= t.spawnChance)
            {
                return t;
            }
            totalRarity -= t.spawnChance;
            tileRandom = Random.Range(0, totalRarity);
        }
        return null;
    }

    public void DrawVisible()
    {
        foreach (Transform child in mapObject.transform)
        {
            child.GetComponent<GeneratedTile>().ShowTile();
        }
    }

    public void DrawInvisible()
    {
        foreach (Transform child in mapObject.transform)
        {
            child.GetComponent<GeneratedTile>().HideTile();
        }
    }
}
