namespace Navigator.Navigating;

public class WayNode
{
    public WayNode(int x, int y, bool isStartPoint, bool isEndPoint)
    {
        X = x;
        Y = y;
        IsStartPoint = isStartPoint;
        IsEndPoint = isEndPoint;
    }

    public int X { get; }
    public int Y { get; }

    public int Weight
    {
        get => _weight;
        set
        {
            IsCalculated = true;
            _weight = value;
        }
    }

    private int _weight;
    
    public WayNode? PreviousNode { get; set; }

    public int DistanceFromMainNode { get; set; }
    
    public bool IsCalculated { get; private set; }
    
    public bool IsStartPoint { get; }
    public bool IsEndPoint { get; }
}