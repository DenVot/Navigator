namespace Navigator.Triangulation;

public class TriangulationInfo
{
    public TriangulationInfo(TriangulationPoint a, TriangulationPoint b, TriangulationPoint c)
    {
        A = a;
        B = b;
        C = c;
    }

    public TriangulationPoint A { get; }
    public TriangulationPoint B { get; }
    public TriangulationPoint C { get; }
}