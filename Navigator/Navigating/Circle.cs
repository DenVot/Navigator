namespace Navigator.Navigating;

public class Circle
{
    private readonly int _cx;
    private readonly int _cy;
    private readonly int _r;

    public Circle(int cx, int cy, int r)
    {
        _cx = cx;
        _cy = cy;
        _r = r;
    }

    public void Draw(Grid grid, ConsoleColor color = ConsoleColor.Red)
    {
        for (var ctxX = _cx - _r; ctxX < _cx + _r; ctxX++)
        {
            for (var deltaY = 0; deltaY < _r; deltaY++)
            {
                if (CircleContains(ctxX, _cy + deltaY))
                {
                    try
                    {
                        grid.SetCellColor(ctxX, _cy + deltaY, color);
                        grid.SetCellColor(ctxX, _cy - deltaY, color);
                        grid.SetCellValue(ctxX, _cy + deltaY, 1);
                        grid.SetCellValue(ctxX, _cy - deltaY, 1);
                    }
                    catch (Exception e)
                    {
                        // ignored
                    }
                }
                else break;
            }
        }
    }

    private bool CircleContains(int x, int y) // Проверяет, если точка внутри круга
    {
        var f = Math.Pow(x - _cx, 2) + Math.Pow(y - _cy, 2);

        return f < _r * _r;
    }
}