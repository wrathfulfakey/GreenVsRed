namespace GreenVsRed
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            //Size input for the grid
            int[] gridSize = Console.ReadLine()
                            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

            //Create the grid
            Grid generationZero = new Grid(gridSize);
            string[,] grid = generationZero.Matrix;

            //Fill the grid rows with 0s and 1s
            for (int row = 0; row < grid.GetLength(0); row++)
            {
                char[] input = Console.ReadLine()
                    .ToCharArray();

                for (int col = 0; col < grid.GetLength(1); col++)
                {
                    grid[row, col] = input[col].ToString();
                }
            }

            //Get the arguments for which cell to calculate how many generations it has been green
            int targetRow, targetCol, generations;
            GetArguments(out targetRow, out targetCol, out generations);

            Cell targetCell = new Cell(grid, targetRow, targetCol);

            //Calculate how many times the target cell has been green during the generations
            int finalResult = generationZero.GenerationsCount(targetCell, generations);

            Console.WriteLine($"Result: {finalResult}");
        }

        private static void GetArguments(out int targetRow, out int targetCol, out int generations)
        {
            int[] arguments = Console.ReadLine()
                            .Split(", ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

            targetRow = arguments[0];
            targetCol = arguments[1];
            generations = arguments[2];
        }
    }
}
