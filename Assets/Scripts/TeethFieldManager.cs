using System.Collections.Generic;
using UnityEngine;

public class TeethFieldManager : MonoBehaviour
{
    public GameObject toothPrefab;
    public int rows = 2;
    public int cols = 15;
    public int numberOfBombs = 5;

    [Tooltip("Provide spawn points row-major: first 15 for row 0 (top), next 15 for row 1 (bottom)")]
    public List<Transform> spawnPoints; // Should contain rows * cols = 30

    private Tooth[,] grid;

    void Start()
    {
        if (spawnPoints.Count != rows * cols)
        {
            Debug.LogError("Spawn point count doesn't match grid size.");
            return;
        }

        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tooth[cols, rows];

        // Instantiate teeth at spawn points
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                int index = y * cols + x;
                Transform spawn = spawnPoints[index];

                GameObject toothObj = Instantiate(toothPrefab, spawn.position, spawn.rotation, spawn);
                Tooth tooth = toothObj.GetComponent<Tooth>();
                grid[x, y] = tooth;
            }
        }

        // Place bombs randomly
        List<Vector2Int> availablePositions = new List<Vector2Int>();
        for (int x = 0; x < cols; x++)
            for (int y = 0; y < rows; y++)
                availablePositions.Add(new Vector2Int(x, y));

        for (int i = 0; i < numberOfBombs && availablePositions.Count > 0; i++)
        {
            int index = Random.Range(0, availablePositions.Count);
            Vector2Int pos = availablePositions[index];
            grid[pos.x, pos.y].isMine = true;
            availablePositions.RemoveAt(index);
        }

        // Compute adjacency
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (grid[x, y].isMine)
                    continue;

                int count = 0;
                foreach (Vector2Int dir in GetDirections())
                {
                    int nx = x + dir.x;
                    int ny = y + dir.y;

                    if (IsValid(nx, ny) && grid[nx, ny].isMine)
                        count++;
                }
                grid[x, y].adjacentMines = count;
            }
        }
    }

    List<Vector2Int> GetDirections()
    {
        return new List<Vector2Int>
        {
            new Vector2Int(-1, -1), new Vector2Int(0, -1), new Vector2Int(1, -1),
            new Vector2Int(-1,  0),                    new Vector2Int(1,  0),
            new Vector2Int(-1,  1), new Vector2Int(0,  1), new Vector2Int(1,  1)
        };
    }

    bool IsValid(int x, int y)
    {
        return x >= 0 && x < cols && y >= 0 && y < rows;
    }
}
