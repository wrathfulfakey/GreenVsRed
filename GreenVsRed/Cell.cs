namespace GreenVsRed
{
    public class Cell
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public CellColor Color { get; set; }

        public Cell(string[,] grid, int row, int column)
        {
            this.Row = row;
            this.Column = column;
            SetColor(grid, row, column);
        }

        public void SetColor(string[,] grid, int width, int height)
        {
            int parsedCell = int.Parse(grid[width, height]);

            if (parsedCell == 1)
            {
                this.Color = CellColor.Green;
            }
            else if (parsedCell == 0)
            {
                this.Color = CellColor.Red;
            }
        }
    }
}
