namespace GreenVsRed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public string[,] Matrix { get; set; }

        public HashSet<int> GreenNumbers { get; set; } = new HashSet<int>() { 0, 1, 4, 5, 7, 8 };

        public HashSet<int> RedNumbers { get; set; } = new HashSet<int>() { 0, 1, 2, 4, 5, 7, 8 };

        public Grid(int[] gridSize)
        {
            int width = gridSize[0];
            int height = gridSize[1];

            CheckLimitForGrid(width, height);

            this.Matrix = new string[width, height];
        }

        //Get the next generation 
        public string[,] GetNewGridGeneration(string[,] matrix)
        {
            string[,] grid = matrix.Clone() as string[,];
            Cell currentCell;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    currentCell = new Cell(matrix, row, col);

                    switch (currentCell.Color.ToString())
                    {
                        case "Red":
                            if (CanChangeColor(currentCell))
                            {
                                grid[row, col] = "1";
                            }
                            break;
                        case "Green":
                            if (CanChangeColor(currentCell))
                            {
                                grid[row, col] = "0";
                            }
                            break;
                    }
                }
            }

            return grid;
        }

        //Get the surrounding cells of the current cell
        public List<string> GetSurroundingCells(string[,] grid, Cell cell)
        {
            List<string> surroundings = new List<string>();

            for (int rowIndexLimit = -1; rowIndexLimit <= 1; rowIndexLimit++)
            {
                for (int columnIndexLimit = -1; columnIndexLimit <= 1; columnIndexLimit++)
                {
                    int row = cell.Row + rowIndexLimit;
                    int column = cell.Column + columnIndexLimit;

                    if (IsInGrid(grid, row, column))
                    {
                        if ((rowIndexLimit != 0) || (columnIndexLimit != 0))
                        {
                            surroundings.Add(grid[row, column]);
                        }
                    }
                }
            }

            return surroundings;
        }

        //Count how many times the target cell has been green during the generations
        public int GenerationsCount(Cell cell, int generations)
        {
            int targetCellGreenTimesMet = 0;

            if (IsInGrid(this.Matrix, cell.Row, cell.Column))
            {
                for (int i = 0; i < generations; i++)
                {
                    this.Matrix = GetNewGridGeneration(this.Matrix);

                    if (this.Matrix[cell.Row, cell.Column] == "1")
                    {
                        targetCellGreenTimesMet++;
                    }
                }

                return targetCellGreenTimesMet;
            }

            return targetCellGreenTimesMet;
        }

        //Check if the cell can change color depending on how many green neighbours it has
        public bool CanChangeColor(Cell currentCell)
        {
            List<string> surroundingCells = GetSurroundingCells(this.Matrix, currentCell);

            int greensCount = surroundingCells.Count(x => x == "1");

            if (currentCell.Color.ToString() == "Green")
            {
                return this.GreenNumbers.Contains(greensCount);
            }
            else if (currentCell.Color.ToString() == "Red")
            {
                return !this.RedNumbers.Contains(greensCount);
            }

            return false;
        }

        //Check if the cell is in the grid
        public bool IsInGrid(string[,] grid, int row, int col)
        {
            return row >= 0
                && row < grid.GetLength(0)
                && col >= 0
                && col < grid.GetLength(1);
        }

        //Check for the limits on the Grid
        public void CheckLimitForGrid(int width, int height)
        {
            if (width > height)
            {
                Console.WriteLine(GlobalConstants.WIDTH_CANNOT_BE_GREATER_THAN_HEIGHT);
            }
            else if (height >= 1000 || width >= 1000)
            {
                Console.WriteLine(GlobalConstants.CANNOT_BE_MORE_THAN_1000);
            }
        }
    }
}
