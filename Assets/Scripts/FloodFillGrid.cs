using System.Collections.Generic;
using UnityEngine;

public class FloodFillGrid : MonoBehaviour
{
    public GameObject CellPrefab; //Asign cell here
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float cellSize = 1.1f; //space between Cells
    public Color clearCellColor = Color.white;
    public Color mineCellColor = Color.red;

    private Cell[,] grid; // Grid of Cells

    // Start is called before the first frame update
    void Start()
    {
        Generategrid();
    }

    void Generategrid()
    {
        grid = new Cell[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Create new cell
                GameObject newCell = Instantiate(CellPrefab, new Vector3(x * cellSize - 5, y * cellSize - 5, 0), Quaternion.identity);
                newCell.transform.SetParent(transform);

                //Randomly decide if its mine or clear
                bool isMine = Random.Range(0, 3) == 0; //20% chance to be mine

                //Set upo the cell compont
                Cell cell = newCell.AddComponent<Cell>();
                cell.Init(x, y, isMine, this);

                //Set Cell color
                if (isMine)
                    cell.SetColor(mineCellColor);
                else
                    cell.SetColor(clearCellColor);
                // store the cell in the grid
                grid[x, y] = cell;
            }
        }
    }
    public void FloodFill(int startX, int startY)
    {
        //early exit if the start cell is invalid
        if (grid[startX, startY].isMine) return;

        //flood fill algorithem using a stack
        Stack<Cell> stack = new Stack<Cell>();
        stack.Push(grid[startX, startY]);

        while (stack.Count > 0)
        {
            Cell cell = stack.Pop();
            
            if (cell.isRevealed || cell.isMine) continue;

            cell.Reveal();

            //check all 4 directions (N, S, E, W)
            CheckAndAddCellToStack(cell.x + 1, cell.y, stack);
            CheckAndAddCellToStack(cell.x - 1, cell.y, stack);
            CheckAndAddCellToStack(cell.x, cell.y + 1, stack);
            CheckAndAddCellToStack(cell.x, cell.y - 1, stack);
        }
    }
    private void CheckAndAddCellToStack(int x, int y, Stack<Cell> stack)
    {
        if (x >= 0 && x < gridWidth && y >= 0 && y < gridHeight && !grid[x, y].isRevealed && !grid[x, y].isMine)
            stack.Push(grid[x, y]);
    }
}
