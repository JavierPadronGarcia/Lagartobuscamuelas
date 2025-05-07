using System.Collections.Generic;
using UnityEngine;

public class TeethFieldManager : MonoBehaviour
{
    public GameObject toothPrefab;
    public int rows = 2;
    public int cols = 16;
    public int numberOfBombs = 5;

    [Tooltip("Parent object that contains all spawn points as children.")]
    public Transform spawnParent;

    private List<Transform> supSpawnPoints = new List<Transform>();
    private List<Transform> infSpawnPoints = new List<Transform>();

    private Tooth[,] grid;

    void Start()
    {
        supSpawnPoints.Clear();
        infSpawnPoints.Clear();

        foreach (Transform child in spawnParent)
        {
            if (child.name.Contains("Sup"))
                supSpawnPoints.Add(child);
            else if (child.name.Contains("Inf"))
                infSpawnPoints.Add(child);
        }

        supSpawnPoints.Sort((a, b) => a.position.x.CompareTo(b.position.x));
        infSpawnPoints.Sort((a, b) => a.position.x.CompareTo(b.position.x));

        if (supSpawnPoints.Count != cols || infSpawnPoints.Count != cols)
        {
            Debug.LogError("Each row must contain exactly " + cols + " spawn points.");
            return;
        }

        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tooth[cols, rows];

        // Instantiate Sup (row 0) and Inf (row 1)
        for (int x = 0; x < cols; x++)
        {
            Transform supSpawn = supSpawnPoints[x];
            Transform infSpawn = infSpawnPoints[x];

            // Row 0 - Sup
            GameObject supToothObj = Instantiate(toothPrefab, supSpawn.position, supSpawn.rotation, supSpawn);
            Tooth supTooth = supToothObj.GetComponent<Tooth>();
            grid[x, 0] = supTooth;

            // Row 1 - Inf
            GameObject infToothObj = Instantiate(toothPrefab, infSpawn.position, infSpawn.rotation, infSpawn);
            Tooth infTooth = infToothObj.GetComponent<Tooth>();
            grid[x, 1] = infTooth;
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
