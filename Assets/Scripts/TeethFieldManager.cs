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
    public List<Tooth> allTeeth = new List<Tooth>();

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
            supTooth.isSup = true;
            grid[x, 0] = supTooth;
            allTeeth.Add(supTooth);

            // Row 1 - Inf
            GameObject infToothObj = Instantiate(toothPrefab, infSpawn.position, infSpawn.rotation, infSpawn);
            Tooth infTooth = infToothObj.GetComponent<Tooth>();
            grid[x, 1] = infTooth;
            allTeeth.Add(infTooth);
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

        for (int y = 0; y < rows; y++) // sup - inf
        {
            for (int x = 0; x < cols; x++) // diente a diente
            {
                Tooth diente = grid[x, y];

                if (diente.isMine) continue;

                int mines = 0;

                // comprobar minas juntas
                for (int dy = -1; dy <= 1; dy++)
                {
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        if (dx == 0 && dy == 0) continue;

                        int nx = x + dx;
                        int ny = y + dy;

                        if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
                        {
                            if (grid[nx, ny].isMine)
                            {
                                mines++;
                            }
                        }
                    }
                }

                diente.adjacentMines = mines;
            }
        }

        // x x x x x x x x x x x x
        // x x x x x x x x x x x x


    }
}
