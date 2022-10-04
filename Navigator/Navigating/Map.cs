namespace Navigator.Navigating;

public class Map
{
    public Grid Grid { get; }

    public Map()
    {
        Grid = Grid.GenerateNullGrid();
    }
    
    public void SetAnomaly(int x, int y, double intensive)
    {
        if(intensive <= 2) return;

        var circle = new Circle(x, y, (int) Math.Sqrt(intensive / 2));
        
        circle.Draw(Grid);
    }
}