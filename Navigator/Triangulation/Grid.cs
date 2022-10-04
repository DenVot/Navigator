using Navigator.Navigating;

namespace Navigator.Triangulation;

public class Grid
{
    public const int Width = 30;
    public const int Height = 40;

    private readonly Cell[,] _cells = new Cell[Height, Width];
    
    private static Random _random = new();

    public Grid()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                _cells[y, x] = new Cell();
            }
        }
    }
    
    private void PrintCell(Cell cell)
    {
        Console.ForegroundColor = cell.Color;
        Console.Write(cell.Value + " ");
    }

    private void PrintRow(int rowIndex)
    {
        Console.WriteLine();
        for (var cellIndex = 0; cellIndex < Width; cellIndex++) PrintCell(_cells[rowIndex, cellIndex]);
    }

    public void Print()
    {
        for(var rowIndex = 0; rowIndex < Height; rowIndex++) PrintRow(rowIndex);
    }

    public void SetCellValue(int x, int y, int value) =>
        _cells[y, x].Value = value;

    public void SetCellColor(int x, int y, ConsoleColor color) =>
        _cells[y, x].Color = color;

    public double GetValue(int x, int y) =>
        _cells[y, x].Value;
    
    public static Grid GenerateRandomGrid(int minValue = 0, int maxValue = 9) =>
        GenerateGrid(() => _random.Next(minValue, maxValue));

    public static Grid GenerateNullGrid() => GenerateGrid(() => 0);

    public static bool IsOutOfGrid(int x, int y) => x >= Grid.Width || y >= Grid.Height;
    
    private static Grid GenerateGrid(Func<int> generateAction)
    {
        var grid = new Grid();

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                grid.SetCellValue(x, y, generateAction());      
            }
        }

        return grid;
    }
}