namespace Navigator.Triangulation;

/// <summary>
/// Точкак для триангуляции источника
/// </summary>
public class TriangulationPoint
{
    public TriangulationPoint(int x, int y, double i)
    {
        X = x;
        Y = y;
        Int = i;
    }

    public int X { get; }
    public int Y { get; }
    public double Int { get; }
}