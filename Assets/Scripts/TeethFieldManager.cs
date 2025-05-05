using System.Collections.Generic;
using UnityEngine;

public class TeethFieldManager : MonoBehaviour
{
    public GameObject toothPrefab;
    public int rows = 2;
    public int cols = 15;
    public float spacing = 1.2f;
    public int numberOfBombs = 5;

    private Tooth[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        grid = new Tooth[cols, rows];

        // Instantiate the grid
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Vector3 position = new Vector3(x * spacing, 0, y * spacing);
                GameObject toothObj = Instantiate(toothPrefab, position, Quaternion.identity, transform);

                Tooth tooth = toothObj.GetComponent<Tooth>();
                grid[x, y] = tooth;
            }
        }

        // Place bombs
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

        // Calculate adjacent mine counts
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
