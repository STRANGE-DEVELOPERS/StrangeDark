using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grid2D : MonoBehaviour
{
    public Vector3 gridWorldSize;
    public float nodeRadius;
    public Node2D[,] Grid;
    public List<Node2D> path;
    List<Tilemap> obstaclemap = new List<Tilemap>();
    Vector3 worldBottomLeft;

    float nodeDiameter;
    public int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        Invoke("CreateGrid", 0.2f);
    }


    void FindObstacleTilemaps()
    {
        var obstacles = GameObject.FindGameObjectsWithTag("ObstacleMap");
        foreach (var obstacle in obstacles)
        {
            obstaclemap.Add(obstacle.GetComponent<Tilemap>());
        }
    }

    void CreateGrid()
    {
        FindObstacleTilemaps();

        Grid = new Node2D[gridSizeX, gridSizeY];
        worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

        // Initializing grid
        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.up * (y * nodeDiameter + nodeRadius);
                Grid[x, y] = new Node2D(false, worldPoint, x, y);

            }

        // placing obstacles
        for (int x = 0; x < gridSizeX; x++)
            for (int y = 0; y < gridSizeY; y++)
            {
                if (TileHasObstacles(Grid[x, y].worldPosition))
                {
                    Grid[x, y].SetObstacle(true);
                     foreach (Node2D neighbourNode in GetNeighbors(Grid[x, y]))
                         neighbourNode.SetCost(2);
                }
                else
                    Grid[x, y].SetObstacle(false);
            }
    }

    bool TileHasObstacles(Vector3 worldPosition)
    {
        foreach(Tilemap map in obstaclemap)
        {
            if (map.HasTile(map.WorldToCell(worldPosition)))
                return true;
        }
        return false;
    }

    //gets the neighboring nodes in the 4 cardinal directions. If you would like to enable diagonal pathfinding, uncomment out that portion of code
    public List<Node2D> GetNeighbors(Node2D node)
    {
        List<Node2D> neighbors = new List<Node2D>();

        //checks and adds top neighbor
        if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY + 1]);

        //checks and adds bottom neighbor
        if (node.GridX >= 0 && node.GridX < gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX, node.GridY - 1]);

        //checks and adds right neighbor
        if (node.GridX + 1 >= 0 && node.GridX + 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
            neighbors.Add(Grid[node.GridX + 1, node.GridY]);

        //checks and adds left neighbor
        if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY >= 0 && node.GridY < gridSizeY)
            neighbors.Add(Grid[node.GridX - 1, node.GridY]);

        //checks and adds top right neighbor
        if (node.GridX + 1 >= 0 && node.GridX + 1< gridSizeX && node.GridY + 1 >= 0 && node.GridY + 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX + 1, node.GridY + 1]);
        //checks and adds bottom right neighbor
        if (node.GridX + 1>= 0 && node.GridX + 1 < gridSizeX && node.GridY - 1 >= 0 && node.GridY - 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX + 1, node.GridY - 1]);
        //checks and adds top left neighbor
        if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY + 1>= 0 && node.GridY + 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX - 1, node.GridY + 1]);
        //checks and adds bottom left neighbor
        if (node.GridX - 1 >= 0 && node.GridX - 1 < gridSizeX && node.GridY  - 1>= 0 && node.GridY  - 1 < gridSizeY)
            neighbors.Add(Grid[node.GridX - 1, node.GridY - 1]);


        return neighbors;
    }


    public Node2D NodeFromWorldPoint(Vector3 worldPosition)
    {

        int x = Mathf.RoundToInt(worldPosition.x - 1 + (gridSizeX / 2));
        int y = Mathf.RoundToInt(worldPosition.y + (gridSizeY / 2));
        return Grid[x, y];
    }
}